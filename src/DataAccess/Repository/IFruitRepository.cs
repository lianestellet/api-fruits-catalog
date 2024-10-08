using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IFruitRepository
    {
        IEnumerable<Fruit> FindAllFruits();
        Fruit FindFruitById(long id);
        void SaveFruit(Fruit fruit);
        void UpdateFruit(Fruit updatedFruit);
        void DeleteFruit(long id);
    }
}
