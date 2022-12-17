using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


    public class Chicken
    {
        private const int MinAge = 0;
        private const int MaxAge = 15;

        private string name;
        private int age;

        public Chicken(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name
        {
            get
            {return this.name;}

            internal set
            {
                if (value != null || value != string.Empty || value != " ")
                {
                    this.name = value;
                }
                else
                {
                    throw new ArgumentException("Name cannot be empty.");
                }
            }
        }

        public int Age
        {
            get
            {return this.age;}

            protected set
            {
                if (value > MinAge || value < MaxAge)
                {
                    this.age = value;
                }
                else
                {
                    throw new ArgumentException("Age should be between 0 and 15.");
                }
            }
        }

        public double GetProductPerDay()
        {
            return this.CalculateProductPerDay();
        }

        private double CalculateProductPerDay()
        {
            switch (this.Age)
            {
                case 0:
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                case 5:
                case 6:
                    return 2;
                case 7:
                case 8:
                case 9:
                case 10:
                    return 1;
                default:
                    return 0.5;
            }
        }
    }

    public class Program3
    {
        public static void Main(string[] args)
        {
            Type chickenType = typeof(Chicken);
            FieldInfo[] fields = chickenType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo[] methods = chickenType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(fields.Where(f => f.IsPrivate).Count() == 2);
            Debug.Assert(methods.Where(m => m.IsPrivate).Count() == 1);

            try
            {
                string name = Console.ReadLine();
                int age = int.Parse(Console.ReadLine());

                Chicken chicken = new Chicken(name, age);
                Console.WriteLine("Chicken {0} (age is {1}) can produce {2} eggs per day.",
                    chicken.Name, chicken.Age, chicken.GetProductPerDay());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }