# FSharpPluginTemplate

星光框架的最小 F# 源码插件模板。F# loader 在装载时用 FSharp.Compiler.Service 编译
`main.fsx`，把程序集放进 collectible ALC 运行；作者不需要自己构建。

## 使用

1. 在服务端之外改这个仓库：改 `hosihikari.json` 的 `name`，重命名 `MyFSharpPlugin.conf`，再改
   `main.fsx` 里的类型名。包目录名必须与清单里的 `name` 一致。
2. 用框架打包器打包，再把 `dist/MyFSharpPlugin/` 复制到服务端 `plugins/MyFSharpPlugin/`。打包器需要
   并列的 `PluginManager` 检出，或用 `HOSIHIKARI_PLUGINMANAGER` 指向一个：

   ```powershell
   pwsh ./pack.ps1
   ```

   ```bash
   bash ./pack.sh
   ```
3. 用 Seiko 安装载体 bundle 与 F# loader：

   ```powershell
   sk init                                  # 每个服务端根目录执行一次
   sk add hosihikari-hosting-levilamina     # LeviLamina
   # 或：sk add hosihikari-hosting-endstone
   # 或：sk add hosihikari-hosting-selfhosted
   sk add hosihikari-fsharp
   ```

4. 重启服务端，日志里会看到插件加载与启用。

自持态载体用 `sk add hosihikari-hosting-selfhosted`，按平台选 Windows 或 Linux；通过 `hosihikari-server`（Windows 上是 `hosihikari-server.exe`）启动。加载器与插件仍然放在 `plugins/` 下。

## 文件

- `hosihikari.json` —— `loader: fsharp`，依赖 `hosihikari` 与 `hosihikari-fsharp`；父级的
  `manifest.json` 信封由打包器生成，是产物。
- `pack.ps1` / `pack.sh` —— 调框架打包器；`dist/MyFSharpPlugin/` 是可安装的包。还停在 1.x
  `manifest.json` 的项目会在同一条命令里迁移（`-MigrateOnly` / `--migrate-only` 只重写元数据）。
- `main.fsx` —— 实现 `IPlugin` 的无参类型，用 `#load` 引入 `helper.fsx`。
- `helper.fsx` —— 第二个编译单元，被入口脚本加载。
- `MyFSharpPlugin.conf` —— 包内默认配置。

## 配置与数据

- 包内默认：`plugins/<name>/<name>.conf`。
- 服务端覆盖：`config/hosihikari/<name>.conf`。
- 环境变量覆盖同名键。
- 运行期数据：`data/hosihikari/<name>/`。

## 生命周期

`OnLoad` 在依赖就绪后执行；`OnEnable` 打开事件/定时器闸门，拒绝则保持已加载未启用；
`OnUnload` 释放插件持有的东西。钩子抛出异常属于故障，宿主会卸载插件。四个钩子都是
`ValueTask` 并接收 `CancellationToken`：尊重它，等不到就尽早返回。

编译细节见 [F# loader](https://github.com/HosihikariFramework/FSharpLoader)。

## 许可

MIT-0，见 [LICENSE.md](LICENSE.md)。
