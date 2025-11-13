#!/bin/bash

# CheatSheets (cs) - Interactive CLI for managing your personal knowledge base
# Version: 1.0.0
# Focus: Adding commands in < 10 seconds

set -euo pipefail

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
MAGENTA='\033[0;35m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color
BOLD='\033[1m'

# Script directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
CS_ROOT="${CS_ROOT:-$SCRIPT_DIR}"
CONFIG_FILE="${CS_ROOT}/.cs.config"
PERSONAL_DIR="${CS_ROOT}/personal"
INDEX_FILE="${CS_ROOT}/.cs_index"
CACHE_DIR="${CS_ROOT}/.cs_cache"

# Default configuration
DEFAULT_EDITOR="${EDITOR:-nano}"
DEFAULT_CATEGORY="personal"
QUICK_ADD_FILE="${PERSONAL_DIR}/quick.md"
AUTO_SYNC=${CS_AUTO_SYNC:-false}
CAPTURE_THRESHOLD=${CS_CAPTURE_THRESHOLD:-3}

# Load configuration if exists
if [[ -f "$CONFIG_FILE" ]]; then
    source "$CONFIG_FILE"
fi

# Ensure directories exist
mkdir -p "$PERSONAL_DIR" "$CACHE_DIR"

#==============================================================================
# UTILITY FUNCTIONS
#==============================================================================

print_success() {
    echo -e "${GREEN}✓${NC} $*"
}

print_error() {
    echo -e "${RED}✗${NC} $*" >&2
}

print_info() {
    echo -e "${BLUE}ℹ${NC} $*"
}

print_warning() {
    echo -e "${YELLOW}⚠${NC} $*"
}

print_header() {
    echo -e "${BOLD}${CYAN}$*${NC}"
}

# Check if command exists
has_command() {
    command -v "$1" >/dev/null 2>&1
}

# Get timestamp
timestamp() {
    date +"%Y-%m-%d %H:%M:%S"
}

#==============================================================================
# CORE FUNCTIONS
#==============================================================================

# Initialize index
init_index() {
    print_info "Building search index..."

    # Create empty index
    : > "$INDEX_FILE"

    # Process markdown files (simple, no nested loops)
    find "$CS_ROOT" -name "*.md" -type f \
        ! -path "*/.git/*" \
        ! -path "*/node_modules/*" \
        ! -path "*/personal/*" \
        2>/dev/null \
        -print0 | while IFS= read -r -d '' mdfile; do

        local relpath="${mdfile#$CS_ROOT/}"

        # Index table commands (sed in one pass)
        grep -E '^\|[[:space:]]*`' "$mdfile" 2>/dev/null | \
            sed -E "s|^\|[[:space:]]*\`([^\`]+)\`[^|]*\|[[:space:]]*([^|]+)\|.*|\1|\2|${relpath}|" \
            >> "$INDEX_FILE" 2>/dev/null || true

        # Index code blocks (awk with variable)
        awk -v file="$relpath" \
            '/```bash/,/```/ {if (!/```/ && !/^#/ && NF>0) print $0 "||" file}' \
            "$mdfile" >> "$INDEX_FILE" 2>/dev/null || true
    done

    local count=$(wc -l < "$INDEX_FILE" 2>/dev/null || echo "0")
    print_success "Index built with $count entries"
    return 0
}

# Search commands
search_commands() {
    local query="$1"
    local category="${2:-}"
    local use_fzf="${3:-true}"

    # Ensure index exists
    [[ ! -f "$INDEX_FILE" ]] && init_index

    local results
    if [[ -n "$category" ]]; then
        results=$(grep -i "$query" "$INDEX_FILE" | grep "$category")
    else
        results=$(grep -i "$query" "$INDEX_FILE")
    fi

    if [[ -z "$results" ]]; then
        print_warning "No results found for: $query"
        return 1
    fi

    # Use fzf for interactive selection if available
    if $use_fzf && has_command fzf; then
        echo "$results" | fzf --delimiter='|' \
            --preview 'cat {3}' \
            --preview-window=right:60%:wrap \
            --header="Search: $query" \
            --bind 'enter:execute(bat {3} 2>/dev/null || cat {3})+abort' \
            --with-nth=1,2
    else
        echo "$results" | while IFS='|' read -r cmd desc file; do
            echo -e "${GREEN}${cmd}${NC}"
            [[ -n "$desc" ]] && echo -e "  ${CYAN}→${NC} ${desc}"
            echo -e "  ${YELLOW}📁${NC} ${file}"
            echo
        done
    fi
}

