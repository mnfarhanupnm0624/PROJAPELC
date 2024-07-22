namespace APELC.Models
{
    public class SessionModel
    {
        public string? RESULTSET { set; get; }
        public bool PAPARAN_ONLY { set; get; }

        //RUJUK KEPADA APELC_MOHON
        public int MOHON_PK { set; get; }
        public string? MOHON_PK_ENC { set; get; }
        public string? MOHON_NO { set; get; }
        public string? COMPLAINER_FK { set; get; }
        public string? COMPLAINER_NO_KP { set; get; }
        public string? MAKLUMAT_PERIBADI_FK { set; get; }

        //RUJUK KEPADA HR_BK_TINDAKAN
        public string? STATUS_FK { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public int SIASATAN_PK { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }
        public string? STATUS_MOHON { set; get; }

        //RUJUK KEPADA HR_INV_PENGESAHAN
        public string? STATUS_RAYUAN { set; get; }
        public string? STATUS_UJIAN_CBRN { set; get; }

        // Check Session Model
        public bool sessionValue { set; get; }

    }
}
