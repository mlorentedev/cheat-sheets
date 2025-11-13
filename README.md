# CheatSheets - Interactive Knowledge Base

Transform your scattered command knowledge into an organized, searchable personal reference system.

A personal second brain for DevOps commands, configurations, and scripts. Built for engineers who value their time and want to capture knowledge as they discover it - in under 10 seconds.

## Quick Start

```bash
# Clone the repository
git clone https://github.com/mlorentedev/cheat-sheets ~/.cheat-sheets
cd ~/.cheat-sheets

# Run the installer
./install.sh

# Start using immediately
cs docker           # Search docker commands
cs -i               # Interactive browser
cs quick "kubectl get pods -A"  # Capture a command instantly
```

## The Problem

You discover useful commands daily, but capturing them interrupts your flow. Traditional cheatsheets are static and hard to maintain.

## The Solution

A CLI tool (`cs`) that makes adding commands as natural as running them. If it takes more than 10 seconds to add a command, the system has failed.

### Features

- Quick Capture: `cs quick "command"` takes about 2 seconds
- Fast Search: Find commands in milliseconds
- Zero Friction: Add without breaking your flow
- Smart Organization: Auto-categorizes commands
- Interactive Browser: Explore with fzf integration
- Auto-Sync: Optional git sync on every change
- Track Learning: See your knowledge growth

## Daily Usage

### Finding Commands

```bash
# Simple search
cs docker                   # All docker commands
cs "list pods"              # Natural language search
cs kubectl -c tools         # Search specific category

# Interactive browser (requires fzf)
cs -i                       # Browse everything
cs browse                   # Same as -i

# List what's available
cs list                     # All categories and files
cs stats                    # Usage statistics
```

Example output:
```
docker system prune -a
  → Remove all unused containers, networks, images
  📁 tools/docker.md

docker run -d -p 8080:80 nginx
  → Run nginx in background on port 8080
  📁 tools/docker.md
```

### Adding Commands

#### Quick Add (2 seconds)

Just discovered a useful command? Save it instantly:

```bash
cs quick "terraform state rm aws_instance.example"
cs q "git reset --hard HEAD~1"  # Short alias
```

Commands are added to `personal/quick.md` with a timestamp. Perfect for capturing during active work.

#### Standard Add (5 seconds)

Add with context:

```bash
cs add "kubectl rollout restart deploy/app" \
  --desc "Restart deployment without downtime" \
  --tags k8s,restart,deploy

cs add "docker logs -f container 2>&1 | grep ERROR" \
  --desc "Follow container logs and filter errors" \
  --tags docker,debug \
  --cat tools/docker
```

#### Interactive Mode (10 seconds)

Guided process with prompts:

```bash
cs add -i
```

You'll be prompted for:
- Command (required)
- Description (optional)
- Tags (optional)
- Category (auto-detected, customizable)
- Example (optional)

Example session:
```
=== Interactive Command Add ===

Command: kubectl get pods -A
Description (optional): List all pods in all namespaces
Tags (comma-separated, optional): k8s,debug,pods
Category [tools/kubectl]:
Example (optional):

Preview:
Command: kubectl get pods -A
Description: List all pods in all namespaces
Tags: k8s,debug,pods
Category: tools/kubectl

Add this command? [Y/n]: y
✓ Added to: tools/kubectl.md
```

#### Capture from History

Just ran something useful? Capture it:

```bash
cs capture                  # Show last 10 commands, select to save
cs capture --last 5         # Last 5 commands
cs capture --auto           # Auto-capture frequently used commands
```

Shell alias for instant capture:
```bash
alias csc='cs capture --last 1'
```

Then after running any command:
```bash
terraform plan -var-file=prod.tfvars
csc  # Captures the terraform command
```

### Creating New Sections

Need to organize a new tool or category?

```bash
# Create new tool section
cs new consul                    # Creates tools/consul.md
cs new tool ansible              # Same as above

# Create in specific category
cs new infra/mongodb             # Creates infra/mongodb.md
cs new monitoring/prometheus     # Creates monitoring/prometheus.md

# Use templates
cs new istio --template k8s      # Use Kubernetes template
cs new myapp --template simple   # Minimal template
```

Available templates:
- `tool` (default): Full tool documentation structure
- `k8s`: Kubernetes resource template
- `simple`: Minimal template

