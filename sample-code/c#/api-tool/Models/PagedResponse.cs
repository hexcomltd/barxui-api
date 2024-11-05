namespace api_tool.Models;
public class PagedResponse<T>
{
  public IEnumerable<T> Values { get; set; }
  public int Page { get; set; }
  public int TotalPages { get; set; } = 0;
  public int TotalItems { get; set; }
  public int PageSize { get; set; }


  public PagedResponse()
  {
    // needed for deserialisation
  }

  public PagedResponse(T singleRow)
  {
    Values = new[] { singleRow };
    Page = 0;
    PageSize = 1;
    TotalItems = 1;
    TotalPages = 1;
  }

  public PagedResponse(IEnumerable<T> values, int page, int pageSize, int totalItems)
  {
    Values = values;
    Page = page;
    PageSize = pageSize;
    TotalItems = totalItems;
    if (pageSize != 0)
    {
      TotalPages = totalItems / pageSize;

      if (totalItems % pageSize > 0)
      {
        TotalPages += 1;
      }
    }
  }
}