//using APEL.LocalServices.Public;
using APEL.LocalServices.Siasatan;
using APEL.LocalShared;

namespace APEL.Models
{
    public class CarianSiasatanMain
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
        public string? CSTSAKTIF { set; get; }

        //RUJUK KEPADA HR_MAKLUMAT_PERIBADI
        public string? NO_KP_BARU { set; get; }

        // Siasatan
        public string? CNOADUAN { set; get; }
        public string? CKATEGORIADUAN { set; get; }
        public string? CKATEGORIPENGADU { set; get; }
        public string? CSTSPENGADU { set; get; }
        public string? CTKH_ADUANMULA { set; get; }
        public string? CTKH_ADUANTAMAT { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public string? CADUAN_PK_ENC { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public string? CSIASATAN_PK_ENC { set; get; }

        public List<ModelHrPengadu> ListCarianPengadu = new();
        public List<ModelHrStafPenyiasat> ListCarianStaf = new();
        public ModelHrPengadu Pengadu = new();
        public ModelHrStaffPeribadi stafPeribadi = new();
        public ModelHrStaffPeribadi stafPeribadi2 = new();
        public ModelPelajarPeribadi pelajarInfo = new();
        public ModelHrVisitorPeribadi visitorinfo = new();

        // Chat
        public string? CPEGCHAT { set; get; }
        public ModelHrStafPenyiasat pegChat = new();

        // DDL List
        //public List<ModelParameterHr> ListJabatan = PublicServices.ListBahagianAll("").ToList();
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
        //public List<ModelParameterHr> ListKatAduan = SiasatanProcess.ListKatAduan().ToList();
        //public List<ModelParameterHr> ListKatPengadu = SiasatanProcess.ListKatPengadu().ToList();
        //public List<ModelParameterHr> ListStsAduan = SiasatanProcess.ListStsAduan().ToList();
        //public List<ModelParameterHr> ListJawatanAll = SiasatanProcess.ListJawatanAll("KP").ToList();
        //public List<ModelParameterHr> ListStsAktif = SiasatanProcess.ListStsAktif().ToList();
    }
}
