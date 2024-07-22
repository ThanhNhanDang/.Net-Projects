using OnlineStoreManagement.Services.Interfaces;

namespace OnlineStoreManagement.Services.OderProcessing
{
    /*
    OrderProcessingStrategyFactory cho phép chúng ta tạo các instance của IOrderProcessingStrategy
    mà không cần biết chi tiết về cách chúng được implement.
     */
    public class OrderProcessingStrategyFactory
    {
        public IOrderProcessingStrategy CreateStrategy(string orderType)
        {
            switch (orderType.ToLower())
            {
                case "standard":
                    return new StandardOrderProcessing();
                case "express":
                    return new ExpressOrderProcessing();
                default:
                    throw new ArgumentException("Invalid order type");
            }
        }
    }
}
