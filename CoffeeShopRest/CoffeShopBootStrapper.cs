using Nancy;
using Nancy.Diagnostics;

namespace CoffeeShopRest
{
    public class CoffeeShopBootstrapper : DefaultNancyBootstrapper
    {
        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = "password" }; }
        }
    }
}
