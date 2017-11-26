using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Repository
{
    public class ProductRepository
    {
        public ApplicationDbContext Db;

        public List<Product> GetList()
        {
            using (Db = new ApplicationDbContext())
            {
                return Db.Products.ToList();
            }
        }

        public Product Create(Product item)
        {
            using (Db = new ApplicationDbContext())
            {
                var product = Db.Set<Product>().Add(item);
                Save();
                return product;
            }
        }

        public bool Delete(int id)
        {
            using (Db = new ApplicationDbContext())
            {
                var item = Db.Set<Product>().Find(id);
                if (item != null)
                {
                    Db.Set<Product>().Remove(item);
                    Save();
                }
                return item != null;
            }
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}