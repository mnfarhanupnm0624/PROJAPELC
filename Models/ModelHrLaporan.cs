using System.ComponentModel.DataAnnotations;

namespace APEL.Models
{
    public class ModelHrLaporan
    {
        public int PENCIPTA_FK { set; get; }
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }

        //RUJUK KEPADA HR_INV_LPRN_SSTN
        public int LPRN_SSTN_PK { set; get; }
        public string? LPRN_SSTN_PK_ENC { set; get; }
        public string? TAJUK_KES { set; get; }
        public string? BTIRN_LPRN { set; get; }

        //RUJUK KEPADA HR_INV_RUMUSAN
        public int RUMUSAN_PK { set; get; }
        public string? RUMUSAN_PK_ENC { set; get; }
        public string? PERKARA { set; get; }
        public string? TARIKH { set; get; }
    }
}
