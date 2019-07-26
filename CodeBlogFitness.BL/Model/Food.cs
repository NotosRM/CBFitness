using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogFitness.BL.Model
{
    [Serializable]
    public class Food
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public double Callories { get; set; }
        public virtual ICollection<Eating> Eatings { get; set; }

        public int MyProperty { get; set; }
        /// <summary>
        /// Белки
        /// </summary>
        public double Proteins { get; set; }
        /// <summary>
        /// Жиры
        /// </summary>
        public double Fats { get; set; }
        /// <summary>
        /// Углеводы
        /// </summary>
        public double Carbohydrates { get; set; }
        /// <summary>
        /// Калории за сто грамм продукта
        /// </summary>
        public double Calories { get; set; }


        #endregion
        public Food() { }
        public Food(string name) : this(name, 0, 0, 0, 0) { }

        public Food(string name, double calories, double proteins, double fats, double carbohydrates)
        {
            //TODO: проверка
            Name = name;
            Proteins = proteins;
            Calories = calories / 100.0;
            Fats = fats / 100.0;
            Carbohydrates = carbohydrates / 100.0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
