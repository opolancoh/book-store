using AutoMapper;
using BookStore.Application.Features.Books.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Features.Books.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetBookWithDetailsAsync(id);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
    {
        var book = _mapper.Map<Book>(createBookDto);
        await _bookRepository.AddAsync(book);

        return _mapper.Map<BookDto>(book);
    }

    public async Task UpdateBookAsync(UpdateBookDto updateBookDto)
    {
        var book = await _bookRepository.GetByIdAsync(updateBookDto.Id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {updateBookDto.Id} not found");
        }

        _mapper.Map(updateBookDto, book);
        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found");
        }

        await _bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId)
    {
        var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(Guid categoryId)
    {
        var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }
}