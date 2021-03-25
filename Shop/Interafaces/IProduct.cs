using System;
using System.Collections.Generic;
using System.Text;
using ShopModels;

namespace Shop.Interafaces
{
    interface IProduct
    {
         int Id { get; set; }
         string Name { get; set; }
         int OccupiedSize { get; set; }
        decimal Price { get; set; }
    }
}
