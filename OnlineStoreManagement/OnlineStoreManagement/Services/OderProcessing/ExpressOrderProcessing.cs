using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Services.Interfaces;

namespace OnlineStoreManagement.Services.OderProcessing
{
    public class ExpressOrderProcessing : IOrderProcessingStrategy
    {
        public async Task ProcessOrder(Order order)
        {
            // Xử lý đơn hàng nhanh
            Console.WriteLine("Processing express order");
        }
    }
}
