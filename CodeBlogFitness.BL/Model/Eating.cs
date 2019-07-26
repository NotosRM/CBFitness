using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogFitness.BL.Model
{
    /// <summary>
    /// Прием пищи
    /// </summary>
    [Serializable]
    public class Eating
    {
        public int Id { get; set; }
        public DateTime Moment { get; set; }

        public Dictionary<Food, double> Foods { get; } // Не красиво передавать в dictionary ссылчный тип, теряем большое количество его преимуществ
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public Eating() { }
        public Eating(User user)
        {
            User = user ?? throw new ArgumentNullException("Пользователь не может бытть пустым.", nameof(user));
            Moment = DateTime.UtcNow;
            Foods = new Dictionary<Food, double>();

        }
        public void Add(Food food, double weight)
        {
            var product =  Foods.Keys.FirstOrDefault(f => f.Name.Equals(food.Name));

            if (product is null)
            {
                Foods.Add(food, weight);
            }
            else
            {
                Foods[product] += weight;
            }
        }
    }
}
