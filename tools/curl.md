# Curl Cheat-Sheet

## Basic Requests

| Command | Description |
| --- | --- |
| `curl http://example.com` | Simple GET request |
| `curl -i http://example.com` | Include response headers |
| `curl -I http://example.com` | HEAD request (headers only) |
| `curl -v http://example.com` | Verbose output |
| `curl -s http://example.com` | Silent mode (no progress bar) |
| `curl -L http://example.com` | Follow redirects |

## HTTP Methods

| Command | Description |
| --- | --- |
| `curl -X GET http://example.com` | GET request (explicit) |
| `curl -X POST http://example.com` | POST request |
| `curl -X PUT http://example.com` | PUT request |
| `curl -X DELETE http://example.com` | DELETE request |
| `curl -X PATCH http://example.com` | PATCH request |

## Sending Data

| Command | Description |
| --- | --- |
| `curl -d "param1=value1&param2=value2" http://example.com` | POST form data |
| `curl -d @data.json -H "Content-Type: application/json" http://example.com` | POST JSON from file |
| `curl -d '{"key":"value"}' -H "Content-Type: application/json" http://example.com` | POST JSON data |
| `curl -F "file=@upload.txt" http://example.com/upload` | Upload file |
| `curl -F "name=John" -F "file=@photo.jpg" http://example.com` | Form with file upload |

## Headers

| Command | Description |
| --- | --- |
| `curl -H "Authorization: Bearer token123" http://example.com` | Add authorization header |
| `curl -H "Content-Type: application/json" http://example.com` | Set content type |
| `curl -H "User-Agent: MyApp/1.0" http://example.com` | Custom user agent |
| `curl -H "Accept: application/json" http://example.com` | Set accept header |
| `curl -A "Custom-Agent/1.0" http://example.com` | Set user agent (shorthand) |

## Authentication

| Command | Description |
| --- | --- |
| `curl -u username:password http://example.com` | Basic authentication |
| `curl -u username http://example.com` | Basic auth (prompt for password) |
| `curl --digest -u username:password http://example.com` | Digest authentication |
| `curl -H "Authorization: Bearer YOUR_TOKEN" http://example.com` | Bearer token |

## Cookies

| Command | Description |
| --- | --- |
| `curl -c cookies.txt http://example.com` | Save cookies to file |
| `curl -b cookies.txt http://example.com` | Load cookies from file |
| `curl -b "session=abc123" http://example.com` | Send specific cookie |
| `curl -c cookies.txt -b cookies.txt http://example.com` | Save and load cookies |

## Output Options

| Command | Description |
| --- | --- |
| `curl -o output.html http://example.com` | Save to file |
| `curl -O http://example.com/file.zip` | Save with remote filename |
| `curl http://example.com > output.html` | Redirect output |
| `curl -w "%{http_code}\n" http://example.com` | Show only HTTP status code |
| `curl -D headers.txt http://example.com` | Save response headers to file |

## Advanced Options

| Command | Description |
| --- | --- |
| `curl --max-time 30 http://example.com` | Set timeout (30 seconds) |
| `curl --connect-timeout 10 http://example.com` | Connection timeout |
| `curl --retry 3 http://example.com` | Retry on failure |
| `curl -k https://example.com` | Ignore SSL certificate errors |
| `curl --proxy proxy.example.com:8080 http://example.com` | Use proxy |

## Multiple URLs

| Command | Description |
| --- | --- |
| `curl http://example.com http://google.com` | Multiple URLs |
| `curl "http://example.com/file[1-10].txt"` | URL with range |
| `curl -Z http://example.com http://google.com` | Parallel requests |

## API Testing Examples

| Command | Description |
| --- | --- |
| `curl -X POST -H "Content-Type: application/json" -d '{"username":"john","password":"secret"}' http://api.example.com/login` | Login API call |
| `curl -H "Authorization: Bearer TOKEN" http://api.example.com/users` | Authenticated API call |
| `curl -X PUT -H "Content-Type: application/json" -d '{"name":"Updated"}' http://api.example.com/users/1` | Update resource |
| `curl -X DELETE -H "Authorization: Bearer TOKEN" http://api.example.com/users/1` | Delete resource |

## Download and Upload

| Command | Description |
| --- | --- |
| `curl -C - -O http://example.com/largefile.zip` | Resume interrupted download |
| `curl --limit-rate 200k -O http://example.com/file.zip` | Limit download speed |
| `curl -T file.txt ftp://ftp.example.com/` | Upload to FTP |
| `curl -u user:pass -T file.txt ftp://ftp.example.com/` | Upload to FTP with auth |

## Response Analysis

| Command | Description |
| --- | --- |
| `curl -w "Time: %{time_total}s\n" http://example.com` | Show request time |
| `curl -w "Status: %{http_code}\nTime: %{time_total}s\n" http://example.com` | Show status and time |
| `curl -s -o /dev/null -w "%{http_code}" http://example.com` | Silent status check |