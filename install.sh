#!/bin/bash

# CheatSheets Installation Script
# Simple, clear installer with no magic

set -e

# Colors
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m'

print_step() {
    echo -e "${BLUE}==>${NC} $*"
}

print_success() {
    echo -e "${GREEN}✓${NC} $*"
}

print_warning() {
    echo -e "${YELLOW}!${NC} $*"
}

print_error() {
    echo -e "${RED}✗${NC} $*" >&2
}

# Detect shell
detect_shell() {
    if [ -n "$ZSH_VERSION" ]; then
        echo "zsh"
    elif [ -n "$BASH_VERSION" ]; then
        echo "bash"
    else
        echo "unknown"
    fi
}

# Get shell RC file
get_shell_rc() {
    local shell_type=$(detect_shell)
    case "$shell_type" in
        zsh)
            echo "$HOME/.zshrc"
            ;;
        bash)
            if [ -f "$HOME/.bashrc" ]; then
                echo "$HOME/.bashrc"
            else
                echo "$HOME/.bash_profile"
            fi
            ;;
        *)
            echo ""
            ;;
    esac
}

echo ""
echo "CheatSheets Installation"
echo "========================"
echo ""

# Get installation directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
CS_ROOT="${CS_ROOT:-$SCRIPT_DIR}"

print_step "Installation directory: $CS_ROOT"

# Make cs executable
print_step "Making cs executable..."
chmod +x "$CS_ROOT/cs"
print_success "Done"

# Create necessary directories
print_step "Creating directories..."
mkdir -p "$CS_ROOT/personal"
mkdir -p "$CS_ROOT/templates"
print_success "Created personal/ and templates/ directories"

# Check if already in PATH
if echo "$PATH" | grep -q "$CS_ROOT"; then
    print_success "Already in PATH"
else
    # Add to PATH
    SHELL_RC=$(get_shell_rc)

    if [ -n "$SHELL_RC" ]; then
        print_step "Adding to PATH in $SHELL_RC..."

        # Check if already added
        if grep -q "cheat-sheets" "$SHELL_RC" 2>/dev/null; then
            print_warning "PATH entry already exists in $SHELL_RC"
        else
            cat >> "$SHELL_RC" <<EOF

# CheatSheets
export PATH="\$PATH:$CS_ROOT"
export CS_ROOT="$CS_ROOT"
EOF
            print_success "Added to $SHELL_RC"
            echo ""
            print_warning "Run: source $SHELL_RC"
            echo "         or restart your shell to use 'cs' command"
        fi
    else
        print_warning "Could not detect shell config file"
        echo "         Add this to your shell RC file:"
        echo ""
        echo "         export PATH=\"\$PATH:$CS_ROOT\""
        echo "         export CS_ROOT=\"$CS_ROOT\""
    fi
fi

# Check for dependencies
echo ""
print_step "Checking dependencies..."

# Check for fzf
if command -v fzf >/dev/null 2>&1; then
    print_success "fzf is installed (for interactive browsing)"
else
    print_warning "fzf not found (optional, but recommended)"
    echo ""
    echo "Install fzf for better browsing experience:"
    echo "  Ubuntu/Debian: sudo apt install fzf"
    echo "  macOS: brew install fzf"
    echo "  Manual: git clone --depth 1 https://github.com/junegunn/fzf.git ~/.fzf && ~/.fzf/install"
fi

# Check for bat (optional)
if command -v bat >/dev/null 2>&1; then
    print_success "bat is installed (for syntax highlighting)"
else
    print_warning "bat not found (optional, for better previews)"
fi

# Build initial index
echo ""
print_step "Building search index..."
"$CS_ROOT/cs" index
print_success "Index created"

# Create example config
if [ ! -f "$CS_ROOT/.cs.config" ]; then
    print_step "Creating example configuration..."
    cat > "$CS_ROOT/.cs.config.example" <<EOF
# CheatSheets Configuration
# Copy this to .cs.config and customize

# Editor for opening files
EDITOR=\${EDITOR:-nano}

# Auto-sync with git after changes
AUTO_SYNC=false

# Default category for new commands
DEFAULT_CATEGORY=personal

# Where quick adds go
QUICK_ADD_FILE="\${PERSONAL_DIR}/quick.md"

# Threshold for auto-capture (commands used N+ times)
CAPTURE_THRESHOLD=3
EOF
    print_success "Created .cs.config.example"
    echo "         Copy to .cs.config and customize if needed"
fi

# Success message
echo ""
echo "================================"
print_success "Installation complete!"
echo "================================"
echo ""
echo "Quick start:"
echo ""
echo "  1. Search commands:        cs docker"
echo "  2. Quick add:              cs quick \"your command\""
echo "  3. Interactive add:        cs add -i"
echo "  4. Browse all:             cs -i"
echo "  5. See all commands:       cs help"
echo ""
echo "Examples:"
echo ""
echo "  cs quick \"kubectl get pods -A\""
echo "  cs add \"docker system prune -a\" --desc \"Clean up docker\""
echo "  cs search docker"
echo "  cs new consul"
echo ""

if [ -n "$SHELL_RC" ] && ! echo "$PATH" | grep -q "$CS_ROOT"; then
    echo "Don't forget to reload your shell:"
    echo "  source $SHELL_RC"
    echo ""
fi

echo "For full documentation, see: $CS_ROOT/README.md"
echo ""
