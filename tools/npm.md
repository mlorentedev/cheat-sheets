# NPM Cheat-Sheet

## Package Management

| Command | Description |
| --- | --- |
| `npm install` | Install all dependencies from package.json |
| `npm install <package>` | Install package locally |
| `npm install -g <package>` | Install package globally |
| `npm install --save <package>` | Install and add to dependencies |
| `npm install --save-dev <package>` | Install and add to devDependencies |
| `npm install <package>@<version>` | Install specific version |
| `npm install <package>@latest` | Install latest version |
| `npm uninstall <package>` | Remove package |
| `npm uninstall -g <package>` | Remove global package |

## Package Information

| Command | Description |
| --- | --- |
| `npm list` | List installed packages (local) |
| `npm list -g` | List installed packages (global) |
| `npm list --depth=0` | List top-level packages only |
| `npm outdated` | Show outdated packages |
| `npm view <package>` | Show package information |
| `npm info <package>` | Show detailed package information |
| `npm search <keyword>` | Search for packages |
| `npm whoami` | Show currently logged-in user |

## Updates and Versions

| Command | Description |
| --- | --- |
| `npm update` | Update all packages |
| `npm update <package>` | Update specific package |
| `npm update -g` | Update all global packages |
| `npm outdated` | Check for outdated packages |
| `npm audit` | Run security audit |
| `npm audit fix` | Automatically fix security issues |
| `npm version patch` | Bump patch version (1.0.0 → 1.0.1) |
| `npm version minor` | Bump minor version (1.0.0 → 1.1.0) |
| `npm version major` | Bump major version (1.0.0 → 2.0.0) |

## Scripts and Development

| Command | Description |
| --- | --- |
| `npm run <script>` | Run script defined in package.json |
| `npm start` | Run start script |
| `npm test` | Run test script |
| `npm run build` | Run build script |
| `npm run dev` | Run dev script (if defined) |
| `npm run lint` | Run lint script (if defined) |
| `npm run --silent <script>` | Run script without npm output |

## Project Initialization

| Command | Description |
| --- | --- |
| `npm init` | Create new package.json interactively |
| `npm init -y` | Create package.json with defaults |
| `npm init @create-react-app my-app` | Use create-react-app initializer |
| `npm init vue@latest my-project` | Use Vue initializer |

## Publishing

| Command | Description |
| --- | --- |
| `npm login` | Login to npm registry |
| `npm logout` | Logout from npm registry |
| `npm publish` | Publish package to registry |
| `npm publish --access public` | Publish scoped package as public |
| `npm unpublish <package>@<version>` | Remove specific version |
| `npm deprecate <package>@<version> "<message>"` | Deprecate a version |

## Configuration

| Command | Description |
| --- | --- |
| `npm config list` | Show all configuration |
| `npm config get <key>` | Get configuration value |
| `npm config set <key> <value>` | Set configuration value |
| `npm config delete <key>` | Delete configuration |
| `npm config edit` | Edit configuration in editor |
| `npm config set registry <url>` | Set custom registry |

## Cache Management

| Command | Description |
| --- | --- |
| `npm cache verify` | Verify cache integrity |
| `npm cache clean --force` | Clear cache |
| `npm cache ls` | List cached packages |

## Dependencies

| Command | Description |
| --- | --- |
| `npm ls` | Show dependency tree |
| `npm ls <package>` | Show where package is used |
| `npm why <package>` | Explain why package is installed |
| `npm fund` | Show funding information for dependencies |
| `npm diff <package>@<version1> <package>@<version2>` | Compare package versions |

## NPX (Package Runner)

| Command | Description |
| --- | --- |
| `npx <package>` | Execute package without installing |
| `npx create-react-app my-app` | Create React app |
| `npx webpack` | Run webpack without global install |
| `npx --package=<package> <command>` | Specify package to use |
| `npx -p <package1> -p <package2> <command>` | Use multiple packages |

## Security and Auditing

| Command | Description |
| --- | --- |
| `npm audit` | Run security audit |
| `npm audit --json` | Output audit in JSON format |
| `npm audit fix` | Automatically fix vulnerabilities |
| `npm audit fix --force` | Force fix (may break compatibility) |
| `npm ci` | Clean install from package-lock.json |

## Working with Lock Files

| Command | Description |
| --- | --- |
| `npm ci` | Install from lock file (clean install) |
| `npm install --package-lock-only` | Generate lock file without installing |
| `npm shrinkwrap` | Create npm-shrinkwrap.json |

## Common Flags

| Flag | Description |
| --- | --- |
| `--save` | Add to dependencies (default for npm 5+) |
| `--save-dev` | Add to devDependencies |
| `--save-optional` | Add to optionalDependencies |
| `--no-save` | Don't add to package.json |
| `--exact` | Install exact version |
| `--dry-run` | Show what would be done without doing it |
| `--production` | Skip devDependencies |
| `--force` | Force operation |
| `--verbose` | Show verbose output |
| `--silent` | Minimize output |

## Package.json Scripts Examples

```json
{
  "scripts": {
    "start": "node server.js",
    "dev": "nodemon server.js",
    "build": "webpack --mode production",
    "test": "jest",
    "lint": "eslint src/",
    "format": "prettier --write src/",
    "pre-commit": "lint-staged"
  }
}
```

## Useful Aliases

| Command | Alias |
| --- | --- |
| `npm install` | `npm i` |
| `npm install --save-dev` | `npm i -D` |
| `npm install --global` | `npm i -g` |
| `npm test` | `npm t` |
| `npm run` | `npm r` |