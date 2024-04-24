using CRUD_Audio_Collection.Data;

namespace CRUD_Audio_Collection.Views;

public class PlaylistMenu
{
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню плейлистов");
            Console.WriteLine("1. Получить информацию о всех плейлистах");
            Console.WriteLine("2. Добавить новый плейлист");
            Console.WriteLine("3. Изменить информацию о плейлисте");
            Console.WriteLine("4. Удалить плейлист");
            Console.WriteLine("5. Вернуться в главное меню");

            Console.WriteLine("Введите номер пункта:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await PlaylistDataManager.GetAllPlaylists();
                    break;
                case "2":
                    await PlaylistDataManager.AddNewPlaylist();
                    break;
                case "3":
                    await PlaylistDataManager.ChangePlaylistData();
                    break;
                case "4":
                    await PlaylistDataManager.DeletePlaylistData();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}