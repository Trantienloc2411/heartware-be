using System;
using BusinessObjects.Entities;

namespace Repository.Implement;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<Discount> DiscountRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<OrderDetail> OrderDetailRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Review> ReviewRepository { get; }
    IGenericRepository<Role> RoleRepository { get; }
    IGenericRepository<Shipping> ShippingRepository { get; }
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<ProductDetail> ProductDetailRepository { get; }
    void Save();
   


}