### Maintenance

```bash
# Update search index (auto-runs after adds)
cs index

# Sync with git
cs sync                     # Pull, commit, push

# View statistics
cs stats

# List all categories and files
cs list
```

## Real-World Workflows

### Learning a New Tool

You're exploring Consul for the first time:

```bash
# 1. Check if you have any consul commands
cs consul
# No results (new tool)

# 2. Try some commands while learning
consul members
consul catalog services

# 3. Find something useful
consul kv put config/app/db "postgresql://..."

# 4. Capture it immediately
cs quick "consul kv put config/app/db <value>"

# 5. Later, organize properly
cs add "consul kv get config/app/db" \
  --desc "Retrieve key-value from Consul" \
  --tags consul,kv,config \
  --cat tools/consul

# 6. Create dedicated section
cs new consul
```

After a few days, you have a personal Consul reference built from real usage.

### Debugging Production

Production is down at 2 AM:

```bash
# 1. Quick search
cs "pod logs"

# Shows: kubectl logs -f deployment/app

# 2. Run it
kubectl logs -f deployment/myapp -n production

# 3. Find the issue, fix it

# 4. Found a new useful variation during debugging
kubectl logs deployment/myapp -n production --previous

# 5. Save it for next time
cs quick "kubectl logs deployment/app --previous"
```

Next incident, you have the command ready.

### Daily Knowledge Accumulation

Throughout your day:

```bash
# Morning - found useful docker command
cs quick "docker run --rm -v $(pwd):/app -w /app node:alpine npm test"

# Noon - learned terraform trick
cs add "terraform fmt -recursive" --desc "Format all .tf files" --tags terraform

# Afternoon - kubernetes discovery
cs quick "kubectl top nodes"
cs quick "kubectl describe node"

# End of day - review what you captured
cs stats
# Personal: 8 new commands today

# Auto-organize frequent commands
cs capture --auto
```

Result: After one month, you have 100+ real commands you actually use, organized automatically.

## Configuration

### Configuration File

Create `.cs.config` in the repository root:

```bash
# .cs.config - CheatSheets configuration

# Editor for opening files
EDITOR=nvim              # or code, nano, vim

# Auto-sync with git after changes
AUTO_SYNC=true          # true or false

# Default category for new commands
DEFAULT_CATEGORY=personal

# Where quick adds go
QUICK_ADD_FILE="${PERSONAL_DIR}/quick.md"

# Threshold for auto-capture
CAPTURE_THRESHOLD=3     # Capture commands used 3+ times
```

### Shell Aliases (Recommended)

Add to your `~/.bashrc` or `~/.zshrc`:

```bash
# CheatSheets aliases for maximum speed
alias csa='cs add'
alias csq='cs quick'
alias csc='cs capture --last 1'
alias csi='cs -i'
alias css='cs search'
alias csn='cs new'

# Integration with your workflow
alias csl='cs list'
alias csync='cs sync'
```

With aliases, adding a command becomes:
```bash
csq "docker system df"  # 2 seconds total
```

### Environment Variables

```bash
# Set custom root directory
export CS_ROOT="$HOME/my-cheatsheets"

# Enable auto-sync
export CS_AUTO_SYNC=true

# Set capture threshold
export CS_CAPTURE_THRESHOLD=5
```

## Repository Structure

```
cheat-sheets/
├── README.md              # This file
├── LICENSE                # MIT License
├── install.sh             # One-line installer
├── cs                     # Main CLI tool (executable)
├── .cs.config             # Your configuration (optional)
├── .gitignore             # Ignores personal/ directory
│
├── linux/                 # Linux system commands
│   ├── grep.md
│   ├── find.md
│   ├── sed.md
│   └── ...
│
├── tools/                 # DevOps tools
│   ├── docker.md
│   ├── kubectl.md
│   ├── terraform.md
│   ├── git.md
│   ├── helm.md
│   └── ...
│
├── infra/                 # Infrastructure & databases
│   ├── mysql.md
│   ├── postgresql.md
│   ├── zfs.md
│   └── ...
│
├── misc/                  # Protocols and references
│   ├── http-status-codes.md
│   ├── dns.md
│   └── ...
│
├── windows/               # Windows tools
│   ├── powershell.md
│   └── ...
│
├── personal/              # Your additions (gitignored)
│   ├── quick.md          # Quick captures
│   ├── commands.md       # Organized personal commands
│   └── work.md           # Work-specific commands
│
└── templates/             # Templates for new sections
    ├── tool.md
    ├── k8s.md
    └── simple.md
```

