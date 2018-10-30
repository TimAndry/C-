using System;

namespace fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // for(int i = 1; i <=255; i++){
            //     Console.WriteLine(i);
            // }

            // for(int i = 1; i <= 100; i++){
            //     if(i%5==0 || i%3==0){
            //         Console.WriteLine(i);
            //     }    
            // }

            for(int i = 1; i<=100; i++){
                if(i%5==0 && i%3==0){
                    Console.WriteLine("FizzBuzz");
                }else if(i%3==0){
                    Console.WriteLine("Fizz");
                }else if(i%5==0){
                    Console.WriteLine("Buzz");
                }
            }
        }
    }
}
