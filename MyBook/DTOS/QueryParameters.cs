namespace PublicLibrary.DTOS
{
    public class QueryParameters
    {

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;

        // Filtering
        public string? Search { get; set; }
    }
}
