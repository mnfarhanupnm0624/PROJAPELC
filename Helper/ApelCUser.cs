using APELC.Model;
//using Newtonsoft.Json;

namespace APELC.Helper
{
    public static class ApelCUser
    {
        private static IHttpContextAccessor httpContextAccessor;
        public static void Configure(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public static HttpContext Current => httpContextAccessor.HttpContext;

        public static SessionModel GetApel(this ISession session)
        {
            var user = new SessionModel();
            var value = session.GetString("ApelCUser");

            //if (value != null)
            //{
            //    user = JsonConvert.DeserializeObject<SessionModel>(value);
            //}
            //else
            //{
            //    user.PAPARAN_ONLY = true;
            //}


            return user;

        }
    }
}
