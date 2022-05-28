using System.Web;

namespace appSchool.Model {
    public class ApplicationUser {
        public string UserName { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }

    public static class AuthHelper {
        public static bool SignIn(string userName, string password) {
            HttpContext.Current.Session["User"] = CreateDefualtUser();  // Mock user data
            return true;
        }
        public static void SignOut() {
            HttpContext.Current.Session["User"] = null;
        }
        public static bool IsAuthenticated() {
            return GetLoggedInUserInfo() != null;
        }

        public static ApplicationUser GetLoggedInUserInfo() {

            //CreateDefualtUser();
            // return HttpContext.Current.Session["User"] as ApplicationUser;
            HttpContext.Current.Session["User"] = CreateDefualtUser();
            return HttpContext.Current.Session["User"] as ApplicationUser;  // Mock user data
        }



        private static ApplicationUser CreateDefualtUser()
        {

            if(HttpContext.Current.Session["UserName"] == null)
            {
                return new ApplicationUser
                {
                    UserName = "_",
                    FirstName = "_",
                    LastName = "_",
                    Email = "_",
                    UserRole = "_",
                    AvatarUrl = "~/Content/Photo/admin.png"
                };
            }
            else
            {
                return new ApplicationUser
                {
                    UserName = HttpContext.Current.Session["UserName"].ToString(),
                    FirstName = HttpContext.Current.Session["UserName"].ToString(),
                    LastName = HttpContext.Current.Session["UserName"].ToString(),
                    Email = "_",
                    UserRole = HttpContext.Current.Session["UserRoleName"].ToString(),
                    AvatarUrl = "~/Content/Photo/admin.png"
                };
            }

            
        }
        private static ApplicationUser CreateDefualtUserOld() {
            return new ApplicationUser {
                UserName = "JBell",
                FirstName = "Julia",
                LastName = "Bell",
                Email = "julia.bell@example.com",
                AvatarUrl = "~/Content/Photo/admin.png"
            };
        }
    }
}