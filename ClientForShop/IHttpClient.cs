using System;
using System.Collections.Generic;
using System.Text;
using ShopModels;

namespace ClientForShop
{
    public interface IHttpClient
    {
        //MenuModel GetMenu();
        void SelectMenuItem(int menuItemId, int quantity);
        ListShowcaseModel GetShowcaseList();

        void CreateNewShowcase(string nameShowcase, int sizeShowcase);
        //OrderModel GetOrder();
    }
}
