using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await GetServerInfoAsync();

        return Ok(result);
    }

    private async Task<List<KeyValuePair<string, string>>> GetServerInfoAsync()
    {
        var hostName = System.Net.Dns.GetHostName();

        var apiVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";

        // Memory
        var gcMemoryInfo = GC.GetGCMemoryInfo();
        var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;

        // IP
        var ipArray = await System.Net.Dns.GetHostAddressesAsync(hostName);
        var ipList = ipArray.Select(ipAddress => ipAddress.ToString()).ToList();

        var serverInfo = new List<KeyValuePair<string, string>>()
        {
            new("ApiVersion", apiVersion != null ? apiVersion.ToString() : "Undefined"),
            new("DotnetVersion", RuntimeInformation.FrameworkDescription),
            new("DotnetEnvironment",
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "undefined"),
            new("OperatingSystem", RuntimeInformation.OSDescription),
            new("RuntimeIdentifier", RuntimeInformation.RuntimeIdentifier),
            new("ProcessorArchitecture", RuntimeInformation.OSArchitecture.ToString()),
            new("CpuCores", Environment.ProcessorCount.ToString()),
            new(
                "Containerized",
                (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") is not null).ToString()
            ),
            new("User", Environment.UserName),
            new("Memory",
                $"Installed Memory {MemoryInBestUnit(installedMemory)}"),
            new("HostName", hostName),
            new("IpList", string.Join(", ", ipList))
        };

        return serverInfo;
    }

    private string MemoryInBestUnit(long size)
    {
        const long megaByte = 1024 * 1024;
        const long gigaByte = megaByte * 1024;

        switch (size)
        {
            case < megaByte:
                return $"{size} bytes";
            case < gigaByte:
            {
                var megaBytes = (double)size / megaByte;
                return $"{megaBytes:N2} MiB";
            }
            default:
            {
                var gigaBytes = (double)size / gigaByte;
                return $"{gigaBytes:N2} GiB";
            }
        }
    }
}