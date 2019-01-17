using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LINQapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Neighborhoods> list = handleJSON();
            //Print(list);
            IEnumerable<Neighborhoods> noEmptyNames = FilterOutNoNames(list);
            //Print(noEmptyNames);
            IEnumerable<Neighborhoods> noDuplicated = RemoveDuplicates(noEmptyNames);
            Print(noDuplicated);
            
        }

        public static List<Neighborhoods> handleJSON()
        {
            JObject json = JObject.Parse(File.ReadAllText(@"../../../data.json"));

            List<Neighborhoods> neighbordhoods = new List<Neighborhoods>();
            for(int i = 0; i < 100; i++)
            {
                Neighborhoods neigh = new Neighborhoods
                {
                    Neighborhood = (string)json["features"][i]["properties"]["neighborhood"],
                    Zip = (string)json["features"][i]["properties"]["zip"],
                    City = (string)json["features"][i]["properties"]["city"],
                    State = (string)json["features"][i]["properties"]["state"],
                    Borough = (string)json["features"][i]["properties"]["borough"],
                    County = (string)json["features"][i]["properties"]["county"],
                    Latitude = (double)json["features"][i]["geometry"]["coordinates"][0],
                    Longitude = (double)json["features"][i]["geometry"]["coordinates"][1],

               };
                neighbordhoods.Add(neigh);

            }
                return neighbordhoods;
        }

        public static void Print(List<Neighborhoods> list)
        {
            foreach (Neighborhoods neighborhood in list)
            {

                Console.WriteLine(neighborhood.Neighborhood);
                //Console.WriteLine(neighborhood.Zip);
                //Console.WriteLine(neighborhood.State);
                //Console.WriteLine(neighborhood.City);
                //Console.WriteLine(neighborhood.Borough);
                //Console.WriteLine(neighborhood.County);
                //Console.WriteLine(neighborhood.Longitude);
                //Console.WriteLine(neighborhood.Latitude);
                Console.WriteLine();

            }
        }
        public static void Print(IEnumerable<Neighborhoods> list)
        {
            foreach (Neighborhoods neighborhood in list)
            {

                Console.WriteLine(neighborhood.Neighborhood);
                Console.WriteLine();

            }
        }

        public static IEnumerable<Neighborhoods> FilterOutNoNames(List<Neighborhoods> list)
        {
            IEnumerable<Neighborhoods> newList = from n in list
                          where n.Neighborhood.Length > 0
                          select n;

            return newList;
        }

        public static IEnumerable<Neighborhoods> RemoveDuplicates(IEnumerable<Neighborhoods> list)
        {
            String[] testString = new string[200];
            int counter = 0;
            foreach (Neighborhoods neigh in list)
            {
                if (!testString.Contains(neigh.Neighborhood))
                {
                    Console.WriteLine("weeee");
                    counter++;
                    testString[counter] = neigh.Neighborhood;
                }
                else
                {
                    neigh.Duplicate = true;
                    Console.WriteLine("boooo");
                }
            }
            IEnumerable<Neighborhoods> newList = from n in list
                                                 where n.Duplicate == false
                                                 select n;
            Print(newList);
            return newList;
        }
    }
}
