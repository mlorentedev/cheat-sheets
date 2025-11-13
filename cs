#!/usr/bin/env bash

# CheatSheets (cs) - Simple command lookup tool

set -e

# Script directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]:-$0}")" && pwd)"
CS_ROOT="${CS_ROOT:-$SCRIPT_DIR}"
PERSONAL_DIR="${CS_ROOT}/personal"
QUICK_FILE="${PERSONAL_DIR}/quick.md"

mkdir -p "$PERSONAL_DIR"

# Colors
G='\033[0;32m'
B='\033[0;34m'
NC='\033[0m'

# Quick add
quick_add() {
    [ -z "$*" ] && echo "Usage: cs quick <command>" && exit 1

    if [ ! -f "$QUICK_FILE" ]; then
        printf "# Quick Commands\n\n" > "$QUICK_FILE"
    fi

    printf "\n\`\`\`bash\n%s\n\`\`\`\n" "$*" >> "$QUICK_FILE"
    printf "${G}✓${NC} Saved to %s\n" "$QUICK_FILE"
}

# Search
search_commands() {
    [ -z "$*" ] && echo "Usage: cs search <keyword>" && exit 1

    printf "${B}Searching for: %s${NC}\n\n" "$*"
    find "$CS_ROOT" -name "*.md" -type f ! -path "*/.git/*" 2>/dev/null | while read -r file; do
        if grep -i -q "$*" "$file" 2>/dev/null; then
            printf "${G}%s${NC}\n" "${file#$CS_ROOT/}"
            grep -i -C 1 "$*" "$file" 2>/dev/null | head -5
            echo ""
        fi
    done
}

# List
list_all() {
    printf "${B}Available cheatsheets:${NC}\n\n"
    for dir in linux tools infra misc windows personal; do
        [ ! -d "$CS_ROOT/$dir" ] && continue
        printf "${G}%s/${NC}\n" "$dir"
        find "$CS_ROOT/$dir" -name "*.md" -type f 2>/dev/null | while read -r f; do
            printf "  - %s\n" "$(basename "$f" .md)"
        done
        echo ""
    done
}

# View file
view_file() {
    local file=""
    for dir in linux tools infra misc windows personal; do
        if [ -f "$CS_ROOT/$dir/$1.md" ]; then
            file="$CS_ROOT/$dir/$1.md"
            break
        fi
    done

    if [ -z "$file" ]; then
        echo "Not found: $1"
        echo "Try: cs list"
        exit 1
    fi

    if command -v less >/dev/null 2>&1; then
        less "$file"
    else
        cat "$file"
    fi
}

# Help
show_help() {
    cat <<'EOF'
cs - Simple CheatSheets

USAGE:
  cs quick <command>      Save a command
  cs search <keyword>     Search cheatsheets
  cs list                 List all files
  cs <name>               View cheatsheet (e.g., cs docker)
  cs help                 Show this help

EXAMPLES:
  cs quick "docker ps -a"
  cs search kubernetes
  cs docker
  cs list

EOF
}

# Main
if [ $# -eq 0 ]; then
    show_help
    exit 0
fi

case "$1" in
    quick|q) shift; quick_add "$@" ;;
    search|s) shift; search_commands "$@" ;;
    list|ls) list_all ;;
    help|h|-h|--help) show_help ;;
    *) view_file "$1" ;;
esac
