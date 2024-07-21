using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace APELC.Models
{
    public class ModelHrMessage
    {
        public string? RESULTSET { set; get; }
        public int PENCIPTA_FK { set; get; }

        //RUJUK KEPADA HR_INV_DAFTAR_PNYST
        public string? REPORT_NO { set; get; }
        public string? KOD_PRNN_PNYST { set; get; }
        public string? ID_PENGGUNA { set; get; }

        //RUJUK KEPADA HR_INV_MINIT_SSTN
        public int SIASATAN_PK { set; get; }
        public string? SIASATAN_PK_ENC { set; get; }
        public int PENGGUNA_FK { set; get; }
        public string? PENGGUNA_FK_ENC { set; get; }

        [DataType(DataType.MultilineText)]
        public string? CATATAN { set; get; }

        public string? DATE_CIPTA { set; get; }
        public string? MASA_CIPTA { set; get; }
    }
}
