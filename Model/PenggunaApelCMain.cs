using APELC.LocalServices.Public;
using APELC.LocalServices.Login;
using APELC.LocalShared;

namespace APELC.Model
{
    public class PenggunaApelCMain
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public string? PENGGUNA_UPNM_PK_ENC { set; get; }
        public string? KAT_PENGGUNA { set; get; }
        public string? KATA_LALUAN { set; get; }
        public string? JENIS_MODUL { set; get; }
        public string? JENIS_MODUL_EN { set; get; }
        public string? NOKP { set; get; }

        public string? STATUS_PILIH_NO_KP_FK { set; get; }


        //RUJUK KEPADA MAKLUMAT_PERIBADI_STAF
        public string? NOPEKERJA { set; get; }
        public string? STATUS_PILIH_NO_STAF_FK { set; get; }

        //RUJUK KEPADA MAKLUMAT_PERIBADI_PELAJAR
        public string? NOMATRIK { set; get; }
        public string? STATUS_PILIH_NO_MATRIK_FK { set; get; }
        // Pengguna
        public string? TKH_CIPTA { set; get; }
        public string? PENCIPTA_PENGGUNA_UPNM_FK { set; get; }


        public List<ModelPemohonApelC> ListCarianPemohon = new();
        //public List<ModelHrStafPenyiasat> ListCarianStaf = new();
        public List<ModelParameterAPEL> JenisAPEL = new LoginProcess.ListJenisAPEL().ToList();
        //public ModelHrStaffMaklumatPeribadi stafPeribadi = new();
        //public ModelMaklumatPeribadiPelajar pelajarInfo = new();

        // DDL List
        //public List<ModelParameterAPEL> ListJenisAPEL = PublicServices.ListBahagianAll("").ToList();
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
        //public List<ModelParameterHr> ListKatAduan = StatistikProcess.ListKatAduan().ToList();
        //public List<ModelParameterHr> ListKatPemohon = StatistikProcess.ListKatPemohon().ToList();
        //public List<ModelParameterHr> ListStsAduan = StatistikProcess.ListStsAduan().ToList();
        //public List<ModelParameterHr> ListJawatanAll = StatistikProcess.ListJawatanAll("KP").ToList();
    }
}

