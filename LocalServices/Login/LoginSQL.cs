using Microsoft.CodeAnalysis.CSharp.Syntax;
using Org.BouncyCastle.Asn1.X509;
using static LinqToDB.Reflection.Methods.LinqToDB;
using static LinqToDB.Sql;

namespace APELC.LocalServices.Login
{
    public class LoginSQL
    {
        internal static string SQL_GetPhoto()
        {
            return @"
            SELECT 
                GAMBAR as PHOTO, '2' as RESULTSET 
            FROM 
               HR_GAMBAR HG
               INNER JOIN HR_STAF ST ON ST.MAKLUMAT_PERIBADI_FK = HG.MAKLUMAT_PERIBADI_FK AND ST.TKH_HAPUS IS NULL
            WHERE 
                ST.STAF_PK = :HRSTAFFK ";
        }

        public static string SQL_LIST_KUMPULAN_MODUL()
        {
            return @"
            SELECT PARAM_PK AS PARAM_PK,
            NAMA_PARAMETER AS DESKRIPSI_MODUL,
            NAMA_PARAMETER AS DESKRIPSI_MODUL_EN 
            FROM apelc.APELC_PARAMETER WHERE KUMPULAN_FK=24 AND STATUS_AKTIF='Y'
            AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        public static string SQL_LIST_STATUS_HPK_CLO()
        {
            return @"
            SELECT PARAM_PK AS PARAM_PK,
            NAMA_PARAMETER AS DESKRIPSI_STATUS_HPK,
            NAMA_PARAMETER_EN AS DESKRIPSI_STATUS_HPK_EN 
            FROM apelc.APELC_PARAMETER WHERE KUMPULAN_FK=7 AND STATUS_AKTIF='Y'
            AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";

        }

        public static string SQL_LIST_KATEGORI_LOKASI_UJIAN_CBRN()
        {
            return @"
            SELECT PARAM_PK AS PARAM_PK,
            NAMA_PARAMETER AS DESKRIPSI_KATEGORI_LOKASI_UJIAN_CBRN,
            NAMA_PARAMETER_EN AS DESKRIPSI_KATEGORI_LOKASI_UJIAN_CBRN_EN 
            FROM apelc.APELC_PARAMETER WHERE KUMPULAN_FK=28 AND STATUS_AKTIF='Y'
            AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }


        internal static string SQL_GetKatPenggunaACL()
        {
            return @"
                SELECT NAMA_PARAMETER as KATEGORI_PERANAN
                FROM apelc.APELC_PARAMETER 
                WHERE KUMPULAN_FK = 1 AND STATUS_AKTIF = 'Y' AND TKH_HAPUS IS NULL";
        }

        internal static string SQL_GetStatusAktifStaf()
        {
            return @"
                SELECT NAMA_PARAMETER as STATUS_AKTIF_STAF
                FROM apelc.APELC_PARAMETER 
                WHERE KUMPULAN_FK = 10 AND STATUS_AKTIF = 'Y' AND TKH_HAPUS IS NULL";
        }

        //internal static string SQL_GetJenisPerananACL()
        //{
        //    return @"
        //        SELECT PARAM_PK,NAMA_PARAMETER as PERANAN_ACL,
        //        NAMA_PARAMETER as PERANAN_ACL_EN
        //        FROM apelc.APELC_PARAMETER 
        //        WHERE KUMPULAN_FK = 2 AND NAMA_PARAMETER='PENGGUNA SUPER' 
        //        AND STATUS_AKTIF = 'Y' AND TKH_HAPUS IS NULL 
        //        ORDER BY PARAM_PK;";
        //}
        //    // Get Info Staf UPNM
        internal static string SQL_MtdGetAclPengguna()
        {
            return @"SELECT 
                KAT_PENGGUNA_UPNM_FK AS KAT_PENGGUNA_UPNM_FK,
                NO_PEKERJA AS NO_PEKERJA,
                STATUS_PILIH_NO_STAF AS STATUS_PILIH_NO_STAF_FK,
                NO_MATRIK AS NO_MATRIK,
                STATUS_PILIH_NO_MATRIK AS STATUS_PILIH_NO_MATRIK_FK,
                NO_KP AS NO_KP,
                STATUS_PILIH_NO_KP AS STATUS_PILIH_NO_KP_FK,
                KATA_LALUAN_PENGGUNA AS KATA_LALUAN_PENGGUNA,
                JENIS_MODUL_PENGGUNA_UPNM AS JENIS_MODUL,
                SESSION_TIMEOUT AS SESSION_TIMEOUT,
                STATUS_AKTIF_PENGGUNA_UPNM AS STATUS_AKTIF_PENGGUNA_UPNM_FK
                FROM apelc.APELC_PERANAN_UPNM
                WHERE 
                STATUS_AKTIF_PENGGUNA_UPNM_FK=90 AND 
                STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL
                ORDER BY PARAM_PK; 
                 ";
        }
        internal static string SQL_MtdStatusAktifPengguna()
        {
            string _SQL = SQL_MtdGetAclPengguna +
                @" STATUS_AKTIF_PENGGUNA_UPNM_FK=90 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

            //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdListJenisAPEL()
        {
            return "@ SELECT PARAM_PK," +
                " NAMA_PARAMETER as JENIS_MODUL," +
                "NAMA_PARAMETER_EN as JENIS_MODUL_EN" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 24 AND " +
                "NAMA_PARAMETER='APEL.C'" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdGetPerananPenggunaApelC()
        {
            return @"SELECT distinct(B.KOD_FUNGSI) 
                                   FROM APELC_PERANAN_UPNM A, APELC_PERANAN_SJRH_UPNM B,APELC_PENGGUNA_UPNM C 
                                  WHERE A.ID_PENGGUNA = :ID_PENGGUNA
                                    AND A.KOD_PERANAN = B.KOD_PERANAN 
                                    AND (B.KOD_FUNGSI like 'HM95%' OR B.KOD_FUNGSI = 'DEVELOPER') 
                                    AND (sysdate between A.TKH_MULA and A.TKH_TAMAT)
                                    AND A.TKH_HAPUS is null
                                     ORDER BY PENGGUNA_UPNM_PK ";
        }
        //    internal static string SQL_MtdPentadbirAPELCWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=5 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdBendahariWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=6 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdPemohonWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=7 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdPengawasUjianCbrnWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=8 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }
        //    internal static string SQL_MtdPanelPenilaiWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=9 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdModeratorWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=10 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdPenasihatAkademikWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=11 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }


        //    internal static string SQL_MtdPenggubalWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=12 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdPenilaiInstrumenWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=13 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }



        //    internal static string SQL_MtdJKFakultiWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=14 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //    internal static string SQL_MtdSenatWujud()
        //    {
        //        string _SQL = SQL_MtdGetAclPeranan +
        //            @" AKSES_KWLN_STAF_FK=15 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL";

        //        //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //        return _SQL;
        //    }

        //}
    }
}
