using System.ComponentModel.DataAnnotations;

namespace APEL.Models
{
    public class ModelHrCatatan
    {
        public int PENCIPTA_FK { set; get; }
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }

        //RUJUK KEPADA HR_INV_CTTN_SSTN
        public int SIASATAN_FK { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }
        public string? TKH_CTTN { set; get; }    
        public string? MASA { set;get; }
        public int BLOK_FK { set; get; }
        public string? BLOK { set; get; }
        public string? DATE_TKH_CTTN { set; get; }
        public string? MASA_TKH_CTTN { set; get; }
        public int KOD_PRNN_PNYST_FK { set; get; }
        public string? KOD_PRNN_PNYST { set; get; }

        [DataType(DataType.MultilineText)]
        public string? CTTN_LOKASI { set; get; }

        [DataType(DataType.MultilineText)]
        public string? BUTIRAN_SSTN { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TKH_CTTN_DATETIME { get; set; }

        // RUJUK KEPADA HR_BLOK
        public string? KAMPUS { set; get; }
        public string? KOD_ZON { set; get; }
    }
}
