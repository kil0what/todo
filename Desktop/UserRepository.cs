using System;
using System.Collections.Generic;
using System.Linq;
using Desktop.Entities;

namespace Desktop.Repository
{
    public class UserRepository
    {
        // Список пользователей в памяти
        private static List<UserModel> _users = new List<UserModel>();

        public static void Register(UserModel user)
        {
            // Проверка уникальности Email
            if (_users.Any(u => u.Email == user.Email))
            {
                throw new Exception("Пользователь с такой почтой уже существует");
            }
            _users.Add(user);
        }

        public static UserModel Authenticate(string email, string password)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}