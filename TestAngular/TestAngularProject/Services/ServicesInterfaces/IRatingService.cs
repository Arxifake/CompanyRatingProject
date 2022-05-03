using DTO.ModelViewsObjects;

namespace Services.ServicesInterfaces;

public interface IRatingService
{
    public RatingDto EditRate(string id);
    public void SaveRate(RatingDto rate);
}