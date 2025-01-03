using System.Numerics;
using UnityEngine;

namespace Helpers
{
    public static class Formulas
    {
        public static int GetPayWall(int level)
        {
            return Mathf.FloorToInt(Mathf.Pow(3, Mathf.Floor(level / 5f)));
        }
        
        public static BigInteger GetBuy(int level, int priceMultiplaier)
        {
            return BigInteger.Multiply(Mathf.CeilToInt(Mathf.Pow(level, 1.25f) * priceMultiplaier), GetPayWall(level));
        }
    }
}