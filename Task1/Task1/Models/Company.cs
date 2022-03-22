using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1
{
    public class Company: IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Grade1AVG { get; set; }
        public double Grade2AVG { get; set; }
        public double Grade3AVG { get; set; }
        public double Grade4AVG { get; set; }

        public double Grade5AVG { get; set; }
        public double TotalAVG { get; set; }
        public ICollection<Rating> Ratings { get; set; }


        public int CompareTo(object? obj)
        {
            Company otherCompany = obj as Company;
            
            return this.TotalAVG.CompareTo(otherCompany.TotalAVG);
        }
    }
}
