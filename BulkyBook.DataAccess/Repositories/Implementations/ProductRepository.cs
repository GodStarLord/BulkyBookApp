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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productFromDB = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            
            if (productFromDB != null)
            {
                productFromDB.Title = product.Title;
                productFromDB.Description = product.Description;
                productFromDB.ISBN = product.ISBN;
                productFromDB.Author = product.Author;
                productFromDB.ListPrice = product.ListPrice;
                productFromDB.Price100 = product.Price100;
                productFromDB.Price50 = product.Price50;
                productFromDB.Price = product.Price;
                productFromDB.CategoryId = product.CategoryId;
                productFromDB.CoverTypeId = product.CoverTypeId;

                if (!string.IsNullOrEmpty(product.ImageURL))
                {
                    productFromDB.ImageURL = product.ImageURL;
                }
            }
        }
    }
}
