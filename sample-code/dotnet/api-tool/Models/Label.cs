namespace api_tool.Models;


public class Label {

    public Guid ID { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTimeOffset? Created { get; set; }
    public DateTimeOffset? LastUpdated { get; set; }
    public string CultureCode { get; set; } = "";

    public string[]? Inputs { get; set; }
    public string[]? QueryParameters { get; set; }
    public string[]? Fields { get; set; }
}