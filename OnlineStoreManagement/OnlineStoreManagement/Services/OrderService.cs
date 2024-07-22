using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Services.Interfaces;

namespace OnlineStoreManagement.Services
{
    public class OrderService
    {
        private readonly IOrderProcessingStrategy _processingStrategy;

        public OrderService(IOrderProcessingStrategy processingStrategy)
        {
            _processingStrategy = processingStrategy;
        }

        public async Task ProcessOrder(Order order)
        {
            await _processingStrategy.ProcessOrder(order);
        }
    }
}
