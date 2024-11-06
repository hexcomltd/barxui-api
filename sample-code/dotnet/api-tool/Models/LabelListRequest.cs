namespace api_tool.Models;

public class LabelListRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public string? SearchText { get; set; }
    public LabelSearchOrderEnum OrderBy { get; set; }
    public bool Descending { get; set; }

}

public enum LabelSearchOrderEnum
{
    Name,
    Updated,
    Category
}

