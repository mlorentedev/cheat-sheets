#!/bin/bash

# CheatSheets Test Suite
# Can be run locally or in CI

# Don't exit on first error - we want to count all failures
set +e

# Colors
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

export CS_ROOT="$SCRIPT_DIR"

TESTS_PASSED=0
TESTS_FAILED=0

print_test() {
    echo -e "${BLUE}[TEST]${NC} $*"
}

print_pass() {
    echo -e "${GREEN}[PASS]${NC} $*"
    ((TESTS_PASSED++))
}

print_fail() {
    echo -e "${RED}[FAIL]${NC} $*"
    ((TESTS_FAILED++))
}

print_info() {
    echo -e "${YELLOW}[INFO]${NC} $*"
}

# Cleanup function
cleanup() {
    print_info "Cleaning up test files..."
    rm -rf personal/ 2>/dev/null || true
    rm -f .cs_index 2>/dev/null || true
    rm -rf .cs_cache/ 2>/dev/null || true
    rm -f tools/test-tool.md 2>/dev/null || true
    git checkout tools/docker.md 2>/dev/null || true
    git checkout tools/terraform.md 2>/dev/null || true
}

# Trap exit to cleanup (but don't fail on cleanup errors)
trap 'cleanup || true' EXIT

echo ""
echo "========================================="
echo "CheatSheets Test Suite"
echo "========================================="
echo ""

# Test 1: Shell script syntax
print_test "Validating shell script syntax..."
if bash -n cs && bash -n install.sh; then
    print_pass "Shell scripts have valid syntax"
else
    print_fail "Shell script syntax validation failed"
fi

# Test 2: cs executable
print_test "Checking cs is executable..."
if [ -x cs ]; then
    print_pass "cs is executable"
else
    print_fail "cs is not executable"
    chmod +x cs
fi

# Test 3: install.sh executable
print_test "Checking install.sh is executable..."
if [ -x install.sh ]; then
    print_pass "install.sh is executable"
else
    print_fail "install.sh is not executable"
    chmod +x install.sh
fi

# Test 4: cs help
print_test "Testing cs help command..."
if ./cs help > /dev/null 2>&1; then
    print_pass "cs help works"
else
    print_fail "cs help failed"
fi

# Test 5: cs index
print_test "Testing cs index command..."
if ./cs index > /dev/null 2>&1; then
    if [ -f .cs_index ]; then
        print_pass "cs index created index file"
    else
        print_fail "cs index did not create index file"
    fi
else
    print_fail "cs index failed"
fi

# Test 6: cs quick
print_test "Testing cs quick command..."
if ./cs quick "test command for suite" > /dev/null 2>&1; then
    if [ -f personal/quick.md ] && grep -q "test command for suite" personal/quick.md; then
        print_pass "cs quick works"
    else
        print_fail "cs quick did not add command"
    fi
else
    print_fail "cs quick failed"
fi

# Test 7: cs add
print_test "Testing cs add command..."
if ./cs add "docker ps -a" --desc "List all containers" --tags docker --cat tools/docker > /dev/null 2>&1; then
    if grep -q "docker ps -a" tools/docker.md; then
        print_pass "cs add works"
    else
        print_fail "cs add did not add command"
    fi
else
    print_fail "cs add failed"
fi

# Test 8: cs search
print_test "Testing cs search command..."
if ./cs search docker > /dev/null 2>&1; then
    print_pass "cs search works"
else
    print_fail "cs search failed"
fi

# Test 9: cs list
print_test "Testing cs list command..."
if ./cs list > /dev/null 2>&1; then
    print_pass "cs list works"
else
    print_fail "cs list failed"
fi

# Test 10: cs stats
print_test "Testing cs stats command..."
if ./cs stats > /dev/null 2>&1; then
    print_pass "cs stats works"
else
    print_fail "cs stats failed"
fi

# Test 11: cs new
print_test "Testing cs new command..."
if echo "" | ./cs new test-tool --template tool > /dev/null 2>&1; then
    if [ -f tools/test-tool.md ]; then
        print_pass "cs new works"
    else
        print_fail "cs new did not create file"
    fi
else
    print_fail "cs new failed"
fi

# Test 12: Templates exist
print_test "Checking templates exist..."
all_templates_exist=true
for template in tool k8s simple; do
    if [ ! -f "templates/${template}.md" ]; then
        all_templates_exist=false
        break
    fi
done

if $all_templates_exist; then
    print_pass "All templates exist"
else
    print_fail "Some templates missing"
fi

# Test 13: Required directories
print_test "Checking required directories..."
all_dirs_exist=true
for dir in linux tools infra misc templates; do
    if [ ! -d "$dir" ]; then
        all_dirs_exist=false
        break
    fi
done

if $all_dirs_exist; then
    print_pass "All required directories exist"
else
    print_fail "Some directories missing"
fi

# Test 14: Configuration example
print_test "Checking configuration example..."
if [ -f .cs.config.example ] && bash -n .cs.config.example 2>/dev/null; then
    print_pass "Configuration example is valid"
else
    print_fail "Configuration example invalid or missing"
fi

# Test 15: .gitignore
print_test "Checking .gitignore..."
all_patterns_exist=true
for pattern in "personal/" ".cs.config" ".cs_index"; do
    if ! grep -q "$pattern" .gitignore; then
        all_patterns_exist=false
        break
    fi
done

if $all_patterns_exist; then
    print_pass ".gitignore has required patterns"
else
    print_fail ".gitignore missing patterns"
fi

# Test 16: README exists and has content
print_test "Checking README.md..."
if [ -f README.md ] && [ -s README.md ] && grep -q "CheatSheets" README.md; then
    print_pass "README.md exists and has content"
else
    print_fail "README.md missing or empty"
fi

# Test 17: Markdown files are not empty
print_test "Checking markdown files are not empty..."
empty_files=0
find . -name "*.md" -type f ! -path "*/.*" ! -path "*/personal/*" | while read -r file; do
    if [ ! -s "$file" ]; then
        ((empty_files++))
    fi
done

if [ $empty_files -eq 0 ]; then
    print_pass "No empty markdown files"
else
    print_fail "Found $empty_files empty markdown files"
fi

# Summary
echo ""
echo "========================================="
echo "Test Summary"
echo "========================================="
echo -e "${GREEN}Passed: $TESTS_PASSED${NC}"
echo -e "${RED}Failed: $TESTS_FAILED${NC}"
echo ""

if [ $TESTS_FAILED -eq 0 ]; then
    echo -e "${GREEN}All tests passed!${NC}"
    exit 0
else
    echo -e "${RED}Some tests failed!${NC}"
    exit 1
fi
