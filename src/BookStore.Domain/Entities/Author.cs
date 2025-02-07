namespace BookStore.Domain.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    
    public ICollection<Book> Books { get; set; } = new List<Book>();
}