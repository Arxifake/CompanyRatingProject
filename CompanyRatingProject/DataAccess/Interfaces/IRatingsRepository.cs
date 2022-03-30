using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IRatingsRepository
    {
        public List<Rating> GetRatingsByCompanyId(int id);
        public void AddRate(Rating rating);
        public Rating GetRatingByAuthorId(int id,int companyId);
        public Rating GetRatingById(int id);
        public void ChangeRate(Rating rate);

    }
}
