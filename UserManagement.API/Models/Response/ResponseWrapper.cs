namespace UserManagement.API.Models.Response
{
    public class ResponseWrapper
    {
        public required ResponseWrapperStatus Status { get; set; }
        public required dynamic Data { get; set; }
    }

    public class ResponseWrapperStatus
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class DeleteResponse
    {
        public required bool Result { get; set; }

        public required string Message { get; set; }
    }

    public class GetAllResponse
    {
        public required dynamic Data { get; set; }
        public required int Page { get; set; }
        public required int PageSize { get; set;}
        public required int TotalCount { get; set;}
    }
}
