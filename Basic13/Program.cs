using System;

namespace Basic13
{
    class Program
    {
        public int MyProperty { get; set; }
        public static void PrintNum(){
            for(int i = 1; i<256; i++){
                Console.WriteLine(i);
            }
        }

        public static void PrintOdd(){
            for(int i = 1; i<256; i+=2){
                Console.WriteLine(i);
            }
        }

        public static void PrintSum(){
            int sum = 0;
            for(int i = 0; i<256; i++){
                sum+= i;
                Console.WriteLine("Number: " + i + ", Sum: " + sum);
            }
        }

        public static void LoopArray(int[] x){
            for(int i = 0; i<x.Length; i++){
                Console.WriteLine(x[i]);
            }
        }

        public static void FindMax(int[] x){
            int max = x[0];
            for(int i = 0; i<x.Length; i++){
                if(x[i] > max){
                    max = x[i];
                }
            }
            Console.WriteLine("The max value is " + max);
        }

        public static void GetAverage(int[] x){
            float sum = 0;
            float length = x.Length;
            for(int i = 0; i<x.Length; i++){
                sum+=x[i];
                Console.WriteLine(sum);
            }
            float avg = sum/length;
            Console.WriteLine(sum + ", " + length + ", " + avg);
        }
        
        static void Main(string[] args){
            //PrintNum();
            //PrintOdd();
            //PrintSum();
            int[] x = new int[] {1,3,16,4,9,14};
            //LoopArray(x);
            //FindMax(x);
            //GetAverage(x);
        }

        
    }
}
