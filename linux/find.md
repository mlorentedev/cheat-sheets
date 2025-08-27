# Find Cheat-Sheet

## Basic Usage

| Command | Description |
| --- | --- |
| `find .` | Find all files in current directory and subdirectories |
| `find /path` | Find all files in specified path |
| `find . -name "*.txt"` | Find all .txt files |
| `find . -name "*.txt" -o -name "*.log"` | Find all .txt OR .log files |
| `find . -iname "*.TXT"` | Case-insensitive name search |
| `find . -not -name "*.txt"` | Find files NOT matching pattern |

## Search by Type

| Command | Description |
| --- | --- |
| `find . -type f` | Find only files |
| `find . -type d` | Find only directories |
| `find . -type l` | Find only symbolic links |
| `find . -type f -name "*.sh" -executable` | Find executable shell scripts |

## Search by Size

| Command | Description |
| --- | --- |
| `find . -size +100M` | Find files larger than 100MB |
| `find . -size -10k` | Find files smaller than 10KB |
| `find . -size 50c` | Find files exactly 50 bytes |
| `find . -empty` | Find empty files and directories |

## Search by Time

| Command | Description |
| --- | --- |
| `find . -mtime -7` | Find files modified in last 7 days |
| `find . -mtime +30` | Find files modified more than 30 days ago |
| `find . -atime -1` | Find files accessed in last 24 hours |
| `find . -newer file.txt` | Find files newer than file.txt |

## Search by Permissions

| Command | Description |
| --- | --- |
| `find . -perm 644` | Find files with exact permissions 644 |
| `find . -perm -644` | Find files with at least permissions 644 |
| `find . -perm /u+w` | Find files writable by owner |
| `find . -user username` | Find files owned by username |
| `find . -group groupname` | Find files owned by groupname |

## Execute Actions

| Command | Description |
| --- | --- |
| `find . -name "*.tmp" -delete` | Delete all .tmp files |
| `find . -name "*.log" -exec rm {} \;` | Delete all .log files using exec |
| `find . -name "*.txt" -exec cp {} /backup \;` | Copy all .txt files to /backup |
| `find . -name "*.sh" -exec chmod +x {} \;` | Make all .sh files executable |
| `find . -type f -exec ls -l {} \;` | List details of all files |

## Advanced Examples

| Command | Description |
| --- | --- |
| `find . -name "*.jpg" -o -name "*.png" \| xargs -I {} cp {} /images/` | Find and copy image files |
| `find . -type f -name "*.log" -mtime +7 -delete` | Delete log files older than 7 days |
| `find . -type f -size +100M -exec du -h {} \;` | Show size of files larger than 100MB |
| `find . -name "core" -type f -delete` | Delete all core dump files |