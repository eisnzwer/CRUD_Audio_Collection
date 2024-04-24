using CRUD_Audio_Collection.Data;

namespace CRUD_Audio_Collection.Views;

public class AlbumMenu
{
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню альбомов");
            Console.WriteLine("1. Получить информацию о всех альбомах");
            Console.WriteLine("2. Добавить новый альбом");
            Console.WriteLine("3. Изменить информацию об альбоме");
            Console.WriteLine("4. Удалить альбом");
            Console.WriteLine("5. Вернуться в главное меню");

            Console.WriteLine("Введите номер пункта:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AlbumDataManager.GetAllTracks();
                    break;
                case "2":
                    await AlbumDataManager.AddNewAlbum();
                    break;
                case "3":
                    await AlbumDataManager.ChangeAlbumData();
                    break;
                case "4":
                    await AlbumDataManager.DeleteAlbumData();
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