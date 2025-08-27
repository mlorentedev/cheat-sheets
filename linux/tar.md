# Tar Cheat-Sheet

## Creating Archives

| Command | Description |
| --- | --- |
| `tar -cf archive.tar files/` | Create archive from files/ directory |
| `tar -czf archive.tar.gz files/` | Create compressed gzip archive |
| `tar -cjf archive.tar.bz2 files/` | Create compressed bzip2 archive |
| `tar -cJf archive.tar.xz files/` | Create compressed xz archive |
| `tar -czf archive.tar.gz file1 file2` | Archive multiple specific files |
| `tar -czf archive.tar.gz --exclude='*.log' project/` | Create archive excluding .log files |

## Extracting Archives

| Command | Description |
| --- | --- |
| `tar -xf archive.tar` | Extract tar archive |
| `tar -xzf archive.tar.gz` | Extract gzip compressed archive |
| `tar -xjf archive.tar.bz2` | Extract bzip2 compressed archive |
| `tar -xJf archive.tar.xz` | Extract xz compressed archive |
| `tar -xzf archive.tar.gz -C /destination/` | Extract to specific directory |
| `tar -xzf archive.tar.gz file.txt` | Extract only specific file |

## Viewing Archives

| Command | Description |
| --- | --- |
| `tar -tf archive.tar` | List contents of archive |
| `tar -tzf archive.tar.gz` | List contents of compressed archive |
| `tar -tvf archive.tar` | List contents with detailed information |
| `tar -tzf archive.tar.gz \| head -20` | Show first 20 files in archive |

## Advanced Options

| Command | Description |
| --- | --- |
| `tar -czf archive.tar.gz --exclude-vcs project/` | Exclude version control files (.git, .svn) |
| `tar -czf archive.tar.gz --exclude='*.tmp' --exclude='node_modules' project/` | Multiple exclusions |
| `tar -czf backup.tar.gz --newer-mtime='2023-01-01' /home/user/` | Only files newer than date |
| `tar -czf archive.tar.gz -T filelist.txt` | Archive files listed in filelist.txt |
| `tar --update -czf archive.tar.gz files/` | Add only newer files to existing archive |

## Progress and Verbosity

| Command | Description |
| --- | --- |
| `tar -czvf archive.tar.gz files/` | Verbose output while creating |
| `tar -xzvf archive.tar.gz` | Verbose output while extracting |
| `tar -czf archive.tar.gz files/ --checkpoint=1000 --checkpoint-action=echo` | Show progress every 1000 files |
| `pv archive.tar.gz \| tar -xzf -` | Show progress bar with pv |

## Backup Examples

| Command | Description |
| --- | --- |
| `tar -czf backup-$(date +%Y%m%d).tar.gz /home/user/documents/` | Create timestamped backup |
| `tar -czf - /home/user/ \| ssh user@remote 'cat > backup.tar.gz'` | Create and transfer archive via SSH |
| `tar -czf backup.tar.gz --exclude='*.cache' --exclude='*.tmp' /home/user/` | Backup excluding cache files |

## Working with Remote Archives

| Command | Description |
| --- | --- |
| `tar -czf - files/ \| ssh user@server 'tar -xzf -'` | Archive and extract on remote server |
| `ssh user@server 'tar -czf - /remote/path/' \| tar -xzf -` | Download and extract from remote |

## Splitting Large Archives

| Command | Description |
| --- | --- |
| `tar -czf - largedir/ \| split -b 1G - archive.tar.gz.` | Split archive into 1GB chunks |
| `cat archive.tar.gz.* \| tar -xzf -` | Combine and extract split archive |

## Compression Comparison

| Command | Description |
| --- | --- |
| `tar -cf archive.tar files/` | No compression (fastest) |
| `tar -czf archive.tar.gz files/` | Gzip compression (good speed/size balance) |
| `tar -cjf archive.tar.bz2 files/` | Bzip2 compression (better compression, slower) |
| `tar -cJf archive.tar.xz files/` | XZ compression (best compression, slowest) |