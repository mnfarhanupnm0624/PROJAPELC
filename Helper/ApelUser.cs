using APEL.Models;
//using Newtonsoft.Json;

namespace APEL.Helper
{
    public static class ApelUser
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
            var value = session.GetString("ApelUser");

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
