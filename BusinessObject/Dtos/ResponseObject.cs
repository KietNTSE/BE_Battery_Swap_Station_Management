namespace BusinessObject.DTOs;

public class ResponseObject <T>
{
    public T? Content { get; set; }
    public string Message { get; set; }
    public string Code { get; set; }
    public bool Success { get; set; }
    public PaginationResponse? Pagination { get; set; }
    public ResponseObject()
    {
        Message = string.Empty;
        Code = string.Empty;
        Success = false;
    }

    public ResponseObject(T? content, string message, string code, bool success, PaginationResponse? pagination = null)
    {
        Content = content;
        Message = message;
        Code = code;
        Success = success;
        Pagination = pagination;
    }
}

public class PaginationResponse
{
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
}

public class PaginationWrapper<T, TU> where T : IList<TU>
{
    public T Items { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    
    public PaginationWrapper(T items, int page, int totalCount, int pageSize)
    {
        Items = items;
        Page = page;
        TotalCount = totalCount;
        PageSize = pageSize;
    }
}