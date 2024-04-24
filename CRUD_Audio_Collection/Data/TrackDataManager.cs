using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Audio_Collection.Data;

public class TrackDataManager
{
    public static async Task GetAllTracks()
    {
        using (var context = new AppDbContext())
        {
            var tracks = await context.Tracks
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .Include(t => t.Genre)
                .ToListAsync();

            foreach (var track in tracks)
            {
                Console.WriteLine(
                    $"Трек: {track.Name}, Артист: {track.Artist.Name}, Альбом: {track.Album.Name}, Жанр: {track.Genre.Name}");
            }
        }
    }

    public static async Task AddNewTrack()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя трека:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите длину трека:");
            int length = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите имя артиста:");
            string artistName = Console.ReadLine();
            var artist = await context.Artists
                .FirstOrDefaultAsync(a => a.Name == artistName);

            if (artist == null)
            {
                Console.WriteLine("Артист не найден");
                return;
            }

            Console.WriteLine("Введите название альбома:");
            string albumName = Console.ReadLine();
            var album = await context.Albums
                .FirstOrDefaultAsync(a => a.Name == albumName);

            if (album == null)
            {
                Console.WriteLine("Альбом не найден");
                return;
            }

            Console.WriteLine("Введите название жанра:");
            string genreName = Console.ReadLine();
            var genre = await context.Genres.
                FirstOrDefaultAsync(g => g.Name == genreName);
            if (genre == null)
            {
                Console.WriteLine("Жанр не найден.");
                return;
            }

            var newTrack = new Track
            {
                Name = name,
                Length = length,
                ArtistId = artist.Id,
                AlbumId = album.Id,
                GenreId = genre.Id
            };

            context.Tracks.Add(newTrack);
            await context.SaveChangesAsync();
            Console.WriteLine("Трек успешно добавлен");
        }
    }

    public static async Task ChangeArtistData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя трека, данные которого вы хотите поменять: ");
            string name = Console.ReadLine();

            var track = await context.Tracks
                .FirstOrDefaultAsync(t => t.Name == name);

            if (track == null)
            {
                Console.WriteLine("Трек не найден");
            }
            else
            {
                Console.WriteLine("Введите новое имя трека:");
                string newName = Console.ReadLine();

                Console.WriteLine("Введите новую длину трека:");
                int newLength = int.Parse(Console.ReadLine());

                track.Name = newName;
                track.Length = newLength;
                Console.WriteLine("Данные успешно изменены");
            }

            await context.SaveChangesAsync();
        }
    }

    public static async Task DeleteTrackData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите название трека, который вы хотите удалить");
            string name = Console.ReadLine();

            var track = await context.Tracks.
                Include(t => t.Playlists)
                .FirstOrDefaultAsync(t => t.Name == name);

            if (track == null)
            {
                Console.WriteLine("Трек не найден");
                return;
            }

            foreach (var playlist in track.Playlists.ToList())
            {
                playlist.Tracks.Remove(track);
            }

            context.Tracks.Remove(track);
            await context.SaveChangesAsync();
            Console.WriteLine("Трек успешно удален");
        }
    }
}