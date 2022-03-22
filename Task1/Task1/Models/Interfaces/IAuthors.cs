namespace Task1.Models.Interfaces;

public interface IAuthors
{
    public List<Author> AuthorList();
    public Author getAuthor(int id);
    public void NewAuthor(Author author);
}