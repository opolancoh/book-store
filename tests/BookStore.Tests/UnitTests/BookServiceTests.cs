using AutoMapper;
using BookStore.Application.Features.Books.DTOs;
using BookStore.Application.Features.Books.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Moq;

namespace BookStore.Tests.UnitTests;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockMapper = new Mock<IMapper>();
        _bookService = new BookService(_mockBookRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateBookAsync_ValidDto_ReturnsCreatedBookDto()
    {
        // Arrange
        var createBookDto = new CreateBookDto();
        var expectedBookDto = new BookDto();
        var book = new Book();

        _mockMapper.Setup(x => x.Map<Book>(createBookDto))
            .Returns(book);
        _mockMapper.Setup(x => x.Map<BookDto>(book))
            .Returns(expectedBookDto);

        // Act
        var result = await _bookService.CreateBookAsync(createBookDto);

        // Assert
        Assert.NotNull(result);
        _mockBookRepository.Verify(x => x.AddAsync(book), Times.Once);
        _mockMapper.Verify(x => x.Map<Book>(createBookDto), Times.Once);
        _mockMapper.Verify(x => x.Map<BookDto>(book), Times.Once);
    }
    
    [Fact]
    public async Task GetAllBooksAsync_ReturnsAllBooks()
    {
        // Arrange
        var books = new List<Book> { new Book(), new Book() };
        var expectedBookDtos = new List<BookDto> { new BookDto(), new BookDto() };

        _mockBookRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(books);
        _mockMapper.Setup(x => x.Map<IEnumerable<BookDto>>(books))
            .Returns(expectedBookDtos);

        // Act
        var result = await _bookService.GetAllBooksAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBookDtos.Count, result.Count());
        _mockBookRepository.Verify(x => x.GetAllAsync(), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<BookDto>>(books), Times.Once);
    }

    [Fact]
    public async Task GetBookByIdAsync_ExistingId_ReturnsBookDto()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var book = new Book { Id = bookId };
        var expectedBookDto = new BookDto { Id = bookId };

        _mockBookRepository.Setup(x => x.GetBookWithDetailsAsync(bookId))
            .ReturnsAsync(book);
        _mockMapper.Setup(x => x.Map<BookDto>(book))
            .Returns(expectedBookDto);

        // Act
        var result = await _bookService.GetBookByIdAsync(bookId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookId, result.Id);
        _mockBookRepository.Verify(x => x.GetBookWithDetailsAsync(bookId), Times.Once);
        _mockMapper.Verify(x => x.Map<BookDto>(book), Times.Once);
    }

    [Fact]
    public async Task GetBookByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _mockBookRepository.Setup(x => x.GetBookWithDetailsAsync(bookId))
            .ReturnsAsync((Book)null);

        // Act
        var result = await _bookService.GetBookByIdAsync(bookId);

        // Assert
        Assert.Null(result);
        _mockBookRepository.Verify(x => x.GetBookWithDetailsAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task UpdateBookAsync_ExistingBook_UpdatesSuccessfully()
    {
        // Arrange
        var updateBookDto = new UpdateBookDto { Id = Guid.NewGuid() };
        var existingBook = new Book { Id = updateBookDto.Id };

        _mockBookRepository.Setup(x => x.GetByIdAsync(updateBookDto.Id))
            .ReturnsAsync(existingBook);

        // Act
        await _bookService.UpdateBookAsync(updateBookDto);

        // Assert
        _mockBookRepository.Verify(x => x.GetByIdAsync(updateBookDto.Id), Times.Once);
        _mockMapper.Verify(x => x.Map(updateBookDto, existingBook), Times.Once);
        _mockBookRepository.Verify(x => x.UpdateAsync(existingBook), Times.Once);
    }

    [Fact]
    public async Task UpdateBookAsync_NonExistingBook_ThrowsKeyNotFoundException()
    {
        // Arrange
        var updateBookDto = new UpdateBookDto { Id = Guid.NewGuid() };

        _mockBookRepository.Setup(x => x.GetByIdAsync(updateBookDto.Id))
            .ReturnsAsync((Book)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _bookService.UpdateBookAsync(updateBookDto));
        _mockBookRepository.Verify(x => x.GetByIdAsync(updateBookDto.Id), Times.Once);
        _mockBookRepository.Verify(x => x.UpdateAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task DeleteBookAsync_ExistingBook_DeletesSuccessfully()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var existingBook = new Book { Id = bookId };

        _mockBookRepository.Setup(x => x.GetByIdAsync(bookId))
            .ReturnsAsync(existingBook);

        // Act
        await _bookService.DeleteBookAsync(bookId);

        // Assert
        _mockBookRepository.Verify(x => x.GetByIdAsync(bookId), Times.Once);
        _mockBookRepository.Verify(x => x.DeleteAsync(bookId), Times.Once);
    }

    [Fact]
    public async Task DeleteBookAsync_NonExistingBook_ThrowsKeyNotFoundException()
    {
        // Arrange
        var bookId = Guid.NewGuid();

        _mockBookRepository.Setup(x => x.GetByIdAsync(bookId))
            .ReturnsAsync((Book)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _bookService.DeleteBookAsync(bookId));
        _mockBookRepository.Verify(x => x.GetByIdAsync(bookId), Times.Once);
        _mockBookRepository.Verify(x => x.DeleteAsync(bookId), Times.Never);
    }

    [Fact]
    public async Task GetBooksByAuthorAsync_ReturnsAuthorBooks()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var books = new List<Book> { new Book(), new Book() };
        var expectedBookDtos = new List<BookDto> { new BookDto(), new BookDto() };

        _mockBookRepository.Setup(x => x.GetBooksByAuthorAsync(authorId))
            .ReturnsAsync(books);
        _mockMapper.Setup(x => x.Map<IEnumerable<BookDto>>(books))
            .Returns(expectedBookDtos);

        // Act
        var result = await _bookService.GetBooksByAuthorAsync(authorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBookDtos.Count, result.Count());
        _mockBookRepository.Verify(x => x.GetBooksByAuthorAsync(authorId), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<BookDto>>(books), Times.Once);
    }

    [Fact]
    public async Task GetBooksByCategoryAsync_ReturnsCategoryBooks()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var books = new List<Book> { new Book(), new Book() };
        var expectedBookDtos = new List<BookDto> { new BookDto(), new BookDto() };

        _mockBookRepository.Setup(x => x.GetBooksByCategoryAsync(categoryId))
            .ReturnsAsync(books);
        _mockMapper.Setup(x => x.Map<IEnumerable<BookDto>>(books))
            .Returns(expectedBookDtos);

        // Act
        var result = await _bookService.GetBooksByCategoryAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBookDtos.Count, result.Count());
        _mockBookRepository.Verify(x => x.GetBooksByCategoryAsync(categoryId), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<BookDto>>(books), Times.Once);
    }
}