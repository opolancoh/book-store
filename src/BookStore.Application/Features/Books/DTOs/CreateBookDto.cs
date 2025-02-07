namespace BookStore.Application.Features.Books.DTOs;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PublicationYear { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}