using DTO.ModelViewsObjects;

namespace Services.ServicesInterfaces;

public interface IRatingService
{
    public RatingDto EditRate(int id);
    public void SaveRate(RatingDto rate);
}