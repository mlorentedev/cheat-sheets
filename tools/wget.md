# Wget Cheat-Sheet

## Basic Downloads

| Command | Description |
| --- | --- |
| `wget http://example.com/file.zip` | Download single file |
| `wget -O newname.zip http://example.com/file.zip` | Download and rename file |
| `wget -P /download/path/ http://example.com/file.zip` | Download to specific directory |
| `wget -c http://example.com/file.zip` | Resume interrupted download |
| `wget -q http://example.com/file.zip` | Quiet mode (no output) |
| `wget -v http://example.com/file.zip` | Verbose mode |

## Multiple Files

| Command | Description |
| --- | --- |
| `wget -i urls.txt` | Download URLs from file |
| `wget http://example.com/file1.zip http://example.com/file2.zip` | Download multiple URLs |
| `wget -r http://example.com/` | Recursive download (entire site) |
| `wget -r -l 2 http://example.com/` | Recursive with depth limit (2 levels) |

## Website Mirroring

| Command | Description |
| --- | --- |
| `wget -m http://example.com/` | Mirror website |
| `wget -m -k http://example.com/` | Mirror with converted links |
| `wget -m -p http://example.com/` | Mirror with all page assets |
| `wget -r -np http://example.com/folder/` | Recursive, no parent directories |
| `wget -r -A jpg,png http://example.com/` | Download only specific file types |

## Authentication

| Command | Description |
| --- | --- |
| `wget --user=username --password=password http://example.com/` | HTTP authentication |
| `wget --ask-password --user=username http://example.com/` | Prompt for password |
| `wget --header="Authorization: Bearer TOKEN" http://example.com/` | Custom header auth |
| `wget --certificate=cert.pem --private-key=key.pem https://example.com/` | Certificate authentication |

## Rate Limiting and Delays

| Command | Description |
| --- | --- |
| `wget --wait=2 -r http://example.com/` | Wait 2 seconds between requests |
| `wget --random-wait -r http://example.com/` | Random wait (0.5 to 1.5 * wait time) |
| `wget --limit-rate=200k http://example.com/file.zip` | Limit download speed |
| `wget -w 1 --random-wait -r http://example.com/` | Polite crawling |

## Retry and Timeout

| Command | Description |
| --- | --- |
| `wget -t 3 http://example.com/file.zip` | Retry 3 times on failure |
| `wget -T 30 http://example.com/file.zip` | Set timeout to 30 seconds |
| `wget --retry-connrefused http://example.com/file.zip` | Retry on connection refused |
| `wget -t inf http://example.com/file.zip` | Retry indefinitely |

## SSL/HTTPS Options

| Command | Description |
| --- | --- |
| `wget --no-check-certificate https://example.com/` | Ignore SSL certificate errors |
| `wget --ca-certificate=ca-cert.pem https://example.com/` | Use custom CA certificate |
| `wget --certificate=client.pem --private-key=key.pem https://example.com/` | Client certificate |

## Filtering and Exclusions

| Command | Description |
| --- | --- |
| `wget -r -A "*.pdf" http://example.com/` | Download only PDF files |
| `wget -r -R "*.gif,*.jpg" http://example.com/` | Exclude image files |
| `wget -r --exclude-directories=temp,cache http://example.com/` | Exclude directories |
| `wget -r --include-directories=docs,files http://example.com/` | Include only specific directories |

## Advanced Options

| Command | Description |
| --- | --- |
| `wget --spider http://example.com/file.zip` | Check if file exists (don't download) |
| `wget -S http://example.com/` | Show server response headers |
| `wget --server-response http://example.com/` | Show detailed server response |
| `wget --background http://example.com/largefile.zip` | Run download in background |
| `wget -o download.log http://example.com/file.zip` | Log output to file |

## User Agent and Headers

| Command | Description |
| --- | --- |
| `wget --user-agent="Custom Agent 1.0" http://example.com/` | Set custom user agent |
| `wget --header="Accept: application/json" http://example.com/` | Add custom header |
| `wget --referer="http://google.com" http://example.com/` | Set referer header |

## FTP Downloads

| Command | Description |
| --- | --- |
| `wget ftp://ftp.example.com/file.zip` | Download from FTP |
| `wget --ftp-user=username --ftp-password=password ftp://ftp.example.com/file.zip` | FTP with authentication |
| `wget -r ftp://ftp.example.com/directory/` | Recursive FTP download |

## Practical Examples

| Command | Description |
| --- | --- |
| `wget -r -np -k -p -E -H -D example.com http://example.com/` | Complete website backup |
| `wget -A pdf,doc,docx -r -l 2 http://example.com/` | Download documents recursively |
| `wget --mirror --convert-links --backup-converted --page-requisites http://example.com/` | Perfect mirror with link conversion |
| `wget -q -O - http://example.com/ \| grep "pattern"` | Download and pipe to grep |