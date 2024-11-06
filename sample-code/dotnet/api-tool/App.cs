using api_tool.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace api_tool;

public class App
{
  private readonly APIOptions _options;

  private readonly JsonSerializerOptions _jsonSettings = new()
  {
    PropertyNameCaseInsensitive = true
  };

  public App(IOptions<APIOptions> options)
  {
    _options = options.Value;
  }

  public async Task ListAction(ListOptions options)
  {
    switch (options.Type)
    {
      case ListTypeEnum.Agent:
        await ListAgents(options);
        break;

      case ListTypeEnum.Printer:
        await ListPrinters(options);
        break;

      case ListTypeEnum.Label:
        await ListLabels(options);
        break;

      default:
        Console.Error.WriteLine("Invalid Type");
        break;

    }
  }

  private async Task ListAgents(ListOptions options)
  {

    Console.WriteLine("Listing Agents\n");
    using var client = GetClient();

    try
    {
      var response = await client.GetAsync("/api/v2/agent/list?page=0&pageSize=10");

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
          Console.Error.WriteLine("Empty Response");
          return;
        }

        var agents = JsonSerializer.Deserialize<PagedResponse<Agent>>(content, _jsonSettings);
        if (agents is null || agents.TotalItems == 0)
        {
          Console.Error.WriteLine("No agents found");
          return;
        }

        Console.WriteLine("{0,-40}{1,-30}{2,-20}", "ID", "Name", "Host");
        Console.WriteLine(new string('-', 90));
        foreach (var agent in agents.Values)
        {
          Console.WriteLine("{0,-40}{1,-30}{2,-20}", agent.ID, agent.Name, agent.HostName);
        }

      }
      else
      {
        Console.Error.WriteLine($"Error: {response.StatusCode}");
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error: {ex.Message}");
    }
  }

  private async Task ListPrinters(ListOptions options)
  {

    Console.WriteLine("Listing Printers\n");
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
          Console.Error.WriteLine("Empty Response");
          return;
        }

        var printers = JsonSerializer.Deserialize<PagedResponse<Printer>>(content, _jsonSettings);
        if (printers is null || printers.TotalItems == 0)
        {
          Console.Error.WriteLine("No printers found");
          return;
        }

        Console.WriteLine("{0,-40}{1,-30}", "ID", "Name");
        Console.WriteLine("---------------------------------------------------------------");
        foreach (var printer in printers.Values)
        {
          Console.WriteLine("{0,-40}{1,-30}", printer.ID, printer.Name);
        }

      }
      else
      {
        Console.Error.WriteLine($"Error: {response.StatusCode}");
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error: {ex.Message}");
    }
  }

  private async Task ListLabels(ListOptions options)
  {
    Console.WriteLine("Listing Labels\n");
    using var client = GetClient();

    try
    {
      var request = new LabelListRequest
      {
        Page = options.Page ?? 0,
        PageSize = options.PageSize ?? 10,
        SearchText = "", // can search for labels by name with wildcards, eg, "Widget*"
        OrderBy = LabelSearchOrderEnum.Name,
        Descending = false
      };

      var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

      var response = await client.PostAsync("/api/v2/label/list", requestContent);

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
          Console.Error.WriteLine("Empty Response");
          return;
        }

        var labels = JsonSerializer.Deserialize<PagedResponse<Label>>(content, _jsonSettings);
        if (labels is null || labels.TotalItems == 0)
        {
          Console.Error.WriteLine("No labels found");
          return;
        }

        Console.WriteLine("{0,-40}{1,-30}", "ID", "Name");
        Console.WriteLine("---------------------------------------------------------------");
        foreach (var label in labels.Values)
        {
          Console.WriteLine("{0,-40}{1,-30}", label.ID, label.Name);
        }
      }
      else
      {
        Console.Error.WriteLine($"Error: {response.StatusCode}");
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error: {ex.Message}");
    }
  }


  public async Task PrintAction(PrintOptions options)
  {
    using var client = GetClient();

    try
    {

      Guid labelId;

      if (!Guid.TryParse(options.Label, out labelId))
      {
        var label = await GetLabel(options.Label);
        if (label is not null)
        {
          labelId = label.ID;
        }
        else
        {
          Console.Error.WriteLine("Label not found");
          return;
        }
      }

      var outputMode = options.Output.ToUpper() switch
      {
        "PDF" => PrintRequest.OutputModeEnum.PDF,
        "PNG" => PrintRequest.OutputModeEnum.PNG,
        _ => PrintRequest.OutputModeEnum.PRINT,
      };

      Guid? printerId = null;
      string? fileName = null;

      switch (outputMode)
      {


        case PrintRequest.OutputModeEnum.PRINT:
          {
            if (string.IsNullOrWhiteSpace(options.Output))
            {
              Console.Error.WriteLine("Output is required for PRINT mode");
              return;
            }

            if (Guid.TryParse(options.Output, out Guid id))
            {
              printerId = id;
            }
            else
            {

              var printer = await GetPrinter(options.Output);
              if (printer is null)
              {
                Console.Error.WriteLine("Printer not found");
                return;
              }

              printerId = printer.ID;
            }
            break;
          }

        case PrintRequest.OutputModeEnum.PNG:
          {
            if (string.IsNullOrWhiteSpace(options.FileName))
            {
              Console.Error.WriteLine("Filename is required for PNG mode");
              return;
            }

            fileName = options.FileName;
            if (!fileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
              fileName += ".zip";
            break;
          }

        case PrintRequest.OutputModeEnum.PDF:
          {
            if (string.IsNullOrWhiteSpace(options.FileName))
            {
              Console.Error.WriteLine("Filename is required for PDF mode");
              return;
            }

            fileName = options.FileName;
            if (!fileName.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase))
              fileName += ".pdf";
            break;
          }
      }


      var request = new PrintRequest
      {
        LabelId = labelId,
        OutputMode = outputMode
      };
      request.OutputParameters.Copies = options.Copies;
      request.OutputParameters.PrinterId = printerId;
      request.OutputParameters.FileName = fileName;

      if (!string.IsNullOrWhiteSpace(options.DataFile) && File.Exists(options.DataFile))
        request.Data = JsonSerializer.Deserialize<Dictionary<string, string>[]>(await File.ReadAllTextAsync(options.DataFile));
      if (!string.IsNullOrWhiteSpace(options.QueryFile) && File.Exists(options.QueryFile))
        request.QueryParams = JsonSerializer.Deserialize<Dictionary<string, string>>(await File.ReadAllTextAsync(options.QueryFile));
      if (!string.IsNullOrWhiteSpace(options.InputsFile) && File.Exists(options.InputsFile))
        request.Input = JsonSerializer.Deserialize<Dictionary<string, string>>(await File.ReadAllTextAsync(options.InputsFile));

      var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

      Console.WriteLine($"Printing...");
      var response = await client.PostAsync("/api/v2/print", requestContent);

      if (response.IsSuccessStatusCode)
      {
        if (outputMode == PrintRequest.OutputModeEnum.PRINT)
        {
          var content = await response.Content.ReadAsStringAsync();
          var printResponse = JsonSerializer.Deserialize<PrintResponse>(content, _jsonSettings);
          if (printResponse is null)
          {
            Console.Error.WriteLine("Error: No response");
            return;
          }

          if (printResponse.ErrorMessage is not null)
          {
            Console.Error.WriteLine($"Print Error: {printResponse.ErrorMessage}");
          }
          else
          {
            Console.WriteLine($"Printed: {printResponse.Success}");
          }
        }
        else
        {

          // get the file from the response
          var content = await response.Content.ReadAsByteArrayAsync();

          // write the byte array to the file
          await File.WriteAllBytesAsync(fileName!, content);

          Console.WriteLine($"File written: {fileName}");
        }
      }
      else
      {
        Console.Error.WriteLine($"Error: {response.StatusCode}");
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error: {ex.Message}");
    }
  }

  private async Task<Label?> GetLabel(string labelName)
  {
    using var client = GetClient();

    var request = new LabelListRequest
    {
      Page = 0,
      PageSize = 10,
      SearchText = labelName,
      OrderBy = LabelSearchOrderEnum.Name,
      Descending = false
    };

    var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
    var response = await client.PostAsync("/api/v2/label/list", requestContent);

    if (response.IsSuccessStatusCode)
    {
      var content = await response.Content.ReadAsStringAsync();

      if (!string.IsNullOrWhiteSpace(content))
      {
        var labels = JsonSerializer.Deserialize<PagedResponse<Label>>(content, _jsonSettings);
        if (labels is not null && labels.Values.Count() > 0)
        {
          return labels.Values.First();
        }
      }
    }

    return null;
  }

  private async Task<Printer?> GetPrinter(string printerName)
  {
    using var client = GetClient();

    var page = 0;

    while (true)
    {
      var response = await client.GetAsync($"/api/v2/printer/list?page={page}&pageSize=20");

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(content))
        {
          var printers = JsonSerializer.Deserialize<PagedResponse<Printer>>(content, _jsonSettings);

          if (printers is null || !printers.Values.Any())
          {
            break;
          }

          if (printers.Values.Any(p => p.Name == printerName))
          {
            return printers.Values.First(p => p.Name == printerName);
          }

          page += 1;
        }
      }

      break;
    }

    return null;
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