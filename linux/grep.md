# Grep Cheat-Sheet

## Basic Usage

| Command | Description |
| --- | --- |
| `grep "pattern" file.txt` | Search for pattern in file |
| `grep "pattern" file1 file2` | Search in multiple files |
| `grep -r "pattern" /path/` | Recursive search in directory |
| `grep -i "pattern" file.txt` | Case-insensitive search |
| `grep -v "pattern" file.txt` | Invert match (lines NOT containing pattern) |
| `grep -w "word" file.txt` | Match whole words only |
| `grep -x "exact line" file.txt` | Match entire line exactly |

## Output Control

| Command | Description |
| --- | --- |
| `grep -n "pattern" file.txt` | Show line numbers |
| `grep -c "pattern" file.txt` | Count matching lines |
| `grep -l "pattern" *.txt` | Show only filenames with matches |
| `grep -L "pattern" *.txt` | Show only filenames without matches |
| `grep -h "pattern" *.txt` | Suppress filename in output |
| `grep -H "pattern" file.txt` | Force showing filename |

## Context Control

| Command | Description |
| --- | --- |
| `grep -A 3 "pattern" file.txt` | Show 3 lines after match |
| `grep -B 2 "pattern" file.txt` | Show 2 lines before match |
| `grep -C 2 "pattern" file.txt` | Show 2 lines before and after match |
| `grep -m 5 "pattern" file.txt` | Stop after 5 matches |

## Regular Expressions

| Command | Description |
| --- | --- |
| `grep "^pattern" file.txt` | Match lines starting with pattern |
| `grep "pattern$" file.txt` | Match lines ending with pattern |
| `grep "^$" file.txt` | Match empty lines |
| `grep "[0-9]" file.txt` | Match lines containing digits |
| `grep "[a-zA-Z]" file.txt` | Match lines containing letters |
| `grep "colou?r" file.txt` | Match "color" or "colour" |
| `grep "pattern.*other" file.txt` | Match pattern followed by other |

## Extended Regular Expressions

| Command | Description |
| --- | --- |
| `grep -E "pattern1\|pattern2" file.txt` | Match pattern1 OR pattern2 |
| `grep -E "pattern+" file.txt` | Match one or more occurrences |
| `grep -E "pattern{2,4}" file.txt` | Match 2 to 4 occurrences |
| `grep -E "\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z\|a-z]{2,}\b" file.txt` | Match email addresses |

## Practical Examples

| Command | Description |
| --- | --- |
| `ps aux \| grep "process_name"` | Find running processes |
| `history \| grep "command"` | Search command history |
| `grep -r "TODO" --include="*.py" /project/` | Find TODOs in Python files |
| `grep -r "function.*(" --include="*.js" .` | Find function definitions in JS |
| `log \| grep -E "(error\|warning)" -i` | Filter log for errors and warnings |
| `grep -o -E "\b[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\b" file.txt` | Extract IP addresses |
| `grep -v "^#\|^$" config.file` | Show config file without comments and empty lines |