using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::NKM.Entities;

namespace NKM.DAL
{
    
   

    namespace NKM.DAL
    {
        public interface IProductRepository
        {
            void Add(Product p);
            List<Product> GetAll();
        }
    }

}
