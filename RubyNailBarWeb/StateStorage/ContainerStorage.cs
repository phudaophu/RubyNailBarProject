using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;

namespace RubyNailBarWeb.StateStorage
{
    public class ContainerStorage
    {
        
        public ContainerStorage() { }

        private PaginationData paginationIndexPage { set; get; } = new PaginationData();
        private PaginationData paginationUsersPage { set; get; } = new PaginationData();
        private PaginationData paginationCustomersPage { set; get; } = new PaginationData();
        private PaginationData paginationInvoicesPage { set; get; } = new PaginationData();
        private List<string> allowedToResetPaginationDataPathList { set; get; } = new List<string>() { "", "users", "customers", "invoices" };

        private string savedPaginationPath { set; get; } = string.Empty;

        
        private PaginationData PaginationSelector(string currentPath)
        {
            if (!string.IsNullOrEmpty(currentPath))
            {
                switch (currentPath)
                {
                    case var path when string.Equals(path, "users", StringComparison.OrdinalIgnoreCase):
                        return paginationUsersPage;
                    case var path when string.Equals(path, "customers", StringComparison.OrdinalIgnoreCase):
                        return paginationCustomersPage;
                    case var path when string.Equals(path, "invoices", StringComparison.OrdinalIgnoreCase):
                        return paginationInvoicesPage;
                    case var path when string.Equals(path, string.Empty):
                        return paginationIndexPage;
                        //default:
                        //    throw new ArgumentException($"Loi: Pagination Data in {nameof(currentPath)} can not be found !!!");
                }
            }
            return new PaginationData() { };
        }
       
        public bool CheckOptionalDataExist(string currentPath)
        {
            var paginationData = PaginationSelector(currentPath);
            if (paginationData.optionalFilterDataDict.Count > 0)
            {
                foreach (var key in paginationData.optionalFilterDataDict.Keys.ToList())
                {
                   if( paginationData.optionalFilterDataDict[key] > 0)
                    {
                        return true;
                    }
                }
            }
            return false;   
        } 

        public string GetSavePaginaionPath()
        {
            return this.savedPaginationPath;
        }
        public void ResetPaginationData(string currentPath)
        {
            if ( !savedPaginationPath.Equals(currentPath, StringComparison.OrdinalIgnoreCase)
                    && allowedToResetPaginationDataPathList.Any(p => p.Contains(currentPath, StringComparison.OrdinalIgnoreCase)))
            {
                var paginationData = PaginationSelector(savedPaginationPath);
                if (paginationData.optionalFilterDataDict.Count > 0)
                {
                    foreach (var key in paginationData.optionalFilterDataDict.Keys.ToList())
                    {
                        paginationData.optionalFilterDataDict[key] = 0;
                    }
                }
                paginationData.currentPage = 1;
                paginationData.totalPages = 1;
                paginationData.pageSize = 5;

                if (!string.IsNullOrWhiteSpace(paginationData.searchedText))
                    paginationData.searchedText = string.Empty;

                if (paginationData.selectedRecordId != 0)
                    paginationData.selectedRecordId = 0;

                this.savedPaginationPath = currentPath;
            }
        }
        

        public void SetPaginationData(string currentPath ,int currentPage, int totalPages, int pageSize, int? selectedRecordId=null, string? searchedText = null, Dictionary<String,int>? optionalData = null )
        {
            var paginationData = PaginationSelector(currentPath);
            paginationData.currentPage = currentPage;
            paginationData.totalPages = totalPages;
            paginationData.pageSize = pageSize;
            if( selectedRecordId.HasValue)
            {
                paginationData.selectedRecordId = selectedRecordId.Value;
            }
            if(!string.IsNullOrWhiteSpace(searchedText))
            {
                paginationData.searchedText = searchedText;
            }
            if (optionalData != null) 
            {
                paginationData.optionalFilterDataDict = optionalData;
            }
        }

        public void SetOptionalData(string currentPath,string keyName, int value)
        {
            if (value >= 0)
            {
                PaginationSelector(currentPath).optionalFilterDataDict[keyName] = value;
            }
        }

        public Dictionary<string,int> GetOptionalData(string currentPath)
        {
            var optionalDataDict = PaginationSelector(currentPath).optionalFilterDataDict;
            if (optionalDataDict.Count > 0)
            {
                return optionalDataDict;
            }
            return new Dictionary<string, int>();
        }


        public void SetSearchedText(string currentPath, string keyword)
        {
            if(!PaginationSelector(currentPath).searchedText.Equals(keyword))
                PaginationSelector(currentPath).searchedText = keyword; 
            
        }

        public void SetSelectedRecordId(string currentPath, int selectedRecordId)
        {
            if(PaginationSelector(currentPath).selectedRecordId != selectedRecordId)
                PaginationSelector(currentPath).selectedRecordId = selectedRecordId;
        }

        public void SetPageSize(string currentPath,int pageSize)
        {
            if(PaginationSelector(currentPath).totalPages != pageSize)
                PaginationSelector(currentPath).totalPages = pageSize;

        }

        public void SetTotalPages(string currentPath, int totalPages)
        {
            if(PaginationSelector(currentPath).totalPages != totalPages)
                PaginationSelector(currentPath).totalPages = totalPages;
        }

        public void SetCurrentPage(string currentPath, int currentPage)
        {
            if(PaginationSelector(currentPath).currentPage != currentPage)
                PaginationSelector(currentPath).currentPage = currentPage;
        }

        public int GetLastCurrentPage(string currentPath)
        {
            return PaginationSelector(currentPath).currentPage;
        }
        public int GetLastTotalPage(string currentPath)
        {
            return PaginationSelector(currentPath).totalPages;
        }

        public int GetLastPageSize(string currentPath)
        {
            return PaginationSelector(currentPath).pageSize;
        }
        public int GetLastSelectedRecordId(string currentPath)
        {
            return PaginationSelector(currentPath).selectedRecordId;
        }

        public string GetLastSearchedText(string currentPath)
        {
            return PaginationSelector(currentPath).searchedText;
        }
    }

    public class PaginationData
    {
        public Dictionary<string,int> optionalFilterDataDict { get; set; } = new Dictionary<string, int>();
        public string searchedText { get; set; } = string.Empty;
        public int selectedRecordId { get; set; } = 0;  
        public int currentPage { get; set; } = 1;
        public int totalPages { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public PaginationData() { }
        public PaginationData( int currentPage, int totalPages, int pageSize, int selectedRecordId,string searchedText, Dictionary<string,int> optionalFilterDataDict)
        {
            this.currentPage = currentPage;
            this.totalPages = totalPages;
            this.pageSize = pageSize;
            this.selectedRecordId = selectedRecordId;
            this.searchedText = searchedText;
            this.optionalFilterDataDict = optionalFilterDataDict;   
        }
    }
}
