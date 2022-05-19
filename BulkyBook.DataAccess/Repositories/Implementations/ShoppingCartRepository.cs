using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repositories.Interfaces;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories.Implementations
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int DecrementCount(ShoppingCart cart, int decrementValue)
        {
            cart.Count -= decrementValue;
            return cart.Count;
        }

        public int IncrementCount(ShoppingCart cart, int incrementValue)
        {
            cart.Count += incrementValue;
            return cart.Count;
        }
    }
}
