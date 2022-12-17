using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PizzaDough {
    private string backingtechnic;
    private string flourtype;
    private double weigh;
    public const double white = 1.5;
    public const double wholegrain = 1.5;
    public const double crispy = 1;
    public const double chewy = 0.9;
    public const double homemade = 1;
    private List<Products> bag;
    public PizzaDough(string backingtechnic, string flourtype, double weigh)
    {
        this.Backingtechnic = backingtechnic;
        this.Flourtype = flourtype;
        this.Weigh = weigh;
        this.bag = new List<Products>();
    }
    public double Weigh
    {
        get { return this.weigh; }
        set
        {
            if (value >= 1 || value <=200)
            {
                this.Weigh = value;
            }
            else
                throw new ArgumentException("Dough weight should be in the range [1..200].");

        }
    }
    public string Flourtype
    {
        get { return this.flourtype; }
        set
        {
            if (value == "white" || value == "wholegrain")
            {
                this.Flourtype = value;
            }
            else
                throw new ArgumentException("Invalid type of dough.");

        }
    }
    public string Backingtechnic
    {
        get { return this.backingtechnic; }
        set
        {
            if (value == "chewy" || value == "crispy" || value == "homemade")
            {
                this.Backingtechnic = value;
            }
            else
                throw new ArgumentException("Invalid type of dough.");

        }
    }
   
}

class program5
{
    static void main()
    {
        var people = new List<Products>();
        Console.WriteLine("dough");
        var alldough = Console.ReadLine().Split(new[] { ';' });

        try
        {
            foreach (var pair in alldough)
            {
                var pizzaInfo = pair.Split(' ');
                var pizza = new PizzaDough(pizzaInfo[0], pizzaInfo[1], double.Parse(pizzaInfo[2]));
                people.Add(pizza);
            }

            string purchase;
            while ((purchase = Console.ReadLine()) != "END")
            {
                var purchaseInfo = purchase.Split(' ');
                var buyerName = purchaseInfo[0];

                var buyer = people.FirstOrDefault(b => b.Flourtype == buyerName);
                try
                {
                    Console.WriteLine($"{buyerName} bought {productName}");
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
                    ? string.Join(", ", boughtProducts.Select(pr => pr.Flourtype).ToList())
                    : "Nothing bought";
                Console.WriteLine($"{person.Flourtype} - {result}");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

    }

}

