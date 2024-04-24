using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Audio_Collection.Data;

public class PlaylistDataManager
{
    public static async Task GetAllPlaylists()
    {
        using (var context = new AppDbContext())
        {
            var playlists = await context.Playlists
                .Include(p => p.Tracks)
                .ToListAsync();

            foreach (var playlist in playlists)
            {
                Console.WriteLine($"Название плейлиста: {playlist.Name}");

                Console.WriteLine("Треки в плейлисте:");
                foreach (var track in playlist.Tracks)
                {
                    Console.WriteLine($"- {track.Name}");
                }
            }
        }
    }

    public static async Task AddNewPlaylist()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите название нового плейлиста:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите имя пользователя, которому будет принадлежать плейлист:");
            string userName = Console.ReadLine();

            var user = await context.Users
                .FirstOrDefaultAsync(u => u.NickName == userName);

            if (user == null)
            {
                Console.WriteLine("Пользователь с таким именем не найден");
                return;
            }

            var newPlaylist = new Playlist
            {
                Name = name,
                User = user
            };

            Console.WriteLine("Сколько треков нужно добавить в плейлист?");
            int trackCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < trackCount; i++)
            {
                Console.WriteLine($"Введите название трека {i + 1}:");
                string trackName = Console.ReadLine();

                var track = await context.Tracks.
                    FirstOrDefaultAsync(t => t.Name == trackName);
                if (track != null)
                {
                    newPlaylist.Tracks.Add(track);
                }
                else
                {
                    Console.WriteLine($"Трек с названием '{trackName}' не найден.");
                }
            }

            context.Playlists.Add(newPlaylist);
            await context.SaveChangesAsync();

            Console.WriteLine("Новый плейлист успешно добавлен");
        }
    }

    public static async Task ChangePlaylistData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя плейлиста, данные которого вы хотите изменить:");
            string name = Console.ReadLine();

            var playlist = await context.Playlists
                .Include(p => p.Tracks)
                .FirstOrDefaultAsync(p => p.Name == name);

            if (playlist == null)
            {
                Console.WriteLine("Плейлист не найден");
                return;
            }

            Console.WriteLine("Введите новое имя плейлиста:");
            string newName = Console.ReadLine();
            playlist.Name = newName;
            Console.WriteLine("Данные успешно изменены");

            Console.WriteLine("Выберите действие для управления треками в плейлисте:");
            Console.WriteLine("1. Удалить трек из плейлиста");
            Console.WriteLine("2. Добавить трек в плейлист");
            Console.WriteLine("3. Оставить плейлист без изменений");

            string actionChoice = Console.ReadLine();

            switch (actionChoice)
            {
                case "1":
                    Console.WriteLine("Введите название трека, который нужно удалить из плейлиста:");
                    string trackNameToRemove = Console.ReadLine();
                    var trackToRemove = playlist.
                        Tracks.FirstOrDefault(t => t.Name == trackNameToRemove);
                    if (trackToRemove != null)
                    {
                        playlist.Tracks.Remove(trackToRemove);
                        Console.WriteLine($"Трек '{trackNameToRemove}' успешно удален из плейлиста");
                    }
                    else
                    {
                        Console.WriteLine($"Трек '{trackNameToRemove}' не найден в плейлисте");
                    }

                    break;
                case "2":
                    Console.WriteLine("Введите название трека, который вы хотите добавить в плейлист:");
                    string trackNameToAdd = Console.ReadLine();
                    var trackToAdd = await context.Tracks.
                        FirstOrDefaultAsync(t => t.Name == trackNameToAdd);
                    if (trackToAdd != null && !playlist.Tracks.Contains(trackToAdd))
                    {
                        playlist.Tracks.Add(trackToAdd);
                        Console.WriteLine($"Трек '{trackNameToAdd}' успешно добавлен в плейлист");
                    }
                    else if (trackToAdd == null)
                    {
                        Console.WriteLine($"Трек '{trackNameToAdd}' не найден");
                    }
                    else
                    {
                        Console.WriteLine($"Трек '{trackNameToAdd}' уже находится в плейлисте");
                    }

                    break;
                case "3":
                    Console.WriteLine("Плейлист оставлен без изменений");
                    break;
                default:
                    Console.WriteLine("Некорректный выбор действия");
                    break;
            }

            await context.SaveChangesAsync();
        }
    }

    public static async Task DeletePlaylistData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя плейлиста, который вы хотите удалить: ");
            string name = Console.ReadLine();

            var playlist = await context.Playlists
                .Include(p => p.Tracks)
                .FirstOrDefaultAsync(p => p.Name == name);

            if (playlist == null)
            {
                Console.WriteLine("Плейлист не найден");
            }
            else
            {
                context.Remove(playlist);
                await context.SaveChangesAsync();
                Console.WriteLine("Плейлист и треки, входящие в него, успешно удалены");
            }
        }
    }
}