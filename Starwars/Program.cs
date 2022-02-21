using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Starwars
{
    class Program
    {
        static List<Planet> planets = new List<Planet>();

        static void Main(string[] args)
        {
            planets = LoadData();

            Opgave1();
            Opgave2();
            Opgave3();
            Opgave4();
            Opgave5();
            Opgave6();
            Opgave7();
            Opgave8();
            Opgave9();
            Opgave10();
            Opgave11();
            Opgave12();
            Opgave13();
            Opgave14();
            Opgave15();
            Opgave16();

            Console.WriteLine("Exiting program...");
            Console.ReadKey();
        }

        private static void Opgave16()
        {
            Console.WriteLine("Opgave 16:");
            Regex rg = new Regex(@"(kk|ll|rr|nn)");

            // Conditional Query
            var result = from planet in planets
                         where rg.IsMatch(planet.Name)
                         orderby planet.Name descending
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => rg.IsMatch(planet.Name)).OrderByDescending(planet => planet.Name);
            WritePlanetsResult(result2, false);
        }

        private static void Opgave15()
        {
            Console.WriteLine("Opgave 15");
            Regex rg = new Regex(@"(aa|ee|ii|oo|uu|yy)");

            // Conditional Query
            var result = from planet in planets
                         where rg.IsMatch(planet.Name)
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => rg.IsMatch(planet.Name));
            WritePlanetsResult(result2, false);
        }

        private static void Opgave14()
        {
            Console.WriteLine("Opgave 14:");

            // Conditional Query
            var result = from planet in planets
                         where planet.Terrain != null
                         from terrian in planet.Terrain
                         where terrian.Contains("swamp")
                         orderby planet.Name
                         orderby planet.RotationPeriod
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => planet.Terrain != null)
                            .Where(planet => planet.Terrain.Any(y => y.Contains("swamp")))
                            .OrderBy(planet => planet.RotationPeriod)
                            .ThenBy(planet => planet.Name);
            WritePlanetsResult(result2, false);
        }

        private static void Opgave13()
        {
            Console.WriteLine("Opgave 13");

            // Conditional Query
            var result = from planet in planets
                         where planet.Terrain != null
                         from terrian in planet.Terrain
                         where terrian.Contains("desert")
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => planet.Terrain != null).Where(planet => planet.Terrain.Any(y => y.Contains("desert")));
            WritePlanetsResult(result2, false);
        }

        private static void Opgave12()
        {
            Console.WriteLine("Opgave 12:");

            // Condition Method
            var listA = from planet in planets
                        where planet.Name.StartsWith("a", StringComparison.OrdinalIgnoreCase) || planet.Name.EndsWith("s", StringComparison.OrdinalIgnoreCase)
                        select planet;
            var listB = from planet in planets
                        where planet.Terrain != null && planet.Terrain.Contains("rainforests")
                        select planet;
            var result = listA.Union(listB);
            WritePlanetsResult(result, true);

            // Method query
            var result2 = planets.Where(planet => planet.Name.StartsWith("a", StringComparison.OrdinalIgnoreCase) || planet.Name.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                            .Union(planets.Where(planet => planet.Terrain != null && planet.Terrain.Contains("rainforests")));
            WritePlanetsResult(result2, false);
        }

        private static void Opgave11()
        {
            Console.WriteLine("Opgave 11:");

            // Condition Query
            var temp1 = from planet in planets
                        where planet.RotationPeriod > 0
                        select planet;

            var result = planets.Except(temp1, new PlanetNameComparer());

            WritePlanetsResult(result, true);

            // Method Query
            var temp2 = planets.Where(x => x.RotationPeriod > 0); // making temp list only contains planets with rotations period larger then 0
            var temp3 = new List<Planet>(); // making temp list of planets and making every name start with lower case to confirm equals method override works
            foreach (var planet in planets)
            {
                var tempPlanet = planet;
                tempPlanet.Name.ToLower();
                temp3.Add(tempPlanet);
            }
            var result2 = temp3.Except(temp2, new PlanetNameComparer()); // result contains every planet that has unknown rotation period!
            WritePlanetsResult(result2, false);
        }

        private static void Opgave10()
        {
            Console.WriteLine("Opgave 10");

            // Conditional Query
            var result = from planet in planets
                         where planet.Diameter > 0 && planet.Population > 0
                         orderby (planet.Population / (4 * Math.PI * Math.Pow(planet.Diameter / 2, 2))) descending
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => planet.Diameter > 0).Where(planet => planet.Population > 0).OrderByDescending(x => x.Population / (4 * Math.PI * Math.Pow(x.Diameter / 2, 2)));
            WritePlanetsResult(result2, false);
        }

        private static void Opgave9()
        {
            Console.WriteLine("Opgave 9:");

            // Conditional Query
            var result = from planet in planets
                         where planet.SurfaceWater > 0
                         orderby planet.SurfaceWater descending
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(x => x.SurfaceWater > 0).OrderByDescending(x => x.SurfaceWater);
            WritePlanetsResult(result2, false);
        }

        private static void Opgave8()
        {
            Console.WriteLine("Opgave 8:");

            // Conditional Query
            var result = from planet in planets
                         where planet.RotationPeriod < 30 || planet.SurfaceWater > 50
                         where planet.Name.Contains("ba")
                         orderby planet.RotationPeriod
                         orderby planet.SurfaceWater
                         orderby planet.Name
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => planet.RotationPeriod < 30 || planet.SurfaceWater > 50)
                                 .Where(planet => planet.Name.Contains("ba"))
                                 .OrderBy(x => x.Name).ThenBy(x => x.SurfaceWater).ThenBy(x => x.RotationPeriod);
            WritePlanetsResult(result2, false);
        }

        private static void Opgave7()
        {
            Console.WriteLine("Opgave 7:");

            // Conditional Query
            var result = from planet in planets
                         where planet.RotationPeriod > 30
                         orderby planet.RotationPeriod
                         orderby planet.Name
                         select planet;
            WritePlanetsResult(result, true);

            // Method condition
            var result2 = planets.Where(planet => planet.RotationPeriod > 30).OrderBy(planet => planet.Name).ThenBy(planet => planet.RotationPeriod);
            WritePlanetsResult(result2, false);
            var result3 = planets.Where(planet => planet.RotationPeriod > 30).OrderBy(planet => planet.RotationPeriod).OrderBy(planet => planet.Name);
            // Same result as above, but uses the syntax of the conditional query, 
            // this is because conditional query "reads" from the back of the code,
            //so to simulate the same syntax you should first order by rotation and then by name.
            WritePlanetsResult(result3, false);
        }

        private static void Opgave6()
        {
            Console.WriteLine("Opgave 6:");

            // Condition Query
            var result = from planet in planets
                         where planet.RotationPeriod > 10
                         where planet.RotationPeriod < 20
                         orderby planet.Name
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(planet => planet.RotationPeriod > 10 && planet.RotationPeriod < 20).OrderBy(planet => planet.Name);
            WritePlanetsResult(result2, false);
        }

        private static void Opgave5()
        {
            Console.WriteLine("Opgave 5:");

            // Condition Query
            var result = from planet in planets
                         where planet.RotationPeriod > 40
                         orderby planet.RotationPeriod
                         select planet;
            WritePlanetsResult(result, true);

            // Method Qeury
            var result2 = planets.Where(planet => planet.RotationPeriod > 40).OrderBy(planet => planet.RotationPeriod);
            WritePlanetsResult(result2, false);
        }

        private static void Opgave4()
        {
            Console.WriteLine("Opgave 4:");

            // Condition Query
            var result = from planet in planets
                         where planet.Name.Substring(1, 1).Equals("a")
                         where planet.Name.Substring(planet.Name.Length - 1, 1).Equals("e")
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(x => x.Name.Substring(1, 1).Equals("a")
                            && x.Name.Substring(x.Name.Length - 1, 1).Equals("e"));
            WritePlanetsResult(result2, false);
        }

        private static void Opgave3()
        {
            Console.WriteLine("Opgave 3:");

            // Condition Query
            var result = from planet in planets
                         where planet.Name.Length > 9
                         where planet.Name.Length < 15
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(x => x.Name.Length > 9 && x.Name.Length < 15);
            WritePlanetsResult(result, false);
        }

        private static void Opgave2()
        {
            Console.WriteLine("Opgave 2:");

            // Condition Query
            var result = from planet in planets
                         where planet.Name.ToLower().Contains("y")
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var resutl2 = planets.Where(x => x.Name.ToLower().Contains("y"));
            WritePlanetsResult(resutl2, false);
        }


        private static void Opgave1()
        {
            Console.WriteLine("Opgave 1:");

            // Condition Query
            var result = from planet in planets
                         where planet.Name.StartsWith("M")
                         select planet;
            WritePlanetsResult(result, true);

            // Method Query
            var result2 = planets.Where(x => x.Name.StartsWith("M"));
            WritePlanetsResult(result, false);
        }

        private static void WritePlanetsResult(IEnumerable<Planet> result, bool state)
        {
            string type = string.Empty;
            if (state)
            {
                type = "Conditional Query: ";
            }
            else
            {
                type = "Method Query: ";
            }
            foreach (var planet in result)
            {
                Console.WriteLine(type + planet.Name);
            }
            Console.WriteLine();
        }

        static List<Planet> LoadData()
        {
            List<Planet> planets = new List<Planet>()
            {
                new Planet { Name="Corellia", Terrain= new List<string>{ "plains", "urban", "hills", "forests" },RotationPeriod=25,SurfaceWater=70, Diameter=11000, Population=3000000000},
                new Planet { Name="Rodia", Terrain= new List<string>{ "jungles", "oceans", "urban", "swamps" },RotationPeriod=29,SurfaceWater=60, Diameter=7549, Population=1300000000},
                new Planet { Name="Nal Hutta", Terrain= new List<string>{ "urban", "oceans", "bogs", "swamps" },RotationPeriod=87, Diameter=12150, Population=7000000000},
                new Planet { Name="Dantooine",Terrain= new List<string>{ "savannas", "oceans", "mountains", "grasslands" },RotationPeriod=25, Diameter=9830,Population=1000},
                new Planet { Name="Bestine IV",Terrain= new List<string>{ "rocky islands", "oceans" },RotationPeriod=26,SurfaceWater=98, Diameter=6400,Population=62000000},
                new Planet { Name="Ord Mantell",Terrain= new List<string>{ "plains", "seas","mesas" },RotationPeriod=26,SurfaceWater=10, Diameter=14050, Population=4000000000},
                new Planet { Name="Trandosha",Terrain= new List<string>{ "mountains", "seas","grasslands" ,"deserts"},RotationPeriod=25, Diameter=0, Population=42000000},
                new Planet { Name="Socorro", Terrain= new List<string>{ "mountains", "deserts"},RotationPeriod=20, Population=300000000},
                new Planet { Name="Mon Cala",Terrain= new List<string>{ "oceans", "reefs","islands"},RotationPeriod=21,SurfaceWater=100,Diameter=11030,Population=27000000000},
                new Planet { Name="Chandrila", Terrain= new List<string>{ "plains", "forests"},RotationPeriod=20,SurfaceWater=40,Diameter=13500,Population=1200000000},
                new Planet { Name="Sullust", Terrain= new List<string>{ "mountains", "volcanoes","rocky deserts"},RotationPeriod=20,SurfaceWater=5, Diameter=12780, Population=18500000000},
                new Planet { Name="Toydaria", Terrain= new List<string>{ "swamps", "lakes"},RotationPeriod=21, Diameter=7900, Population=11000000},
                new Planet { Name="Malastare",Terrain= new List<string>{ "swamps", "deserts","jungles","mountains"},RotationPeriod=26, Diameter=18880, Population=2000000000},
                new Planet { Name="Dathomir",Terrain= new List<string>{ "forests", "deserts","savannas"},RotationPeriod=24, Diameter=10480, Population=5200},
                new Planet { Name="Ryloth",Terrain= new List<string>{ "mountains", "valleys","deserts","tundra"},RotationPeriod=30,SurfaceWater=5,Diameter=10600, Population=1500000000 },
                new Planet { Name="Aleen Minor"},
                new Planet { Name="Vulpter",Terrain= new List<string>{ "urban", "barren"} ,RotationPeriod=22, Diameter=14900, Population=421000000},
                new Planet { Name="Troiken",Terrain= new List<string>{ "desert", "tundra","rainforests","mountains"} },
                new Planet { Name="Tund",Terrain= new List<string>{ "barren", "ash"} ,RotationPeriod=48, Diameter=12190},
                new Planet { Name="Haruun Kal",Terrain= new List<string>{ "toxic cloudsea", "plateaus","volcanoes"},RotationPeriod=25,Diameter=10120,Population=705300},
                new Planet { Name="Cerea",Terrain= new List<string>{ "verdant"},RotationPeriod=27,SurfaceWater=20,Population=450000000},
                new Planet { Name="Glee Anselm",Terrain= new List<string>{ "islands","lakes","swamps", "seas"},RotationPeriod=33,SurfaceWater=80, Diameter=15600,Population=500000000},
                new Planet { Name="Iridonia",Terrain= new List<string>{ "rocky canyons","acid pools"},RotationPeriod=29},
                new Planet { Name="Tholoth"},
                new Planet { Name="Iktotch",Terrain= new List<string>{ "rocky"},RotationPeriod=22},
                new Planet { Name="Quermia",},
                new Planet { Name="Dorin",RotationPeriod=22, Diameter=13400},
                new Planet { Name="Champala",Terrain= new List<string>{ "oceans","rainforests","plateaus"},RotationPeriod=27, Population=3500000000},
                new Planet { Name="Mirial",Terrain= new List<string>{ "deserts"}},
                new Planet { Name="Serenno",Terrain= new List<string>{ "rivers","rainforests","mountains"}},
                new Planet { Name="Concord Dawn",Terrain= new List<string>{ "jungles","forests","deserts"}},
                new Planet { Name="Zolan" },
                new Planet { Name="Ojom",Terrain= new List<string>{ "oceans","glaciers"},SurfaceWater=100, Population=500000000},
                new Planet { Name="Skako", Terrain = new List<string>{ "urban","vines"},RotationPeriod=27, Population=500000000000},
                new Planet { Name="Muunilinst",Terrain= new List<string>{ "plains","forests","hills","mountains"} ,RotationPeriod=28,SurfaceWater=25, Diameter=13800, Population=5000000000},
                new Planet { Name="Shili",Terrain= new List<string>{ "cities","savannahs","seas","plains"}},
                new Planet { Name="Kalee",Terrain= new List<string>{ "rainforests","cliffs","seas","canyons"},RotationPeriod=23, Diameter=13850, Population=4000000000},
                new Planet { Name="Umbara"},
                new Planet { Name="Tatooine",Terrain= new List<string>{ "deserts"},RotationPeriod=23,SurfaceWater=1, Diameter=10465, Population=200000 },
                new Planet { Name="Jakku",Terrain= new List<string>{ "deserts"}},
                new Planet { Name="Alderaan",Terrain= new List<string>{ "grasslands","mountains"},RotationPeriod=24,SurfaceWater=40, Diameter=12500, Population= 2000000000},
                new Planet { Name="Yavin IV", Terrain= new List<string>{ "rainforests","jungle"},RotationPeriod=24,SurfaceWater=8,Diameter=10200,Population=     1000},
                new Planet { Name="Hoth", Terrain= new List<string>{ "tundra","ice caves","mountain ranges"},RotationPeriod=23,SurfaceWater=100},
                new Planet { Name="Dagobah",Terrain= new List<string>{ "swamp","jungles"},RotationPeriod=23,SurfaceWater=8},
                new Planet { Name="Bespin",Terrain= new List<string>{ "gas giant"},RotationPeriod=12, Diameter=118000,Population=  6000000},
                new Planet { Name="Endor",Terrain= new List<string>{ "forests","mountains","lakes"},RotationPeriod=18,SurfaceWater=8, Diameter=4900, Population= 30000000},
                new Planet { Name="Naboo",Terrain= new List<string>{ "grassy hills","swamps","forests","mountains"},RotationPeriod=26,SurfaceWater=12, Diameter=12120, Population=  4500000000},
                new Planet { Name="Coruscant",Terrain= new List<string>{ "cityscape","mountains"},RotationPeriod=24,Diameter=12240,Population=1000000000000},
                new Planet { Name="Kamino",Terrain= new List<string>{ "ocean"},RotationPeriod=27,SurfaceWater=100,Diameter=19720, Population=1000000000},
                new Planet { Name="Geonosis",Terrain= new List<string>{ "rock","desert","mountain","barren"},RotationPeriod=30,SurfaceWater=5,Diameter=11370, Population=100000000000},
                new Planet { Name="Utapau",Terrain= new List<string>{ "scrublands","savanna","canyons","sinkholes"},RotationPeriod=27,SurfaceWater=0.9f,Diameter=12900,Population=  95000000},
                new Planet { Name="Mustafar",Terrain= new List<string>{ "volcanoes","lava rivers","mountains","caves"},RotationPeriod=36,  Diameter=4200, Population=20000},
                new Planet { Name="Kashyyyk",Terrain= new List<string>{ "jungle","forests","lakes","rivers"},RotationPeriod=26 ,SurfaceWater=60,Diameter=12765, Population=45000000},
                new Planet { Name="Polis Massa",Terrain= new List<string>{ "airless","asteroid"},RotationPeriod=24, Diameter=0, Population=1000000},
                new Planet { Name="Mygeeto",Terrain= new List<string>{ "glaciers","mountains","ice canyons"},RotationPeriod=12, Diameter=10088, Population=  19000000},
                new Planet { Name="Felucia",Terrain= new List<string>{ "fungus","forests"},RotationPeriod=34, Diameter=9100,Population=8500000},
                new Planet { Name="Cato Neimoidia",Terrain= new List<string>{ "mountains","fields","forests","rock arches"},RotationPeriod=25, Population=10000000},
                new Planet { Name="Saleucami",Terrain= new List<string>{ "caves", "deserts","mountains","volcanoes"},RotationPeriod=26, Population=1400000000, Diameter=14920},
                new Planet { Name="Stewjon",Terrain= new List<string>{ "grass"}},
                new Planet { Name="Eriadu",Terrain= new List<string>{ "cityscape"},RotationPeriod=24, Diameter=13490  , Population= 22000000000},
             };
            return planets;
        }
    }
}
