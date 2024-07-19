using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
//using System.Web.Mvc;

namespace APEL.Models
{
    public class ModelHrPengadu
    {
        public string? RESULTSET { set; get; }
        public int PENCIPTA_FK { set; get; }
        public string? EMAIL { set; get; }

        //RUJUK KEPADA HR_BK_ADUAN
        public int ADUAN_PK { set; get; }
        public string? ADUAN_PK_ENC { set; get; }
        public string? REPORT_NO { set; get; }
        public string? TKH_ADUAN { set; get; }
        public string? DATE_TKH_ADUAN { set; get; }
        public string? MASA_TKH_ADUAN { set; get; }
        public string? COMPLAINER_FK { set; get; }
        public string? COMPLAINER_FK_ENC { set; get; }
        public string? COMPLAINER_NO_KP { set; get; }
        public string? COMPLAINER_NO_KP_ENC { set; get; }
        public string? NAMA_PENGADU { set; get; }
        public string? NO_ID { set; get; }
        public string? NO_KP { set; get; }
        public string? MAKLUMAT_PERIBADI_FK { set; get; }
        public string? INFO_PENGADU_STAF { set; get; }
        public string? INFO_PENGADU_PELAJAR { set; get; }
        public string? INFO_LAIN { set; get; }
        public string? ACCEPTER_FK { set; get; }
        public string? CATATAN_ADUAN { set; get; }
        public string? BAHASA { set; get; }
        public string? KAMPUS_DESC { set; get; }
        public string? ZON { set; get; }
        public string? LOCATION_RECORD { set; get; }
        public string? UNIT { set; get; }
        public string? CATEGORY_PENGADU { set; get; }
        public string? SUB_CATEGORY_PENGADU { set; get; }

        //RUJUK KEPADA HR_BK_TINDAKAN
        public string? STATUS_FK { set; get; }
        public string? STATUS_DESC { set; get; }
        public string? KATEGORI_KES_DESC { set; get; }

        //RUJUK KEPADA HR_INV_SIASATAN
        public int SIASATAN_PK { set; get; }
        public int SIASATAN_FK { set; get; }
		public string? SIASATAN_PK_ENC { set; get; }
        public string? TAJUK_RNGKSN { set; get; }
        public string? AKBT_KJDN { set; get; }
        public string? STATUS_SSTN { set; get; }

        //RUJUK KEPADA HR_INV_PENGESAHAN
        public int INV_PENGESAHAN_PK { set; get; }
        public int? APPROVAL_ROLE_PK { set; get; }
        public string? STS_RNGKSAN { set; get; }
        public string? STS_LAPORAN { set; get; }
        public string? KATEGORI_PENGESAHAN_FK { set; get; }

		//[AllowHtml]
        public string? RMSN_AWAL { set; get; }

        [DataType(DataType.MultilineText)]
        public string? BTRN_ULSN { set; get; }

        //Datatable Status
        public string? LABEL_STATUS { set; get; }

        //TAMBAHAN FIELD UNTUK CARIAN SIASATAN
        public string? CNOADUAN { set; get; }
        public string? CKATEGORIPENGADU { set; get; }
        public string? CKAMPUS { set; get; }
        public string? CSTSPENGADU { set; get; }
        public string? CKATEGORIADUAN { set; get; }
        public string? CTKH_ADUANMULA { set; get; }
        public string? CTKH_ADUANTAMAT { set; get; }
    }
}
