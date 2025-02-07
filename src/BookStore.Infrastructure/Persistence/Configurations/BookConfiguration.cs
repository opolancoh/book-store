using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.ISBN)
            .IsRequired()
            .HasMaxLength(13);
            
        builder.Property(x => x.Description)
            .HasMaxLength(2000);
            
        builder.Property(x => x.Price)
            .HasPrecision(18, 2);
            
        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents the deletion of an author if it has associated books
            
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents the deletion of a category if it has associated books
    }
}