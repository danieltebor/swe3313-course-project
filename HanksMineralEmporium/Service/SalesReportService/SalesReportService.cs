using HanksMineralEmporium.Shared.Util;

namespace HanksMineralEmporium.Service.SalesReportService
{
    public class SalesReportService
    {
        private readonly ISalesManager _salesManager;

        public SalesReportServiceService(ISalesManager salesManager)
        {
            _salesManager = salesManager;
        }


        public async Task<IReceipt> GetReceiptForSaleAsync(ulong saleId)
        {
            return await _salesManager.GetReceiptForSaleAsync(ulong saleId);            
        }
    }
}