namespace Domain.Entities
{
    public class OrdenResponse
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<OrdenDto> Orders { get; set; }

        public OrdenResponse(int currentPage, int pageSize, int totalCount, int totalpages, List<OrdenDto> orders)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalpages;
            Orders = orders;
        }
    }

}
