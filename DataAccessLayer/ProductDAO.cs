using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{


    public class ProductDAO
    {
        private static List<Product> listProducts;

        public ProductDAO()
        {
            Product chai = new Product(1, "Chai", 3, 12, 18);
            Product chang = new Product(2, "Chang", 19, 17, 40);
            Product aniseed = new Product(3, "Aniseed Syrup", 10, 13, 70);
            listProducts = new List<Product> { chai, chang, aniseed };
        }

        public static List<Product> GetProducts()
        {
            return listProducts;
        }

        //public static List<Product> GetProducts()
        //{ 
        //    var listProducts = new List<Product>();
        //    try
        //    {
        //        using var db = new MyStoreContext();
        //        listProducts = db.Products.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return listProducts;
        //}

        public static void SaveProduct(Product p)
        {
            listProducts.Add(p);
        }

        public static void UpdateProduct(Product product)
        {
            foreach (Product p in listProducts.ToList())
            {
                if(p.ProductId == product.ProductId)
                {
                    p.ProductName = product.ProductName;
                    p.CategoryId = product.CategoryId;
                    p.UnitInStock = product.UnitInStock;
                    p.UnitPrice = product.UnitPrice;
                    p.CategoryId = product.CategoryId;
                }
            }
        }

        public static void DeleteProduct(Product product)
        {
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    listProducts.Remove(p);
                }
            }
        }

        public static Product GetProductById(int id)
        {
            foreach (Product p in listProducts.ToList())
            {
                if(p.ProductId == id)
                {
                    return p;
                }
            }
            return null;
        }
    }
}
