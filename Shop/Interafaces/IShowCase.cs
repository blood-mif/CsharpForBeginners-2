using System;
using System.Collections.Generic;
using System.Text;
using Shop.Models;
using ShopModels;
namespace Shop.Interafaces
{
    interface IShowCase
    {
         uint Id { get; set; }
         string Name { get; set; }
         int Size { get; set; }
         DateTime CreationTime { get; set; }
         DateTime RemovalTime { get; set; }


        void ShowProducts(ShowCase showCase);
        void AddProduct(ShowCase showCase);
        void RemoveProduct(ShowCase showCase);
        
    }
}
