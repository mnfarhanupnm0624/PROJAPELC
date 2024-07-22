//using System.Web.Mvc;

namespace APELC.Models
{
    public class ModelHrRakaman
    {
        public int PENCIPTA_FK { set; get; }
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public int KTGRI_PRCKPN_FK { set; get; }
        public int SIASATAN_FK { set; get; }
        public int KOD_PRNN_PNYST_FK { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }
        public string? SELECT { set; get; }
        public string? NAME { set; get; }
        public string? CHECKED { set; get; }
        public string? CHECKED2 { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public string? INFO_Pemohon_STAF { set; get; }
        public string? COMPLAINER_FK { set; get; }
        public string? NAMA_PEMOHON { set; get; }
        public string? NO_ID { set; get; }
        public string? NO_KP { set; get; }
        public string? KATEGORI_SAKSI { set; get; }
        public string? COMPLAINER_NO_KP { set; get; }
        public string? INFO_Pemohon_PELAJAR { set; get; }

        //RUJUK KEPADA HR_INV_DAFTAR_PNYST
        public int DAFTAR_PNYST_PK { set; get; }
        public string? KOD_PRNN_PNYST { set; get; }

        //RUJUK KEPADA HR_INV_PERIBADI_PRCKPN
        public int PERIBADI_PRCKPN_PK { set; get; }
        public string? PERIBADI_PRCKPN_PK_ENC { set; get; }
        public string? KOD_PRNN_PRCKPN { set; get; }
        public string? DATE_TKHCIPTA_RKMN { set; get; }
        public string? MASA_TKHCIPTA_RKMN { set; get; }
        public string? KATEGORI_BRG_FK { set; get; }

        //RUJUK KEPADA HR_INV_PERINCIAN_PRCKPN
        public int PERINCIAN_PRCKPN_PK { set; get; }
        public string? PERINCIAN_PRCKPN_PK_ENC { set; get; }
        public string? KOD_BRG_PRCKPN { set; get; }
        public string? STATUS_PRCKPN { set; get; }
        public string? DATE_TKHCIPTA_RKMN_PRNCN { set; get; }
        public string? MASA_TKHCIPTA_RKMN_PRNCN { set; get; }
        public string? DATE_TKHKEMASKINI_RKMN_PRNCN { set; get; }
        public string? MASA_TKHKEMASKINI_RKMN_PRNCN { set; get; }
        public string? BIODATA_AWAL { set; get; }
        public string? TEKS_PENGESAHAN { set; get; }
        public string? SUMPAH_AKUR_JANJI_FK { set; get; }
        public string? KATEGORI_PENTERJEMAH { set; get; }

        //[AllowHtml]
        public string? SOALAN_JAWAPAN { set; get; }

        // RUJUKAN KEPADA HR_INV_PRCKPN_STAF_LAIN2
        public string? INFO_SAKSI_STAF { set; get; }

        //RUJUK KEPADA HR_INV_PRCKPN_TERJEMAHAN
        public int PRCKPN_TERJEMAHAN_PK { set; get; }
        public string? PRCKPN_TERJEMAHAN_PK_ENC { set; get; }
        public string? KATEGORI_PENTERJEMAH_FK { set; get; }
        public string? PENTERJEMAH_FK { set; get; }

        //RUJUK KEPADA HR_INV_PRCKPN_PLJR
        public string? NO_KP_PELAJAR { set; get; }

        // TAMBAHAN CARIAN
        public string? CSIASATAN_PK_ENC { set; get; }
        public string? CKAT_PRCKPN { set; get; }
        public string? CJNS_KOD { set; get; }
        public int CKATEGORI_BRG_FK { set; get; }
        public string? CKOD_PRNN_PNYST_FK_ENC { set; get; }
        public string? CKOD_PRNN_PRCKPN { set; get; }
        public string? CKTGRI_PRCKPN_FK { set; get; }
    }
}
