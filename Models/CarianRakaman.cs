using APEL.LocalServices.Siasatan;

namespace APEL.Models
{
    public class CarianRakaman
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public string? NO_KP { set; get; }
        public string? CSIASATAN_PK_ENC { set; get; }
        public string? KATEGORI_SAKSI { set; get; }

        //RUJUK KEPADA HR_INV_PERIBADI_PNYST
        public string? CPERIBADI_PRCKPN_PK_ENC { set; get; }
        public string? CKAT_PRCKPN { set; get; }
        public string? CKOD_PRNN_PRCKPN { set; get; }
        public string? CJNS_KOD { set; get; }
        public string? CKATEGORISAKSI { set; get; }

        //RUJUK KEPADA HR_INV_PERINCIAN_PRCKPN
        public string? CPERINCIAN_PRCKPN_PK_ENC { set; get; }


        public ModelHrStaffPeribadi stafPeribadi = new();
        public ModelHrRakaman butiran = new();
        public ModelPelajarPeribadi pelajarInfo = new();
        public List<ModelHrRakaman> ListRkmn = new();

        // DDL List
        //public List<ModelParameterHr> ListKatPrckpn = SiasatanProcess.ListKatPrckpn().ToList();
        //public List<ModelParameterHr> ListKatSaksi = SiasatanProcess.ListKatPengadu().ToList();
    }
}
