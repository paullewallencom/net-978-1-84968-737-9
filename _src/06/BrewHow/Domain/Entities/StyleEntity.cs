using System;

namespace BrewHow.Domain.Entities
{
    public class StyleEntity
    {
        public int StyleId { get; set; }
        public CategoryEntity Category { get; set; }
        public string Name { get; set; }
    }
}