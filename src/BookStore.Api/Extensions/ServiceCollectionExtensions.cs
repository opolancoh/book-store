using BookStore.Application.Features.Books.Mappings;
using BookStore.Application.Features.Books.Services;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddAutoMapper(typeof(BookMappingProfile));
        services.AddScoped<DataSeeder>();

        return services;
    }
}