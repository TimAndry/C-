using System;

namespace human
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Tim = new Human("Tim");
            Human Poverty = new Human("Poverty");
            Tim.Attack(Poverty);
        }
    }
}
