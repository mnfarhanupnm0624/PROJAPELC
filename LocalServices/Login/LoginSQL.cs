using APELC.LocalShared;
using APELC.LocalServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Text;
using System.Net;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Net6HrPublicLibrary.Model;
using static LinqToDB.Reflection.Methods.LinqToDB;
using static LinqToDB.Sql;
using APELC.Model;



namespace APELC.LocalServices.Login
{
    public class LoginSQL
    {
        //internal static string SQL_GetPhoto()
        //{
        //    return @"
        //    SELECT 
        //        GAMBAR as PHOTO, '2' as RESULTSET 
        //    FROM 
        //       HR_GAMBAR HG
        //       INNER JOIN HR_STAF ST ON ST.MAKLUMAT_PERIBADI_FK = HG.MAKLUMAT_PERIBADI_FK AND ST.TKH_HAPUS IS NULL
        //    WHERE 
        //        ST.STAF_PK = :HRSTAFFK ";
        //}

        internal static string SQL_MtdTambahPenggunaAkaun()
        {
            return "@INSERT INTO apelc.APELC_PENGGUNA_UPNM(ID_PENGGUNA," +
                "KATA_LALUAN_PENGGUNA," +
                "JENIS_MODUL_PENGGUNA_UPNM_FK," +
                "SESSION_TIMEOUT," +
                "BIL_GAGAL_LOGIN," +
                "LINK_DIGITAL_SIGNATURE," +
                "LINK_PHOTO," +
                "STATUS_AKTIF_PENGGUNA_UPNM_FK," +
                "PENCIPTA_PENGGUNA_UPNM_FK)" +
                "VALUES(:ID_PENGGUNA," +
                ":KATA_LALUAN_PENGGUNA," +
                ":JENIS_MODUL_PENGGUNA_UPNM_FK," +
                ":SESSION_TIMEOUT," +
                ":BIL_GAGAL_LOGIN," +
                ":LINK_DIGITAL_SIGNATURE," +
                ":STATUS_AKTIF_PENGGUNA_UPNM_FK," +
                ":PENCIPTA_PENGGUNA_UPNM_FK)";
        }

        internal static string SQL_MtdTambahKatPenggunaAkaun()
        {
            return "@INSERT INTO apelc.APELC_PENGGUNA_KAT_UPNM(PENGGUNA_KAT_UPNM_FK," +
                "KAT_PENGGUNA_UPNM_FK," +
                "NO_UNIQUE_ID," +
                "LINK_PHOTO," +
                "STATUS_AKTIF_PENGGUNA_KAT_UPNM_FK," +
                "PENCIPTA_PENGGUNA_KAT_UPNM_FK)" +
                "VALUES(:PENGGUNA_KAT_UPNM_FK," +
                ":KAT_PENGGUNA_UPNM_FK," +
                ":NO_UNIQUE_ID," +
                ":LINK_PHOTO," +
                ":TKH_MULA," +
                ":TKH_TAMAT," +
                ":STATUS_AKTIF_PENGGUNA_KAT_UPNM_FK," +
                ":PENCIPTA_PENGGUNA_KAT_UPNM_FK)";
        }

        internal static string SQL_MtdTambahBtrnPenggunaAkaun()
        {
            return "@INSERT INTO apelc.APELC_PENGGUNA_BTRN_UPNM(PENGGUNA_KAT_BTRN_UPNM_FK," +
                "NAMA," +
                "ALAMAT_EMEL," +
                "NO_TELEFON," +
                ":PENCIPTA_PENGGUNA_BTRN_UPNM_FK)";
        }

        internal static string SQL_MtdTambahPerananAkaun()
        {
            return "@INSERT INTO apelc.APELC_PERANAN_UPNM(PENGGUNA_KAT_PERANAN_UPNM_FK," +
                "JENIS_PERANAN_FK," +
                "PERANAN_UPNM_KURSUS_FK," +
                "TKH_MULA," +
                "TKH_AKHIR," +
                "CAJ_PERANAN_UPNM_FK," +
                "PUSAT_KOS_PERANAN_UPNM_FK," +
                "BAKI_AMAUN," +
                "KOD_MATAWANG_PERANAN_UPNM_FK," +
                "STATUS_AKTIF_PERANAN_UPNM_FK," +
                "PENCIPTA_PERANAN_UPNM_FK)" +
                "VALUES(:PENGGUNA_KAT_PERANAN_UPNM_FK," +
                ":JENIS_PERANAN_FK," +
                ":PERANAN_UPNM_KURSUS_FK," +
                ":TKH_MULA," +
                ":TKH_AKHIR," +
                ":CAJ_PERANAN_UPNM_FK," +
                ":PUSAT_KOS_PERANAN_UPNM_FK," +
                ":BAKI_AMAUN," +
                ":KOD_MATAWANG_PERANAN_UPNM_FK," +
                ":STATUS_AKTIF_PERANAN_UPNM_FK," +
                ":PENCIPTA_PERANAN_UPNM_FK)";
        }

