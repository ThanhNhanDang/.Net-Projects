using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Services.Interfaces;

namespace OnlineStoreManagement.Services.OderProcessing
{
    public class StandardOrderProcessing : IOrderProcessingStrategy
    {
        public async Task ProcessOrder(Order order)
        {
            // Xử lý đơn hàng tiêu chuẩn
            Console.WriteLine("Processing standard order");
        }
    }
}
