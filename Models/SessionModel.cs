namespace APELC.Models
{
    public class SessionModel
    {
        public string? RESULTSET { set; get; }
        public bool PAPARAN_ONLY { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public int ADUAN_PK { set; get; }
        public string? ADUAN_PK_ENC { set; get; }
        public string? REPORT_NO { set; get; }
        public string? COMPLAINER_FK { set; get; }
        public string? COMPLAINER_NO_KP { set; get; }
        public string? MAKLUMAT_PERIBADI_FK { set; get; }

        //RUJUK KEPADA HR_BK_TINDAKAN
        public string? STATUS_FK { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public int SIASATAN_PK { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }
        public string? STATUS_SSTN { set; get; }

        //RUJUK KEPADA HR_INV_PENGESAHAN
        public string? STS_RNGKSAN { set; get; }
        public string? STS_LAPORAN { set; get; }

        // Check Session Model
        public bool sessionValue { set; get; }

    }
}
