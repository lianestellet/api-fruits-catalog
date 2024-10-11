using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Fruit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set;  }
        public long FruitTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FruitType? FruitType { get; set; }

        public Fruit() { }

        public Fruit(long fruitTypeId, string name, string description)
        {            
            FruitTypeId = fruitTypeId;
            Name = name;
            Description = description;
        }
    }
}
