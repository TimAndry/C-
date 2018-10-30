using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================
            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine("Artist from Mount Vernon");
            IEnumerable<Artist> artist  = Artists.Where(name => name.Hometown == "Mount Vernon");
            foreach( var name in artist){
                System.Console.WriteLine(name.ArtistName + ": " + name.Hometown);
            }

            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine("Youngest artist in our collection of artists");
            var youngest  = Artists.OrderByDescending(name => name.Age).Last();
            System.Console.WriteLine(youngest.ArtistName + ": " + youngest.Age);
            
            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine("3 oldest artists from Atlanta");
            var AtlArtists = Artists.OrderByDescending(name => name.Age).Take(3);
            foreach( var name in AtlArtists){
                System.Console.WriteLine(name.ArtistName + ": " + name.Age);
            }

            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine("All artists with 'William' somewhere in their real name.");
            var williams = Artists.Where(name => name.RealName.Contains("William"));
            foreach( var name in williams){
                System.Console.WriteLine(name.ArtistName + ": " + name.RealName);
            }
            
            
            //(Optional) Display the Group Name of all groups that have members that are not from New York City

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
	    Console.WriteLine(Groups.Count);
        }
    }
}
