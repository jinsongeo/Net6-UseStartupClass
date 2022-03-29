## Net6-UseStartupClass

### Using Startup Class with .NET 6

### Comparing the Problem & ProblemDetails Results

### Setting up [Serilog](https://serilog.net/)
* [Serilog Aspnetcore](https://github.com/serilog/serilog-aspnetcore)
* [Two Stage Initialization](https://github.com/serilog/serilog-aspnetcore#two-stage-initialization)
* [Serilog Settings Configurations](https://github.com/serilog/serilog-settings-configuration)
* [Serilog Sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks)
    1. Console
    1. File
    1. Seq

### Setting up [Seq](https://datalust.co/seq)
```
docker pull datalust/seq

docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```