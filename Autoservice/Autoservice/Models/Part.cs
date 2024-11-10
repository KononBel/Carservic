using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Models
{
    class Part
    {
        public Part(string name, int cost)
        {
            Name = name;
            Cost = cost;
            IsBroken = false;
        }

        public string Name { get; private set; }

        public int Cost { get; private set; }

        public bool IsBroken { get; private set; }

        public void BrokePart()
        {
            IsBroken = true;
        }
    }
}
