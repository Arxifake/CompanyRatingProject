using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IRatingsRepository
    {
        public List<Rating> GetRatingsByCompanyId(string id);
        public void AddRate(Rating rating);
        public Rating GetRatingById(string id);
        public void ChangeRate(Rating rate);

    }
}
