using System.Collections.Generic;
using CoffeeShopRest.DataModels;

namespace CoffeeShopRest.BusinessLayer
{
    public class DataBaseService
    {
        public IEnumerable<dynamic> GetAllCompanies()
        {
            dynamic table = new Company();
            return table.Query("SELECT * FROM Company");
        }

        public dynamic AddCompany(string name, string description)
        {
            dynamic table = new Company();
            return table.Insert(new { Name = name, Description = description });
        }
    }
}
