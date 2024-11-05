using api_tool.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace api_tool;

public class App
{
  private readonly ILogger<App> _logger;
  private readonly APIOptions _options;

  private readonly JsonSerializerOptions _jsonSettings = new()
  {
    PropertyNameCaseInsensitive = true
  };

  public App(
    IOptions<APIOptions> options,
    ILogger<App> logger)
  {
    _logger = logger;
    _options = options.Value;
  }

  public async Task ListAction(ListOptions options)
  {
    _logger.LogInformation("ListAction");
    _logger.LogInformation($"Type: {options.Type}");
    _logger.LogInformation($"Page: {options.Page}");
    _logger.LogInformation($"Page Size: {options.PageSize}");


    switch (options.Type)
    {
      case ListTypeEnum.Agent:
        await ListAgents(options);
        break;

      case ListTypeEnum.Printer:
        await ListPrinters(options);
        break;

      default:
        _logger.LogError("Invalid Type");
        break;

    }
  }

  private async Task ListAgents(ListOptions options)
  {

    using var client = GetClient();

    try
    {
      var response = await client.GetAsync("/api/v2/agent/list?page=0&pageSize=10");

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
          _logger.LogInformation("Empty Response");
          return;
        }

        _logger.LogDebug(content);
        var agents = JsonSerializer.Deserialize<PagedResponse<Agent>>(content, _jsonSettings);
        if (agents?.TotalItems == 0)
        {
          _logger.LogInformation("No agents found");
          return;
        }

        foreach (var agent in agents.Values)
        {
          _logger.LogInformation($"Agent: {agent.Name} ID: {agent.ID} Host: {agent.HostName}");
        }

      }
      else
      {
        _logger.LogError($"Error: {response.StatusCode}");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error");
    }
  }

  private async Task ListPrinters(ListOptions options)
  {

    using var client = GetClient();

    try
    {
      var request = new
      {
        page = options.Page,
        pageSize = options.PageSize
      };

      var response = await client.GetAsync("/api/v2/printer/list?page=0&pageSize=10");

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
          _logger.LogInformation("Empty Response");
          return;
        }

        _logger.LogDebug(content);
        var printers = JsonSerializer.Deserialize<PagedResponse<Printer>>(content, _jsonSettings);
        if (printers?.TotalItems == 0)
        {
          _logger.LogInformation("No printers found");
          return;
        }

        foreach (var printer in printers.Values)
        {
          _logger.LogInformation($"Printer: {printer.Name} ID: {printer.ID} ");
        }

      }
      else
      {
        _logger.LogError($"Error: {response.StatusCode}");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error");
    }
  }

  public async Task PrintAction(PrintOptions options)
  {
    _logger.LogInformation("PrintAction");
    _logger.LogInformation($"Label: {options.Label}");
    _logger.LogInformation($"DataFile: {options.DataFile}");
    _logger.LogInformation($"InputsFile: {options.InputsFile}");
    _logger.LogInformation($"QueryFile: {options.QueryFile}");
    _logger.LogInformation($"Output: {options.Output}");
    _logger.LogInformation($"Copies: {options.Copies}");
  }


  private HttpClient GetClient()
  {
    var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _options.APIKey);
    client.BaseAddress = new Uri(_options.APIUrl);
    client.Timeout = TimeSpan.FromMinutes(1);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("User-Agent", "api-tool");
    return client;
  }
}