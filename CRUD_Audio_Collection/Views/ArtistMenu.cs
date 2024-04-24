using CRUD_Audio_Collection.Data;

namespace CRUD_Audio_Collection.Views;

public class ArtistMenu
{
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню артистов");
            Console.WriteLine("1. Получить информацию о всех артистах");
            Console.WriteLine("2. Добавить нового артиста");
            Console.WriteLine("3. Изменить информацию об артисте");
            Console.WriteLine("4. Удалить артиста");
            Console.WriteLine("5. Вернуться в главное меню");

            Console.WriteLine("Введите номер пункта:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ArtistDataManager.GetAllArtists();
                    break;
                case "2":
                    await ArtistDataManager.AddNewArtist();
                    break;
                case "3":
                    await ArtistDataManager.ChangeArtistData();
                    break;
                case "4":
                    await ArtistDataManager.DeleteArtistData();
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