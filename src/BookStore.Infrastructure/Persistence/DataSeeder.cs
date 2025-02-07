using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Persistence;

public class DataSeeder
{
    // Hardcoded GUIDs for Categories
    private static class CategoryIds
    {
        public static readonly Guid Fiction = new("c0a80121-0000-0000-0000-000000000001");
        public static readonly Guid NonFiction = new("c0a80121-0000-0000-0000-000000000002");
        public static readonly Guid ScienceFiction = new("c0a80121-0000-0000-0000-000000000003");
        public static readonly Guid Mystery = new("c0a80121-0000-0000-0000-000000000004");
        public static readonly Guid Biography = new("c0a80121-0000-0000-0000-000000000005");
    }

    // Hardcoded GUIDs for Authors
    private static class AuthorIds
    {
        public static readonly Guid GeorgeOrwell = new("c0a80121-0000-0000-0001-000000000001");
        public static readonly Guid JKRowling = new("c0a80121-0000-0000-0001-000000000002");
        public static readonly Guid StephenKing = new("c0a80121-0000-0000-0001-000000000003");
        public static readonly Guid AgathaChristie = new("c0a80121-0000-0000-0001-000000000004");
        public static readonly Guid IsaacAsimov = new("c0a80121-0000-0000-0001-000000000005");
    }

