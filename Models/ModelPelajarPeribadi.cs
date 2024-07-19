using System.ComponentModel.DataAnnotations;

namespace APEL.Models
{
    public class ModelPelajarPeribadi
    {
        public string? PENCIPTA_ID { set; get; }
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }

        //RUJUK KEPADA MAKLUMAT PELAJAR DARI AIMS TABLE
        //student info
        public string? NAMA_PELAJAR { set; get; }
        public string? NAMA_PROGRAM { set; get; }
        public string? NAMA_FAKULTI { set; get; }
        public string? MATRIK { set; get; }
        public DateTime TARIKH_MASUK { set; get; }
        public string? NAMA_PENASIHAT { set; get; }
        public string? NAMA_PENYELIA { set; get; }
        public string? NAMA_JENIS_PENGAJIAN { set; get; }
        public string? JENIS_PENGAJIAN { set; get; }
        public string? KOD_KAMPUS { set; get; }
        public string? KOD_FAKULTI { set; get; }

        public string? CONTACT_NO { set; get; }
        public string? SESISEM { set; get; }
        public string? EMAIL_RASMI { set; get; }

        //student particulars
        public string? SPR_NOKP { set; get; }
        public string? SBG_EMAIL_RASMI { set; get; }
        public string? SBG_EMAIL { set; get; }
        public string? SBG_HANDPHONE { set; get; }
        public DateTime DATEOB { set; get; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string? NEGARA_1 { set; get; }
        public string? NEGARA { set; get; }
        public string? AGAMA { set; get; }
        public string? KAUM { set; get; }
        public string? WARGANEGARA { set; get; }
        public string? JANTINA { set; get; }
        public string? KAHWIN { set; get; }
        public string? CACAT { set; get; }
        public string? KOD_PROGRAM { set; get; }
        public string? SSM_SESISEM { set; get; }
        public string? NEGERI { set; get; }
        public byte[]? SSP_PHOTO { set; get; }
        public string? SSP_PHOTO_STR { set; get; }
        public string? DASAR_KEMASUKAN { set; get; }
        public string? TELNO { set; get; }
        public string? STATE { set; get; }
        public string? CITY { set; get; }
        public string? POSTCODE { set; get; }
        public string? ADD2 { set; get; }
        public string? ADD1 { set; get; }
        public string? SAL_ALAMAT1 { set; get; }
        public string? SAL_ALAMAT2 { set; get; }
        public string? SAL_POSKOD { set; get; }
        public string? SAL_BANDAR { set; get; }
        public string? SAL_NEGERI { set; get; }
        public string? JMF_FACULTY { set; get; }
        public string? DATE_TKH_LAHIR { set; get; }
        public string? YEAR { set; get; }
        public string? MONTH { set; get; }


        //public List<ModelParameterHr> ListFakultiAll = PublicServices.ListFakulti2018().ToList();
        //public List<ModelParameterHr> ListJawatanAll = PublicServices.ListJawatanAll().ToList();
        //public List<ModelParameterHr> ListHrUnit = PublicServices.ListHrUnitByKodFakulti("").ToList();
        //public List<ModelParameterHr> ListJenisJawatan = PublicServices.ItemJenisPerkhidmatan().ToList();

        public string? PAPAR_TKH_LAHIR { set; get; }
        public string? NAMA_NEGERI { set; get; }
        public DateTime TKH_CIPTA { set; get; }
    }
}
