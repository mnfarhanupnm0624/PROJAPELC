using APELC.LocalShared;

namespace APELC.LocalServices.Senarai
{
    public class SenaraiSql
    {
        readonly static string _studsmu = SecurityConstants.StudSchema();

        internal static string SqlGetApelPengaduInfo =
            @"SELECT
                A.ADUAN_PK AS ADUAN_PK,
                A.TKH_ADUAN,
                A.REPORT_NO,
                TO_CHAR(A.TKH_ADUAN,'DD/MM/YYYY') AS DATE_TKH_ADUAN,
                TO_CHAR(A.TKH_ADUAN,'HH:MI AM') AS MASA_TKH_ADUAN,
                A.COMPLAINER_FK,
                A.COMPLAINER_NO_KP,
                A.MAKLUMAT_PERIBADI_FK,
                DECODE (SUBSTR(A.REPORT_NO,1,2), 'PG','PAGOH','KL','KUALA LUMPUR','JB','JOHOR BAHRU') AS KAMPUS_DESC,
                (SELECT (UPPER(TRIM(B.NAMA)) || '~' || B.NO_PEKERJA || '~' || B.NO_KP_BARU) FROM HR_STAF A, HR_MAKLUMAT_PERIBADI B WHERE A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK 
                AND A.STAF_PK = A.COMPLAINER_FK AND A.TKH_HAPUS IS NULL AND B.TKH_HAPUS IS NULL AND A.STATUS_AKTIF = 'Y') AS INFO_PENGADU_STAF,
                (SELECT (UPPER(TRIM(NAMA)) || '~' || NO_KP_BARU) FROM HR_MAKLUMAT_PERIBADI WHERE MAKLUMAT_PERIBADI_PK = A.MAKLUMAT_PERIBADI_FK AND TKH_HAPUS IS NULL) AS INFO_LAIN,
                B.STATUS_KES_SSTN_FK AS STATUS_FK,
                B.ApelC_PK AS SIASATAN_PK
              FROM
                HR_BK_ADUAN A
                INNER JOIN HR_INV_SIASATAN B ON B.ADUAN_FK = A.ADUAN_PK AND B.TKH_HAPUS IS NULL 
            ";

        internal static string SQL_MtdGetPengaduList(string? _noaduan, string? _katPengadu, string? _cKampus, string? _cStsAduan, string? _cKatAduan, string? _cTkhMula, string? _cTkhTamat, string userAll, int stafPk)
        {
            string _SQL = SqlGetApelPengaduInfo;
            if (stafPk > 0)
            {
                _SQL += " INNER JOIN HR_INV_DAFTAR_PNYST C ON C.ApelC_FK = B.ApelC_PK AND C.TKH_HAPUS IS NULL";
                _SQL += " WHERE A.TKH_HAPUS IS NULL";
                _SQL += " AND C.STAF_PP_FK = " + stafPk + " ";
            }
            else
            {
                _SQL += " WHERE A.TKH_HAPUS IS NULL";

                if (userAll != null && userAll == "ALL")
                {
                }
            }
            if (_noaduan != null && _noaduan.Length > 0)
            {
                _SQL += " AND UPPER(A.REPORT_NO) like '%" + _noaduan + "%'  ";
            }
            if (_katPengadu != null && _katPengadu.Length > 0)
            {
                _SQL += " AND A.COMPLAINER_CATEGORY_FK = '" + _katPengadu + "'  ";
            }
            if (_cKampus != null && _cKampus.Length > 0)
            {
                _SQL += " AND SUBSTR(A.REPORT_NO,0,1) = '" + _cKampus + "'  ";
            }
            if (_cStsAduan != null && _cStsAduan.Length > 0)
            {
                _SQL += " AND A.STATUS_FK = '" + _cStsAduan + "'  ";
            }
            if (_cKatAduan != null && _cKatAduan.Length > 0)
            {
                _SQL += " AND A.REPORT_SUBCATEGORY_FK = '" + _cKatAduan + "'  ";
            }
            if (_cTkhMula != null && _cTkhMula.Length > 0 && _cTkhTamat != null && _cTkhTamat.Length > 0)
            {
                _SQL += " AND TO_DATE(TO_CHAR(A.TKH_ADUAN, 'DD/MM/YYYY'), 'DD/MM/YYYY') BETWEEN TO_DATE ('" + _cTkhMula + "', 'DD/MM/YYYY') AND TO_DATE ('" + _cTkhTamat + "','DD/MM/YYYY')  ";
            }

            _SQL += @" ORDER BY A.TKH_ADUAN DESC ";

            return _SQL;
        }
    }
}
