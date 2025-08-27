# JQ Cheat-Sheet

## Basic Usage

| Command | Description |
| --- | --- |
| `jq '.' file.json` | Pretty print JSON |
| `cat file.json \| jq` | Pretty print JSON from stdin |
| `jq -c '.' file.json` | Compact JSON output |
| `jq -r '.' file.json` | Raw output (no quotes around strings) |
| `jq -n 'empty'` | Produce no output |
| `jq --tab '.' file.json` | Use tabs for indentation |

## Basic Filters

| Command | Description |
| --- | --- |
| `jq '.key' file.json` | Get value of key |
| `jq '.key.subkey' file.json` | Get nested value |
| `jq '.["key with spaces"]' file.json` | Get value with special characters in key |
| `jq '.key?' file.json` | Get value, null if key doesn't exist |
| `jq '.key1, .key2' file.json` | Get multiple values |

## Array Operations

| Command | Description |
| --- | --- |
| `jq '.array[]' file.json` | Get all array elements |
| `jq '.array[0]' file.json` | Get first array element |
| `jq '.array[-1]' file.json` | Get last array element |
| `jq '.array[1:3]' file.json` | Get array slice (elements 1 and 2) |
| `jq '.array \| length' file.json` | Get array length |
| `jq '.array \| reverse' file.json` | Reverse array |
| `jq '.array \| sort' file.json` | Sort array |
| `jq '.array \| unique' file.json` | Get unique array elements |

## Object Operations

| Command | Description |
| --- | --- |
| `jq 'keys' file.json` | Get all object keys |
| `jq 'keys_unsorted' file.json` | Get keys in original order |
| `jq 'values' file.json` | Get all object values |
| `jq 'to_entries' file.json` | Convert object to key-value pairs |
| `jq 'from_entries' file.json` | Convert key-value pairs to object |
| `jq 'has("key")' file.json` | Check if object has key |

## Filtering and Selection

| Command | Description |
| --- | --- |
| `jq '.[] \| select(.age > 18)' file.json` | Filter array elements by condition |
| `jq '.users[] \| select(.name == "John")' file.json` | Filter by exact match |
| `jq '.[] \| select(.name \| test("^J"))' file.json` | Filter by regex (names starting with J) |
| `jq '.[] \| select(.tags \| contains(["red"]))' file.json` | Filter by array containing value |
| `jq 'map(select(.active))' file.json` | Keep only elements where active is true |

## Transformation

| Command | Description |
| --- | --- |
| `jq 'map(.name)' file.json` | Extract name from each object |
| `jq 'map({name: .name, age: .age})' file.json` | Create new objects with selected fields |
| `jq '.[] \| {name, age}' file.json` | Shorthand for creating objects |
| `jq 'map(. + {status: "active"})' file.json` | Add field to each object |
| `jq 'map(del(.password))' file.json` | Remove field from each object |

## Grouping and Aggregation

| Command | Description |
| --- | --- |
| `jq 'group_by(.category)' file.json` | Group array by field |
| `jq 'map(.price) \| add' file.json` | Sum all prices |
| `jq 'map(.price) \| min' file.json` | Find minimum price |
| `jq 'map(.price) \| max' file.json` | Find maximum price |
| `jq 'map(.age) \| add / length' file.json` | Calculate average age |
| `jq '[.[] \| .count] \| add' file.json` | Sum count field from array |

## String Operations

| Command | Description |
| --- | --- |
| `jq '.name \| ascii_downcase' file.json` | Convert to lowercase |
| `jq '.name \| ascii_upcase' file.json` | Convert to uppercase |
| `jq '.text \| split(" ")' file.json` | Split string into array |
| `jq '.words \| join(" ")' file.json` | Join array into string |
| `jq '.text \| length' file.json` | Get string length |
| `jq '.email \| contains("@")' file.json` | Check if string contains substring |
| `jq '.text \| startswith("Hello")' file.json` | Check if string starts with |
| `jq '.text \| endswith("!")' file.json` | Check if string ends with |

## Date and Number Operations

| Command | Description |
| --- | --- |
| `jq '.timestamp \| todate' file.json` | Convert Unix timestamp to ISO date |
| `jq '.date \| fromdate' file.json` | Convert ISO date to Unix timestamp |
| `jq '.number \| floor' file.json` | Round down number |
| `jq '.number \| ceil' file.json` | Round up number |
| `jq '.number \| round' file.json` | Round number |
| `jq '.price \| tonumber' file.json` | Convert string to number |
| `jq '.count \| tostring' file.json` | Convert number to string |

## Conditional Operations

| Command | Description |
| --- | --- |
| `jq 'if .age >= 18 then "adult" else "minor" end' file.json` | Simple if-then-else |
| `jq '.users \| map(if .active then .name else empty end)' file.json` | Conditional mapping |
| `jq '.status // "unknown"' file.json` | Default value if null/false |
| `jq '.data.value? // 0' file.json` | Default value for missing keys |

## Advanced Queries

| Command | Description |
| --- | --- |
| `jq '.users[] \| select(.roles[] == "admin")' file.json` | Find users with admin role |
| `jq '[.products[] \| select(.price < 100) \| .name]' file.json` | Names of products under $100 |
| `jq 'reduce .items[] as $item (0; . + $item.quantity)' file.json` | Sum quantities using reduce |
| `jq '.users \| map(select(.orders \| length > 0)) \| length' file.json` | Count users with orders |

## Working with Multiple Files

| Command | Description |
| --- | --- |
| `jq -s '.[0] + .[1]' file1.json file2.json` | Merge two JSON objects |
| `jq -s 'add' file1.json file2.json` | Combine arrays from multiple files |
| `jq -s 'flatten' file1.json file2.json` | Flatten arrays from multiple files |

## Output Formatting

| Command | Description |
| --- | --- |
| `jq -r '.users[] \| "\(.name): \(.email)"' file.json` | Format output as strings |
| `jq -r '.[] \| [.name, .age] \| @csv' file.json` | Output as CSV |
| `jq -r '.[] \| [.name, .age] \| @tsv' file.json` | Output as TSV |
| `jq -r '@base64' file.json` | Encode as base64 |
| `jq -r '@uri' file.json` | URL encode |

## Real-world Examples

| Command | Description |
| --- | --- |
| `curl -s "https://api.github.com/users/octocat" \| jq '.name'` | Get name from GitHub API |
| `kubectl get pods -o json \| jq '.items[] \| select(.status.phase=="Running") \| .metadata.name'` | Get running pod names |
| `aws ec2 describe-instances \| jq '.Reservations[].Instances[] \| select(.State.Name=="running") \| .InstanceId'` | Get running EC2 instances |