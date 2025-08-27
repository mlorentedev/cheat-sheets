# Git Cheat-Sheet

## Repository Management

| Command | Description |
| --- | --- |
| `git init` | Initialize a new Git repository |
| `git clone <url>` | Clone a remote repository |
| `git status` | Show the working tree status |
| `git add <file>` | Add a file to the staging area |
| `git commit -m <message>` | Commit changes to the repository |
| `git push` | Push changes to the remote repository |
| `git pull` | Pull changes from the remote repository |
| `git fetch` | Fetch changes from the remote repository |
| `git merge <branch>` | Merge a branch into the current branch |
| `git branch` | List all branches |
| `git branch <branch>` | Create a new branch |
| `git checkout <branch>` | Switch to a branch |
| `git checkout -b <branch>` | Create and switch to a new branch |
| `git branch -d <branch>` | Delete a branch |
| `git log` | Show commit logs |
| `git diff` | Show changes between commits |
| `git blame <file>` | Show who changed each line in a file |
| `git reflog` | Show a log of changes to HEAD |
| `git reset --hard <commit>` | Reset the repository to a commit |
| `git revert <commit>` | Revert a commit |
| `git stash` | Stash changes in the working directory |
| `git stash pop` | Apply stashed changes to the working directory |
| `git tag <tag>` | Create a tag for a commit |

## Configuration

| Command | Description |
| --- | --- |
| `git config --global user.name <user>` | Set the user name for Git |
| `git config --global user.email <email>` | Set the user email for Git |
| `git config --global core.editor <editor>` | Set the default text editor for Git |
| `git config --global color.ui auto` | Enable colored output for Git |

## Remote Repositories

| Command | Description |
| --- | --- |
| `git remote add <repository> <url>` | Add a remote repository |
| `git remote -v` | List remote repositories |
| `git remote show <repository>` | Show information about a remote repository |
| `git remote rename <repository> <new_repository>` | Rename a remote repository |
| `git remote remove <repository>` | Remove a remote repository |

## Advanced Branching

| Command | Description |
| --- | --- |
| `git switch <branch>` | Switch to branch (newer alternative to checkout) |
| `git switch -c <branch>` | Create and switch to new branch |
| `git branch -m <old> <new>` | Rename branch |
| `git branch -r` | List remote branches |
| `git branch -a` | List all branches (local and remote) |
| `git branch --merged` | List branches merged into current branch |
| `git branch --no-merged` | List branches not merged into current branch |
| `git push origin --delete <branch>` | Delete remote branch |
| `git cherry-pick <commit>` | Apply commit to current branch |

## Stashing

| Command | Description |
| --- | --- |
| `git stash save "message"` | Stash with custom message |
| `git stash list` | List all stashes |
| `git stash show` | Show stash changes |
| `git stash apply` | Apply stash without removing it |
| `git stash drop` | Delete most recent stash |
| `git stash clear` | Delete all stashes |
| `git stash pop stash@{2}` | Apply specific stash |
| `git stash -u` | Include untracked files in stash |

## History and Logs

| Command | Description |
| --- | --- |
| `git log --oneline` | Show commits in one line each |
| `git log --graph` | Show commit graph |
| `git log --author="name"` | Show commits by specific author |
| `git log --since="2 weeks ago"` | Show commits from specific time |
| `git log --grep="pattern"` | Search commit messages |
| `git log -p <file>` | Show file history with changes |
| `git log --follow <file>` | Follow file history through renames |
| `git shortlog -s -n` | Show commit count by author |

## Undoing Changes

| Command | Description |
| --- | --- |
| `git reset HEAD <file>` | Unstage file |
| `git reset --soft HEAD~1` | Undo last commit, keep changes staged |
| `git reset --mixed HEAD~1` | Undo last commit, unstage changes |
| `git reset --hard HEAD~1` | Undo last commit, discard changes |
| `git checkout -- <file>` | Discard changes to file |
| `git restore <file>` | Restore file (newer alternative) |
| `git restore --staged <file>` | Unstage file |
| `git clean -fd` | Remove untracked files and directories |

## Rebasing and Interactive Rebase

| Command | Description |
| --- | --- |
| `git rebase <branch>` | Rebase current branch onto another |
| `git rebase -i HEAD~3` | Interactive rebase last 3 commits |
| `git rebase --continue` | Continue rebase after resolving conflicts |
| `git rebase --abort` | Cancel rebase |
| `git pull --rebase` | Pull with rebase instead of merge |

## Tagging

| Command | Description |
| --- | --- |
| `git tag -a v1.0 -m "Version 1.0"` | Create annotated tag |
| `git tag -l "v1.*"` | List tags matching pattern |
| `git push origin <tag>` | Push specific tag |
| `git push origin --tags` | Push all tags |
| `git tag -d <tag>` | Delete local tag |
| `git push origin --delete <tag>` | Delete remote tag |

## Searching and Finding

| Command | Description |
| --- | --- |
| `git grep "pattern"` | Search for text in repository |
| `git grep -n "pattern"` | Search with line numbers |
| `git bisect start` | Start binary search for bug |
| `git bisect good <commit>` | Mark commit as good |
| `git bisect bad <commit>` | Mark commit as bad |
| `git show <commit>` | Show commit details |

## Workflow Examples

| Command | Description |
| --- | --- |
| `git commit -am "message"` | Stage and commit all changes |
| `git push -u origin <branch>` | Push and set upstream branch |
| `git merge --no-ff <branch>` | Merge without fast-forward |
| `git rebase --onto main dev~3 dev` | Advanced rebase |
| `git cherry-pick <commit1>..<commit2>` | Cherry-pick range of commits |
