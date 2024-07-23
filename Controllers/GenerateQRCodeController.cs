using Microsoft.AspNetCore.Mvc;
//using APELC.PublicShared;
using APELC.LocalShared;
using APELC.Model;
//using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace APELC.Controllers
{
	public class GenerateQRCodeController : Controller
	{
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

        public IActionResult Index()
        {
            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
            if (_ePUserId != "")
            {
                MtdGetDashboardSession("");
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string _path01 = config["MySetting:ServerPathMain"];
                string _path = _path01;
                if (_path != null && _path.Length > 0)
                {
                    _path = '/' + _path01;
                }
                //var urlX = HttpContext.Request.GetEncodedUrl();
                //var url2 = HttpContext.Request.GetDisplayUrl();
                string _method = "MtdSkrinTimeLokasi";
                var url1 = HttpContext.Request.Scheme;
                var url2 = HttpContext.Request.Host;
                string _url = url1 + "://" + url2 + _path;
                ModelQRCode _data = new();
                string _anugerakPk = "1"; // GET VALUE FROM SYSTEM eg: AcaraProcess.MtdGetCurrentEventPk().ToString();
                string _getQrText = MtdGetCadanganQRText(_anugerakPk, _url, "", "", _method);
                _data.QRCodeText = _getQrText;
                _data.CEVENT = _anugerakPk;
                _data.CSEMAKMASA = "Y";
                return View(_data);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
        private void MtdGetDashboardSession(string id)
        {
            HttpContext.Session.SetString("_titleTop", "QR Code Generate");
            HttpContext.Session.SetString("_titleSmall", "Index");
        }

        //[HttpPost]
        //public IActionResult MtdGetQRCode(ModelQRCode _data)
        //{
        //    string _inputText = _data.QRCodeText;
        //    if (_inputText != null && _inputText.Length > 0)
        //    {
        //        QRCodeGenerator QrGenerator = new QRCodeGenerator();
        //        QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(_inputText, QRCodeGenerator.ECCLevel.Q);
        //        QRCode QrCode = new QRCode(QrCodeInfo);
        //        //Bitmap QrBitmap = QrCode.GetGraphic(60);
        //        //Bitmap QrBitmap = QrCode.GetGraphic(20, "#800000", "#FAEBD7");
        //        //Bitmap QrBitmap = QrCode.GetGraphic(20, Color.Maroon, Color.AntiqueWhite, (Bitmap)Bitmap.FromFile("C:\\logoUTMbw.jpg"));
        //        Bitmap QrBitmap = QrCode.GetGraphic(20, Color.Maroon, Color.AntiqueWhite, (Bitmap)Bitmap.FromFile("C:\\Log\\LogoUtm01.png"), 30);
        //        byte[] BitmapArray = QrBitmap.BitmapToByteArray();
        //        string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
        //        ViewBag.QrCodeUri = QrUri;
        //    }
        //    else
        //    {
        //        ViewBag.QrCodeUri = null;
        //    }
        //    return View("Index", _data);
        //}

        // GET AJAX : MtdGetQRCodeTextAjax
        [HttpPost]
        public JsonResult MtdGetQRCodeTextAjax([FromBody] ModelQRCode _carian)
        {
            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
            if (_ePUserId != "")
            {
                string _eventFk = _carian.CEVENT ?? "0";
                string _masaMula = _carian.CMASA_MULA ?? "";
                string _masaTamt = _carian.CMASA_TAMAT ?? "";
                string _semak = _carian.CSEMAKMASA ?? "T"; // Value Y-T
                string _method = "MtdSkrinLokasi";
                if (_semak == "Y") _method = "MtdSkrinTimeLokasi";
                var url1 = HttpContext.Request.Scheme;
                var url2 = HttpContext.Request.Host;
                string _url = url1 + "://" + url2 + _carian.CPATH;
                string _getQrText = MtdGetCadanganQRText(_eventFk, _url, _masaMula, _masaTamt, _method);
                return Json(_getQrText);
            }
            else
            {
                return Json("");
            }
        }

        // GET AJAX : MtdGetQRCodeTextTimeAjax
        [HttpPost]
        public JsonResult MtdGetQRCodeTextTimeAjax([FromBody] ModelQRCode _carian)
        {
            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
            if (_ePUserId != "")
            {
                string _eventFk = _carian.CEVENT ?? "0";
                string _masaMula = _carian.CMASA_MULA ?? "";
                string _masaTamt = _carian.CMASA_TAMAT ?? "";
                string _method = "MtdSkrinTimeLokasi";
                var url1 = HttpContext.Request.Scheme;
                var url2 = HttpContext.Request.Host;
                string _url = url1 + "://" + url2 + _carian.CPATH;
                string _getQrText = MtdGetCadanganQRText(_eventFk, _url, _masaMula, _masaTamt, _method);
                return Json(_getQrText);
            }
            else
            {
                return Json("");
            }
        }

        private string MtdGetCadanganQRText(string _eventFk, string _url, string _masaMula, string _masaTamat, string _method)
        {
            string _returnText = "";
            string _text = _masaMula + "~~~" + _masaTamat;
            //string _anugerakPkEnc = EncryptHr.NewEncrypt(_eventFk, _encryptCode);
            //string _timeEnc = EncryptHr.NewEncrypt(_text, _encryptCode);
            //_returnText = _url + "/Acara/MtdSkrinLokasi/" + _anugerakPkEnc + "&xlms=";
            //_returnText = _url + "/Acara/MtdSkrinTimeLokasi/" + _anugerakPkEnc + "&xlms=";
            //_returnText = _url + "/Acara/" + _method + "/" + _anugerakPkEnc + "&time=" + _timeEnc + "&xlms=";
            return _returnText;
        }
    }

    //Extension method to convert Bitmap to Byte Array
    public static class BitmapExtension
    {
//        public static byte[] BitmapToByteArray(this Bitmap bitmap)
//        {
//            using (MemoryStream ms = new MemoryStream())
//            {
//#pragma warning disable CA1416 // Validate platform compatibility
//                //bitmap.Save(ms, ImageFormat.Png);
//#pragma warning restore CA1416 // Validate platform compatibility
//                return ms.ToArray();
//            }
//        }
    }
}