namespace OnlineStoreManagement.Repositories.Interfaces
{
    /* Single Responsibility Principle (SRP) & Repository Pattern:
     Định nghĩa interface chung cho repository
     SRP nói rằng mỗi class chỉ nên có một lý do để thay đổi.Repository Pattern giúp chúng ta tách biệt logic truy cập dữ liệu ra khỏi business logic.
    */

    /*
     Interface Segregation Principle (ISP):

    ISP nói rằng không nên ép buộc client phải phụ thuộc vào các interface mà họ không sử dụng. 
     */
    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T> where T : class
    {
    }
}
