//using Net6HrPublicLibrary.PublicShared;

namespace APELC.Models
{
    public class ModelHrPolis
    {
        public string? RESULTSET { set; get; }

        //RUJUK KEPADA HR_BK_LAMPIRAN
        public Byte[]? FAIL { set; get; }

        //RUJUK KEPADA HR_BK_LAPORAN_POLIS
        public int ADUAN_FK { set; get; }
        public int SIASATAN_FK { set; get; }
        public string? ADUAN_PK_ENC { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }
        public string? NO_LPRN_POLIS { set; get; }
        public string? DATE_TKH_LPRN { set; get; }
        public string? MASA_TKH_LPRN { set; get; }
        public string? KOD_RUJUKAN { set; get; }
        public string? HARI_TKH_LPRN { set; get; }

    }
}
