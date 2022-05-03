using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models
{
    public class Company: IComparable
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Grade1Avg { get; set; }
        public double Grade2Avg { get; set; }
        public double Grade3Avg { get; set; }
        public double Grade4Avg { get; set; }

        public double Grade5Avg { get; set; }
        public double TotalAvg { get; set; }

        public int CompareTo(object? obj)
        {
            Company otherCompany = obj as Company;
            
            return this.TotalAvg.CompareTo(otherCompany.TotalAvg);
        }
    }
}
