using System;
using System.Threading.Tasks;
using CRUD_Audio_Collection.Data;

namespace CRUD_Audio_Collection.Views
{
    public static class UserMenu
    {
        public static async Task Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню пользователя");
                Console.WriteLine("1. Добавить нового пользователя");
                Console.WriteLine("2. Получить информацию о всех пользователях");
                Console.WriteLine("3. Изменить информацию о пользователе");
                Console.WriteLine("4. Удалить пользователя");
                Console.WriteLine("5. Вернуться в главное меню");

                Console.WriteLine("Введите номер пункта:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await UserDataManager.AddNewUser();
                        break;
                    case "2":
                        await UserDataManager.GetAllUsers();
                        break;
                    case "3":
                        await UserDataManager.ChangeUserData();
                        break;
                    case "4":
                        await UserDataManager.DeleteUserData();
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
}