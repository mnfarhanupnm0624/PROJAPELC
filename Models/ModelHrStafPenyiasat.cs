using System.ComponentModel.DataAnnotations;

namespace APELC.Models
{
    public class ModelHrStafPenyiasat
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public int PENCIPTA_FK { set; get; }
        public string? CNAMA { set; get; }

        //RUJUK KEPADA HR_MAKLUMAT_PERIBADI
        public string? NAMA { set; get; }
        public string? NO_PEKERJA { set; get; }
        public string? NO_KP_BARU { set; get; }
        public string? JAWATAN_DESC { set; get; }
        public string? KAMPUS_DESC { set; get; }
        public int KATEGORI_SAKSI { set; get; }

        //RUJUK KEPADA HR_STAF
        public string? STATUS_AKTIF { set; get; }
        public string? STATUSAKTIF_DESC { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public int ADUAN_PK { set; get; }
        public string? MOHON_PK_ENC { set; get; }

        //RUJUK KEPADA HR_BK_TINDAKAN
        public string? STATUS_FK { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public int SIASATAN_PK { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }

        //RUJUK KEPADA HR_INV_DAFTAR_PNYST
        public int STAF_PP_FK { set; get; }
        public string? STAF_PP_FK_ENC { set; get; }
        public string? KOD_PRNN_PNYST { set; get; }
        public string? MASA_TKH_PNYST { set; get; }
        public string? DATE_TKH_PNYST { set; get; }
        public string? KATEGORI_KOD_FK { set; get; }
        public int SIASATAN_FK { set; get; }
        public int KOD_PRNN_PNYST_FK { set; get; }
        public int DAFTAR_PNYST_PK { set; get; }

        //RUJUK KEPADA HR_INV_PERIBADI_PRCKPN
        public int PERIBADI_PRCKPN_PK { set; get; }

        //RUJUK KEPADA HR_INV_PERINCIAN_PRCKPN
        public string? KOD_BRG_PRCKPN { set; get; }

        //RUJUK KEPADA HR_INV_PENGESAHAN
        [DataType(DataType.MultilineText)]
        public string? BTRN_ULSN { set; get; }
        public string? DATE_CIPTA { set;get; }
        public string? MASA_CIPTA { set; get; }
        public string? DATE_KEMASKINI { set; get; }
        public string? MASA_KEMASKINI { set; get; }


        //Datatable Status
        public string? LABEL_STATUS { set; get; }

        //TAMBAHAN FIELD UNTUK CARIAN DAFTAR PENYIASAT
        public string? CNOPEKERJA { set; get; }
        public string? CJAWATAN { set; get; }
        public string? CKAMPUS { set; get; }
        public string? CSIASATAN_PK_ENC { set; get; }
        public string? CSTSAKTIF { set; get; }
        public string? CKOD_PRNN_PRCKPN { set; get; }
        public string? CKOD_PRNN_PNYST_FK_ENC { set; get; }

        //Chat 
        public string? MOHON_NO { set; get; }
    }
}
