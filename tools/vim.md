# Vim Cheat-Sheet

## Modes

| Command | Description |
| --- | --- |
| `i` | Enter insert mode at cursor |
| `I` | Enter insert mode at beginning of line |
| `a` | Enter insert mode after cursor |
| `A` | Enter insert mode at end of line |
| `o` | Open new line below and enter insert mode |
| `O` | Open new line above and enter insert mode |
| `Esc` | Return to normal mode |
| `v` | Enter visual mode |
| `V` | Enter visual line mode |
| `Ctrl+v` | Enter visual block mode |

## Basic Navigation

| Command | Description |
| --- | --- |
| `h` | Move left |
| `j` | Move down |
| `k` | Move up |
| `l` | Move right |
| `w` | Move to next word |
| `b` | Move to previous word |
| `e` | Move to end of word |
| `0` | Move to beginning of line |
| `$` | Move to end of line |
| `gg` | Go to first line |
| `G` | Go to last line |
| `Ctrl+u` | Page up |
| `Ctrl+d` | Page down |

## Editing

| Command | Description |
| --- | --- |
| `x` | Delete character under cursor |
| `X` | Delete character before cursor |
| `dd` | Delete entire line |
| `D` | Delete from cursor to end of line |
| `yy` | Copy (yank) entire line |
| `Y` | Copy from cursor to end of line |
| `p` | Paste after cursor |
| `P` | Paste before cursor |
| `u` | Undo |
| `Ctrl+r` | Redo |
| `r` | Replace single character |
| `R` | Enter replace mode |

## Cut, Copy, Paste

| Command | Description |
| --- | --- |
| `yiw` | Yank inner word |
| `yaw` | Yank a word (with spaces) |
| `yi"` | Yank inside quotes |
| `ya"` | Yank around quotes (including quotes) |
| `diw` | Delete inner word |
| `daw` | Delete a word |
| `ciw` | Change inner word |
| `caw` | Change a word |
| `"ay` | Yank to register 'a' |
| `"ap` | Paste from register 'a' |

## Search and Replace

| Command | Description |
| --- | --- |
| `/pattern` | Search forward for pattern |
| `?pattern` | Search backward for pattern |
| `n` | Next search result |
| `N` | Previous search result |
| `*` | Search forward for word under cursor |
| `#` | Search backward for word under cursor |
| `:s/old/new` | Replace first occurrence in current line |
| `:s/old/new/g` | Replace all occurrences in current line |
| `:%s/old/new/g` | Replace all occurrences in file |
| `:%s/old/new/gc` | Replace all with confirmation |

## File Operations

| Command | Description |
| --- | --- |
| `:w` | Save file |
| `:w filename` | Save as filename |
| `:q` | Quit (fails if unsaved changes) |
| `:q!` | Force quit without saving |
| `:wq` | Save and quit |
| `:x` | Save and quit (only if changes made) |
| `:e filename` | Edit another file |
| `:r filename` | Read file contents into current file |
| `:!command` | Execute shell command |

## Multiple Files and Windows

| Command | Description |
| --- | --- |
| `:split` | Split window horizontally |
| `:vsplit` | Split window vertically |
| `Ctrl+w h` | Move to left window |
| `Ctrl+w j` | Move to window below |
| `Ctrl+w k` | Move to window above |
| `Ctrl+w l` | Move to right window |
| `Ctrl+w w` | Cycle through windows |
| `Ctrl+w =` | Make all windows equal size |
| `Ctrl+w q` | Close current window |
| `:bn` | Next buffer |
| `:bp` | Previous buffer |
| `:bd` | Delete buffer |

## Advanced Navigation

| Command | Description |
| --- | --- |
| `f{char}` | Find next occurrence of char in line |
| `F{char}` | Find previous occurrence of char in line |
| `t{char}` | Move to before next occurrence of char |
| `T{char}` | Move to before previous occurrence of char |
| `;` | Repeat last f, F, t, or T command |
| `,` | Repeat last f, F, t, or T command in reverse |
| `%` | Jump to matching parenthesis/bracket |
| `{` | Move to previous paragraph |
| `}` | Move to next paragraph |

## Text Objects

| Command | Description |
| --- | --- |
| `iw` | Inner word |
| `aw` | A word (including spaces) |
| `is` | Inner sentence |
| `as` | A sentence |
| `ip` | Inner paragraph |
| `ap` | A paragraph |
| `i"` | Inside quotes |
| `a"` | Around quotes |
| `i(` | Inside parentheses |
| `a(` | Around parentheses |
| `i{` | Inside braces |
| `a{` | Around braces |

## Marks and Jumps

| Command | Description |
| --- | --- |
| `ma` | Set mark 'a' at current position |
| `'a` | Jump to mark 'a' |
| `''` | Jump back to previous position |
| `Ctrl+o` | Jump to older position |
| `Ctrl+i` | Jump to newer position |
| `:marks` | List all marks |

## Configuration

| Command | Description |
| --- | --- |
| `:set number` | Show line numbers |
| `:set nonumber` | Hide line numbers |
| `:set hlsearch` | Highlight search results |
| `:set nohlsearch` | Turn off search highlighting |
| `:set ignorecase` | Case insensitive search |
| `:set smartcase` | Smart case search |
| `:syntax on` | Enable syntax highlighting |