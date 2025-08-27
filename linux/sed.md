# Sed Cheat-Sheet

## Basic Substitution

| Command | Description |
| --- | --- |
| `sed 's/old/new/' file.txt` | Replace first occurrence of 'old' with 'new' on each line |
| `sed 's/old/new/g' file.txt` | Replace all occurrences of 'old' with 'new' |
| `sed 's/old/new/2' file.txt` | Replace second occurrence on each line |
| `sed 's/old/new/i' file.txt` | Case-insensitive replacement |
| `sed 's/old/new/ig' file.txt` | Case-insensitive global replacement |
| `sed -i 's/old/new/g' file.txt` | Edit file in-place |

## Line Operations

| Command | Description |
| --- | --- |
| `sed -n '5p' file.txt` | Print only line 5 |
| `sed -n '1,5p' file.txt` | Print lines 1 to 5 |
| `sed -n '$p' file.txt` | Print last line |
| `sed '3d' file.txt` | Delete line 3 |
| `sed '1,5d' file.txt` | Delete lines 1 to 5 |
| `sed '/pattern/d' file.txt` | Delete lines containing pattern |
| `sed '/^$/d' file.txt` | Delete empty lines |

## Insert and Append

| Command | Description |
| --- | --- |
| `sed '2i\New line' file.txt` | Insert text before line 2 |
| `sed '2a\New line' file.txt` | Append text after line 2 |
| `sed '/pattern/i\New line' file.txt` | Insert text before lines matching pattern |
| `sed '/pattern/a\New line' file.txt` | Append text after lines matching pattern |
| `sed '1s/^/Header: /' file.txt` | Add text to beginning of first line |

## Advanced Substitutions

| Command | Description |
| --- | --- |
| `sed 's/\(.*\)/[\1]/' file.txt` | Surround each line with brackets |
| `sed 's/\([0-9]*\)\.\([0-9]*\)/\2.\1/' file.txt` | Swap parts before and after decimal |
| `sed 's/.*/\U&/' file.txt` | Convert entire line to uppercase |
| `sed 's/.*/\L&/' file.txt` | Convert entire line to lowercase |
| `sed 's/\b\w/\u&/g' file.txt` | Capitalize first letter of each word |

## Multiple Commands

| Command | Description |
| --- | --- |
| `sed -e 's/old1/new1/g' -e 's/old2/new2/g' file.txt` | Multiple substitutions |
| `sed '1d; $d' file.txt` | Delete first and last line |
| `sed '/start/,/end/d' file.txt` | Delete from 'start' pattern to 'end' pattern |
| `sed -n '/start/,/end/p' file.txt` | Print from 'start' pattern to 'end' pattern |

## Practical Examples

| Command | Description |
| --- | --- |
| `sed 's/#.*//' file.txt` | Remove comments (everything after #) |
| `sed '/^$/d; /^#/d' file.txt` | Remove empty lines and comment lines |
| `sed 's/^[ \t]*//' file.txt` | Remove leading whitespace |
| `sed 's/[ \t]*$//' file.txt` | Remove trailing whitespace |
| `sed 's/[ \t]\+/ /g' file.txt` | Replace multiple spaces/tabs with single space |
| `sed 'N;s/\n/ /' file.txt` | Join consecutive lines with space |
| `sed 's/\([0-9]\{3\}\)\([0-9]\{3\}\)\([0-9]\{4\}\)/(\1) \2-\3/' file.txt` | Format phone numbers |
| `sed -n 'l' file.txt` | Show file with line endings and special characters visible |

## Working with Files

| Command | Description |
| --- | --- |
| `sed -i.backup 's/old/new/g' file.txt` | Edit in-place with backup |
| `sed 's/old/new/g' file.txt > newfile.txt` | Save changes to new file |
| `sed -f script.sed file.txt` | Apply sed script from file |