# Quick add - fastest way to add a command
quick_add() {
    local cmd="$1"
    local timestamp=$(date +"%Y-%m-%d %H:%M")

    # Ensure personal directory and quick file exist
    mkdir -p "$PERSONAL_DIR"

    if [[ ! -f "$QUICK_ADD_FILE" ]]; then
        cat > "$QUICK_ADD_FILE" <<EOF
# Quick Captures

Commands I've discovered and want to remember, captured quickly without interrupting flow.

EOF
    fi

    # Add command to quick file
    cat >> "$QUICK_ADD_FILE" <<EOF

## Added: $timestamp

\`\`\`bash
$cmd
\`\`\`

EOF

    print_success "Added to quick captures: $QUICK_ADD_FILE"

    # Rebuild index
    init_index >/dev/null 2>&1 || true

    # Auto-sync if enabled
    [[ "$AUTO_SYNC" == "true" ]] && git_sync || true

    return 0
}

# Standard add with metadata
standard_add() {
    local cmd="$1"
    local desc="${2:-}"
    local tags="${3:-}"
    local category="${4:-$DEFAULT_CATEGORY}"
    local example="${5:-}"

    # Determine file based on category
    local file
    if [[ "$category" == "personal" ]]; then
        file="${PERSONAL_DIR}/commands.md"
        mkdir -p "$PERSONAL_DIR"
    else
        # Parse category (e.g., "tools/docker" or "docker")
        if [[ "$category" == */* ]]; then
            local dir="${category%/*}"
            local name="${category##*/}"
            file="${CS_ROOT}/${dir}/${name}.md"
        else
            # Try to find existing file
            local found=$(find "$CS_ROOT" -name "${category}.md" -type f ! -path "*/personal/*" | head -1)
            if [[ -n "$found" ]]; then
                file="$found"
            else
                file="${CS_ROOT}/tools/${category}.md"
            fi
        fi
    fi

    # Create file if doesn't exist
    if [[ ! -f "$file" ]]; then
        local title=$(basename "$file" .md)
        title="$(echo "$title" | sed 's/-/ /g' | awk '{for(i=1;i<=NF;i++)sub(/./,toupper(substr($i,1,1)),$i)}1')"
        cat > "$file" <<EOF
# $title

## Commands

| COMMAND | DESCRIPTION |
| --- | --- |
EOF
    fi

    # Format description and tags
    local full_desc="$desc"
    if [[ -n "$tags" ]]; then
        full_desc="$desc [Tags: $tags]"
    fi

    # Add to file (append to table)
    echo "| \`$cmd\` | $full_desc |" >> "$file"

    # Add example if provided
    if [[ -n "$example" ]]; then
        cat >> "$file" <<EOF

### Example: $cmd

\`\`\`bash
$example
\`\`\`
EOF
    fi

    print_success "Added to: $file"

    # Rebuild index
    init_index >/dev/null 2>&1 || true

    # Auto-sync if enabled
    [[ "$AUTO_SYNC" == "true" ]] && git_sync || true

    return 0
}

# Interactive add
interactive_add() {
    echo -e "${BOLD}=== Interactive Command Add ===${NC}\n"

    # Get command
    read -p "Command: " cmd
    [[ -z "$cmd" ]] && print_error "Command cannot be empty" && return 1

    # Get description
    read -p "Description (optional): " desc

    # Get tags
    read -p "Tags (comma-separated, optional): " tags

    # Detect category from command
    local detected_category=""
    case "$cmd" in
        docker*) detected_category="tools/docker" ;;
        kubectl*|k8s*) detected_category="tools/kubectl" ;;
        git*) detected_category="tools/git" ;;
        terraform*) detected_category="tools/terraform" ;;
        helm*) detected_category="tools/helm" ;;
        mysql*) detected_category="infra/mysql" ;;
        psql*|postgres*) detected_category="infra/postgresql" ;;
        *) detected_category="personal" ;;
    esac

    read -p "Category [$detected_category]: " category
    category="${category:-$detected_category}"

    # Get example
    read -p "Example (optional, press Enter to skip): " example

    # Confirm
    echo -e "\n${BOLD}Preview:${NC}"
    echo -e "Command: ${GREEN}$cmd${NC}"
    [[ -n "$desc" ]] && echo -e "Description: $desc"
    [[ -n "$tags" ]] && echo -e "Tags: $tags"
    echo -e "Category: $category"
    [[ -n "$example" ]] && echo -e "Example: $example"

    read -p $'\n'"Add this command? [Y/n]: " confirm
    confirm="${confirm:-Y}"

    if [[ "$confirm" =~ ^[Yy]$ ]]; then
        standard_add "$cmd" "$desc" "$tags" "$category" "$example"
    else
        print_info "Cancelled"
    fi
}

# Capture from history
capture_from_history() {
    local limit="${1:-10}"
    local auto_mode="${2:-false}"

    if ! has_command fzf; then
        print_error "fzf is required for history capture. Install it first."
        return 1
    fi

    # Get history file
    local hist_file="${HISTFILE:-$HOME/.bash_history}"
    if [[ -f "$HOME/.zsh_history" ]]; then
        hist_file="$HOME/.zsh_history"
    fi

    if [[ ! -f "$hist_file" ]]; then
        print_error "History file not found"
        return 1
    fi

    if $auto_mode; then
        print_info "Auto-capture mode: finding frequently used commands..."
        # Find commands used more than threshold times
        tail -1000 "$hist_file" | sed 's/^: [0-9]*:[0-9]*;//' | \
            sort | uniq -c | sort -rn | head -20 | \
            awk -v thresh="$CAPTURE_THRESHOLD" '$1 >= thresh {$1=""; print substr($0,2)}'
    else
        # Interactive selection
        local selected
        selected=$(tail -500 "$hist_file" | sed 's/^: [0-9]*:[0-9]*;//' | \
            grep -v '^cs ' | \
            awk '!seen[$0]++' | \
            tail -"$limit" | \
            fzf --multi --reverse --header="Select commands to save (TAB for multi-select)")

        if [[ -n "$selected" ]]; then
            while IFS= read -r cmd; do
                echo -e "\n${BOLD}Capture:${NC} ${GREEN}$cmd${NC}"
                read -p "Add this command? [Y/n/q]: " confirm
                confirm="${confirm:-Y}"

                case "$confirm" in
                    [Qq]*) break ;;
                    [Yy]*)
                        read -p "Description (optional): " desc
                        read -p "Tags (optional): " tags
                        standard_add "$cmd" "$desc" "$tags" "personal"
                        ;;
                    *) continue ;;
                esac
            done <<< "$selected"
        fi
    fi
}

# Create new section
create_section() {
    local name="$1"
    local template="${2:-default}"

    # Determine directory and filename
    local file
    if [[ "$name" == */* ]]; then
        local dir="${name%/*}"
        local section="${name##*/}"
        mkdir -p "${CS_ROOT}/${dir}"
        file="${CS_ROOT}/${dir}/${section}.md"
    else
        # Default to tools
        file="${CS_ROOT}/tools/${name}.md"
    fi

    if [[ -f "$file" ]]; then
        print_warning "File already exists: $file"
        return 1
    fi

    # Create from template
    local title=$(basename "$file" .md)
    title="$(echo "$title" | sed 's/-/ /g' | awk '{for(i=1;i<=NF;i++)sub(/./,toupper(substr($i,1,1)),$i)}1')"

    case "$template" in
        tool|default)
            cat > "$file" <<EOF
# $title

## Installation

\`\`\`bash
# Installation instructions
\`\`\`

## Basic Usage

| COMMAND | DESCRIPTION |
| --- | --- |
| \`$name --help\` | Show help |
| \`$name --version\` | Show version |

## Common Commands

| COMMAND | DESCRIPTION |
| --- | --- |

## Advanced Usage

| COMMAND | DESCRIPTION |
| --- | --- |

## Troubleshooting

| ISSUE | SOLUTION |
| --- | --- |

## Resources

- Official docs:
- GitHub:
EOF
            ;;
        k8s|kubernetes)
            cat > "$file" <<EOF
# $title

## List Resources

| COMMAND | DESCRIPTION |
| --- | --- |
| \`kubectl get $name\` | List all $name |
| \`kubectl get $name -A\` | List across all namespaces |
| \`kubectl describe $name <name>\` | Show detailed information |

## Create/Update

| COMMAND | DESCRIPTION |
| --- | --- |
| \`kubectl apply -f <file>\` | Create/update from file |
| \`kubectl create $name <name>\` | Create new resource |

## Delete

| COMMAND | DESCRIPTION |
| --- | --- |
| \`kubectl delete $name <name>\` | Delete resource |

## Debugging

| COMMAND | DESCRIPTION |
| --- | --- |
| \`kubectl logs $name/<name>\` | View logs |
| \`kubectl exec -it $name/<name> -- /bin/bash\` | Access shell |
EOF
            ;;
        simple)
            cat > "$file" <<EOF
# $title

| COMMAND | DESCRIPTION |
| --- | --- |

EOF
            ;;
    esac

    print_success "Created: $file"

    # Open in editor if available
    if has_command "${EDITOR:-nano}"; then
        read -p "Open in editor? [Y/n]: " open_editor
        if [[ "${open_editor:-Y}" =~ ^[Yy]$ ]]; then
            ${EDITOR:-nano} "$file" || true
        fi
    fi

    # Update index
    init_index >/dev/null 2>&1 || true

    return 0
}

# Git sync
git_sync() {
    if ! git -C "$CS_ROOT" rev-parse --git-dir > /dev/null 2>&1; then
        print_warning "Not a git repository"
        return 1
    fi

    print_info "Syncing with git..."

    # Pull latest
    git -C "$CS_ROOT" pull --quiet 2>/dev/null || true

    # Check if there are changes
    if git -C "$CS_ROOT" diff-index --quiet HEAD --; then
        print_info "No changes to commit"
    else
        # Add all changes
        git -C "$CS_ROOT" add .

        # Commit
        local msg="chore: auto-update cheatsheets [$(date +%Y-%m-%d)]"
        git -C "$CS_ROOT" commit -m "$msg" --quiet

        print_success "Changes committed"
    fi
}

# Show statistics
show_stats() {
    local total_files=$(find "$CS_ROOT" -name "*.md" -type f ! -path "*/.git/*" | wc -l)
    local total_commands=$(grep -c '|' "$INDEX_FILE" 2>/dev/null || echo "0")
    local categories=$(find "$CS_ROOT" -mindepth 1 -maxdepth 1 -type d ! -name ".*" ! -name "personal" | wc -l)
    local personal_commands=$(find "$PERSONAL_DIR" -name "*.md" -type f 2>/dev/null | xargs cat 2>/dev/null | grep -c '```bash' || echo "0")

    print_header "📊 CheatSheets Statistics"
    echo
    echo -e "${BOLD}Repository:${NC}"
    echo -e "  Files: $total_files"
    echo -e "  Categories: $categories"
    echo -e "  Total commands: $total_commands"
    echo
    echo -e "${BOLD}Personal:${NC}"
    echo -e "  Commands: $personal_commands"
    echo

    # Most recent additions
    if [[ -f "$QUICK_ADD_FILE" ]]; then
        local recent=$(tail -5 "$QUICK_ADD_FILE" | grep '^## Added:' | wc -l)
        echo -e "${BOLD}Recent Activity:${NC}"
        echo -e "  Quick captures today: $recent"
    fi

    return 0
}

