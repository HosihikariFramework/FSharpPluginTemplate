# FSharpPluginTemplate

A minimal source-compiled F# plugin for the Hosihikari framework. The F# provider compiles
`main.fsx` at load with FSharp.Compiler.Service and runs the assembly in a collectible ALC; there is
no build step for the author.

## Use it

1. Copy this repository into your server as `plugins/MyFSharpPlugin/`.
2. Rename the plugin: change `manifest.json` `name`, rename `MyFSharpPlugin.conf`, and rename the
   type in `main.fsx`. The package directory name must equal the manifest `name`.
3. Install the shim variant for the server and the F# provider:

   ```powershell
   lip install github.com/HosihikariFramework/Shim#levilamina   # or #endstone
   lip install github.com/HosihikariFramework/FSharpProvider
   ```

4. Restart the server. The log shows the plugin loading and enabling.

For the self-hosted Linux carrier, install the suffix-less shim package and start the server through
`hosihikari-server`; providers and plugins still live under `plugins/`.

## Files

- `manifest.json` — `provider: fsharp`, depends on `hosihikari` and `hosihikari-fsharp`.
- `main.fsx` — a parameterless type implementing `IPlugin`. It loads `helper.fsx` with `#load`.
- `helper.fsx` — the second compilation unit, loaded into the entry script.
- `MyFSharpPlugin.conf` — package defaults.

## Configuration and data

- Package defaults: `plugins/<name>/<name>.conf`.
- Server override: `config/hosihikari/<name>.conf`.
- Environment variables override the same keys.
- Runtime data: `data/hosihikari/<name>/`.

## Lifecycle

`OnLoad` runs after dependencies are ready. `OnEnable` opens the event/timer gate; a refusal keeps
the plugin loaded and disabled. `OnUnload` releases what the plugin owns. An exception from a hook
is a fault and the host unloads the plugin.

See the [F# provider](https://github.com/HosihikariFramework/FSharpProvider) for the compilation
details.

## Licence

MIT-0, see [LICENSE.md](LICENSE.md).
