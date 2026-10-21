#load "helper.fsx"

open Hosihikari
open Microsoft.Extensions.Logging

/// The F# provider finds this parameterless type implementing IPlugin.
type MyFSharpPlugin() =
    interface IPlugin with
        member this.OnLoad(context: IPluginContext) =
            context.Logger.LogInformation("MyFSharpPlugin loaded from {Message}", TemplateHelper.message)
        member this.OnEnable() = PluginHookResult.Allow()
        member this.OnDisable() = PluginHookResult.Allow()
        member this.OnUnload() = ()
