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
    public class EatingController : BaseController
    {
        private readonly User user;
        public List<Food> Foods { get; }
        public Eating Eating { get; }

        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть путсым. ", nameof(user));
            Foods = GetAllFoods();
            Eating = GetEating();

        }
        #region Add
        //public bool Add(string foodName, double foodweight)
        //{
        //    var food = Foods.SingleOrDefault(f => f.Name == foodName);
        //    if (food != null)
        //    {
        //        Eating.Add(food, foodweight);
        //        Save();
        //        return true;
        //    }
        //    return false;
        //}
        #endregion

        public void Add(Food food, double foodweight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if (product == null) 
            {
                Foods.Add(food);
                Eating.Add(food, foodweight);
                Save();
            }
            else
            {
                Eating.Add(product, foodweight);
                Save(); 
            }

        }

        private Eating GetEating()
        {
            return Load<Eating>().FirstOrDefault() ?? new Eating(user);
        }

        private List<Food> GetAllFoods()
        {
            return Load<Food>() ?? new List<Food>();
        }

        public void Save()
        {
            Save(Foods);
            Save(new List<Eating>() { Eating });
        }
    }
}
