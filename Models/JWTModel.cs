////using System.IdentityModel.Tokens.Jwt;

//namespace APEL.Models
//{
//    public class JWTModel
//    {
//#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//        public JWTModel(string token)
//#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//        {
//            //var handler = new JwtSecurityTokenHandler();
//            //var tokenS = handler.ReadToken(token) as JwtSecurityToken;

//            //if staf
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            if (tokenS.Claims.FirstOrDefault(claim => claim.Type == "employeeNumber") != null)
//            {
//                StaffNo = tokenS.Claims.FirstOrDefault(claim => claim.Type == "employeeNumber").Value;
//            }

//            //if student
//            if (tokenS.Claims.FirstOrDefault(claim => claim.Type == "studentNumber") != null)
//            {
//                StudentNo = tokenS.Claims.FirstOrDefault(claim => claim.Type == "studentNumber").Value;
//            }

//            FullName = tokenS.Claims.First(claim => claim.Type == "fullname").Value;
//            UserName = tokenS.Claims.First(claim => claim.Type == "sub").Value;
//            UserEmail = tokenS.Claims.First(claim => claim.Type == "email").Value;

//            //role user STAF/PELAJAR
//            if (tokenS.Claims.FirstOrDefault(claim => claim.Type == "roles") != null)
//            {
//                UserRoles = tokenS.Claims.FirstOrDefault(claim => claim.Type == "roles").Value;
//            }
//#pragma warning restore CS8602 // Dereference of a possibly null reference.

//        }

//        public string StaffNo { get; set; }
//        public string StudentNo { get; set; }
//        public string FullName { get; set; }
//        public string UserName { get; set; }
//        public string UserEmail { get; set; }
//        public string UserRoles { get; set; }
//    }
//}
