using BookStore.Application.Features.Books.DTOs;

namespace BookStore.Application.Features.Books.Services;

public interface IBookService
{
    Task<BookDto?> GetBookByIdAsync(Guid id);
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
    Task UpdateBookAsync(UpdateBookDto updateBookDto);
    Task DeleteBookAsync(Guid id);
    Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId);
    Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(Guid categoryId);
}