//using APELC.LocalServices.Public;
//using APELC.LocalServices.ApelC;
using APELC.LocalShared;

namespace APELC.Model
{
    public class CarianPerananApelCMain
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public int PENCIPTA_FK { set; get; }
        public string? CNAMA { set; get; }
        public string? CJABATAN { set; get; }
        public string? CKAMPUS { set; get; }
        public string? CNOPEKERJA { set; get; }
        public string? CNOKP { set; get; }
        public string? CJAWATAN { set; get; }

        //RUJUK KEPADA MAKLUMAT_PERIBADI
        public string? NO_KP_BARU { set; get; }

        // Mohon
        public string? CNOMOHON { set; get; }
        public string? CKATEGORIPERANAN { set; get; }
        public string? CPERANAN { set; get; }
        public string? CSTSPERANAN { set; get; }
        public string? TKHMULA { set; get; }
        public string? TKHTAMAT { set; get; }

        //RUJUK KEPADA APELC_MOHON
        public string? CMOHON_PK_ENC { set; get; }

        public string? MOHON_NO { set; get; }

        public string? MOHON_PK_ENC { set; get; }

        public string? STATUS_MOHON { set; get; }

        //RUJUK KEPADA APELC_RAYUAN

        public string? CRAYUAN_PK_ENC { set; get; }

        public string? RAYUAN_PK_ENC { set; get; }

        public string? STATUS_RAYUAN { set; get; }

        //RUJUK KEPADA UJIAN_CBARAN_PEMOHON

        public string? STATUS_UJIAN_CABARAN { set; get; }

        public List<ModelPemohonApelC> ListCarianPemohon = new();
        //public List<ModelHrStafPenyiasat> ListCarianStaf = new();
        public List<ModelPemohonApelC> Pemohon = new();
        //public ModelHrStaffMaklumatPeribadi stafPeribadi = new();
        //public ModelMaklumatPeribadiPelajar pelajarInfo = new();

        // DDL List
        //public List<ModelParameterHr> ListJabatan = PublicServices.ListBahagianAll("").ToList();
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
        //public List<ModelParameterHr> ListKatAduan = StatistikProcess.ListKatAduan().ToList();
        //public List<ModelParameterHr> ListKatPemohon = StatistikProcess.ListKatPemohon().ToList();
        //public List<ModelParameterHr> ListStsAduan = StatistikProcess.ListStsAduan().ToList();
        //public List<ModelParameterHr> ListJawatanAll = StatistikProcess.ListJawatanAll("KP").ToList();
    }
}

