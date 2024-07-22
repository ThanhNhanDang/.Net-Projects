using OnlineStoreManagement.Entities;

namespace OnlineStoreManagement.Services.Interfaces
{
    /*
     Open/Closed Principle (OCP) & Strategy Pattern:

    OCP nói rằng các entities nên mở cho việc mở rộng nhưng đóng cho việc sửa đổi.
    Strategy Pattern cho phép chúng ta định nghĩa một tập hợp các thuật toán,
    đóng gói từng cái, và làm cho chúng có thể thay thế cho nhau.
     */

    public interface IOrderProcessingStrategy
    {
        Task ProcessOrder(Order order);
    }
}
