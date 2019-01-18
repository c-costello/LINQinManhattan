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
            List<Neighborhoods> list = handleJSON(GetJSON("../../../data.json"));
            IEnumerable<Neighborhoods> noEmptyNames = FilterOutNoNames(list);
            IEnumerable<Neighborhoods> noDuplicated = RemoveDuplicates(noEmptyNames);
            IEnumerable<Neighborhoods> noDuplicatesOrEmptyNames = FilerNamesAndRemoveDupilcates();

            //Console.WriteLine("List: ");
            //Print(list);
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("List with Empty Names Removed: ");
            //Print(noEmptyNames);
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("List with Duplicates and Empty Names Removed: ");
            //Print(noDuplicated);
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("List with Duplicates and Empty Names Removed and Combined: ");
            //Print(noDuplicatesOrEmptyNames);
            //Console.WriteLine();
            //Console.WriteLine();

            Console.WriteLine("List with Empty Names Removed with Lamda Statement: ");
            Print(FilterOutNoNamesLamda(list));

        }
        /// <summary>
        /// Creates JObject 
        /// </summary>
        /// <param name="path">string</param>
        /// <returns>JObject</returns>
        public static JObject GetJSON(string path)
        {
            JObject json = JObject.Parse(File.ReadAllText(@path));
            return json;
        }
        /// <summary>
        /// converts Json object into a list of Neighborhood objects
        /// </summary>
        /// <returns>List\<Neighbords\></returns>
        public static List<Neighborhoods> handleJSON(JObject json)
        {
            var obj = json["features"];            
            List<Neighborhoods> neighbordhoods = new List<Neighborhoods>();
            foreach(var feature in obj)
            {
                Neighborhoods neigh = new Neighborhoods
                {
                    Neighborhood = (string)feature["properties"]["neighborhood"],
                    Zip = (string)feature["properties"]["zip"],
                    City = (string)feature["properties"]["city"],
                    State = (string)feature["properties"]["state"],
                    Borough = (string)feature["properties"]["borough"],
                    County = (string)feature["properties"]["county"],
                    Latitude = (double)feature["geometry"]["coordinates"][0],
                    Longitude = (double)feature["geometry"]["coordinates"][1],

                };
                neighbordhoods.Add(neigh);

            }
                return neighbordhoods;
        }


        /// <summary>
        /// Prints neighborhood names
        /// </summary>
        /// <param name="list">List</param>
        public static void Print(List<Neighborhoods> list)
        {
            foreach (Neighborhoods neighborhood in list)
            {

                Console.WriteLine(neighborhood.Neighborhood);

            }
        }
        /// <summary>
        /// Prints neighborhood name
        /// </summary>
        /// <param name="list">IEnumerable</param>
        public static void Print(IEnumerable<Neighborhoods> list)
        {
            foreach (Neighborhoods neighborhood in list)
            {

                Console.WriteLine(neighborhood.Neighborhood);

            }
        }

        /// <summary>
        /// Filters out neighborhoods that are not named
        /// </summary>
        /// <param name="list">List</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Neighborhoods> FilterOutNoNames(List<Neighborhoods> list)
        {
            IEnumerable<Neighborhoods> newList = from n in list
                          where n.Neighborhood.Length > 0
                          select n;

            return newList;
        }

        /// <summary>
        /// Removes neighborhoods with duplicate names
        /// </summary>
        /// <param name="list">Inumerable</param>
        /// <returns>Inumerable</returns>
        public static IEnumerable<Neighborhoods> RemoveDuplicates(IEnumerable<Neighborhoods> list)
        {
            String[] testString = new string[200];
            int counter = 0;
            foreach (Neighborhoods neigh in list)
            {
                if (!testString.Contains(neigh.Neighborhood))
                {
                    counter++;
                    testString[counter] = neigh.Neighborhood;
                }
                else
                {
                    neigh.Duplicate = true;
                }
            }
            IEnumerable<Neighborhoods> newList = from n in list
                                                 where n.Duplicate == false
                                                 select n;
            return newList;
        }

        /// <summary>
        /// Removes and filters neighborhoods
        /// </summary>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Neighborhoods> FilerNamesAndRemoveDupilcates()
        {
            List<Neighborhoods> list = handleJSON(GetJSON("../../../data.json"));
            String[] testString = new string[200];
            int counter = 0;
            foreach (Neighborhoods neigh in list)
            {
                if (!testString.Contains(neigh.Neighborhood))
                {
                    counter++;
                    testString[counter] = neigh.Neighborhood;
                }
                else
                {
                    neigh.Duplicate = true;
                }
            }
            IEnumerable<Neighborhoods> filteredList = list.Where(n => n.Neighborhood.Length > 0 && n.Duplicate == false);
            return filteredList;
        }

        /// <summary>
        /// Removes neighborhoods with no names using lamda 
        /// </summary>
        /// <param name="list">List</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Neighborhoods> FilterOutNoNamesLamda(List<Neighborhoods> list)
        {
            IEnumerable<Neighborhoods> newList = list.Where(n => n.Neighborhood.Length > 0);

            return newList;

        }
    }



}
