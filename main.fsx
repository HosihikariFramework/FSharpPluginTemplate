#load "helper.fsx"

open System.Threading
open System.Threading.Tasks
open Hosihikari
open Microsoft.Extensions.Logging

/// The F# loader finds this parameterless type implementing IPlugin.
type MyFSharpPlugin() =
    interface IPlugin with
        member this.OnLoad(context: IPluginContext, cancellationToken: CancellationToken) : ValueTask =
            context.Logger.LogInformation("MyFSharpPlugin loaded from {Message}", TemplateHelper.message)
            ValueTask()
        member this.OnEnable(cancellationToken: CancellationToken) : ValueTask<PluginHookResult> =
            ValueTask.FromResult(PluginHookResult.Allow())
        member this.OnDisable(cancellationToken: CancellationToken) : ValueTask<PluginHookResult> =
            ValueTask.FromResult(PluginHookResult.Allow())
        member this.OnUnload(cancellationToken: CancellationToken) : ValueTask = ValueTask()
