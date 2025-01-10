//using APELC.LocalServices.ApelC;
using APELC.LocalShared;

namespace APELC.Model
{
    public class CarianCatatan
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public int PENCIPTA_FK { set; get; }

        // RUJUK KEPADA HR_BLOK
        public string? CKAMPUS { set; get; }
        public string? KOD_ZON { set; get; }

        // DDL
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampusFk;
        //public List<ModelParameterHr> ListKampus = SiasatanProcess.ItemListKampusFk().ToList();

        // List
        public List<ModelHrCatatan> ListCatatanSstn = new();
    }
}
