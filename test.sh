#!/bin/bash

# Simple test suite for cs

set +e

# Colors
GREEN='\033[0;32m'
RED='\033[0;31m'
BLUE='\033[0;34m'
NC='\033[0m'

TESTS_PASSED=0
TESTS_FAILED=0

print_pass() {
    echo -e "${GREEN}[PASS]${NC} $*"
    ((TESTS_PASSED++))
}

print_fail() {
    echo -e "${RED}[FAIL]${NC} $*"
    ((TESTS_FAILED++))
}

# Cleanup
cleanup() {
    rm -rf personal/ 2>/dev/null || true
    rm -f tools/test-tool.md 2>/dev/null || true
}

trap 'cleanup || true' EXIT

echo "========================================="
echo "CheatSheets Test Suite"
echo "========================================="
echo ""

# Test 1: cs is executable
if [ -x cs ]; then
    print_pass "cs is executable"
else
    print_fail "cs is not executable"
fi

# Test 2: cs help works
if ./cs help >/dev/null 2>&1; then
    print_pass "cs help works"
else
    print_fail "cs help failed"
fi

# Test 3: cs quick works
if ./cs quick "test command" >/dev/null 2>&1; then
    if [ -f personal/quick.md ] && grep -q "test command" personal/quick.md; then
        print_pass "cs quick works"
    else
        print_fail "cs quick did not save command"
    fi
else
    print_fail "cs quick failed"
fi

# Test 4: cs search works
if ./cs search docker >/dev/null 2>&1; then
    print_pass "cs search works"
else
    print_fail "cs search failed"
fi

# Test 5: cs list works
if ./cs list >/dev/null 2>&1; then
    print_pass "cs list works"
else
    print_fail "cs list failed"
fi

# Test 6: install.sh is executable
if [ -x install.sh ]; then
    print_pass "install.sh is executable"
else
    print_fail "install.sh is not executable"
fi

# Test 7: README exists
if [ -f README.md ] && [ -s README.md ]; then
    print_pass "README.md exists"
else
    print_fail "README.md missing"
fi

# Test 8: .gitignore has personal/
if grep -q "personal/" .gitignore; then
    print_pass ".gitignore has personal/"
else
    print_fail ".gitignore missing personal/"
fi

# Test 9: Required directories exist
all_dirs_exist=true
for dir in linux tools infra misc; do
    if [ ! -d "$dir" ]; then
        all_dirs_exist=false
        break
    fi
done

if $all_dirs_exist; then
    print_pass "Required directories exist"
else
    print_fail "Some directories missing"
fi

# Test 10: Markdown files are not empty
empty_files=0
find . -name "*.md" -type f ! -path "*/.*" ! -path "*/personal/*" 2>/dev/null | while read -r file; do
    if [ ! -s "$file" ]; then
        ((empty_files++))
    fi
done

if [ $empty_files -eq 0 ]; then
    print_pass "No empty markdown files"
else
    print_fail "Found empty markdown files"
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
