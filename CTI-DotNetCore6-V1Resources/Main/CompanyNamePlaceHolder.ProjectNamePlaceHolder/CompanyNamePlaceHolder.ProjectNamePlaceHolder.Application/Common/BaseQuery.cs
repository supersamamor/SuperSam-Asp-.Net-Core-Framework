namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;

public record BaseQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public string[]? SearchColumns { get; set; }
    public string? SearchValue { get; set; }

    public BaseQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public BaseQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }
}
