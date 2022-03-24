using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IAuthorsRepository
{
    public List<Author> AuthorList();
    public Author GetAuthorById(int id);
    public void NewAuthor(Author author);
}