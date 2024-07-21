//using APELC.LocalServices.Public;
using APELC.LocalServices.Aduan;
using APELC.LocalShared;

namespace APELC.Models
{
    public class CarianApelCMain
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

        //RUJUK KEPADA HR_MAKLUMAT_PERIBADI
        public string? NO_KP_BARU { set; get; }

        // Aduan
        public string? CNOADUAN { set; get; }
        public string? CKATEGORIADUAN { set; get; }
        public string? CKATEGORIPENGADU { set; get; }
        public string? CSTSPENGADU { set; get; }
        public string? TKHMULA { set; get; }
        public string? TKHTAMAT { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public string? CADUAN_PK_ENC { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        //public string? CADUAN_PK_ENC { set; get; }

        public List<ModelHrPengadu> ListCarianPengadu = new();
        //public List<ModelHrStafPenyiasat> ListCarianStaf = new();
        //public ModelHrPengaduMaklumat Pengadu = new();
        //public ModelHrStaffMaklumatPeribadi stafPeribadi = new();
        //public ModelMaklumatPeribadiPelajar pelajarInfo = new();

        // DDL List
        //public List<ModelParameterHr> ListJabatan = PublicServices.ListBahagianAll("").ToList();
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
        //public List<ModelParameterHr> ListKatAduan = StatistikProcess.ListKatAduan().ToList();
        //public List<ModelParameterHr> ListKatPengadu = StatistikProcess.ListKatPengadu().ToList();
        //public List<ModelParameterHr> ListStsAduan = StatistikProcess.ListStsAduan().ToList();
        //public List<ModelParameterHr> ListJawatanAll = StatistikProcess.ListJawatanAll("KP").ToList();
    }
}

