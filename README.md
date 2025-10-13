# Unity Test Project

Small Unity starter repo with clean Git hygiene, enforced C# formatting, and protected `main` via GitHub Rulesets.

## Contents
- [Requirements](#requirements)
- [Project Setup](#project-setup)
- [Repo Structure](#repo-structure)
- [Git & GitHub Workflow](#git--github-workflow)
- [Formatting (CSharpier)](#formatting-csharpier)
- [CI Checks](#ci-checks)
- [Branch Protection / Ruleset](#branch-protection--ruleset)
- [Contributing](#contributing)
- [License](#license)

---

## Requirements
- **Unity**: any LTS
- **.NET SDK**: 8.0+  
  - Windows: install from Microsoft  
  - macOS: `brew install dotnet-sdk`  
  - Verify: `dotnet --version` → shows `8.0.x`.

---

## Project Setup
1. **Clone the repo**
   ```bash
   git clone https://github.com/<your-org-or-user>/<repo>.git
   cd <repo>
   ```
2. **Open in Unity Hub** (open the folder containing `Assets/`, `Packages/`, `ProjectSettings/`).
3. **Unity editor settings (verify)**
   - Edit → Project Settings → Version Control  
     - Mode: **Visible Meta Files**
   - Edit → Editor
     - Asset Serialization → Mode: **Force Text**

---

## Repo Structure
```
<repo>/
├─ Assets/            # Scenes, scripts, prefabs, etc. (tracked)
├─ Packages/          # Unity packages manifest + lock (tracked)
├─ ProjectSettings/   # Unity project configuration (tracked)
├─ .vscode/           # Local editor settings (optional, tracked)
├─ .github/workflows/ # CI (format checks)
├─ .config/           # dotnet tool manifest (CSharpier)
├─ .gitignore         # Excludes Library/Temp/etc.
└─ README.md
```

---

## Git & GitHub Workflow
**Never push directly to `main`.** Create a feature branch, open a PR, get review, pass checks, merge.

1. Create a branch
   ```bash
   git checkout -b feat/some-change
   ```
2. Code. Save often (auto-format runs if you set up VS Code).
3. Commit & push
   ```bash
   git add -A
   git commit -m "Describe the change"
   git push -u origin feat/some-change
   ```
4. Open a **Pull Request** → fix any failing checks → get **1 approval** → **Merge**.

---

## Formatting (CSharpier)
We enforce formatting with **CSharpier**. CI blocks unformatted code.

### Install (local, once per machine)
From repo root:
```bash
dotnet new tool-manifest      # creates .config/dotnet-tools.json if missing
dotnet tool install csharpier
dotnet tool restore
dotnet csharpier --version
```

### VS Code setup (Windows & macOS)
Install extension: **“CSharpier – C# Formatter”** (publisher: `csharpier`).

Create (or edit) `.vscode/settings.json`:
```json
{
  "editor.formatOnSave": true,
  "[csharp]": {
    "editor.defaultFormatter": "csharpier.csharpier-vscode"
  }
}
```

### Commands you’ll actually use
- Format all files (writes changes):  
  ```bash
  dotnet csharpier format .
  ```
- Check only (used by CI):  
  ```bash
  dotnet csharpier check .
  ```
- Restore tools (if VS Code complains):  
  ```bash
  dotnet tool restore
  ```

---

## CI Checks
GitHub Actions runs on PRs:
- **`format-check / csharp-format`** — runs `dotnet csharpier check .` and fails if anything’s off.

If CI fails formatting: run `dotnet csharpier format .`, commit, push. Done.

---

## Branch Protection / Ruleset
The repo uses a **Ruleset** to protect `main`:
- Require **Pull Request** before merging.
- Require **status checks** to pass:
  - `format-check / csharp-format`
- Require **1 approval**.
- **Block force pushes**.
- (Optional) **Require linear history** (squash/rebase merges only).

If you don’t see `format-check` when editing the ruleset, run a PR once so GitHub “registers” the check name.

---

## Contributing
- Keep PRs small and focused.
- Commit messages: imperative, clear: “Add X”, “Fix Y”.
- Don’t commit `Library/`, `Temp/`, `Logs/`, `.csproj`, `.sln`, etc. (already ignored).
- If adding assets, make sure their **`.meta`** files are present (Unity auto-creates them).

---

## License
TBD
