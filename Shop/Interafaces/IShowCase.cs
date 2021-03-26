using System;
using System.Collections.Generic;
using System.Text;
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


        void ShowProducts(ShowcaseModel showCase);
        void AddProduct(ShowcaseModel showCase);
        void RemoveProduct(ShowcaseModel showCase);
        
    }
}
