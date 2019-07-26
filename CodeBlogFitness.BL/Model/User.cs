using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogFitness.BL.Model
{
    [Serializable]
    /// <summary>
    /// Пользователь
    /// </summary
    public class User
    {
        #region Свойства
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get;  }
        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }
        public int? GenderId { get; set; }
        /// <summary>
        /// Дата Рождения
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// Вес
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Рост, у него Height
        /// </summary>
        public double Growth { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get
                             {
                               DateTime nowDate = DateTime.Today;
                               int age = nowDate.Year - BirthDay.Year;
                               if (BirthDay > nowDate.AddYears(-age))
                               {
                                    age--;
                               }
                               return age;
                             }
                       }
        #endregion

        public User() { }
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="name"> Имя. </param>
        /// <param name="gender"> Пол. </param>
        /// <param name="birthDay"> День рождения. </param>
        /// <param name="weight"> Вес. </param>
        /// <param name="growth"> Рост. </param>
        public User(string name, Gender gender, DateTime birthDay, double weight, double growth)
        {
            #region Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым или null. ", nameof(name));
            }
            if (gender == null)
            {
                throw new ArgumentNullException("Пол не может быть null. ",nameof(gender));
            }
            if (birthDay < DateTime.Parse("01.01.1919") || birthDay >= DateTime.Now)
            {
                throw new ArgumentException("Невозможная дата рождения. ", nameof(birthDay));
            }
            if (weight <= 0)
            {
                throw new ArgumentException("Вес не модет быть меньше 0 или равным 0. ", nameof(weight));
            }
            if (growth <=0)
            {
                throw new ArgumentException("Рост не модет быть меньше 0 или равным 0. ", nameof(growth));
            }
            #endregion
            
            Name = name;
            Gender = gender;
            BirthDay = birthDay;
            Weight = weight;
            Growth = growth;
        }
        public User (string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым или null. ", nameof(name));
            }
            Name = name;
        }
        public override string ToString()
        {
            return Name + " " + Age;
        }

    }
}
