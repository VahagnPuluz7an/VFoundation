using System.Numerics;

namespace Utils
{
    public static class MyStringFormt
    {
        public static string CoinToWords(this BigInteger value)
        {
            if (value < 1000)
                return value.ToString();
            
            if (value < 1000000)
            {
                float a = (float)(value / 100);
                a /= 10;
                return a.ToString("0.#") + "K";
            }

            if (value < 1000000000)
            {
                float a = (float)(value / 100000);
                a /= 10;
                return a.ToString("0.#") + "M";
            }

            if (value < 1000000000000)
            {
                float a = (float)(value / 100000000);
                a /= 10;
                return a.ToString("0.#") + "B";
            }

            if (value < 1000000000000000)
            {
                float a = (float)(value / 100000000000);
                a /= 10;
                return a.ToString("0.#") + "T";
            }

            return value.ToString("E"); 
        }
        
        public static string CoinToWordsWithSprite(this BigInteger value)
        {
            string a = CoinToWords(value);
            return a.AddZeroSprite();
        }
        
        public static string CoinToWords(this int value)
        {
            if (value < 1000)
                return value.ToString();
            
            if (value < 1000000)
            {
                float a = value / 100f;
                return a.ToString("0.#") + "K";
            }

            if (value < 1000000000)
            {
                float a = value / 100000f;
                return a.ToString("0.#") + "M";
            }
            
            return value.ToString("E"); 
        }
        
        public static string CoinToWordsWithSprite(this int value)
        {
            string a = CoinToWords(value);
            return a.AddZeroSprite();
        }
    }
}