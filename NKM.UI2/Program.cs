using NKM.BLL;
using NKM.DAL;
using NKM.DAL.NKM.DAL;
using NKM.Entities;
using System;


namespace NKM.ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        { 
            IProductRepository repository = new ProductRepository();   
            ProductService service = new ProductService(repository);

            while (true)
            {
                Console.WriteLine("\n1- Ürün Ekle");
                Console.WriteLine("2- Ürünleri Listele");
                Console.WriteLine("0- Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "1")
                {
                    Console.Write("Ürün adı: ");
                    string name = Console.ReadLine();
                    Console.Write("Fiyat: ");
                    decimal price = Convert.ToDecimal(Console.ReadLine());

                    try
                    {
                        ILogger logger = new Logger();
                        Product p = new Product { Name = name, Price = (double)price };
                        service.AddProduct(p);
                        Console.WriteLine("Ürün eklendi.");
                        try
                        {

                            service.AddProduct(p);
                            Console.WriteLine("Ürün eklendi.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Hata: {ex.Message}");
                            logger.Log(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hata: {ex.Message}");
                    }
                }
                else if (secim == "2")
                {
                    var products = service.GetAllProducts();
                    foreach (var p in products)
                    {
                        Console.WriteLine($"Ad: {p.Name}, Fiyat: {p.Price:C}");
                    }
                }
                else if (secim == "0")
                {
                    break;
                }
                

               



            }
        }
    }
}
