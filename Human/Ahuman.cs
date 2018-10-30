namespace human
{
    public class Human
    {
        //PROPERTIES
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Dexterity { get; set; }
        public int Health { get; set; }


        //CONSTRUCTORS
        public Human(string name, int strength, int intelligence, int dexterity, int health)
        {
            Name = name;
            Strength = strength;
            Intelligence = intelligence;
            Dexterity = dexterity;
            Health = health;
        }//This constructor is used for when we want to set parameters manually

        public Human(string name)
        {
            Name = name;
            Strength = 3;
            Intelligence = 3;
            Dexterity = 3;
            Health = 100;
        }//This constructor provides default parameters

        public void Attack(Human victim)
        {
            int attack = Strength * 5;
            victim.Health -= attack;
        }//function used by one human to attack another


        
    }
}