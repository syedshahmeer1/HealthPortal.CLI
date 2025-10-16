# HealthPortal

HealthPortal is a small .NET solution containing a CLI project and a library. This repository contains the source for the CLI (`HealthPortal.CLI`) and a library (`HealthPortalLibrary`).

## Projects

- HealthPortal.CLI/ — console CLI for interacting with the HealthPortal components
- HealthPortalLibrary/ — reusable models and services used by the CLI

## Quick start

Requirements: .NET 8 SDK (or compatible SDK).

Build the solution:

```bash
dotnet build
```

Run the CLI project:

```bash
dotnet run --project HealthPortal.CLI
```

Or run the published binary (after publishing):

```bash
dotnet publish -c Release -o ./out
./out/HealthPortal.CLI
```

## How to turn this into a GitHub repo

Below are step-by-step commands you can run locally to initialize git, commit, create a GitHub repo (using the GitHub CLI `gh`), and push the code. See the "Manual (web)" option if you prefer to create the repo on github.com and add the remote.

### Using GitHub CLI (recommended)

Make sure you have `git` and `gh` installed and are authenticated with `gh auth login`.

```bash
# from the workspace root (where the solution file / README is)
cd /path/to/this/workspace
git init
git add .
git commit -m "Initial commit"
# create the remote repo (replace `--public` with `--private` if desired)
gh repo create <OWNER>/<REPO-NAME> --source=. --remote=origin --push --public
```

### Manual / web flow

```bash
cd /path/to/this/workspace
git init
git add .
git commit -m "Initial commit"
# create a repo on https://github.com/new, copy the remote URL
git remote add origin git@github.com:<owner>/<repo>.git
git branch -M main
git push -u origin main
```

## Next steps

- Replace the LICENSE placeholder name (if desired).
- Add GitHub Actions CI/workflows if you want automated builds/tests.
- Add contribution guidelines and issue/PR templates.

If you want, I can also run the `git init` and `gh repo create` commands here for you (I will need you to confirm and ensure the environment has `gh` authenticated), or I can just provide the commands and you run them locally. Which do you prefer?