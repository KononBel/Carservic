using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Models
{
    static class Parts
    {
        private static Dictionary<string, int> s_parts;

        static Parts()
        {
            s_parts = new Dictionary<string, int>();

            s_parts.Add("Колесо", 80);
            s_parts.Add("Фара", 105);
            s_parts.Add("Двигатель", 503);
            s_parts.Add("Коробка передач", 301);
            s_parts.Add("Привод", 107);
            s_parts.Add("Тормозной диск", 34);
            s_parts.Add("Радиатор охлаждения", 268);
            s_parts.Add("Стабилизатор устойчивости", 20);
            s_parts.Add("Сиденье", 51);
            s_parts.Add("Педали", 17);
        }

        public static List<Part> GetCarParts()
        {
            List<Part> newParts = new List<Part>();

            foreach (var part in s_parts)
                newParts.Add(new Part(part.Key, part.Value));

            return newParts;
        }

        public static List<Part> GetCarParts(int partCountEch)
        {
            List<Part> newParts = new List<Part>();

            foreach (var part in s_parts)
            {
                for (int i = 0; i < partCountEch; i++)
                    newParts.Add(new Part(part.Key, part.Value));
            }

            return newParts;
        }
    }
}
