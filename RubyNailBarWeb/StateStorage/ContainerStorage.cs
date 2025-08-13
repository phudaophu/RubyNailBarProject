using Microsoft.Identity.Client;

namespace RubyNailBarWeb.StateStorage
{
    public class ContainerStorage
    {
        
        public ContainerStorage() { }

        private PaginationData paginationData { set; get; } = new PaginationData();
        private List<string> allowedToResetPaginationDataPathList { set; get; } = new List<string>() { "", "users", "customers" };
        private string savedPaginationPath { set; get; } = string.Empty;   
        public void ResetPaginationData(string currentPath)
        {
            if ( allowedToResetPaginationDataPathList.Any(p => p.Contains(currentPath,StringComparison.OrdinalIgnoreCase)) 
                && !currentPath.Equals(savedPaginationPath, StringComparison.OrdinalIgnoreCase))
            {
                this.savedPaginationPath = currentPath;
                paginationData.selectedRecordId = 0;
                paginationData.currentPage = 1;
                paginationData.totalPages = 1;
                paginationData.pageSize = 5;
            }
        } 

        public void SetPaginationData(int currentPage, int totalPages, int pageSize, int selectedRecordId)
        {
            paginationData.currentPage = currentPage;
            paginationData.totalPages = totalPages;
            paginationData.pageSize = pageSize;
            paginationData.selectedRecordId = selectedRecordId;

        }

        public void SetSelectedRecordId(int selectedRecordId)
        {
            paginationData.selectedRecordId = selectedRecordId;
        }

        public int GetLastSelectedRecordId()
        {
            return paginationData.selectedRecordId;
        }

        public void SetTotalPages(int totalPages)
        {
             paginationData.totalPages = totalPages;
        }

        public void SetCurrentPage(int currentPage)
        {
            paginationData.currentPage = currentPage;
        }

        public int GetLastCurrentPage()
        {
           return paginationData.currentPage;
        }
        public int GetLastTotalPage()
        {
            return paginationData.totalPages;
        }

        public int GetLastPageSize()
        {
            return paginationData.pageSize;
        }
    }

    public class PaginationData
    {
        // selectRecordId is used to store the ID of the currently selected record in the UI.
        public int selectedRecordId { get; set; } = 0;  
        public int currentPage { get; set; } = 1;
        public int totalPages { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public PaginationData() { }
        public PaginationData( int currentPage, int totalPages, int pageSize, int selectedRecordId)
        {
            this.currentPage = currentPage;
            this.totalPages = totalPages;
            this.pageSize = pageSize;
            this.selectedRecordId = selectedRecordId;

        }
    }
}
