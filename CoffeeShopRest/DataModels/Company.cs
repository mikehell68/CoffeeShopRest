using Massive;

namespace CoffeeShopRest.DataModels
{
    public class Company : DynamicModel 
    {
        public Company() : base("CoffeeHouse", "Company", "Id") { }

    }
}
