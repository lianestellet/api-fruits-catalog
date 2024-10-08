using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Fruit(long Id, string Name, string? Description = null)
    {
        public long Id { get; } = Id;
        public string Name { get; set; } = Name;
        public string? Description { get; set; } = Description;
    }
}
