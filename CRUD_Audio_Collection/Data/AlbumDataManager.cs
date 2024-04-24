using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Audio_Collection.Data;

public class AlbumDataManager
{
    public static async Task GetAllTracks()
    {
        using (var context = new AppDbContext())
        {
            var albums = await context.Albums
                .Include(a => a.Tracks)
                .ToListAsync();

            foreach (var album in albums)
            {
                Console.WriteLine($"Название альбома: {album.Name}, Дата выхода альбома: {album.Date}");

                Console.WriteLine("Треки в альбоме:");
                foreach (var track in album.Tracks)
                {
                    Console.WriteLine($"- {track.Name}");
                }
            }
        }
    }

    public static async Task AddNewAlbum()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите название нового альбома:");
            string albumName = Console.ReadLine();

            Console.WriteLine("Введите дату выхода нового альбома:");
            var releaseDate = DateOnly.Parse(Console.ReadLine());

            var newAlbum = new Album
            {
                Name = albumName,
                Date = releaseDate
            };

            Console.WriteLine("Введите имя артиста: ");
            string artistName = Console.ReadLine();
            var artist = await context.Artists.
                FirstOrDefaultAsync(a => a.Name == artistName);
            if (artist == null)
            {
                Console.WriteLine($"Артист с именем {artistName} не найден.");
                return;
            }

            Console.WriteLine("Введите название жанра: ");
            string genreName = Console.ReadLine();
            var genre = await context.Genres.
                FirstOrDefaultAsync(g => g.Name == genreName);
            if (genre == null)
            {
                Console.WriteLine($"Жанр с названием {genreName} не найден.");
                return;
            }

            Console.WriteLine("Сколько треков вы хотите добавить в этот альбом?");
            int trackCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < trackCount; i++)
            {
                Console.WriteLine($"Введите название трека {i + 1}:");
                string trackName = Console.ReadLine();

                Console.WriteLine($"Введите длину трека {i + 1} в секундах:");
                int trackLength = int.Parse(Console.ReadLine());

                var newTrack = new Track
                {
                    Name = trackName,
                    Length = trackLength,
                    ArtistId = artist.Id,
                    GenreId = genre.Id
                };

                newAlbum.Tracks.Add(newTrack);
            }

            context.Albums.Add(newAlbum);
            await context.SaveChangesAsync();

            Console.WriteLine("Новый альбом с треками успешно добавлен.");
        }
    }

    public static async Task ChangeAlbumData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя альбома, данные которого вы хотите поменять: ");
            string name = Console.ReadLine();

            var album = await context.Albums
                .FirstOrDefaultAsync(a => a.Name == name);

            if (album == null)
            {
                Console.WriteLine("Альбом не найден");
            }
            else
            {
                Console.WriteLine("Введите новое имя альбома: ");
                string newName = Console.ReadLine();
                
                Console.WriteLine("Введите новую дату альбома: ");
                DateOnly newDate = DateOnly.Parse(Console.ReadLine());

                album.Name = newName;
                album.Date = newDate;
                Console.WriteLine("Данные успешно изменены");
            }

            await context.SaveChangesAsync();
        }
    }

    public static async Task DeleteAlbumData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя альбома, который вы хотите удалить: ");
            string name = Console.ReadLine();

            var album = await context.Albums
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(a => a.Name == name);

            if (album == null)
            {
                Console.WriteLine("Альбом не найден");
            }
            else
            {
                context.Remove(album);
                await context.SaveChangesAsync();
                Console.WriteLine("Альбом и треки, входящие в него, успешно удален");
            }
        }
    }
}