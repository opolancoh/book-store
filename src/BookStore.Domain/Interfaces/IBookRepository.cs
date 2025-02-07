using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId);
    Task<Book?> GetBookWithDetailsAsync(Guid id);
}