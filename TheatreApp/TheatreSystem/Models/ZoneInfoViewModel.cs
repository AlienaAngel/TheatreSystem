using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class ZoneInfoViewModel
    {
        public string Name { get; set; }
        public int CountFree { get; set; }
        public int CountOrdered { get; set; }
        public List<Place> FreePlaces { get; set; }

        public ZoneInfoViewModel(string name, int countFree, int countOrdered, List<Place> freePlaces)
        {
            Name = name;
            CountFree = countFree;
            CountOrdered = countOrdered;
            FreePlaces = freePlaces;
        }
    }
}