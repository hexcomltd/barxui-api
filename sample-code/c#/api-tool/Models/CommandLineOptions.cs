namespace api_tool.Models;
using CommandLine;

[Verb("print", HelpText = "Prints a label")]
public class PrintOptions
{
  [Option('l', "label", Required = true, HelpText = "The label to print - name or ID")]
  public string Label { get; set; }
  
  [Option('d', "data", Required = false, HelpText = "The data to send - file in json format.")]
  public string DataFile { get; set; }
  
  [Option('i', "inputs", Required = false, HelpText = "The inputs to send - file in json format.")]
  public string InputsFile { get; set; }
  
  [Option('q', "query", Required = false, HelpText = "The query parameters to send - file in json format.")]
  public string QueryFile { get; set; }
  
  [Option('o', "output", Required = false, HelpText = "The output destination - PDF, PNG or a printer name or printer id.")]
  public string Output { get; set; }
  
  [Option('c', "copies", Required = false, HelpText = "The number of copies to print.")]
  public int Copies { get; set; }
  
}


public enum ListTypeEnum
{
  Agent = 0x0,
  Label = 0x1,
  Printer = 0x2
}


[Verb("list", HelpText = "List objects")]
public class ListOptions
{
  [Option('t', "type", Required = true, HelpText = "The type of object to list - AGENT, PRINTER, LABEL.")]
  public ListTypeEnum Type { get; set; }  

  [Option('p', "page", Required = false, Default = 0, HelpText = "The page of objects") ]
  public int? Page { get; set; }
  
  [Option('s', "pageSize", Required = false, Default = 10, HelpText = "The number of objects per") ]
  public int? PageSize { get; set; }
}


