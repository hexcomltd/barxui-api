namespace api_tool.Models;

public class PrintRequest
{
  public enum OutputModeEnum
  {
    PDF,
    PNG,
    PRINT
  }

  public class PrintRequestOutput
  {
    public int Copies { get; set; } = 1;
    public bool Collate { get; set; } = false;
    public bool ColumnFirst { get; set; } = false;
    public bool CropMarks { get; set; } = false;
    public bool Flip { get; set; } = false;
    public bool Mirror { get; set; } = false;
    public Guid? PrinterId { get; set; }
    public string? FileName { get; set; }
    public bool Test { get; set; } = false;
  }

  public Guid ID { get; set; }
  
  public Guid LabelId { get; set; }
  
  public Dictionary<string, string>? Input { get; set; }
  
  public Dictionary<string, string>? QueryParams { get; set; }
  
  public Dictionary<string, string>[]? Data { get; set; }
  
  public OutputModeEnum OutputMode { get; set; }
  
  public PrintRequestOutput OutputParameters { get; }

  public PrintRequest()
  {
    OutputParameters = new PrintRequestOutput();
  }

}