using Lab3.Entity;
using Lab3.Repository;
using Lab3.Services;

namespace Lab3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ProductRepo _repo = new();
            ProductService _service = new();
            bool running = true;
            int userinput;
            while (running)
            {
                Console.WriteLine("=================================================");
                Console.WriteLine("1.Show specific product " +
                    "\n2.Show All products " +
                    "\n3.Add a product" +
                    "\n4.Update product" +
                    "\n5.Delete product" +
                    "\n0.Exist");

                do { Console.WriteLine("Choose from 0 to 4"); }
                while (!int.TryParse(Console.ReadLine(), out userinput));

                switch (userinput)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        Console.WriteLine("enter id of prefered item");
                        var item =await _repo.GetByIdAsync(int.Parse(Console.ReadLine()));
                        Console.WriteLine(item.ToString());
                        break;
                    case 2:
                        var prod = await _service.GetAllAsync();
                        foreach (var i in prod)
                        {
                            Console.WriteLine(i.ToString());
                        }
                        break;
                    case 3:
                        Console.WriteLine("enter product name then quantity then price");
                        product pr = new()
                        {
                            Name = Console.ReadLine(),
                            Quantity = int.Parse(Console.ReadLine()),
                            Price =int.Parse(Console.ReadLine())
                        };
                        await _service.AddAsync(pr);
                        break;
                    case 4:
                        Console.WriteLine("enter product id and then updated quantity then price");
                        product pr1 = new()
                        {
                            Id = int.Parse(Console.ReadLine()),
                            Quantity = int.Parse(Console.ReadLine()),
                            Price = int.Parse(Console.ReadLine())
                        };
                         _repo.UpdateAsync(pr1);
                        break;
                    case 5:
                        Console.WriteLine("enter product id u wish to delete");
                        await _repo.DeleteAsync(int.Parse(Console.ReadLine()));
                        break;
                    default:
                        Console.WriteLine("Wrong input choose from 0 to 4 ");
                        break;
                }

            }
        }
    }
}
