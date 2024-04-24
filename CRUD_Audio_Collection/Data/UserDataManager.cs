using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Audio_Collection.Data;

public class UserDataManager
{
    public static async Task AddNewUser()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя пользователя:");
            string nickName = Console.ReadLine();

            var existingUser = await context.Users.
                FirstOrDefaultAsync(u => u.NickName == nickName);
            if (existingUser != null)
            {
                Console.WriteLine("Пользователь с таким никнеймом уже существует.");
                return;
            }

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            Console.WriteLine("Введите номер карты:");
            string cardnumber = Console.ReadLine();

            Console.WriteLine("Введите срок действия карты (гггг-мм-дд):");
            DateOnly expirationDate = DateOnly.Parse(Console.ReadLine());

            var newUser = new User
            {
                NickName = nickName,
                Password = password
            };

            newUser.PaymentData = new PaymentData
            {
                CardNumber = cardnumber,
                ExpirationDate = expirationDate
            };

            context.Users.Add(newUser);

            await context.SaveChangesAsync();
            Console.WriteLine("Пользователь успешно добавлен.");
        }
    }

    public static async Task GetAllUsers()
    {
        using (var context = new AppDbContext())
        {
            var users = await context.Users
                .Include(u => u.PaymentData)
                .ToListAsync();

            foreach (var user in users)
            {
                Console.WriteLine($"Ник: {user.NickName}, Пароль: {user.Password}, Номер карты: {user.PaymentData.CardNumber}, Срок действия: {user.PaymentData.ExpirationDate}");
            }
        }
    }

    public static async Task ChangeUserData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя пользователя, данные которого вы хотите поменять: ");
            string nickname = Console.ReadLine();
            
            var user = await context.Users
                .Include(user => user.PaymentData)
                .FirstOrDefaultAsync(u => u.NickName == nickname);
            
            if (user == null)
            {
                Console.WriteLine("Пользователь не найден");
            }
            
            else
            {
                Console.WriteLine("Введите новое имя пользователя:");
                string newNickname = Console.ReadLine();

                Console.WriteLine("Введите новый пароль: ");
                string password = Console.ReadLine();

                Console.WriteLine("Введите новый номер карты: ");
                string cardNumber = Console.ReadLine();

                Console.WriteLine("Введите новый срок действия: ");
                DateOnly expirationDate = DateOnly.Parse(Console.ReadLine());
                
                user.NickName = newNickname;
                user.Password = password;
                user.PaymentData.CardNumber = cardNumber;
                user.PaymentData.ExpirationDate = expirationDate;
                Console.WriteLine("Данные успешно изменены");
            }
            
            await context.SaveChangesAsync();
        }
    }

    public static async Task DeleteUserData()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Введите имя пользователя, которого вы хотите удалить: ");
            string nickname = Console.ReadLine();

            var user = await context.Users
                .Include(user => user.PaymentData)
                .FirstOrDefaultAsync(u => u.NickName == nickname);

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден");
            }
            else
            {
                context.Remove(user);
                await context.SaveChangesAsync();
                Console.WriteLine("Пользователь успешно удален");
            }
        }
    }
}