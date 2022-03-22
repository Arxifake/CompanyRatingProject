namespace Task1.Interfaces
{
    public interface IRaitings
    {
        public List<Rating> getCompanyRaitings(int id);
        public string AddRate(Rating rating);

    }
}