    // Hardcoded GUIDs for Books
    private static class BookIds
    {
        public static readonly Guid Book1984 = new("c0a80121-0000-0000-0002-000000000001");
        public static readonly Guid AnimalFarm = new("c0a80121-0000-0000-0002-000000000002");
        public static readonly Guid HarryPotter1 = new("c0a80121-0000-0000-0002-000000000003");
        public static readonly Guid TheShining = new("c0a80121-0000-0000-0002-000000000004");
        public static readonly Guid MurderOrientExpress = new("c0a80121-0000-0000-0002-000000000005");
        public static readonly Guid Foundation = new("c0a80121-0000-0000-0002-000000000006");
        public static readonly Guid IRobot = new("c0a80121-0000-0000-0002-000000000007");
        public static readonly Guid DeathOnTheNile = new("c0a80121-0000-0000-0002-000000000008");
        public static readonly Guid TheDeadZone = new("c0a80121-0000-0000-0002-000000000009");
        public static readonly Guid HarryPotter2 = new("c0a80121-0000-0000-0002-000000000010");
        public static readonly Guid AndThenThereWereNone = new("c0a80121-0000-0000-0002-000000000011");
        public static readonly Guid RobotsOfDawn = new("c0a80121-0000-0000-0002-000000000012");
        public static readonly Guid PetSematary = new("c0a80121-0000-0000-0002-000000000013");
        public static readonly Guid HarryPotter3 = new("c0a80121-0000-0000-0002-000000000014");
        public static readonly Guid EndOfEternity = new("c0a80121-0000-0000-0002-000000000015");
    }

    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new()
                {
                    Id = CategoryIds.Fiction,
                    Name = "Fiction",
                    Description = "Fiction books",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = CategoryIds.NonFiction,
                    Name = "Non-Fiction",
                    Description = "Non-fiction books",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = CategoryIds.ScienceFiction,
                    Name = "Science Fiction",
                    Description = "Sci-fi books",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = CategoryIds.Mystery,
                    Name = "Mystery",
                    Description = "Mystery and thriller books",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = CategoryIds.Biography,
                    Name = "Biography",
                    Description = "Biographical books",
                    CreatedAt = DateTime.UtcNow
                }
            };
            await _context.Categories.AddRangeAsync(categories);
        }

        if (!_context.Authors.Any())
        {
            var authors = new List<Author>
            {
                new()
                {
                    Id = AuthorIds.GeorgeOrwell,
                    Name = "George Orwell",
                    Biography = "English novelist",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = AuthorIds.JKRowling,
                    Name = "J.K. Rowling",
                    Biography = "British author",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = AuthorIds.StephenKing,
                    Name = "Stephen King",
                    Biography = "American author of horror",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = AuthorIds.AgathaChristie,
                    Name = "Agatha Christie",
                    Biography = "Queen of Mystery",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = AuthorIds.IsaacAsimov,
                    Name = "Isaac Asimov",
                    Biography = "Science fiction master",
                    CreatedAt = DateTime.UtcNow
                }
            };
            await _context.Authors.AddRangeAsync(authors);
        }

        if (_context.ChangeTracker.HasChanges())
        {
            await _context.SaveChangesAsync();
        }

        if (!_context.Books.Any())
        {
            var books = new List<Book>
            {
                new()
                {
                    Id = BookIds.Book1984,
                    Title = "1984",
                    ISBN = "9780451524935",
                    Description = "Dystopian social science fiction",
                    Price = 19.99m,
                    PublicationYear = 1949,
                    AuthorId = AuthorIds.GeorgeOrwell,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.AnimalFarm,
                    Title = "Animal Farm",
                    ISBN = "9780452284241",
                    Description = "Political allegory",
                    Price = 14.99m,
                    PublicationYear = 1945,
                    AuthorId = AuthorIds.GeorgeOrwell,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.HarryPotter1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    ISBN = "9780747532699",
                    Description = "First book in the Harry Potter series",
                    Price = 24.99m,
                    PublicationYear = 1997,
                    AuthorId = AuthorIds.JKRowling,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.TheShining,
                    Title = "The Shining",
                    ISBN = "9780307743657",
                    Description = "Horror novel",
                    Price = 16.99m,
                    PublicationYear = 1977,
                    AuthorId = AuthorIds.StephenKing,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.MurderOrientExpress,
                    Title = "Murder on the Orient Express",
                    ISBN = "9780062693662",
                    Description = "Detective novel",
                    Price = 15.99m,
                    PublicationYear = 1934,
                    AuthorId = AuthorIds.AgathaChristie,
                    CategoryId = CategoryIds.Mystery,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.Foundation,
                    Title = "Foundation",
                    ISBN = "9780553293357",
                    Description = "First book of the Foundation series",
                    Price = 17.99m,
                    PublicationYear = 1951,
                    AuthorId = AuthorIds.IsaacAsimov,
                    CategoryId = CategoryIds.ScienceFiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.IRobot,
                    Title = "I, Robot",
                    ISBN = "9780553294385",
                    Description = "Collection of robot stories",
                    Price = 18.99m,
                    PublicationYear = 1950,
                    AuthorId = AuthorIds.IsaacAsimov,
                    CategoryId = CategoryIds.ScienceFiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.DeathOnTheNile,
                    Title = "Death on the Nile",
                    ISBN = "9780062073556",
                    Description = "Hercule Poirot mystery",
                    Price = 14.99m,
                    PublicationYear = 1937,
                    AuthorId = AuthorIds.AgathaChristie,
                    CategoryId = CategoryIds.Mystery,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.TheDeadZone,
                    Title = "The Dead Zone",
                    ISBN = "9780451155757",
                    Description = "Supernatural thriller",
                    Price = 15.99m,
                    PublicationYear = 1979,
                    AuthorId = AuthorIds.StephenKing,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.HarryPotter2,
                    Title = "Harry Potter and the Chamber of Secrets",
                    ISBN = "9780747538486",
                    Description = "Second book in the Harry Potter series",
                    Price = 24.99m,
                    PublicationYear = 1998,
                    AuthorId = AuthorIds.JKRowling,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.AndThenThereWereNone,
                    Title = "And Then There Were None",
                    ISBN = "9780062073470",
                    Description = "Mystery novel",
                    Price = 13.99m,
                    PublicationYear = 1939,
                    AuthorId = AuthorIds.AgathaChristie,
                    CategoryId = CategoryIds.Mystery,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.RobotsOfDawn,
                    Title = "The Robots of Dawn",
                    ISBN = "9780553299496",
                    Description = "Robot mystery novel",
                    Price = 16.99m,
                    PublicationYear = 1983,
                    AuthorId = AuthorIds.IsaacAsimov,
                    CategoryId = CategoryIds.ScienceFiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.PetSematary,
                    Title = "Pet Sematary",
                    ISBN = "9781476794341",
                    Description = "Horror novel",
                    Price = 17.99m,
                    PublicationYear = 1983,
                    AuthorId = AuthorIds.StephenKing,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.HarryPotter3,
                    Title = "Harry Potter and the Prisoner of Azkaban",
                    ISBN = "9780747542155",
                    Description = "Third book in the Harry Potter series",
                    Price = 24.99m,
                    PublicationYear = 1999,
                    AuthorId = AuthorIds.JKRowling,
                    CategoryId = CategoryIds.Fiction,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = BookIds.EndOfEternity,
                    Title = "The End of Eternity",
                    ISBN = "9780765319195",
                    Description = "Time travel novel",
                    Price = 18.99m,
                    PublicationYear = 1955,
                    AuthorId = AuthorIds.IsaacAsimov,
                    CategoryId = CategoryIds.ScienceFiction,
                    CreatedAt = DateTime.UtcNow
                }
            };
            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }
    }
}