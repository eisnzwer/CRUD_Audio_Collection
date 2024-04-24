namespace CRUD_Audio_Collection.Views
{
    public static class Menu
    {
        public static async Task Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Аудио коллекция - Меню");
                Console.WriteLine("1. Меню пользователя");
                Console.WriteLine("2. Меню альбомов");
                Console.WriteLine("3. Меню жанров");
                Console.WriteLine("4. Меню артистов");
                Console.WriteLine("5. Меню треков");
                Console.WriteLine("6. Меню плейлистов");
                Console.WriteLine("7. Выход из программы");

                Console.WriteLine("Введите номер пункта:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await UserMenu.Show();
                        break;
                    case "2":
                        await AlbumMenu.Show();
                        break;
                    case "3":
                        await GenreMenu.Show();
                        break;
                    case "4":
                        await ArtistMenu.Show();
                        break;
                    case "5":
                        await TrackMenu.Show();
                        break;
                    case "6":
                        await PlaylistMenu.Show();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}