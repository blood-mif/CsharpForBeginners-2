using System;
using System.Collections.Generic;
using System.Text;

namespace ShopModels
{
    public class ShowcaseItemRequestModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RemovalTime { get; set; }
    }
}
