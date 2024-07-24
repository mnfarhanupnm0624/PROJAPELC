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

        internal static string SQL_GetKatPerananACL()
        {
            return @"
            SELECT NAMA_PARAMETER as KATEGORI_PERANAN
            FROM apelc.APELC_PARAMETER 
            WHERE KUMPULAN_FK = 1 AND STATUS_AKTIF = 'Y'";
        }

        // Get Info Staf UTM
        internal static string SQL_MtdGetAclPeranan =
            @"SELECT
                C.PERANAN_STAF_PK AS Key, 
                (SELECT H.NAMA_PERANAN FROM HR_ACL_PERANAN H WHERE H.PERANAN_PK = C.PERANAN_FK AND H.TKH_HAPUS IS NULL) AS ViewField, 
                '1' AS Value
            FROM
                HR_PT_MODUL A
                INNER JOIN HR_PT_SUBMODUL B ON B.PT_MODUL_FK = A.PT_MODUL_PK AND B.TKH_HAPUS IS NULL
                INNER JOIN HR_ACL_PERANAN_STAF C ON C.PT_SUBMODUL_FK = B.PT_SUBMODUL_PK AND C.TKH_HAPUS IS NULL
                INNER JOIN HR_STAF D ON D.STAF_PK = C.STAF_FK AND D.TKH_HAPUS IS NULL
            WHERE
             ";

        internal static string SQL_MtdSuperUserWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdPentadbirAPELCWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdPemohonWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdPanelPenilaiWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdModeratorWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdPengawasUjianCbrnWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdPenggubalWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdPenilaiInstrumenWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdBendahariWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdJKFakultiWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }

        internal static string SQL_MtdSenatWujud()
        {
            string _SQL = SQL_MtdGetAclPeranan +
                @" A.TKH_HAPUS IS NULL AND D.STATUS_AKTIF = 'Y' AND UPPER(A.NAMA_MODUL) = UPPER('apel')";

            _SQL += " AND C.STAF_FK = :STAF_FK AND C.PERANAN_FK = :PERANAN_FK ";

            return _SQL;
        }
        internal static string SQL_MtdBntuPnystWujud()
        {
            return @"
            SELECT
                 DAFTAR_PNYST_PK AS Key, 
                '1' AS Value
            FROM
                HR_INV_DAFTAR_PNYST
            WHERE
                TKH_HAPUS IS NULL AND STATUS_PNYST = 'Y'
                AND STAF_PP_FK = :STAF_FK
                 ";
        }
    }
}
