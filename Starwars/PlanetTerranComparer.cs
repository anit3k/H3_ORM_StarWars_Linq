//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Starwars
//{
//    public class PlanetTerranComparer : IEqualityComparer<Planet>
//    {
//        public bool Equals(Planet x, Planet y)
//        {
//            if (string.Equals(x.Terrain, y.Terrain, StringComparison.OrdinalIgnoreCase))
//            {
//                return true;
//            }
//            return false;
//        }

//        public int GetHashCode(Planet obj)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
