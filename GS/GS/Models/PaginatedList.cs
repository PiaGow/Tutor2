namespace GS.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
            : base(items)
        {
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CurrentPage = pageNumber;
            PageSize = pageSize;
        }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

}
