using Microsoft.AspNetCore.Mvc.Rendering;
using APELC.Model;
using System.Net;
using System.IdentityModel.Tokens.Jwt;

namespace APELC.LocalShared
{
	public class SecurityConstantsLocal
	{
        public static string SCREEN_CARIAN = "_Sc66%KLgHyr";
        public static string SCREEN_CARIAN_CONST = "_Scon%KLgHyr";

        //Constants to represent Access Right Level on Record Level
        public static int CAPAIAN_SEMUA = 1;
        public static int CAPAIAN_PTJ = 2;
        public static int CAPAIAN_DIRISENDIRI = 3;
        public static int CAPAIAN_UNIT = 4;
        public static int CAPAIAN_SUB_UNIT = 5;
        public static int CAPAIAN_KAMPUS = 6;
        public static int CAPAIAN_MULTI_PTJ = 8;
        public static int CAPAIAN_CROSS_KAMPUS = 9;
        
        public static string CAPAIAN2016_SEMUA = "A";
        public static string CAPAIAN2016_UNIVERSITI = "B";
        public static string CAPAIAN2016_MULTI_UNIVERSITI = "C";
        public static string CAPAIAN2016_KAMPUS = "D";
        public static string CAPAIAN2016_MULTI_KAMPUS = "E";
        public static string CAPAIAN2016_PTJ = "F";
        public static string CAPAIAN2016_MULTI_PTJ = "G";
        public static string CAPAIAN2016_JAB = "H";
        public static string CAPAIAN2016_MULTI_JAB = "I";
        public static string CAPAIAN2016_UNIT = "J";
        public static string CAPAIAN2016_MULTI_UNIT = "K";
        public static string CAPAIAN2016_DIRISENDIRI = "L";

        public static string EncryptCode()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["APELC:EncryptCode"];
        }

        //public static string ConnOraRujuk()
        //{
        //    return ConnMySQLUpnmDbDs();
        //}

        //public static string ConnMySQLUpnmDbDs()
        //{
        //    //return PublicConstant.ConnMySQLUpnmDbDs();
        //}


        //public readonly List<ModelParameterAPELC> ItemListKelulusanKP = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "BARU", KOD = "B" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "SOKONG", KOD = "S" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "DILULUSKAN", KOD = "L" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "TIDAK DILULUSKAN", KOD = "TL" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "DIBATALKAN", KOD = "BT" }
        //};


        public readonly List<SelectListItem> ItemListPleaseSelect = new List<SelectListItem>()
        {
            new SelectListItem { Text = "--Sila Pilih--", Value = "" }
        };

        //public readonly List<SelectListItem> ItemListDaftarYN = new List<SelectListItem>()
        //{
        //    new SelectListItem { Text = "YA", Value = "1" },
        //    new SelectListItem { Text = "TIDAK", Value = "2" }
        //};

//        internal static string MtdGetSideLocation()
//        {
//#pragma warning disable SYSLIB0014 // Type or member is obsolete
//            var webClient = new WebClient();
//#pragma warning restore SYSLIB0014 // Type or member is obsolete
//            var _json = webClient.DownloadString(@"C:/Log/LokasiEncrypt.txt");
//           //string _ipLokasi = EncryptHr.NewDecrypt(_json, EncryptCode());
//            string _first = _ipLokasi + ".";
//            Random rnd = new Random();
//            int dice = rnd.Next(46, 254);
//            string _back = _first + dice.ToString();
//            return _back;
//        }
        //public static List<ModelParameterAPELC> MtdGetDDStatusList = new()
        //{
        //    new ModelParameterAPELC { KOD = "344", NAMA_PARAMETER = "DRAFT" },
        //    new ModelParameterAPELC { KOD = "345", NAMA_PARAMETER = "SUBMITTED" },
        //    new ModelParameterAPELC { KOD = "355", NAMA_PARAMETER = "DELETED" },
        //    new ModelParameterAPELC { KOD = "1076", NAMA_PARAMETER = "VERIFIED" },
        //    new ModelParameterAPELC { KOD = "1077", NAMA_PARAMETER = "NOT VERIFIED" },
        //    new ModelParameterAPELC { KOD = "753", NAMA_PARAMETER = "PAY" },
        //    new ModelParameterAPELC { KOD = "349", NAMA_PARAMETER = "APPROVED" },
        //    new ModelParameterAPELC { KOD = "350", NAMA_PARAMETER = "DISAPPROVED" },
        //    new ModelParameterAPELC { KOD = "1022", NAMA_PARAMETER = "CORRECTION" }
        //};

        //public readonly List<ModelParameterAPELC> ItemListYaTidak = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "YA", KOD = "Y" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "TIDAK", KOD = "T" }
        //};

        //public readonly List<ModelParameterAPELC> ItemListYaBukan = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "YA", KOD = "1" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "BUKAN", KOD = "2" }
        //};

        //public readonly List<ModelParameterAPELC> ItemListYaBukanYN = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "YA", KOD = "Y" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "BUKAN", KOD = "N" }
        //};

