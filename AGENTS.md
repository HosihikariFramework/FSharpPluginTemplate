# AGENTS.md

Source-compiled F# plugin template for the Hosihikari Framework. The F# loader compiles `main.fsx`
at load; the package ships source and no build output.

## Repository map

- `main.fsx` — plugin entry and hooks.
- `helper.fsx` — example `#load` helper.
- `hosihikari.json` — package manifest (`loader: fsharp`, `entry: main.fsx`); the packer generates
  the `manifest.json` envelope, so it is never committed.
- `MyFSharpPlugin.conf` — package defaults.
- `pack.ps1`, `pack.sh` — migrate a 1.x `manifest.json` if present, then invoke the core packer
  and write `dist/MyFSharpPlugin/`.
- `.github/workflows/verify.yml` — manifest check, compilation check and pack check.

## Build and verify

```powershell
pwsh ./pack.ps1
```

```bash
bash ./pack.sh
```

The packer needs a sibling `PluginManager` checkout, or `HOSIHIKARI_PLUGINMANAGER` pointing at one.
Copy `dist/MyFSharpPlugin/` to the server's `plugins/` after renaming the manifest, `.conf`, and
entry type.

CI copies `main.fsx` into a check project and builds it with `dotnet build -c Release`. Use the same
approach when changing the entry or `#load` graph.

## Invariants

- The manifest `name`, package directory name, and `.conf` name stay aligned.
- Keep the `hosihikari-fsharp` dependency and `loader: fsharp`.
- Do not commit a generated `manifest.json`; it is an output of the packer.
- `#load` and local `#r` paths stay relative to the package and resolve inside the package.
- Do not commit build output or copied server packages.
- Public default branches are protected. Work on a branch, open a pull request, and squash merge.

## Context

- [F# loader](https://github.com/HosihikariFramework/FSharpLoader).
- Design decisions and terminology are kept by maintainers outside the public repositories; ask for
  the relevant decision when changing lifecycle or public API.
