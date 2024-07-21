namespace APELC.Models
{
    public class CarianSiasatan
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public int PENCIPTA_FK { set; get; }


        //RUJUK KEPADA HR_BK_ADUAN
        public int ADUAN_PK { set; get; }
        public string? ADUAN_PK_ENC { set; get; }

        //RUJUK KEPADA HR_BK_SIASATAN
        public string? CSIASATAN_PK_ENC { set; get; }

        public ModelHrPolis info = new();
        public ModelHrPengadu _eventInfo = new();
        public ModelHrStaffPeribadi stafPeribadi = new();
        public ModelPelajarPeribadi pelajarInfo = new();

        // List
        public List<ModelHrStafPenyiasat> ListCarianStaf = new();
    }
}
