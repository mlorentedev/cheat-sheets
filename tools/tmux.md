# Tmux Cheat-Sheet

## Session Management

| Command | Description |
| --- | --- |
| `tmux` | Start new session |
| `tmux new -s session-name` | Start new session with name |
| `tmux attach -t session-name` | Attach to existing session |
| `tmux ls` | List active sessions |
| `tmux detach` | Detach from session |
| `tmux kill-session -t session-name` | Kill session |
| `tmux rename-session -t old-name new-name` | Rename session |

## Key Bindings (Prefix: Ctrl+b)

### Session Commands
| Command | Description |
| --- | --- |
| `Ctrl+b d` | Detach from session |
| `Ctrl+b s` | List and switch sessions |
| `Ctrl+b $` | Rename current session |
| `Ctrl+b (` | Switch to previous session |
| `Ctrl+b )` | Switch to next session |

### Window Commands
| Command | Description |
| --- | --- |
| `Ctrl+b c` | Create new window |
| `Ctrl+b n` | Switch to next window |
| `Ctrl+b p` | Switch to previous window |
| `Ctrl+b l` | Switch to last used window |
| `Ctrl+b w` | List and switch windows |
| `Ctrl+b 0-9` | Switch to window by number |
| `Ctrl+b &` | Kill current window |
| `Ctrl+b ,` | Rename current window |
| `Ctrl+b f` | Find window by name |

### Pane Commands
| Command | Description |
| --- | --- |
| `Ctrl+b %` | Split window vertically |
| `Ctrl+b "` | Split window horizontally |
| `Ctrl+b arrow` | Switch to pane in direction |
| `Ctrl+b o` | Switch to next pane |
| `Ctrl+b ;` | Switch to last used pane |
| `Ctrl+b x` | Kill current pane |
| `Ctrl+b !` | Break pane into new window |
| `Ctrl+b {` | Move pane left |
| `Ctrl+b }` | Move pane right |

### Pane Resizing
| Command | Description |
| --- | --- |
| `Ctrl+b Ctrl+arrow` | Resize pane by 1 |
| `Ctrl+b Alt+arrow` | Resize pane by 5 |
| `Ctrl+b z` | Toggle pane zoom |
| `Ctrl+b Space` | Toggle pane layout |

## Copy Mode

| Command | Description |
| --- | --- |
| `Ctrl+b [` | Enter copy mode |
| `Space` | Start selection (in copy mode) |
| `Enter` | Copy selection and exit copy mode |
| `Ctrl+b ]` | Paste |
| `q` | Exit copy mode |
| `/` | Search forward (in copy mode) |
| `?` | Search backward (in copy mode) |
| `n` | Next search result |
| `N` | Previous search result |

## Miscellaneous

| Command | Description |
| --- | --- |
| `Ctrl+b ?` | Show all key bindings |
| `Ctrl+b t` | Show time |
| `Ctrl+b :` | Enter command mode |
| `Ctrl+b r` | Reload tmux config (if configured) |

## Configuration Commands

| Command | Description |
| --- | --- |
| `tmux source-file ~/.tmux.conf` | Reload configuration |
| `tmux show-options -g` | Show global options |
| `tmux show-options -s` | Show server options |
| `tmux list-keys` | List all key bindings |

## Advanced Commands

| Command | Description |
| --- | --- |
| `tmux new-window -c "#{pane_current_path}"` | New window in current directory |
| `tmux split-window -c "#{pane_current_path}"` | Split in current directory |
| `tmux send-keys -t session:window 'command' Enter` | Send command to specific pane |
| `tmux capture-pane -t session:window` | Capture pane content |
| `tmux pipe-pane -t session:window 'cat >> log.txt'` | Log pane output to file |

## Scripting and Automation

| Command | Description |
| --- | --- |
| `tmux has-session -t session-name` | Check if session exists |
| `tmux new-session -d -s session-name` | Create session in background |
| `tmux new-window -t session-name -n window-name` | Create named window in session |
| `tmux split-window -t session-name:window-name` | Split window in session |
| `tmux send-keys -t session-name:window-name 'ls' Enter` | Send command to window |

## Session Switching Shortcuts

| Command | Description |
| --- | --- |
| `tmux attach \|\| tmux new` | Attach to session or create new one |
| `tmux new -A -s main` | Attach to 'main' or create if doesn't exist |

## Status Bar Customization

| Command | Description |
| --- | --- |
| `set -g status-position top` | Move status bar to top |
| `set -g status-bg colour235` | Set status bar background color |
| `set -g status-fg white` | Set status bar foreground color |
| `set -g status-left-length 40` | Set left status length |

## Common Workflows

| Command | Description |
| --- | --- |
| `tmux new -s dev -c ~/project` | Start development session in project directory |
| `tmux split-window -h \; split-window -v` | Create three-pane layout |
| `tmux new -s logs -d 'tail -f /var/log/app.log'` | Background session for log monitoring |