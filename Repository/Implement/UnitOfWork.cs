using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MyDbContext context;

        private GenericRepositiory<Category> categoryRepository;
        private GenericRepositiory<Product> productRepository;
        private GenericRepositiory<OrderDetail> orderDetailRepository;
        private GenericRepositiory<Discount> discountRepository;
        private GenericRepositiory<Order> orderRepository;
        private GenericRepositiory<ProductDetail> productDetailRepository;
        private GenericRepositiory<Review> reviewRepository;
        private GenericRepositiory<Role> roleRepository;
        private GenericRepositiory<Shipping> shippinglRepository;
        private GenericRepositiory<User> userRepository;

    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable IDE0290 // Use primary constructor
    public UnitOfWork(MyDbContext _context){
        context = _context;
    }
#pragma warning restore IDE0290 // Use primary constructor
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.



    


    public IGenericRepository<Category> CategoryRepository {
        get{
            return categoryRepository ??= new GenericRepositiory<Category>(context);
        }
    }

    public IGenericRepository<Discount> DiscountRepository {
        get{
            return discountRepository ??= new GenericRepositiory<Discount>(context);
        }
    }

    public IGenericRepository<Order> OrderRepository {
        get{
            return orderRepository ??= new GenericRepositiory<Order>(context);
        }
    }

    public IGenericRepository<OrderDetail> OrderDetailRepository {
        get{
            return orderDetailRepository ??= new GenericRepositiory<OrderDetail>(context);
        }
    }

    public IGenericRepository<Product> ProductRepository {
        get{
            return  productRepository ??= new GenericRepositiory<Product>(context);
        }
    }

    public IGenericRepository<Review> ReviewRepository {
        get{
            return  reviewRepository ??= new GenericRepositiory<Review>(context);
        }
    }

    public IGenericRepository<Role> RoleRepository {
        get{
            return roleRepository  ??= new GenericRepositiory<Role>(context);   
        }
    }

    public IGenericRepository<Shipping> ShippingRepository {
        get{
            return shippinglRepository ??= new GenericRepositiory<Shipping>(context);
        }
    }

    public IGenericRepository<User> UserRepository {
        get{
            return userRepository ??= new GenericRepositiory<User>(context);
        }
    }

    public IGenericRepository<ProductDetail> ProductDetailRepository {
        get{
            return productDetailRepository ??= new GenericRepositiory<ProductDetail>(context);
        }
    }

    public void Save()
        {
            var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(e => e != ValidationResult.Success)
                .ToArray();
            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(Environment.NewLine,
                    validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
                throw new Exception(exceptionMessage);
            }
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
}   
