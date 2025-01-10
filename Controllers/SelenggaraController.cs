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
using APELC.LocalServices.Selenggara;
using Microsoft.AspNetCore.Hosting;
using APELC.LocalServices.Login;
using APELC.LocalShared;



namespace APELC.Controllers
{
    public class SelenggaraController : Controller
    {
        //public IActionResult BackToDashboard()
        //{
        //    return RedirectToAction("./Views/Home/DashboardStatistik.cshtml");
        //    //return Redirect("/Home/Dashboard");
        //}

        //private void MtdGetDashboardSession(string id, string id2)
        //{
        //    HttpContext.Session.SetString("_titleTop", "APEL.C UPNM");
        //    HttpContext.Session.SetString("_titleSmall", id);
        //    HttpContext.Session.SetString("_titleSmallSub", id2);
        //}

        //public IActionResult UnderConstraction()
        //{
        //    MtdGetDashboardSession("Under Constraction", "");
        //    ModelUserDTO _data = new();
        //    return View(_data);
        //}

        // GET : Selenggara Akses Kawalam APELC Skrin List
        //    public IActionResult MtdSkrinSelenggaraAksesKawalanAPELC([FromBody] ModelParameterAPELC _carian)
        //    {
        //        MtdGetDashboardSession("Selenggara - Akses Kawalan APELC", "");

        //        string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
        //        string _stafFkEnc = HttpContext.Session.GetString("_hrStafFkEnc") ?? "";
        //        string _role = HttpContext.Session.GetString("_hrApelCRole") ?? "";
        //        string _userAll = "";
        //        string _userPnyst = "";
        //        if (_eUserIdIn != "")
        //        {
        //            if (_role == "PNYST_KANAN") {
        //                _userAll = "ALL"; // Dapatkan Semua Senarai Permohonan 
        //            }
        //            else if (_role == "PNYST")
        //            {
        //                _userPnyst = _stafFkEnc;
        //            }
        //            CarianPerananApelCMain _data = new();
        //            List<ModelParameterAPELC> _newList = new();
        //            //_newList = SenaraiProcess.MtdGetPemohonList("", "", "", "", "", "", "", _userAll, _userPnyst);
        //            //_data.ListCarianPeranan = _newList.ToList();

        //            // Clear Session
        //            HttpContext.Session.Remove("ApelCUser");
        //            HttpContext.Session.SetString("_butiranpermohonanapelc", "");
        //            HttpContext.Session.SetString("_ringkasankjdn", "");
        //            HttpContext.Session.SetString("_diaripnyst", "");
        //            HttpContext.Session.SetString("_frmPemohonsaksi", "");
        //            HttpContext.Session.SetString("_attchPemohonsaksi", "");
        //            HttpContext.Session.SetString("_attchokt", "");
        //            HttpContext.Session.SetString("_frmokt", "");
        //            HttpContext.Session.SetString("_attchdoclain", "");
        //            HttpContext.Session.SetString("_attchbrgbukti", "");
        //            HttpContext.Session.SetString("_minitsstn", "");
        //HttpContext.Session.SetString("_cttsstn", "");
        //            HttpContext.Session.SetString("_rmsnsstn", "");

        //            return View(_data);
        //        }
        //        else
        //        {
        //            return Redirect("/Login/LoginPageApelC");
        //        }
        //    }

        //[HttpPost]
        //public JsonResult MtdGetSelenggaraCaj()
        //{
        //    string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
        //    string _stafFkEnc = HttpContext.Session.GetString("_hrStafFkEnc") ?? "";
        //    string _role = HttpContext.Session.GetString("_hrApelCRole") ?? "";
        //    string _userAll = "";
        //    string _userPnyst = "";

        //    List<ModelParameterAPELC> _newList = null;
        //    if (_ePUserId != "")
        //    {
        //        if (_role == "PNYST_KANAN")
        //        {
        //            _userAll = "ALL"; // Dapatkan Semua Senarai Permohonan 
        //        }
        //        else if (_role == "PNYST")
        //        {
        //            _userPnyst = _stafFkEnc;
        //        }
        //        //_newList = SenaraiProcess.MtdGetPemohonList(_carian.CNOADUAN, _carian.CKATEGORIPemohon, _carian.CKAMPUS, _carian.CSTSPemohon, _carian.CKATEGORIADUAN, _carian.CTKH_ADUANMULA, _carian.CTKH_ADUANTAMAT, _userAll, _userPnyst);
        //        return Json(_newList);
        //    }
        //    else
        //    {
        //        return Json(_newList);
        //    }
        //}
        //public IActionResult BackToDashboard()
        //{
        //    return Redirect("./Views/Home/DashboardStatistik.cshtml");
        //}

        //private void MtdGetDashboardSession(string id, string id2)
        //{
        //    HttpContext.Session.SetString("_titleTop", "APEL.C UPNM");
        //    HttpContext.Session.SetString("_titleSmall", id);
        //    HttpContext.Session.SetString("_titleSmallSub", id2);
        //}

        //public IActionResult UnderConstraction()
        //{
        //    MtdGetDashboardSession("Under Constraction", "");
        //    ModelUserDTO _data = new();
        //    return View(_data);
        //}

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
