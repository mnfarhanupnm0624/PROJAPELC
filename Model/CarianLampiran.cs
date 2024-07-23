//using APELC.LocalServices.ApelC;
using System.ComponentModel.DataAnnotations;

namespace APELC.Model
{
    public class CarianLampiran
    {
        //RUJUK KEPADA HR_BK_ADUAN
        public string? NO_REPORT { set;get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public string? CSIASATAN_PK_ENC { set; get; }

        //RUJUK KEPADA HR_INV_LAMPIRAN
        public string? INV_LAMPIRAN_PK_ENC { set; get; }
        public string? KATEGORI_KOD_FK { set; get; }

        [DataType(DataType.MultilineText)]
        public string? CATATAN_LAMPIRAN { set; get; }
        public int KOD_PRNN_PNYST_FK { set; get; }
        public string? KOD_PRNN_PNYST_FK_ENC { set; get; }
        public string? RAKAMAN_PRCKPN_FK { set; get; }
        public string? KOD_LMPRN_RKMN { set; get; }
        public string? TAJUK { set; get; }
        public string? CTTN_LMPRN { set; get; }
        public string? URL { set; get; }
        public byte[]? FAIL { set; get; }
        public string? PATH { set; get; }
        public string? NAMA_FAIL { set; get; }

        public ModelHrLampiran info { set; get; }
        public List<ModelHrLampiran> ListLampiran = new();

        // DDL List
        //public List<ModelParameterHr> ListKategoriKodPS = SiasatanProcess.ListKategoriKod("1014").ToList();
        //public List<ModelParameterHr> ListKategoriKodOKT = SiasatanProcess.ListKategoriKod("1015").ToList();
        //public List<ModelParameterHr> ListKategoriKodDocLain = SiasatanProcess.ListKategoriKod("1017").ToList();

        // Tambahan
        public string? SIASATAN_PK_ENC { set; get; }
    }
}
