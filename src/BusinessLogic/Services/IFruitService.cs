using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess.Models;

namespace BusinessLogic.Services
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitDTO>> FindAllAsync();
        IEnumerable<Fruit> FindAllFruits();
        Fruit FindFruitById(long id);
        void SaveFruit(Fruit fruit);
        void UpdateFruit(long id, Fruit updatedFruit);
        void DeleteFruit(long id);
    }
}
