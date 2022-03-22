using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1
{
    public class Rating
    {
        public int Id { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int Grade3 { get; set; }
        public int Grade4 { get; set; }
        public int Grade5 { get; set; }
        public double Total { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

    }
}
