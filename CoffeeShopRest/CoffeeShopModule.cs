﻿using CoffeeShopRest.BusinessLayer;
using log4net;
using Nancy;

namespace CoffeeShopRest
{
    public class CoffeeShopModule: NancyModule
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DataBaseService _dataBaseService = new DataBaseService();
        
        public CoffeeShopModule()
        {
            Get["/"] = parameters =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                var allCompanies = _dataBaseService.GetAllCompanies();

                return View["views/CoffeeShop/StaticView", allCompanies];
            };
            
            Get["/map"] = parameters =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                return View["views/CoffeeShop/map"];
            };

            Get["/register"] = _ =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                return View["views/CoffeeShop/NewUser"];
            };

            Post["/register", true] = async (x, ct) =>
            {
                Log.InfoFormat("Post request {0}", Request.Url);
                var registerResult = _dataBaseService.AddCompany(Request.Form.Name, Request.Form.Description);
                
                return LoginAndRedirect(registerResult);
            };
        }

        private object LoginAndRedirect(dynamic company)
        {
            return View["views/CoffeeShop/Welcome", company];
        }
    }
}
