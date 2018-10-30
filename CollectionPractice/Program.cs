using System;
using System.Collections.Generic;

namespace CollectionPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //ARRAYS
            int[] numArray = new int[] {0,1,2,3,4,5,6,7,8,9};
            string[] strArray = new string[] {"Tim", "Martin", "Nikki", "Sara"};
            bool[] boolArray = new bool[] {true, false, true, false, true, false, true, false, true, false};
            
            //LISTS
            List<string> icecream = new List<string>();
            icecream.Add("Mint Chocolate Chip");
            icecream.Add("Strawberry Cheesecake");
            icecream.Add("Cookies n Cream");
            icecream.Add("Cookie Dough");
            icecream.Add("Rum Raisin");

            Console.WriteLine("We have {0} flavors of icecream", icecream.Count);
            Console.WriteLine("The 3rd most popular flavor is {0}", icecream[2]);
            icecream.RemoveAt(2);
            Console.WriteLine("And we're getting rid of it so our now we only have {0} flavors of icecream", icecream.Count);

            //DICTIONARIES
            Dictionary<string, string> names = new Dictionary<string, string>();
            names.Add("Tim", null);
            names.Add("Martin", null);
            names.Add("Nikki", null);
            names.Add("Sara", null);

            Random rand = new Random();
            names["Tim"] = icecream[rand.Next(4)];
            names["Martin"] = icecream[rand.Next(4)];
            names["Nikki"] = icecream[rand.Next(4)];
            names["Sara"] = icecream[rand.Next(4)];

            foreach (KeyValuePair<string, string> entry in names){
                Console.WriteLine(entry.Key + " - " + entry.Value);
            }
        }
    }
}