# List all categories
list_categories() {
    print_header "📂 Available Categories"
    echo

    for dir in "$CS_ROOT"/{linux,tools,infra,misc,windows,personal}; do
        if [[ -d "$dir" ]]; then
            local category=$(basename "$dir")
            local count=$(find "$dir" -name "*.md" -type f | wc -l)
            echo -e "${CYAN}$category${NC} ($count files)"
            find "$dir" -name "*.md" -type f | while read -r file; do
                local name=$(basename "$file" .md)
                echo -e "  • $name"
            done || true
            echo
        fi
    done

    return 0
}

# Browse with fzf
browse() {
    if ! has_command fzf; then
        print_error "fzf is required for browsing. Install it first."
        return 1
    fi

    # Ensure index exists
    [[ ! -f "$INDEX_FILE" ]] && init_index

    # Interactive browser (don't fail if user cancels)
    cat "$INDEX_FILE" | fzf --delimiter='|' \
        --preview 'bat --color=always --style=numbers {3} 2>/dev/null || cat {3}' \
        --preview-window=right:60%:wrap \
        --header="Browse all commands (Ctrl-C to exit)" \
        --with-nth=1,2 \
        --bind 'enter:execute(less {3})+abort' || true

    return 0
}

# Show help
show_help() {
    cat <<EOF
${BOLD}${CYAN}cs${NC} - CheatSheets CLI

${BOLD}USAGE:${NC}
  cs [COMMAND] [OPTIONS]

${BOLD}QUICK COMMANDS:${NC}
  cs quick "command"              Add command instantly (< 2 seconds)
  cs "search term"                Search for commands
  cs add "cmd" --desc "..."       Add with description
  cs add -i                       Interactive add mode
  cs capture                      Capture from shell history
  cs new <name>                   Create new section

${BOLD}SEARCH & BROWSE:${NC}
  cs search <query>               Search all commands
  cs search <query> -c <category> Search in specific category
  cs browse, cs -i                Interactive browser (requires fzf)
  cs list                         List all categories

${BOLD}CONTENT MANAGEMENT:${NC}
  cs add "command" [OPTIONS]      Add command with metadata
    --desc "description"          Add description
    --tags "tag1,tag2"            Add tags
    --cat "category"              Specify category
    --example "example"           Add example
  cs new section <name>           Create new section
    --template <type>             Use template (tool, k8s, simple)
  cs capture                      Capture from history
    --last N                      Show last N commands
    --auto                        Auto-capture frequent commands

${BOLD}MAINTENANCE:${NC}
  cs index                        Rebuild search index
  cs sync                         Git sync (pull, commit, push)
  cs stats                        Show statistics
  cs validate                     Validate all commands
  cs backup                       Backup personal additions

${BOLD}EXAMPLES:${NC}
  # Quick capture
  cs quick "docker system prune -a"

  # Add with metadata
  cs add "kubectl get pods -A" --desc "List all pods" --tags k8s,debug

  # Interactive add
  cs add -i

  # Search
  cs docker
  cs "list pods"

  # Browse interactively
  cs -i

  # Create new section
  cs new consul --template tool
  cs new monitoring/prometheus

${BOLD}CONFIGURATION:${NC}
  Config file: ${CONFIG_FILE}
  Personal dir: ${PERSONAL_DIR}
  Index file: ${INDEX_FILE}

${BOLD}ENVIRONMENT VARIABLES:${NC}
  CS_ROOT              CheatSheets root directory
  CS_AUTO_SYNC         Auto-sync with git (true/false)
  CS_CAPTURE_THRESHOLD Threshold for auto-capture

For more info: https://github.com/mlorentedev/cheat-sheets
EOF
}

#==============================================================================
# MAIN COMMAND DISPATCHER
#==============================================================================

main() {
    # No arguments - show help
    if [[ $# -eq 0 ]]; then
        show_help
        exit 0
    fi

    local cmd="$1"
    shift

    case "$cmd" in
        # Quick add
        quick|q)
            [[ $# -eq 0 ]] && print_error "Usage: cs quick <command>" && exit 1
            quick_add "$*"
            ;;

        # Standard add
        add|a)
            if [[ "$1" == "-i" ]] || [[ "$1" == "--interactive" ]]; then
                interactive_add
            else
                local command=""
                local desc=""
                local tags=""
                local category="$DEFAULT_CATEGORY"
                local example=""

                # Parse arguments
                while [[ $# -gt 0 ]]; do
                    case "$1" in
                        --desc|--description|-d)
                            desc="$2"
                            shift 2
                            ;;
                        --tags|--tag|-t)
                            tags="$2"
                            shift 2
                            ;;
                        --cat|--category|-c)
                            category="$2"
                            shift 2
                            ;;
                        --example|-e)
                            example="$2"
                            shift 2
                            ;;
                        *)
                            command="$1"
                            shift
                            ;;
                    esac
                done

                [[ -z "$command" ]] && print_error "Usage: cs add <command> [--desc DESC] [--tags TAGS]" && exit 1
                standard_add "$command" "$desc" "$tags" "$category" "$example"
            fi
            ;;

        # Search
        search|s|find|f)
            [[ $# -eq 0 ]] && print_error "Usage: cs search <query>" && exit 1
            local query="$1"
            local category=""
            shift

            # Parse options
            while [[ $# -gt 0 ]]; do
                case "$1" in
                    -c|--category|--cat)
                        category="$2"
                        shift 2
                        ;;
                    *)
                        shift
                        ;;
                esac
            done

            search_commands "$query" "$category"
            ;;

        # Browse
        browse|b|-i|--interactive)
            browse
            ;;

        # Capture
        capture|cap)
            local limit=10
            local auto=false

            while [[ $# -gt 0 ]]; do
                case "$1" in
                    --last|-n)
                        limit="$2"
                        shift 2
                        ;;
                    --auto|-a)
                        auto=true
                        shift
                        ;;
                    *)
                        shift
                        ;;
                esac
            done

            capture_from_history "$limit" "$auto"
            ;;

        # New section
        new|n|create)
            [[ $# -eq 0 ]] && print_error "Usage: cs new <section|tool|category> <name>" && exit 1
            local type="$1"
            shift

            case "$type" in
                section|s|tool|t)
                    local name="$1"
                    local template="tool"
                    shift

                    while [[ $# -gt 0 ]]; do
                        case "$1" in
                            --template|-t)
                                template="$2"
                                shift 2
                                ;;
                            *)
                                shift
                                ;;
                        esac
                    done

                    create_section "$name" "$template"
                    ;;
                category|cat)
                    local name="$1"
                    mkdir -p "${CS_ROOT}/${name}"
                    print_success "Created category: ${CS_ROOT}/${name}"
                    ;;
                *)
                    # Assume it's a name without type
                    create_section "$type" "tool"
                    ;;
            esac
            ;;

        # Index
        index|idx|rebuild)
            init_index
            ;;

        # Sync
        sync)
            git_sync
            ;;

        # Stats
        stats|statistics|info)
            show_stats
            ;;

        # List
        list|ls|categories)
            list_categories
            ;;

        # Help
        help|h|-h|--help)
            show_help
            ;;

        # Version
        version|v|-v|--version)
            echo "cs version 1.0.0"
            ;;

        # Default: treat as search query
        *)
            search_commands "$cmd $*"
            ;;
    esac
}

# Run main
main "$@"
