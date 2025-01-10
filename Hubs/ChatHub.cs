using Microsoft.AspNetCore.SignalR;
using APELC.Helper;
//using APELC.LocalServices.ApelC;
using APELC.Model;
//using System.IdentityModel.Tokens.Jwt;
//using System.Web.Mvc;
using System.Xml.Linq;

namespace APELC.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }

        //public override Task OnConnectedAsync()
        //{
        //    var httpContext = Context.GetHttpContext();
        //    if (httpContext != null)
        //    {
        //        string _penciptaFk = httpContext.Session.GetString("_hrStafFk") ?? "";
        //        //string _siasatanPkEnc = ApelCUser.GetApel(httpContext.Session).MOHON_PK_ENC ?? "";
        //        string _role = httpContext.Session.GetString("_hrApelCRole") ?? "";
        //        string _userAll = "";
        //        if (_role == "PNYST_KANAN")
        //        {
        //            _userAll = "ALL"; // Dapatkan Semua Senarai Permohonan 
        //        }

        //        CarianMessage _data = new();
        //        //_data = SiasatanProcess.MtdGetMaklumatPenyiasatChat(_siasatanPkEnc, _penciptaFk, _userAll);
        //        if (_data != null)
        //        {
        //            string _pegChat = _data.pegChat.MOHON_NO ?? "";
        //            Groups.AddToGroupAsync(Context.ConnectionId, _pegChat);
        //        }

        //        //string _getCookieUser = httpContext.Request.Cookies["sresu"] ?? "";
        //        ////var jwtToken = httpContext.Request.Query["SSOJWT"];
        //        //var handler = new JwtSecurityTokenHandler();
        //        //if (!string.IsNullOrEmpty(_token))
        //        //{
        //        //    var token = handler.ReadJwtToken(_token);
        //        //    var tokenS = token as JwtSecurityToken;

        //        //    // replace email with your claim name
        //        //    var jti = tokenS.Claims.First(claim => claim.Type == "sub").Value;
        //        //    if (jti != null && jti != "")
        //        //    {
        //        //        Groups.AddToGroupAsync(Context.ConnectionId, _getCookieUser);
        //        //    }
        //        //}
        //    }
        //    return base.OnConnectedAsync();

        //    //Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
        //    //return base.OnConnectedAsync();
        //}
        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
