# CheatSheets

[![CI Tests](https://github.com/mlorentedev/cheat-sheets/workflows/CI%20Tests/badge.svg)](https://github.com/mlorentedev/cheat-sheets/actions)

Personal command reference for Linux, Docker, Kubernetes, Terraform, and more.

## Quick Start

```bash
# Clone
git clone https://github.com/mlorentedev/cheat-sheets ~/.cheat-sheets
cd ~/.cheat-sheets

# Add to PATH
echo 'export PATH="$PATH:~/.cheat-sheets"' >> ~/.bashrc
source ~/.bashrc
```

## Usage

```bash
# Save a command
cs quick "docker ps -a"
cs quick "kubectl get pods -A"

# Search for something
cs search docker
cs search kubernetes

# View a cheatsheet
cs docker
cs kubectl
cs git

# List everything
cs list
```

That's it! Just 4 commands.

## What's Inside

- **linux/** - grep, sed, awk, find, tar, cron, etc.
- **tools/** - docker, kubectl, terraform, helm, git, etc.
- **infra/** - mysql, postgresql, sqlite, zfs
- **misc/** - http codes, dns, smtp codes
- **windows/** - powershell, chocolatey
- **personal/** - your own commands (gitignored)

## Examples

### Save commands as you discover them

```bash
# Just ran a useful command? Save it:
docker run -d -p 8080:80 nginx
cs quick "docker run -d -p 8080:80 nginx"
```

### Find commands you forgot

```bash
# What was that kubernetes command?
cs search pods

# Show me all docker commands
cs docker
```

### View cheatsheets

```bash
# Open docker cheatsheet
cs docker

# List all available cheatsheets
cs list
```

## Your Quick Commands

Your saved commands go to `personal/quick.md` - edit it anytime:

```bash
vim personal/quick.md
# or
code personal/quick.md
```

## Manual Additions

Just add to the markdown files:

```bash
vim tools/myapp.md
```

Format:
```markdown
# My App

## Commands

| COMMAND | DESCRIPTION |
| --- | --- |
| `myapp start` | Start the app |
| `myapp stop` | Stop the app |
```

## License

MIT - See [LICENSE](LICENSE)

## More

- [Dotfiles](https://github.com/mlorentedev/dotfiles)
- [Boilerplates](https://github.com/mlorentedev/boilerplates)
