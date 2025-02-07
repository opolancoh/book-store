namespace BookStore.Application.Features.Books.DTOs;

public class UpdateBookDto : CreateBookDto
{
    public Guid Id { get; set; }
}