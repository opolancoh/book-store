using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId)
    {
        return await _dbSet
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Where(x => x.AuthorId == authorId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId)
    {
        return await _dbSet
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();
    }
    
    public async Task<Book?> GetBookWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Author)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}