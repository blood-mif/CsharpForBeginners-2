using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class ShowCase
    {
    
        public uint Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RemovalTime { get; set; }
        public ShowCase() { }
        public ShowCase(string name, int size, uint id)
        {
            Id = id;
            Name = name;
            Size = size;
            CreationTime = DateTime.Now;
        }
        public List<Product> Products = new List<Product>();
    }
}
