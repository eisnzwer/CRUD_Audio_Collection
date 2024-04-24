using CRUD_Audio_Collection.Data;

namespace CRUD_Audio_Collection.Views;

public class TrackMenu
{
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню треков");
            Console.WriteLine("1. Получить информацию о всех треках");
            Console.WriteLine("2. Добавить новый трек");
            Console.WriteLine("3. Изменить информацию о треке");
            Console.WriteLine("4. Удалить трек");
            Console.WriteLine("5. Вернуться в главное меню");

            Console.WriteLine("Введите номер пункта:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await TrackDataManager.GetAllTracks();
                    break;
                case "2":
                    await TrackDataManager.AddNewTrack();
                    break;
                case "3":
                    await TrackDataManager.ChangeArtistData();
                    break;
                case "4":
                    await TrackDataManager.DeleteTrackData();
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