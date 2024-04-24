using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Audio_Collection.Data;

public class ArtistDataManager
{
    public static async Task GetAllArtists()
    {
        using (var context = new AppDbContext())
        {
            var artists = await context.Artists
                .Include(a => a.Tracks)
                .ToListAsync();

            foreach (var artist in artists)
            {
                Console.WriteLine(
                    $"Имя артиста: {artist.Name}, Год рождения: {artist.YearOfBirth}, Страна: {artist.Country} ");

                Console.WriteLine("Треки артиста: ");

                foreach (var track in artist.Tracks)
                {
                    Console.WriteLine($"- {track.Name}");
                }
            }
        }
    }

    public static async Task AddNewArtist()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя артиста:");
            string name = Console.ReadLine();

            var existingUser = await context.Artists.
                FirstOrDefaultAsync(a => a.Name == name);
            if (existingUser != null)
            {
                Console.WriteLine("Артист с таким именем уже существует.");
                return;
            }

            Console.WriteLine("Введите год рождения артиста");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите страну артиста");
            string country = Console.ReadLine();

            var newArtist = new Artist
            {
                Name = name,
                YearOfBirth = year,
                Country = country
            };

            context.Artists.Add(newArtist);

            await context.SaveChangesAsync();
            Console.WriteLine("Артист успешно добавлен");
        }
    }

    public static async Task ChangeArtistData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя артиста, данные которого вы хотите поменять: ");
            string name = Console.ReadLine();

            var artist = await context.Artists
                .FirstOrDefaultAsync(a => a.Name == name);

            if (artist == null)
            {
                Console.WriteLine("Артист не найден");
            }
            else
            {
                Console.WriteLine("Введите новое имя артиста:");
                string newName = Console.ReadLine();

                Console.WriteLine("Введите новый год рождения артиста: ");
                int newYear = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите новую страну артиста:");
                string country = Console.ReadLine();

                artist.Name = newName;
                artist.YearOfBirth = newYear;
                artist.Country = country;
                Console.WriteLine("Данные успешно изменены");
            }
            
            await context.SaveChangesAsync();
        }
    }

    public static async Task DeleteArtistData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя артиста, которого вы хотите удалить: ");
            string name = Console.ReadLine();

            var artist = await context.Artists
                .FirstOrDefaultAsync(a => a.Name == name);

            if (artist == null)
            {
                Console.WriteLine("Пользователь не найден");
            }
            else
            {
                context.Remove(artist);
                await context.SaveChangesAsync();
                Console.WriteLine("Пользователь успешно удален");
            }
        }
    }
}