using NKM.DAL;
using NKM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKM.DAL.NKM.DAL;

namespace NKM.BLL
{
    public class ProductService
    {
        public IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
       

        public void AddProduct(Product p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
                throw new Exception("Ürün adı boş olamaz.");
            if (p.Price <= 0)
                throw new Exception("Fiyat 0'dan büyük olmalı.");

            _repository.Add(p);
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }
    }
}
