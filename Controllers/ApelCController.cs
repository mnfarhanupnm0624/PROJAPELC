using Microsoft.AspNetCore.Mvc;
using APELC.Helper;
using APELC.LocalServices.Senarai;
using APELC.LocalServices.ApelC;
using APELC.Model;
using Newtonsoft.Json;
using Net6HrPublicLibrary.PublicShared;
using Net6HrPublicLibrary.Model;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;



namespace APELC.Controllers
{
    public class ApelCController : Controller
    {

        public IActionResult BackToDashboard()
        {
            return Redirect("./Views/Home/DashboardStatistik.cshtml");
        }

        private void MtdGetDashboardSession(string id, string id2)
        {
            HttpContext.Session.SetString("_titleTop", "APEL.C UPNM");
            HttpContext.Session.SetString("_titleSmall", id);
            HttpContext.Session.SetString("_titleSmallSub", id2);
        }

        public IActionResult UnderConstraction()
        {
            MtdGetDashboardSession("Under Constraction", "");
            ModelUserDTO _data = new();
            return View(_data);
        }

        //private bool saveAduanOnSession(CarianAduanMain aduanModel)
        //{
        //    var aduanHelper = new AduanHelper();
        //    HttpContext.Session.SetString("ApelCUser", JsonConvert.SerializeObject(aduanHelper.GetApelInfo(aduanModel)));
        //    return true;
        //}





        //public IActionResult Pengesahan()
        //{
        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
        //    if (_eUserIdIn != "")
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}

        // Begin: Sub Topic Kes Siasatan

        //public IActionResult SenaraiAduan()
        //{
        //    MtdGetDashboardSession("Senarai Aduan", "Senarai Aduan");

        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
        //    string _aduanPkEnc = ApelCUser.GetApel(HttpContext.Session).MOHON_PK_ENC ?? "";

        //    if (_eUserIdIn != "")
        //    {
        //        //CarianStaff _data = new();
        //        //return View(_data);
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}


        //public IActionResult CttnSstnAwal()
        //{
        //    MtdGetDashboardSession("Senarai Aduan", "Catatan Siasatan Awal");

        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

        //    if (_eUserIdIn != "")
        //    {
        //        CarianStaff _data = new();
        //        return View(_data);
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}

        //public IActionResult ButiranAduan()
        //{
        //    MtdGetDashboardSession("Senarai Aduan", "Butiran Aduan");

        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

        //    if (_eUserIdIn != "")
        //    {
        //        CarianStaff _data = new();
        //        return View(_data);
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}



        //public IActionResult MaklumatAduan()
        //{
        //    MtdGetDashboardSession("Senarai Aduan", "Maklumat Aduan");

        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

        //    if (_eUserIdIn != "")
        //    {
        //        CarianStaff _data = new();
        //        return View(_data);
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}



        //public IActionResult Lampiran()
        //{
        //    MtdGetDashboardSession("Senarai Aduan", "Lampiran");

        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

        //    if (_eUserIdIn != "")
        //    {
        //        CarianStaff _data = new();
        //        return View(_data);
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}


        //public IActionResult PerakuanPegawaiPenyiasatKanan()
        //{
        //    MtdGetDashboardSession("Senarai Aduan", "Perakuan Pegawai Penyiasat");

        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

        //    if (_eUserIdIn != "")
        //    {
        //        CarianAduanMain _data = new();
        //        return View(_data);
        //    }
        //    else
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //}
    }
}
