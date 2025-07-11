using NKM.DAL.NKM.DAL;
using NKM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKM.DAL
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = new List<Product>();

        public void Add(Product p)
        {
            _products.Add(p);
        }

        public List<Product> GetAll()
        {
            return _products;
        }
    }
}
