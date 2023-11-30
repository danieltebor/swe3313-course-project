using HanksMineralEmporium.Shared.Util;
using HanksMineralEmporium.Service.;

namespace HanksMineralEmporium.Service.SalesReportService
{
    public interface ISalesReportService
    {
        public Task GetReceiptForSaleAsync(string username, string itemId)
    }
}