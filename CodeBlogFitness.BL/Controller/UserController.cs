using CodeBlogFitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogFitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    [Serializable]
    public class UserController : BaseController
    {
        /// <summary>
        /// Пользователи приложения.
        /// </summary>
        public List<User> Users { get; } // не надо так делать. Ладно?
        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public User CurrentUser { get; }

        public bool IsNewUser { get; } = false;


        /// <summary>
        /// Создание нового контроллера пользователя. 
        /// </summary>
        /// <param name="user"></param>
        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(userName));
            }
            Users = GetUsersData();

            CurrentUser = Users.SingleOrDefault(u => u.Name == userName); 

            if (CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }

        public void SetNewUserData(string genderName, DateTime birthDay, double weight = 1, double growth = 1)
        {
            // Проверка 

            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDay = birthDay;
            CurrentUser.Weight = weight;
            CurrentUser.Growth = growth;
            Save();

        }
        /// <summary>
        /// Получить сохраненные данные списка пользователей.
        /// </summary>
        /// <returns> Пользователь приложения. </returns>
        private List<User> GetUsersData()
        {
            return Load<User>() ?? new List<User>();
        }
        /// <summary>
        /// Сохранить данные пользователя. 
        /// </summary>
        public void Save()
        {
            Save(Users);
        }
    }
}