### What Gets Committed

**Tracked** (committed to git):
- All category directories (linux/, tools/, infra/, etc.)
- Shared/public cheatsheets
- README, LICENSE, install.sh, cs tool

**Ignored** (not committed):
- `personal/` directory - your private commands
- `.cs.config` - your personal configuration
- `.cs_index` - search index (rebuilt automatically)
- `.cs_cache/` - temporary cache

This lets you:
- Pull updates from upstream
- Keep your private commands separate
- Share your additions via export

## Tips

### Instant Capture Workflow

```bash
# Run command
docker run -d -p 8080:80 --name web nginx

# Works! Save it immediately
csq "docker run -d -p 8080:80 --name web nginx"
```

Time: about 3 seconds including typing.

### Build a Daily Habit

End of each day:

```bash
cs stats                    # Review what you added
cs capture --auto           # Capture frequently used
cs sync                     # Backup to git
```

### Category Auto-Detection

The tool auto-detects categories from command prefixes:

- `docker ...` → `tools/docker`
- `kubectl ...` → `tools/kubectl`
- `git ...` → `tools/git`
- `terraform ...` → `tools/terraform`
- `helm ...` → `tools/helm`
- Everything else → `personal`

Override with `--cat`:
```bash
cs add "aws s3 ls" --cat tools/aws
```

### Search Shortcuts

```bash
# Search is smart - just type keywords
cs docker prune             # Finds docker system prune commands
cs k8s logs                 # Finds kubectl logs
cs "restart deployment"     # Natural language

# Search specific category
cs nginx -c tools
```

### Quick Reference Export

```bash
# Export recent additions for sharing
cs capture --auto > team-snippets.md

# Or manually export category
cat tools/docker.md | grep '|' > docker-ref.txt
```

### Shell Integration

Add to your shell prompt:
```bash
# Show CS stats in prompt
PROMPT_COMMAND='__cs_count=$(find ~/.cheat-sheets/personal -name "*.md" -exec cat {} \; | grep -c "```bash" 2>/dev/null || echo 0)'
PS1='[${__cs_count} cmds] $ '
```

## Track Your Learning

```bash
# View statistics
cs stats

# Example output:
# CheatSheets Statistics
#
# Repository:
#   Files: 42
#   Categories: 5
#   Total commands: 487
#
# Personal:
#   Commands: 67
#
# Recent Activity:
#   Quick captures today: 5
```

## Advanced Usage

### Import from Other Sources

```bash
# Import from another cheatsheet format
cat external-cheatsheet.md | while read line; do
  cs quick "$line"
done

# Bulk import from history
history | tail -100 | cut -d' ' -f4- | while read cmd; do
  echo "Import: $cmd [Y/n]?"
  read confirm
  [[ "$confirm" == "Y" ]] && cs quick "$cmd"
done
```

### Integration with Other Tools

```bash
# With tldr
tldr docker | grep -E '^\s*-' | while read line; do
  cmd=$(echo "$line" | sed 's/^- //')
  cs quick "$cmd"
done

# With your notes
grep '```bash' notes.md | sed 's/```bash//' | while read cmd; do
  cs quick "$cmd"
done
```

### Backup and Restore

```bash
# Backup personal commands
tar -czf cs-backup-$(date +%Y%m%d).tar.gz personal/

# Restore
tar -xzf cs-backup-20250101.tar.gz
```

### Multi-Machine Sync

```bash
# Machine 1: Enable auto-sync
echo "AUTO_SYNC=true" >> .cs.config

# Every change syncs to git automatically
cs quick "new command"
# Added to quick captures
# Syncing with git...
# Changes committed

# Machine 2: Pull updates
cd ~/.cheat-sheets
git pull
cs index  # Rebuild index with new content
```

## Contributing

Found a useful command? Share it.

### Adding to Public Categories

```bash
# Add to a public category (will be committed)
cs add "terraform state list | grep aws_instance" \
  --desc "List all AWS instances in state" \
  --tags terraform,aws \
  --cat tools/terraform

