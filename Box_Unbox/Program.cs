using System;
using System.Collections.Generic;

namespace Box_Unbox
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> rando = new List<object>();
            rando.Add(7);
            rando.Add(28);
            rando.Add(-1);
            rando.Add(true);
            rando.Add("chair");
            int sum = 0;
            for(var i = 0; i<rando.Count; i++){
                if(rando[i] is int){
                    Console.WriteLine(rando[i]);
                    int num = (int)rando[i];
                    sum += num;
                }
                if(rando[i] is bool){
                    Console.WriteLine(rando[i]);
                }
                if(rando[i] is string){
                    Console.WriteLine(rando[i]);
                }
            }
            Console.WriteLine("The sum of the numbers in the list is " + sum);
        }
    }
}
