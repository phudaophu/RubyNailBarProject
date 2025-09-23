using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using RubyNailBarWeb.Models;
using System.Runtime.CompilerServices;

namespace RubyNailBarWeb.StateStorage
{
    public class ContainerStorage
    {
        
        public ContainerStorage() { }
        private  PaginationData paginationIndexPage { set; get; } = new PaginationData();
        private  PaginationData paginationUsersPage { set; get; } = new PaginationData();
        private  PaginationData paginationCustomersPage { set; get; } = new PaginationData();
        private  PaginationData paginationInvoicesPage { set; get; } = new PaginationData();
        private  PaginationData paginationInvoicePage { set;get; } = new PaginationData();
        private  List<string> allowedToResetPaginationDataPathList { set; get; } = new List<string>() { "", "users", "customers", "invoices", "invoice" };

        private Dictionary<string, List<string>> subPathList { set; get; } = new Dictionary<string, List<string>>()
        {
            {"invoice", new List<string>(){ "invoices"} }

        };

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
                    case var path when string.Equals(path, "invoice", StringComparison.OrdinalIgnoreCase):
                        return paginationInvoicePage;
                    case var path when string.Equals(path, string.Empty):
                        return paginationIndexPage;

                }
            }
            return new PaginationData() { };
        }

        public int CalculateTotalInvoiceTime(Invoice invoice)
        {
            if (invoice is null || invoice.IsDeleted)
                return 0;

            if (invoice.InvoiceDetails is null)
                return 0;

            
            return invoice.InvoiceDetails
                .Where(d => d is not null
                            && !d.IsDeleted
                            && d.StartDatetime.HasValue
                            && d.EndDatetime.HasValue
                            && d.EndDatetime >= d.StartDatetime) 
                .Select(d => CalculateTotalServiceTime(d))    
                .DefaultIfEmpty(0)
                .Sum();

        }

        public int CalculateTotalServiceTime(InvoiceDetail invoiceDetail)
        {

            if (!invoiceDetail.IsDeleted && invoiceDetail.StartDatetime is not null && invoiceDetail.EndDatetime is not null)
            {
                var totalServiceTime = (invoiceDetail.EndDatetime - invoiceDetail.StartDatetime)?.TotalMinutes;
                if (totalServiceTime > 0)
                    return (int)Math.Ceiling((double)(totalServiceTime ?? 0));
            }
            return 0;
        }

        public decimal CalculateInvoiceTotalBill(Invoice? invoice)
        {
            if (invoice is not null && !invoice.IsDeleted)
            {
                var tax = (1 + invoice?.GSTax / 100 + invoice?.PSTax / 100);
                var totalServices = ((invoice?.ServicesAmount + invoice?.TipAmount));
                var totalBill = tax * totalServices;
                return Decimal.Round(totalBill??0, 2); 
            }
            return 0;
        }

        public int CountValidInvoiceDetailByStatus(Invoice? invoice, string status)
        {
            if (invoice is not null)
            {
                return invoice.InvoiceDetails.Where(id => !id.IsDeleted)
                                       .Count(id => CheckInvoieDetailStatus(id).status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }
            return 0;
        }


        public (string status, string emoji, int rank) CheckInvoieDetailStatus(InvoiceDetail? invoiceDetail)
        {
            if (invoiceDetail is not null && invoiceDetail.IsDeleted == false)
            {
                if (invoiceDetail.StartDatetime is null && invoiceDetail.EndDatetime is null)
                {
                    return ("Created", "🚩", 1);
                }
                else if ((invoiceDetail.StartDatetime is not null) && (invoiceDetail.EndDatetime is null))
                {
                    return ("Running", "🏁", 2);
                }
                else if (invoiceDetail.StartDatetime is not null && invoiceDetail.EndDatetime is not null)
                {
                    return ("Completed", "♻️", 3);
                }
            }
            return ("", "", 0);
        }



        public bool CheckAnyOptionalDataExist(string currentPath)
        {
            var paginationData = PaginationSelector(currentPath);
            if (paginationData.CheckAnyOptionalDataFromDictExist())
            {

                return true;
            }
            return false;   
        }

        public bool CheckElementOptionalDataExist(string currentPath, string key)
        {
            var paginationData = PaginationSelector(currentPath);
            if (paginationData.CheckElementOptionalDataFromDictExist(key))
            {
                return true;
            }
            return false;
        }


        public void SetSavePaginationPath(string currentPath)
        {
            if (!string.IsNullOrEmpty(currentPath) && !currentPath.Equals(this.savedPaginationPath,StringComparison.OrdinalIgnoreCase))
            {
                this.savedPaginationPath = currentPath;
            }
        }

        public string GetSavePaginaionPath()
        {

            if (subPathList.Keys.Any(path => path.Equals(this.savedPaginationPath, StringComparison.OrdinalIgnoreCase)))
            {
                //if (this.GetOptionalData(savedPaginationPath).Count >= 0 && this.GetOptionalData(savedPaginationPath).Keys.Contains("ObjectId"))
                //{
                //    var objectId = this.GetOptionalData(savedPaginationPath)["ObjectId"];
                //    return $"{savedPaginationPath}/{objectId}";
                //}

                if(this.CheckElementOptionalDataExist(savedPaginationPath, "ObjectId"))
                {
                    var objectId = this.GetElementOptionalData<int>(savedPaginationPath, "ObjectId");
                    return $"{savedPaginationPath}/{objectId}";
                }

            }

            return this.savedPaginationPath;
            
        }
        public void ResetPaginationData(string currentPath)
        {
            if ( !savedPaginationPath.Equals(currentPath, StringComparison.OrdinalIgnoreCase)
                    && allowedToResetPaginationDataPathList.Any(p => p.Equals(currentPath, StringComparison.OrdinalIgnoreCase)))
            {
                ResetDataFunction(savedPaginationPath);

                if (subPathList.Keys.Any(s => s.Equals(savedPaginationPath, StringComparison.OrdinalIgnoreCase))
                   && !subPathList[savedPaginationPath].Any(p =>p.Equals(currentPath, StringComparison.OrdinalIgnoreCase)))
                    {
                        subPathList[savedPaginationPath].ForEach(ResetDataFunction);
                    }
   
                this.savedPaginationPath = currentPath;
            }
        }
        
        public void ResetDataFunction(string path)
        {
            var paginationData = PaginationSelector(path);

            paginationData.ResetOptionalDataFromDict();
            paginationData.currentPage = 1;
            paginationData.totalPages = 1;
            paginationData.pageSize = 5;

            if (!string.IsNullOrWhiteSpace(paginationData.searchedText))
                paginationData.searchedText = string.Empty;

            if (paginationData.selectedRecordId != 0)
                paginationData.selectedRecordId = 0;
        } 


        public void SetPaginationData(string currentPath ,int currentPage, int totalPages, int pageSize, int? selectedRecordId=null, string? searchedText = null, Dictionary<String,Object?>? optionalData = null )
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
                //paginationData.optionalFilterDataDict = optionalData;
                paginationData.SetOptionalFilterDataDict(optionalData);
            }
        }

        public void SetOptionalData(string currentPath,string keyName, Object? value)
        {
            if (value is not null)
            {
                PaginationSelector(currentPath).SetOptionalDataFromDict(keyName, value);
            }
        }

        
        public T GetElementOptionalData<T>(string currentPath, string keyName)
        {

            var p = PaginationSelector(currentPath);

            return p.TryGetElementOptionalDataFromDict<T>(keyName, out var value)
                ? value
                : default!;

        }

        public Dictionary<string,Object?> GetOptionalData(string currentPath)
        {
            return PaginationSelector(currentPath).GetOptionalFilterDataDict();
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
        //public Dictionary<string, int> optionalFilterDataDict { get; set; } = new Dictionary<string, int>();

        private Dictionary<string, Object?> optionalFilterDataDict { get; set; } = new Dictionary<string, Object?>();

        public string searchedText { get; set; } = string.Empty;
        public int selectedRecordId { get; set; } = 0;  
        public int currentPage { get; set; } = 1;
        public int totalPages { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public PaginationData() { }
        public PaginationData( int currentPage, int totalPages, int pageSize, int selectedRecordId,string searchedText, Dictionary<string,Object?> optionalFilterDataDict)
        {
            this.currentPage = currentPage;
            this.totalPages = totalPages;
            this.pageSize = pageSize;
            this.selectedRecordId = selectedRecordId;
            this.searchedText = searchedText;
            this.optionalFilterDataDict = optionalFilterDataDict;   
        }

        public Dictionary<string, Object?> GetOptionalFilterDataDict()
        {
            if (CheckAnyOptionalDataFromDictExist())
            {
                return this.optionalFilterDataDict;

            }
            return new Dictionary<string, object?>();
        }

        public void SetOptionalFilterDataDict(Dictionary<string, Object?> optionalFilterDataDict)
        {
            this.optionalFilterDataDict = optionalFilterDataDict;
        }

        public void ResetOptionalDataFromDict()
        {
            if (this.CheckAnyOptionalDataFromDictExist())
            {
                // test Clear() method instead of Set each element null
                //this.optionalFilterDataDict.Clear();
                // 
                foreach (var key in this.optionalFilterDataDict.Keys.ToList())
                {
                    this.optionalFilterDataDict[key] = null;
                }
            }
        }

        public bool CheckElementOptionalDataFromDictExist(string key)
        {
            if (CheckAnyOptionalDataFromDictExist())
            {
                if (!String.IsNullOrEmpty(key) && this.optionalFilterDataDict.Keys.Contains(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckAnyOptionalDataFromDictExist()
        {
            if (this.optionalFilterDataDict != null && this.optionalFilterDataDict.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void SetOptionalDataFromDict<T>(string key, T value)
        {
            this.optionalFilterDataDict[key] = value;
        }
        public T GetElementOptionalDataFromDict<T>(string keyName, out T value)
        {
            if (this.optionalFilterDataDict.TryGetValue(keyName, out var obj) && obj is T t)
            {
                value = t;
                return value;
            }
            value = default!;
            return value;

        }
        public bool TryGetElementOptionalDataFromDict<T>(string keyName, out T value)
        {
            if (this.optionalFilterDataDict.TryGetValue(keyName, out var obj) && obj is T t)
            {
                value = t;
                return true;
            }
            value = default!;
            return false;
        }

    }
}