# Sync to create commit
cs sync
git push
```

### Sharing Your Discoveries

```bash
# Export your personal additions
cd personal/
cat quick.md commands.md > ../my-contributions.md

# Create a PR with the additions
```

## Command Format in Files

When manually editing `.md` files, use this format:

### Table Format (Recommended)

```markdown
## Category Name

| COMMAND | DESCRIPTION |
| --- | --- |
| `command --flag arg` | What it does [Tags: tag1,tag2] |
| `another command` | Description here |
```

### Code Block Format

```markdown
## Command Name

```bash
# Description of what this does
command --flag argument

# Example with output
command --verbose
# Expected output: ...
```
```

Both formats are indexed and searchable.

## Command Reference

```bash
cs [COMMAND] [OPTIONS]

QUICK COMMANDS:
  cs quick "command"              Add command instantly
  cs "search term"                Search for commands
  cs add "cmd" --desc "..."       Add with description
  cs add -i                       Interactive add mode
  cs capture                      Capture from history
  cs new <name>                   Create new section

SEARCH & BROWSE:
  cs search <query>               Search all commands
  cs browse                       Interactive browser
  cs -i                           Interactive browser (same)
  cs list                         List all categories

CONTENT MANAGEMENT:
  cs add "command" [OPTIONS]      Add command with metadata
    --desc "description"          Add description
    --tags "tag1,tag2"            Add tags
    --cat "category"              Specify category
    --example "example"           Add example

  cs new section <name>           Create new section
    --template <type>             Use template (tool, k8s, simple)

  cs capture [OPTIONS]            Capture from history
    --last N                      Show last N commands
    --auto                        Auto-capture frequent commands

MAINTENANCE:
  cs index                        Rebuild search index
  cs sync                         Git sync (pull, commit, push)
  cs stats                        Show statistics
  cs list                         List all categories
  cs help                         Show this help
  cs version                      Show version
```

## Troubleshooting

### Search not finding commands

```bash
# Rebuild the index
cs index
```

### Want to use fzf for better browsing

```bash
# Install fzf (Ubuntu/Debian)
sudo apt install fzf

# Install fzf (macOS)
brew install fzf

# Install fzf (manual)
git clone --depth 1 https://github.com/junegunn/fzf.git ~/.fzf
~/.fzf/install
```

### Commands not syncing to git

```bash
# Check git status
git status

# Manual sync
git add .
git commit -m "Update cheatsheets"
git push

# Or use cs sync
cs sync
```

### Lost personal commands

```bash
# Check if they exist
ls -la personal/

# Restore from git (if previously synced)
git checkout personal/

# Restore from backup
tar -xzf cs-backup-*.tar.gz
```

## Getting Started

1. Install the tool: `./install.sh`
2. Try quick add: `cs quick "ls -la"`
3. Search: `cs "list files"`
4. Browse: `cs -i`

### Example Daily Workflow

```bash
# Morning: Review what's new
cs stats

# During work: Capture as you discover
cs quick "useful command 1"
cs quick "useful command 2"

# When you have 2 minutes: Organize
cs add -i  # Go through quick captures

# End of day: Sync
cs sync
```

### Building Your Library

Week 1: Quick capture everything
- Focus on `cs quick` only
- Capture 5-10 commands/day
- Don't worry about organization

Week 2: Start organizing
- Use `cs add -i` for important commands
- Add descriptions and tags
- Create 2-3 new sections

Week 3: Optimize your workflow
- Set up aliases
- Enable auto-sync
- Use capture from history

Month 2+: Your knowledge base grows naturally
- 100+ commands captured
- Organized by real usage patterns
- Your go-to reference

## License

MIT License - see [LICENSE](LICENSE) file for details.

## Related Projects

- [Dotfiles](https://github.com/mlorentedev/dotfiles) - My personal dotfiles for Linux
- [Boilerplates](https://github.com/mlorentedev/boilerplates) - Docker, Kubernetes, and more

## Acknowledgments

Built for DevOps engineers who value their time. Inspired by the need to capture knowledge without breaking flow.

If you find this useful, star the repo.
Found a bug? [Open an issue](https://github.com/mlorentedev/cheat-sheets/issues)
Have an idea? [Start a discussion](https://github.com/mlorentedev/cheat-sheets/discussions)
