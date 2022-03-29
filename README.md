## Net6-UseStartupClass

### Using Startup Class with .NET 6

### Comparing the Problem & ProblemDetails Results

### Configuring [Serilog](https://github.com/serilog/serilog-settings-configuration)
> [Sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks)
> 1. Console
> 1. File
> 1. Seq


### Setting up Seq
```
docker pull datalust/seq

docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```