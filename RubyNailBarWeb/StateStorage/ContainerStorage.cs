using Microsoft.Identity.Client;

namespace RubyNailBarWeb.StateStorage
{
    public class ContainerStorage
    {
        
        public ContainerStorage() { }

        
        private PaginationData paginationData { set; get; } = new PaginationData(); 

        public void setPaginationData(int currentPage, int totalPages, int pageSize)
        {

            paginationData.CurrentPage = currentPage;
            paginationData.TotalPages = totalPages;
            paginationData.PageSize = pageSize;
            
        }

        public void setTotalPages(int totalPages)
        {
             paginationData.TotalPages = totalPages;
        }

        public void setCurrentPage(int currentPage)
        {
            paginationData.CurrentPage = currentPage;
        }

        public int getLastCurrentPage()
        {
           return paginationData.CurrentPage;
        }
        public int getLastTotalPage()
        {
            return paginationData.TotalPages;
        }

        public int getLastPageSize()
        {
            return paginationData.PageSize;
        }
    }

    public class PaginationData
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public PaginationData() { }
        public PaginationData(int currentPage, int totalPages, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
        }
    }
}
