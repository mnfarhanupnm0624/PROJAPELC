﻿using System.ComponentModel.DataAnnotations;

namespace APEL.Models
{
    public class ModelHrStaffPeribadi
    {//RUJUK KEPADA HR_STAF
        public string? RESULTSET { set; get; }
        public int STAF_PK { set; get; }
        public string? STAF_PK_ENC { set; get; }
        public int UMUR_PILIHAN_PENCEN_FK { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_PENCEN { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_MULA_JPA { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_MULA_UTM { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_LANTIKAN_TERKINI { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_MULA_KONTRAK { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_SAH_JAWATAN { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_TAMAT_KONTRAK { set; get; }
        public string? GELARAN_PROFESSIONAL { set; get; }
        [Display(Name = "Staf No")]
        public string? NO_PEKERJA { set; get; }
        public string? NO_PEKERJA_ENC { set; get; }
        public string? TKH_HENTI { set; get; }
        public string? KOD_FAKULTI { set; get; }
        public string? KOD_PTJ { set; get; }
        public string? KOD_JAWATAN { set; get; }
        public string? KOD_PTJ_ASAL { set; get; }
        public string? KOD_JAWATAN_GILIRAN { set; get; }
        public string? TKH_KURSUS_BTN { set; get; }
        public string? STATUS_ASSTATK { set; get; }
        public string? STATUS_AKTIF { set; get; }
        public string? ASTARAFK { set; get; }
        public string? STATUS_BTN { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_LAPOR_DIRI { set; get; }
        public string? NO_FAIL { set; get; }
        public string? KOD_JAWATAN_HAKIKI { set; get; }
        public string? PUSAT_KOS { set; get; }
        public string? KOD_PTJ_PENEMPATAN { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_LANTIK_TETAP { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_LANTIKAN_AKADEMIK { set; get; }

        //RUJUK KEPADA HR_MAKLUMAT_PERIBADI
        public int? MAKLUMAT_PERIBADI_PK { set; get; }
        public int? ALAMAT_TETAP_FK { set; get; }
        public int? ALAMAT_MENYURAT_FK { set; get; }
        public int? ALAMAT_PEJABAT_FK { set; get; }
        public string? AGAMA { set; get; }
        public int? BANGSA_FK { set; get; }
        public int? TARAF_JAWATAN_SUB_FK { set; get; }
        public int? GELARAN_PERIBADI_FK { set; get; }
        public string? KEWARGANEGARAAN { set; get; }
        public string? KOD_NEGERI_LAHIR { set; get; }
        public string? STATUS_KAHWIN { set; get; }
        public string? KOD_WARGANEGARA { set; get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TKH_LAHIR { set; get; }
        public DateTime? TKH_MATI { set; get; }
        public string? JANTINA { set; get; }
        public string? NO_KP_LAMA { set; get; }

        [Display(Name = "NRIC No")]
        public string? NO_KP_BARU { set; get; }
        public string? NO_KP_BARU_ENC { set; get; }
        public string? TEMPAT_LAHIR { set; get; }

        [Display(Name = "Name")]
        public string? NAMA { set; get; }
        public string? WARNA_KP { set; get; }
        public string? NO_OKU { set; get; }
        public string? UMUR { set; get; }
        [Display(Name = "Email Address")]
        public string? EMAIL_RASMI { set; get; }
        public string? NO_BARCODE { set; get; }
        public string? EMAIL_KEDUA { set; get; }

        [Display(Name = "HP No")]
        public string? NO_HP { set; get; }
        public string? NO_TEL_BIMBIT { set; get; }
        public string? NO_TEL_PEJABAT { set; get; }
        public string? DESKRIPSI { set; get; }
        public string? DATE_TKH_LAHIR { set; get; }
        public string? YEAR { set; get; }
        public string? MONTH { set; get; }

        public string? PAPAR_TKHPENCEN { set; get; }
        public string? PAPAR_TKHLAHIR { set; get; }
        public string? PAPAR_TKHLANTIKTETAP { set; get; }
        public string? PAPAR_TKHSAHJAWATAN { set; get; }
        public string? PAPAR_TKHLANTIKANAKADEMIK { set; get; }
        public string? JAWATAN_KOD { set; get; }
        public string? JAWATAN_GRED { set; get; }
        public string? JAWATAN_KATEGORI { set; get; }
        public string? JAWATAN_KUMPULAN { set; get; }

        [Display(Name = "Position")]
        public string? JAWATAN_DESC { set; get; }
        public string? JAWATAN_BIDANG { set; get; }
        public string? JAWATAN_KLASIFIKASI { set; get; }
        public string? JAWATAN_KEPAKARAN { set; get; }
        public string? JAWATAN_AKADEMIK { set; get; }
        public string? TAHUN_ENC { set; get; }

        public string? TREK_DESC { set; get; }

        public string? FAKULTI_KOD { set; get; }

        [Display(Name = "Faculty")]
        public string? FAKULTI_DESC { set; get; }
        public string? FAKULTI_SINGKATAN { set; get; }
        public string? FAKULTI_PTJ { set; get; }
        public string? FAKULTI_PTJ_ASAL { set; get; }
        public string? KAMPUS_KOD { set; get; }

        [Display(Name = "Campus")]
        public string? KAMPUS_DESC { set; get; }
        public string? LPPT_DATA { set; get; }
        public string? LPPT_SKOP { set; get; }
        public string? LPPT_SKOP_TEXT { set; get; }
        public string? LPPT_KLASIFIKASI { set; get; }
        public string? PAPAR_TKHHAPUS { set; get; }
        public string? JENIS_JAWATAN { set; get; }

        //TAMBAHAN FIELD UNTUK CARIAN STAF
        public string? CKAMPUS { set; get; }
        public string? CFAKULTI { set; get; }
        public string? CJABATAN { set; get; }
        public string? CUNIT { set; get; }
        public string? CJAWATAN { set; get; }
        public string? CNOPEKERJA { set; get; }
        public string? CNOKP { set; get; }
        public string? CNAMA { set; get; }


        //public ModelHrKadMatrik? KadMatrik { set; get; }
        public ModelSmisKadAsas? KadAsas { set; get; }
        //public SimcCuti? MaklumatCuti { set; get; }

        public byte[]? PHOTOSTAF { set; get; }
        public string? PHOTOSTAFSTR { set; get; }
        public byte[]? WEBSITESTAFF { set; get; }

        //public List<ModelParameterHr> ListFakultiAll = PublicServices.ListFakulti2018().ToList();
        //public List<ModelParameterHr> ListJawatanAll = PublicServices.ListJawatanAll().ToList();
        //public List<ModelParameterHr> ListHrUnit = PublicServices.ListHrUnitByKodFakulti("").ToList();
    }
}
