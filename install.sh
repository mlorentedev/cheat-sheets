#!/bin/bash

# Simple CheatSheets installer

echo "Installing CheatSheets..."
echo ""

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Make cs executable
chmod +x "$SCRIPT_DIR/cs"
echo "✓ Made cs executable"

# Create personal directory
mkdir -p "$SCRIPT_DIR/personal"
echo "✓ Created personal directory"

# Detect shell
if [ -n "$ZSH_VERSION" ]; then
    SHELL_RC="$HOME/.zshrc"
elif [ -n "$BASH_VERSION" ]; then
    SHELL_RC="$HOME/.bashrc"
else
    SHELL_RC=""
fi

# Add to PATH
if [ -n "$SHELL_RC" ]; then
    if ! grep -q "cheat-sheets" "$SHELL_RC" 2>/dev/null; then
        echo "" >> "$SHELL_RC"
        echo "# CheatSheets" >> "$SHELL_RC"
        echo "export PATH=\"\$PATH:$SCRIPT_DIR\"" >> "$SHELL_RC"
        echo "✓ Added to $SHELL_RC"
        echo ""
        echo "Run: source $SHELL_RC"
    else
        echo "✓ Already in PATH"
    fi
else
    echo "⚠ Could not detect shell"
    echo "  Add to your PATH manually:"
    echo "  export PATH=\"\$PATH:$SCRIPT_DIR\""
fi

echo ""
echo "Installation complete!"
echo ""
echo "Try:"
echo "  cs help"
echo "  cs quick \"docker ps -a\""
echo "  cs search docker"
echo "  cs docker"
echo ""
