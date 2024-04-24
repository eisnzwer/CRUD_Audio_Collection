using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Audio_Collection.Data;

public class GenreDataManager
{
    public static async Task GetAllGenres()
    {
        using (var context = new AppDbContext())
        {
            var genres = await context.Genres
                .Include(g => g.Tracks)
                .ToListAsync();

            foreach (var genre in genres)
            {
                Console.WriteLine($"Название жанра: {genre.Name}");

                Console.WriteLine("Треки, относящиеся к жанру:");
                foreach (var track in genre.Tracks)
                {
                    Console.WriteLine($"- {track.Name}");
                }
            }
        }
    }

    public static async Task AddNewGenre()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя жанра:");
            string name = Console.ReadLine();

            var existingGenre = await context.Genres
                .FirstOrDefaultAsync(g => g.Name == name);

            if (existingGenre == null)
            {
                var newGenre = new Genre
                {
                    Name = name
                };

                context.Genres.Add(newGenre);

                await context.SaveChangesAsync();
                Console.WriteLine("Жанр успешно добавлен.");
            }
            else
            {
                Console.WriteLine("Такой жанр уже существует.");
            }
        }
    }

    public static async Task ChangeGenreData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя жанра, данные которого вы хотите поменять: ");
            string name = Console.ReadLine();

            var genre = await context.Genres
                .FirstOrDefaultAsync(g => g.Name == name);

            if (genre == null)
            {
                Console.WriteLine("Жанр не найден");
            }

            else
            {
                Console.WriteLine("Введите новое имя жанра");
                string newName = Console.ReadLine();

                genre.Name = newName;
                Console.WriteLine("Данные успешно изменены");
            }
            
            await context.SaveChangesAsync();
        }
    }

    public static async Task DeleteGenreData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя жанра, который вы хотите удалить");
            string name = Console.ReadLine();

            var genre = await context.Genres
                .FirstOrDefaultAsync(g => g.Name == name);

            if (genre == null)
            {
                Console.WriteLine("Жанр не найден");
            }
            else
            {
                context.Remove(genre);
                await context.SaveChangesAsync();
                Console.WriteLine("Жанр успешно удален");
            }
        }
    }
}