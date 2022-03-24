using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IRatingsRepository
    {
        public List<Rating> GetRatingsByCompanyId(int id);
        public string AddRate(Rating rating);
        public Rating GetRatingByAuthorId(int id);

    }
}