        internal static string SQL_MtdListKatPerananSuperuserDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(608,612,613,614,615,616)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPerananPelajarDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(611,612,613,614,616)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPerananLuarUPNMDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(612,613,614,616)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPerananUrusetiaAPELDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(609,612,613,614,616)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPerananUrusetiaFakultiDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(612,613,614,616,755)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPerananBendahariDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(610,612,613,614,616)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPerananPenasihatAkadDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 158 " +
                "AND PARAM_PK IN(612,613,614,615,616)" +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListKatPenggunaDaftarAkaun()
        {
            return "@SELECT PARAM_PK as Key," +
                " NAMA_PARAMETER as NAMA_PARAMETER," +
                "NAMA_PARAMETER_EN as NAMA_PARAMETER_EN" +
                "STATUS_AKTIF as STATUS_AKTIF" +
                " FROM apelc.APELC_PARAMETER " +
                " WHERE KUMPULAN_FK = 157 " +
                " AND STATUS_AKTIF = 'Y' " +
                "AND TKH_HAPUS IS NULL" +
                " ORDER BY PARAM_PK";
        }

        internal static string SQL_MtdListJenisAPEL()
        {
            return @"
                SELECT PARAM_PK as Key,
                NAMA_PARAMETER as SelectListjenisApel,
                NAMA_PARAMETER_EN as SelectListjenisApel_EN
                STATUS_AKTIF as STATUS_AKTIF
                FROM apelc.APELC_PARAMETER
                WHERE KUMPULAN_FK = 180 
                AND STATUS_AKTIF = 'Y' 
                AND NAMA_PARAMETER = 'APEL.C'
                AND TKH_HAPUS IS NULL
                ORDER BY PARAM_PK";
        }


        internal static string SQL_GetKatPenggunaACL()
        {
            return @"
                SELECT NAMA_PARAMETER as KATEGORI_PERANAN
                FROM apelc.APELC_PARAMETER 
                WHERE KUMPULAN_FK = 1 AND STATUS_AKTIF = 'Y' AND TKH_HAPUS IS NULL";
        }

        //internal static string SQL_GetStatusAktifStaf()
        //{
        //    return @"
        //        SELECT NAMA_PARAMETER as STATUS_AKTIF_STAF
        //        FROM apelc.APELC_PARAMETER 
        //        WHERE KUMPULAN_FK = 10 AND STATUS_AKTIF = 'Y' AND TKH_HAPUS IS NULL";
        //}

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
        //internal static string SQL_MtdGetPenggunaApelCUPNM()
        //{
        //    return @"SELECT 
        //        KAT_PENGGUNA_UPNM_FK AS KAT_PENGGUNA_UPNM_FK,
        //        NO_PEKERJA AS NO_PEKERJA,
        //        STATUS_PILIH_NO_STAF AS STATUS_PILIH_NO_STAF_FK,
        //        NO_MATRIK AS NO_MATRIK,
        //        STATUS_PILIH_NO_MATRIK AS STATUS_PILIH_NO_MATRIK_FK,
        //        NO_KP AS NO_KP,
        //        STATUS_PILIH_NO_KP AS STATUS_PILIH_NO_KP_FK,
        //        KATA_LALUAN_PENGGUNA AS KATA_LALUAN_PENGGUNA,
        //        JENIS_MODUL_PENGGUNA_UPNM AS JENIS_MODUL,
        //        SESSION_TIMEOUT AS SESSION_TIMEOUT,
        //        STATUS_AKTIF_PENGGUNA_UPNM AS STATUS_AKTIF_PENGGUNA_UPNM_FK
        //        FROM apelc.APELC_PERANAN_UPNM
        //        WHERE";
        //}
        //internal static string SQL_MtdStatusAktifPengguna()
        //{
        //    string _SQL = SQL_MtdGetPenggunaApelCUPNM +
        //        @"STATUS_AKTIF_PENGGUNA_UPNM_FK=90 AND STATUS_AKTIF_STAF_FK=90 AND TKH_HAPUS IS NULL ORDER BY PARAM_PK";

        //    //_SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

        //    return _SQL;
        //}       

        //internal static string SQL_MtdGetPerananPenggunaApelC()
        //{
        //    return @"SELECT distinct(B.KOD_FUNGSI) 
        //                           FROM APELC_PERANAN_UPNM A, APELC_PERANAN_SJRH_UPNM B,APELC_PENGGUNA_UPNM C 
        //                          WHERE A.ID_PENGGUNA = :ID_PENGGUNA
        //                            AND A.KOD_PERANAN = B.KOD_PERANAN 
        //                            AND (B.KOD_FUNGSI like 'HM95%' OR B.KOD_FUNGSI = 'DEVELOPER') 
        //                            AND (sysdate between A.TKH_MULA and A.TKH_TAMAT)
        //                            AND A.TKH_HAPUS is null
        //                             ORDER BY PENGGUNA_UPNM_PK ";
        //}
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
