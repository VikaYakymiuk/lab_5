using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Product
{
    private string name;
    private int price;

    public Product(string name, int price)
    {
        this.Name = name;
        this.Price = price;
    }

    public string Name
    {
        get { return this.name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            this.name = value;
        }
    }

    public int Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }
            price = value;
        }
    }
}
public class Person
{
    private string name;
    private int money;
    private List <Product> bag;

    public Person(string name, int money)
    {
        this.Name = name;
        this.Money = money;
        this.bag = new List<Product>();
    }

    public string Name
    {
        get { return this.name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            else
            {
                this.name = value;
            }
            
        }
    }

    public int Money
    {
        get { return this.money; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }
            else
            {
                this.money = value;
            }
        }
    }

    public void BuyProduct(Product product)
    {
        if (this.Money < product.Price)
        {
            throw new InvalidOperationException($"{Name} can't afford {Name}");
        }
        else
        {
            this.bag.Add(product);
            this.money -= product.Price;
        }
    }

    public IList<Product> GetProducts()
    {
        return this.bag.AsReadOnly();
    }
}

   

    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();
            var products = new List<Product>();
        Console.WriteLine("Enter the name: ");
            var allPeople = Console.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("Enter the products: ");
        var allProducts = Console.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("Enter the shopping list: ");

        try
            {
                foreach (var pair in allPeople)
                {
                    var personInfo = pair.Split('=');
                    var person = new Person(personInfo[0], int.Parse(personInfo[1]));
                    people.Add(person);
                }

                foreach (var pair in allProducts)
                {
                    var productInfo = pair.Split('=');
                    var product = new Product(productInfo[0], int.Parse(productInfo[1]));
                    products.Add(product);
                }

                string purchase;
                while ((purchase = Console.ReadLine()) != "END")
                {
                    var purchaseInfo = purchase.Split(' ');
                    var buyerName = purchaseInfo[0];
                    var productName = purchaseInfo[1];

                    var buyer = people.FirstOrDefault(b => b.Name == buyerName);
                    var productToBuy = products.FirstOrDefault(bp => bp.Name == productName);

                    try
                    {
                        buyer.BuyProduct(productToBuy);
                        Console.WriteLine($"{buyerName} = {productName}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                foreach (var person in people)
                {
                    var boughtProducts = person.GetProducts();
                    var result = boughtProducts.Any()
                        ? string.Join(", ", boughtProducts.Select(pr => pr.Name).ToList())
                        : "Nothing bought";
                    Console.WriteLine($"{person.Name} - {result}");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }