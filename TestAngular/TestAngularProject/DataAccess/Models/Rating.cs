using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models
{
    public class Rating
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int Grade3 { get; set; }
        public int Grade4 { get; set; }
        public int Grade5 { get; set; }
        public double Total { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }

    }
}
