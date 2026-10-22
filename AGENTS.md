# AGENTS.md

Source-compiled F# plugin template for the Hosihikari Framework. The F# loader compiles `main.fsx`
at load; the package ships source and no build output.

## Repository map

- `main.fsx` — plugin entry and hooks.
- `helper.fsx` — example `#load` helper.
- `manifest.json` — package manifest (`loader: fsharp`, `entry: main.fsx`).
- `MyFSharpPlugin.conf` — package defaults.
- `.github/workflows/verify.yml` — manifest check and compilation check.

## Build and verify

There is no local pack script. Copy the directory into the server as `plugins/MyFSharpPlugin/` after
renaming the manifest, `.conf`, and entry type.

CI copies `main.fsx` into a check project and builds it with `dotnet build -c Release`. Use the same
approach when changing the entry or `#load` graph.

## Invariants

- The manifest `name`, package directory name, and `.conf` name stay aligned.
- Keep the `hosihikari-fsharp` dependency and `extraInfo.loader: fsharp`.
- `#load` and local `#r` paths stay relative to the package and resolve inside the package.
- Do not commit build output or copied server packages.
- Public default branches are protected. Work on a branch, open a pull request, and squash merge.

## Context

- [F# loader](https://github.com/HosihikariFramework/FSharpLoader).
- Design decisions and terminology are kept by maintainers outside the public repositories; ask for
  the relevant decision when changing lifecycle or public API.
