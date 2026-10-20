# FSharpPluginTemplate

星光框架的最小 F# 源码插件模板。F# Provider 在装载时用 FSharp.Compiler.Service 编译
`main.fsx`，把程序集放进 collectible ALC 运行；作者不需要自己构建。

## 使用

1. 把本仓复制到服务端，目录名用 `plugins/MyFSharpPlugin/`。
2. 改名：改 `manifest.json` 的 `name`，重命名 `MyFSharpPlugin.conf`，再改 `main.fsx` 里的类型名。
   包目录名必须与清单里的 `name` 一致。
3. 按服务端安装 Shim 变体与 F# Provider：

   ```powershell
   lip install github.com/HosihikariFramework/Shim#levilamina   # 或 #endstone / #endstone_linux
   lip install github.com/HosihikariFramework/FSharpProvider
   ```

4. 重启服务端，日志里会看到插件加载与启用。

自持态 Linux 用无后缀 Shim 包，并通过 `hosihikari-server` 启动；Provider 与插件仍然放在 `plugins/` 下。

## 文件

- `manifest.json` —— `provider: fsharp`，依赖 `hosihikari` 与 `hosihikari-fsharp`。
- `main.fsx` —— 实现 `IPlugin` 的无参类型。
- `MyFSharpPlugin.conf` —— 包内默认配置。

## 配置与数据

- 包内默认：`plugins/<name>/<name>.conf`。
- 服务端覆盖：`config/hosihikari/<name>.conf`。
- 环境变量覆盖同名键。
- 运行期数据：`data/hosihikari/<name>/`。

## 生命周期

`OnLoad` 在依赖就绪后执行；`OnEnable` 打开事件/定时器闸门，拒绝则保持已加载未启用；
`OnUnload` 释放插件持有的东西。钩子抛出异常属于故障，宿主会卸载插件。

编译细节见 [F# Provider](https://github.com/HosihikariFramework/FSharpProvider)。
