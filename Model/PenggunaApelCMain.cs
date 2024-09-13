using APELC.LocalServices.Public;
using APELC.LocalServices.Login;
using APELC.LocalShared;

namespace APELC.Model
{
    public class PenggunaApelCMain
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public string? Value { set; get; }

        public string? KOD_USER { set; get; }
        public string? SPR_NOKP { set; get; }

        //RUJUK KEPADA TABLE APELC_PENGGUNA_UPNM
        public string? PENGGUNA_UPNM_PK_ENC { set; get; }
        public string? ID_PENGGUNA { set; get; }
        public string? KATA_LALUAN_PENGGUNA { set; get; }
        public string? JENIS_MODUL_PENGGUNA_UPNM_FK { set; get; }
        public string? SESSION_TIMEOUT { set; get; }      
        public string? STATUS_AKTIF_PENGGUNA_UPNM_FK { set; get; }

        //RUJUK KEPADA APELC_PENGGUNA_SJRH_UPNM
        public string? PENGGUNA_SJRH_UPNM_PK_ENC { set; get; }
        public string? PENGGUNA_SJRH_UPNM_FK { set; get; }
        public string? KAT_PENGGUNA_SJRH_UPNM_FK { set; get; }
        public string? NO_UNIQUE_ID { set; get; }
        public string ? STATUS_AKTIF_PENGGUNA_SJRH_UPNM_FK { set; get; }

        //RUJUK KEPADA APELC_PENGGUNA_BTRN_UPNM
        public string? PENGGUNA_BTRN_UPNM_PK_ENC { set; get; }
        public string? PENGGUNA_SJRH_BTRN_UPNM_FK { set; get; }
        public string? NAMA { set; get; }
        public string? ALAMAT_EMEL { set; get; }
        public string ? NO_TELEFON { set; get; }

        //RUJUK KEPADA MAKLUMAT_PERIBADI_LUAR_PELAWAT
        public string? NO_KP { set; get; }
        public string? STATUS_PILIH_NO_KP { set; get; }


        //RUJUK KEPADA MAKLUMAT_PERIBADI_STAF
        public string? NO_PEKERJA { set; get; }
        public string? STATUS_PILIH_NO_STAF { set; get; }

        //RUJUK KEPADA MAKLUMAT_PERIBADI_PELAJAR
        public string? NO_MATRIK { set; get; }
        public string? STATUS_PILIH_NO_MATRIK { set; get; }

        //RUJUK KEPADA APELC_PERANAN_UPNM
        public string? PERANAN_UPNM_PK_ENC { set; get; }
        public string? PERANAN_SJRH_UPNM_FK { set; get; }

        public string? JENIS_PERANAN_FK { set; get; }

        public string? PERANAN_UPNM_KURSUS_FK { set; get; }

        public string? CAJ_MOHON_UPNM_FK { set; get; }

        public string? CAJ_RAYUAN_US_UPNM_FK { set; get; }
        public string? CAJ_RAYUAN_PS_UPNM_FK { set; get; }

        public string? CAJ_PERANAN_UPNM_FK { set; get; }

        public string? STATUS_AKTIF_PERANAN_UPNM_FK { set; get; }

        public string? STATUS_PELANTIKAN_FK { set; get; }
        public string? TKH_CIPTA { set; get; }
        public string? PENCIPTA_PERANAN_UPNM_FK { set; get; }

        public string? TKH_KEMASKINI { set; get; }
        public string? KEMASKINI_PERANAN_UPNM_FK { set; get; }

        public string? TKH_HAPUS { set; get; }
        public string? PENGHAPUS_PERANAN_UPNM_FK { set; get; }

        // Check Session Model
        public bool sessionValue { set; get; }


        public List<SessionModel> Pengguna = new();
        //public List<ModelHrStafPenyiasat> ListCarianStaf = new();
        //public ModelHrStaffMaklumatPeribadi stafPeribadi = new();
        //public ModelMaklumatPeribadiPelajar pelajarInfo = new();

        // DDL List
        public List<ModelParameterAPEL> JenisAPEL = new LoginProcess.ListJenisAPEL().ToList();
        //public List<ModelParameterAPEL> ListJenisAPEL = PublicServices.ListBahagianAll("").ToList();
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
        //public List<ModelParameterHr> ListKatAduan = StatistikProcess.ListKatAduan().ToList();
        //public List<ModelParameterHr> ListKatPemohon = StatistikProcess.ListKatPemohon().ToList();
        //public List<ModelParameterHr> ListStsAduan = StatistikProcess.ListStsAduan().ToList();
        //public List<ModelParameterHr> ListJawatanAll = StatistikProcess.ListJawatanAll("KP").ToList();
    }
}

