namespace api_tool.Models;

public class Agent
{
  public Guid ID { get; set; }
  public string Name { get; set; }
  public string HostName { get; set; }
  public DateTimeOffset CreateDate { get; set; }
  public DateTimeOffset? UpdateDate { get; set; }
  public string InstalledVersion { get; set; }
}