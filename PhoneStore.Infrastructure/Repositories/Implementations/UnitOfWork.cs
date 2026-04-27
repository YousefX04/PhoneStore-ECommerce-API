using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Repositories.Interfaces;
using PhoneStore.Infrastructure.Persistence;

namespace PhoneStore.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Carts = new Repository<Cart>(_db);
            CartItems = new Repository<CartItem>(_db);
            Categories = new Repository<Category>(_db);
            Orders = new Repository<Order>(_db);
            OrderItems = new Repository<OrderItem>(_db);
            Products = new Repository<Product>(_db);
            Payments = new Repository<Payment>(_db);
        }
        public IRepository<Cart> Carts { get; }
        public IRepository<CartItem> CartItems { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<OrderItem> OrderItems { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Payment> Payments {  get; }



        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
