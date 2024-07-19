//using Net6HrPublicLibrary.PublicShared;
//using APEL.LocalServices.Public;
//using APEL.LocalShared;

//namespace APEL.Models
//{
//    public class CarianStaff
//    {
//        public string? RESULTSET { set; get; }
//        public string? RESULTSET_TEXT { set; get; }
//        public int PENCIPTA_FK { set; get; }
//        public string? CKAMPUS { set; get; }
//        public string? CFAKULTI { set; get; }
//        public string? CJABATAN { set; get; }
//        public string? CBAHAGIAN { set; get; }
//        public string? CNOPEKERJA { set; get; }
//        public string? CNOPEKERJA_ENC { set; get; }
//        public string? CNAMA { set; get; }
//        public string? CNOKP { set; get; }
//        public string? CJAWATAN { set; get; }
//        public int CSTAFFK { set; get; }
//        public string? CSTAFFK_ENC { set; get; }
//        public string? CTAHUN { set; get; }
//        public string? CANUGERAH_FK { set; get; }
//        public string? CKEHADIRAN { set; get; }

//        public ModelHrStaffPeribadi Peribadi = new();

//        public List<ModelHrStaffPeribadi> ListCarianStaf = new();
//        public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
//        public List<ModelParameterHr> ListFakultiAll = PublicServices.ListFakulti2018().ToList();
//        public List<ModelParameterHr> ListJabatan = PublicServices.ListBahagianAll("").ToList();
//        public List<ModelParameterHr> ListJawatanAll = PublicServices.ListJawatanAll().ToList();
//    }
//}
