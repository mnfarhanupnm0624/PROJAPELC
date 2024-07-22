//using APELC.LocalServices.ApelC;
using APELC.LocalShared;

namespace APELC.Models
{
    public class CarianLaporan
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public int PENCIPTA_FK { set; get; }

        //RUJUK KEPADA HR_INV_LPRN_SSTN
        public int SIASATAN_FK { set; get; }
		public string? SIASATAN_PK_ENC { set; get; }
        public int LPRN_SSTN_PK { set; get; }
        public string? LPRN_SSTN_PK_ENC { set; get; }

        public string? TAJUK_KES { set; get; }
        public string? BTIRN_LPRN { set; get; }

        public ModelHrLaporan _laporan = new();

        // List
        public List<ModelHrLaporan> ListLaporanSstn = new();
        public List<ModelHrStafPenyiasat> ListCarianStaf = new();
        public List<ModelHrLaporan> ListRumusan = new();
    }
}
