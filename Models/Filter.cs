namespace mongodb_net_api_crud.Models
{
    public class Filter
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "id";
        public bool OrderDesc { get; set; } = false;
    }
}