        //public readonly List<ModelParameterAPELC> ItemListStatusDapatRawatanKllinik = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "DAFTAR", KOD = "4940" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "RAWATAN", KOD = "4941" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "CETAKAN", KOD = "4942" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "BATAL", KOD = "4943" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "SELESAI", KOD = "4935" }
        //};

        //internal readonly List<ModelParameterAPELC> ItemListKodGred = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "ALL", KOD = "ALL" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = ">= Gred 41 ", KOD = "> '40'" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "<= Gred 40 ", KOD = "< '41'" },
        //};

        //public readonly List<ModelParameterAPELC> ItemList_PleaseSelect = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "- Please Select -", KOD = "" }
        //};

        //public static string MtdGetMinitToJamText(int _minit)
        //{
        //    string _text = "0 Jam 00 Minit";
        //    if (_minit > 0)
        //    {
        //        int _bilJam = NumberHelper.ToIntiger((_minit / 60).ToString().Split('.')[0]);
        //        int _bilMin = _minit % 60;
        //        string _paparJam = _bilJam.ToString("00");
        //        string _paparMin = _bilMin.ToString("00");
        //        _text = _paparJam + " Jam " + _paparMin + " Minit  ";
        //    }
        //    return _text;
        //}
        //public static string MtdGetVerifyCodeApprovalSecture(int _paramPk)
        //{
        //    string _kod = "";
        //    switch (_paramPk)
        //    {
        //        case 4615:
        //            _kod = "99043"; break;
        //        case 4616:
        //            _kod = "99044"; break;
        //        case 4617:
        //            _kod = "99045"; break;
        //        case 4618:
        //            _kod = "99046"; break;
        //        case 4619:
        //            _kod = "99047"; break;
        //        case 4620:
        //            _kod = "99048"; break;
        //        case 4621:
        //            _kod = "99049"; break;
        //        case 4622:
        //            _kod = "99050"; break;
        //        case 4623:
        //            _kod = "99051"; break;
        //        case 4624:
        //            _kod = "99052"; break;
        //        case 4625:
        //            _kod = "99053"; break;
        //        case 4626:
        //            _kod = "99054"; break;
        //        case 4627:
        //            _kod = "99055"; break;
        //    }
        //    return _kod;
        //}
        //public static string MtdGetApproveCodeApprovalSecture(int _paramPk)
        //{
        //    string _kod = "";
        //    switch (_paramPk)
        //    {
        //        case 4615:
        //            _kod = "99056"; break;
        //        case 4616:
        //            _kod = "99057"; break;
        //        case 4617:
        //            _kod = "99058"; break;
        //        case 4618:
        //            _kod = "99059"; break;
        //        case 4619:
        //            _kod = "99060"; break;
        //        case 4620:
        //            _kod = "99061"; break;
        //        case 4621:
        //            _kod = "99062"; break;
        //        case 4622:
        //            _kod = "99063"; break;
        //        case 4623:
        //            _kod = "99064"; break;
        //        case 4624:
        //            _kod = "99065"; break;
        //        case 4625:
        //            _kod = "99066"; break;
        //        case 4626:
        //            _kod = "99067"; break;
        //        case 4627:
        //            _kod = "99068"; break;
        //    }
        //    return _kod;
        //}

        public static bool IsValidEmailAddress(string emailAddress)
        {
            return new System.ComponentModel.DataAnnotations
                                .EmailAddressAttribute()
                                .IsValid(emailAddress);
        }

        //public List<ModelParameterAPELC> ItemListBulan = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "01 - Januari", KOD = "01" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "02 - Februari", KOD = "02" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "03 - Mac", KOD = "03" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "04 - April", KOD = "04" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "05 - Mei", KOD = "05" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "06 - Jun", KOD = "06" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "07 - Julai", KOD = "07" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "08 - Ogos", KOD = "08" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "09 - September", KOD = "09" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "10 - Oktober", KOD = "10" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "11 - November", KOD = "11" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "12 - Disember", KOD = "12" }
        //};

        //internal readonly List<ModelParameterAPELC> ItemListKampus = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "JOHOR BAHRU", KOD = "J" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "KUALA LUMPUR", KOD = "K" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "HTP-PAGOH", KOD = "P" }
        //};

        //internal readonly List<ModelParameterAPELC> ItemListKampusFk = new List<ModelParameterAPELC>()
        //{
        //    new ModelParameterAPELC { NAMA_PARAMETER = "JOHOR BAHRU", KOD = "416" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "KUALA LUMPUR", KOD = "417" },
        //    new ModelParameterAPELC { NAMA_PARAMETER = "PAGOH", KOD = "1510" }
        //};

        internal static string GetFirstValueOf(string _text, int _maxLength)
        {
            if (string.IsNullOrEmpty(_text))
                return _text;
            return _text.Substring(0, Math.Min(_text.Length, _maxLength));
        }

        public static string StudSchema()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["MySetting:StudSchema"];
        }

        public static string AttachmentPath()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["MySetting:AttachmentPathSecurity"];
        }

    }
}
