using System.ComponentModel.DataAnnotations;

namespace APEL.Models
{
    public class ModelHrLampiran
    {
        public int PENCIPTA_FK { set; get; }
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public string? AUDIO { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public string? NO_REPORT { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public string? SIASATAN_PK_ENC { set; get; }
        public int SIASATAN_PK { set; get; }

        //RUJUK KEPADA HR_INV_LAMPIRAN
        public int INV_LAMPIRAN_PK { set; get; }
        public string? INV_LAMPIRAN_PK_ENC { set; get; }
        public int SIASATAN_FK { set;get; }
        public string? KATEGORI_KOD_FK { set; get; }
        public int KOD_PRNN_PNYST_FK { set; get; }
        public string? KOD_PRNN_PNYST_FK_ENC { set; get; }
        public string? RAKAMAN_PRCKPN_FK { set; get; }
        public string? KOD_LMPRN_RKMN { set; get; }
        public string? TAJUK { set; get; }
        public string? CTTN_LMPRN { set; get; }
        public string? URL { set; get; }
        public string? PATH { set; get; }
        public string? NAMA_FAIL { set; get; }
        public string? DATE_TKHCIPTA { set; get; }
        public string? MASA_TKHCIPTA { set; get; }
        public string? DATE_TKHKEMASKINI { set; get; }
        public string? MASA_TKHKEMASKINI { set; get; }
        public string? FILE { set;get; }
        public string? FILE_EXT { set; get; }

        [DataType(DataType.MultilineText)]
        public string? CATATAN_LAMPIRAN { set; get; }

        //RUJUK KEPADA HR_BK_LAMPIRAN
        public int LAMPIRAN_PK { set; get; }
        public string? LAMPIRAN_PK_ENC { set; get; }
        public byte[]? FAIL { set; get; }

        //RUJUK KEPADA HR_INV_PERIBADI_PRCKPN
        public string? PERIBADI_PRCKPN_FK { set; get; }

        //RUJUK KEPADA HR_INV_DAFTAR_PNYST
        public int DAFTAR_PNYST_PK { set; get; }
        public string? KOD_PRNN_PNYST { set; get; }
    }
}
