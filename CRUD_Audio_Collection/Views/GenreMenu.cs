using CRUD_Audio_Collection.Data;

namespace CRUD_Audio_Collection.Views;

public class GenreMenu
{
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню жанров");
            Console.WriteLine("1. Получить информацию о всех жанрах");
            Console.WriteLine("2. Добавить новый жанр");
            Console.WriteLine("3. Изменить информацию о жанре");
            Console.WriteLine("4. Удалить жанр");
            Console.WriteLine("5. Вернуться в главное меню");

            Console.WriteLine("Введите номер пункта:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await GenreDataManager.GetAllGenres();
                    break;
                case "2":
                    await GenreDataManager.AddNewGenre();
                    break;
                case "3":
                    await GenreDataManager.ChangeGenreData();
                    break;
                case "4":
                    await GenreDataManager.DeleteGenreData();
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