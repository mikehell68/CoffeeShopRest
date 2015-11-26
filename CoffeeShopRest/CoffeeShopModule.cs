using System.Threading.Tasks;
using CoffeeShopRest.BusinessLayer;
using log4net;
using Nancy;
using Nancy.Security;


namespace CoffeeShopRest
{
    public class CoffeeShopModule: NancyModule
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMembershipService _membership;
        private readonly DataBaseService _dataBaseService = new DataBaseService();
        
        public CoffeeShopModule(IMembershipService membership)
        {
            _membership = membership;

            Get["/"] = parameters =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                var allCompanies = _dataBaseService.GetAllCompanies();

                return View["views/CoffeeShop/StaticView", allCompanies];
            };


            Get["/testing"] = parameters =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                return View["staticview", Request.Url];
            };

            Get["/map"] = parameters =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                return View["views/CoffeeShop/map"];
            };

            Get["/register"] = _ =>
            {
                Log.InfoFormat("Get request {0}", Request.Url);
                //already logged in
                if (Context.CurrentUser.IsAuthenticated())
                    return Response.AsRedirect("~/");
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

    public class MissingUserIdentity
    {
    }

    public interface IMembershipService
    {
        Task<bool> IsUserNameAvailable(string username);

        Task<NewUser> AddUser(string userName, string email, string password);
    }

    class MembershipService : IMembershipService
    {
        public async Task<bool> IsUserNameAvailable(string username)
        {
            return await Task.Run(() => true);
        }

        public async Task<NewUser> AddUser(string userName, string email, string password)
        {
            return await Task.Run(() => new NewUser {UserName = userName, Email = email, Password = password});
        }
    }
}
