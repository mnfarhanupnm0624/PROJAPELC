using APEL.LocalServices.Senarai;
using APEL.LocalShared;

namespace APEL.LocalServices.Siasatan
{
    public class ApelQSQL
    {
        readonly static string _studsmu = SecurityConstants.StudSchema();

        // Get Info Session dari HR_BK_ADUAN dan HR_INV_SIASATAN
        internal static string SqlGetApelSessionInfo =
            @"SELECT
                ADUAN_PK,
                REPORT_NO,
                STATUS_FK,
                STATUS_SSTN,
                SIASATAN_PK,
                (SELECT NAMA_PARAMETER FROM SMU_PARAMETER S WHERE S.PARAM_PK = STATUS_RNGKSN) AS STS_RNGKSAN, 
                (SELECT NAMA_PARAMETER FROM SMU_PARAMETER S WHERE S.PARAM_PK = STATUS_LAPORAN) AS STS_LAPORAN
            FROM (SELECT
                        A.ADUAN_PK,
                        A.REPORT_NO,
                        A.STATUS_FK,
                        B.STATUS_SSTN,
                        B.SIASATAN_PK,
                        C.STATUS_FK AS STATUS_RNGKSN,
                        D.STATUS_FK AS STATUS_LAPORAN
                 FROM 
                        HR_BK_ADUAN A
                        INNER JOIN HR_INV_SIASATAN B ON B.ADUAN_FK = A.ADUAN_PK AND B.TKH_HAPUS IS NULL
                        LEFT JOIN HR_INV_PENGESAHAN C ON C.SIASATAN_FK = B.SIASATAN_PK AND C.TKH_HAPUS IS NULL AND C.KATEGORI_PENGESAHAN_FK = '5127'
                        LEFT JOIN HR_INV_PENGESAHAN D ON D.SIASATAN_FK = B.SIASATAN_PK AND D.TKH_HAPUS IS NULL AND C.KATEGORI_PENGESAHAN_FK = '5128'
             ";

        internal static string SQL_MtdGetApelSession()
        {
            string _SQL = SqlGetApelSessionInfo +
                @" WHERE
                       A.TKH_HAPUS IS NULL";

            _SQL += @" AND A.ADUAN_PK = :ADUAN_PK ";

            _SQL += @" ORDER BY C.TKH_CIPTA DESC, D.TKH_CIPTA DESC) WHERE ROWNUM = 1 ";

            return _SQL;
        }

        internal static string SQLGetNextValSEQ(string _tableName)
        {
            return @" SELECT " + _tableName + ".NEXTVAL FROM DUAL ";
        }

        // Get Info Staf UTM
        internal static string SQL_MtdGetMaklumatAsas =
            @"      SELECT 
                            A.NO_PEKERJA, 
                            UPPER(B.NAMA) AS NAMA, 
                            D.DESKRIPSI,
                            C.KOD_JAWATAN || ' - '|| D.DESKRIPSI AS JAWATAN_DESC,
                            SUBSTR(A.KOD_PTJ,0,3) || ' - '|| E.DESKRIPSI AS FAKULTI_DESC,
                            DECODE (SUBSTR(A.KOD_PTJ,1,1), 'P','PAGOH','K','KUALA LUMPUR','J','JOHOR BAHRU') AS KAMPUS_DESC,
                            A.STATUS_AKTIF,
                            A.STAF_PK AS STAF_PP_FK,
                            B.TKH_LAHIR, 
                            TO_CHAR(B.TKH_LAHIR,'DD/MM/YYYY') AS DATE_TKH_LAHIR,
                            (SELECT TRUNC(TO_NUMBER(SYSDATE - TO_DATE(B.TKH_LAHIR)) / 365.25) FROM DUAL) AS YEAR,
                            (TRUNC(MOD(MONTHS_BETWEEN(SYSDATE, TO_DATE(B.TKH_LAHIR, 'DD/MM/YYYY')),12))) AS MONTH,
                            B.NO_KP_BARU,
                            F.DESKRIPSI AS WARGANEGARA, 
                            G.NAMA_PARAMETER AS AGAMA, 
                            H.NAMA_PARAMETER AS BANGSA, 
                            I.NAMA_PARAMETER AS STATUS_KAHWIN,
                            J.NAMA_PARAMETER AS JANTINA, 
                            K.EMAIL_RASMI, 
                            K.EMAIL_KEDUA, 
                            L.DESKRIPSI AS NEGERI_LAHIR,
                            K.NO_TEL_PEJABAT,
                            M.NO_TEL_BIMBIT,
                            N.KOD_PRNN_PNYST,
                            TO_CHAR(N.TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKH_PNYST,
                            TO_CHAR(N.TKH_CIPTA,'HH:MI AM') AS MASA_TKH_PNYST
                    FROM 
                            HR_STAF A
                            LEFT JOIN HR_MAKLUMAT_PERIBADI B ON A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK
                            AND B.TKH_HAPUS IS NULL
                            LEFT JOIN HR_KOD_JAWATAN C ON A.KOD_JAWATAN = C.KOD_JAWATAN
                            AND C.TKH_HAPUS IS NULL
                            LEFT JOIN HR_KOD_JENIS_JAWATAN D ON C.KOD_JENIS_JAWATAN = D.KOD_JENIS_JAWATAN AND C.KOD_KLASIFIKASI = D.KOD_KLASIFIKASI
                            AND D.TKH_HAPUS IS NULL
                            LEFT JOIN HR_FAKULTI E ON SUBSTR(A.KOD_PTJ,0,3) = E.KOD_FAKULTI 
                            AND E.TKH_HAPUS IS NULL
                            LEFT JOIN ADM_NEGARA F ON B.KOD_WARGANEGARA = F.KOD_NEGARA 
                            AND F.TKH_HAPUS IS NULL
                            LEFT JOIN HR_PARAMETER G ON B.AGAMA = G.KOD AND G.KUMPULAN_FK = 5
                            AND G.TKH_HAPUS IS NULL
                            LEFT JOIN HR_PARAMETER H ON B.BANGSA_FK = H.PARAM_PK
                            AND H.TKH_HAPUS IS NULL
                            LEFT JOIN HR_PARAMETER I ON B.STATUS_KAHWIN = I.KOD AND I.KUMPULAN_FK = 6
                            AND I.TKH_HAPUS IS NULL
                            LEFT JOIN SMU_PARAMETER J ON B.JANTINA = J.KOD AND J.KUMPULAN_FK = 170
                            AND J.TKH_HAPUS IS NULL
                            LEFT JOIN HR_ALAMAT K ON B.ALAMAT_PEJABAT_FK = K.ALAMAT_PK
                            AND B.TKH_HAPUS IS NULL
                            LEFT JOIN ADM_NEGERI L ON B.KOD_NEGERI_LAHIR = L.KOD_NEGERI
                            AND L.TKH_HAPUS IS NULL
                            LEFT JOIN HR_ALAMAT M ON B.ALAMAT_MENYURAT_FK = K.ALAMAT_PK
                            AND M.TKH_HAPUS IS NULL
                            ";

        internal static string SQL_MtdGetMaklumatAsasByStafPk()
        {
            string _SQL = SQL_MtdGetMaklumatAsas +
                @"  LEFT JOIN HR_INV_DAFTAR_PNYST N ON N.STAF_PP_FK = A.STAF_PK
                            AND N.TKH_HAPUS IS NULL
                    WHERE 
                            A.TKH_HAPUS IS NULL ";

            _SQL += " AND A.STAF_PK= :STAF_PK  ";

            return _SQL;
        }

        internal static string SQL_MtdGetStafPnystList(string? _cNoPekerja, string? _cNama, string? _cKampus, string? _cJawatan, string? _cStsAktif, string userAll)
        {
            string _SQL = SQL_MtdGetMaklumatAsas +
                @"  LEFT JOIN HR_INV_DAFTAR_PNYST N ON N.STAF_PP_FK = A.STAF_PK
                            AND N.TKH_HAPUS IS NULL AND N.SIASATAN_FK = :SIASATAN_PK
                    WHERE 
                            A.TKH_HAPUS IS NULL ";

            if (userAll != null && userAll == "KP")
            {
                _SQL += " AND C.KOD_JAWATAN LIKE '" + userAll + "%'  ";
            }
            if (_cNoPekerja != null && _cNoPekerja.Length > 0)
            {
                _SQL += " AND A.NO_PEKERJA LIKE '" + _cNoPekerja + "%'  ";
            }
            if (_cNama != null && _cNama.Length > 0)
            {
                _SQL += " AND UPPER(B.NAMA) LIKE '%" + _cNama + "%'  ";
            }
            if (_cKampus != null && _cKampus.Length > 0)
            {
                _SQL += " AND SUBSTR(A.KOD_PTJ,1,1) = '" + _cKampus + "'  ";
            }
            if (_cJawatan != null && _cJawatan.Length > 0)
            {
                _SQL += " AND C.KOD_JAWATAN = '" + _cJawatan + "'  ";
            }
            if (_cStsAktif != null && _cStsAktif.Length > 0)
            {
                _SQL += "AND A.STATUS_AKTIF = '" + _cStsAktif + "'  ";
            }

            _SQL += @" ORDER BY N.KOD_PRNN_PNYST ASC NULLS LAST, A.STATUS_AKTIF DESC, NAMA ASC ";

            return _SQL;
        }

        internal static string SQL_GetInfoStafDetails()
        {
            string _SQL = SQL_MtdGetMaklumatAsas +
                @"  LEFT JOIN HR_INV_DAFTAR_PNYST N ON N.STAF_PP_FK = A.STAF_PK
                    WHERE 
                            A.TKH_HAPUS IS NULL ";

            _SQL += " AND B.NO_KP_BARU = :NO_KP  ";

            return _SQL;
        }

        // Get: Diari Penyiasat List
        internal static string SQL_MtdGetDiariPenyiasatList()
        {
            string _SQL = SQL_MtdGetMaklumatAsas +
                @"  INNER JOIN HR_INV_DAFTAR_PNYST N ON N.STAF_PP_FK = A.STAF_PK
                            AND N.TKH_HAPUS IS NULL AND N.SIASATAN_FK = :SIASATAN_PK
                    WHERE 
                            A.TKH_HAPUS IS NULL ";

            _SQL += " ORDER BY N.DAFTAR_PNYST_PK ASC ";

            return _SQL;
        }

        // Get Info Pelajar UTM
        //internal static string SqlGetApelStudentInfo =
        //    @"SELECT 
        //            B.SSM_MATRIK AS MATRIK, 
        //            B.SSM_NOKP AS SPR_NOKP, 
        //            UPPER(E.SPR_NAMA) AS NAMA_PELAJAR, 
        //            E.SPR_JANTINA, 
        //            E.SPR_UGAMA, 
        //            E.SPR_KAUM, 
        //            B.SSM_KURSUS, 
        //            UPPER(D.JMF_FACULTY) AS JMF_FACULTY, 
        //            E.SPR_TKHLAHIR, 
        //            E.SPR_NEGARA, 
        //            E.SPR_TKHMASUK, 
        //            G.KETERANGAN, 
        //            F.SPH_BLOK, 
        //            F.SPH_BILIK, 
        //            H.SBG_HANDPHONE, 
        //            H.SBG_EMAIL_RASMI, 
        //            TO_CHAR(E.SPR_TKHLAHIR,'DD/MM/YYYY') AS DATE_TKH_LAHIR,
        //            (SELECT TRUNC(TO_NUMBER(SYSDATE - TO_DATE(E.SPR_TKHLAHIR)) / 365.25) FROM DUAL) AS YEAR,
        //            (TRUNC(MOD(MONTHS_BETWEEN(SYSDATE, TO_DATE(E.SPR_TKHLAHIR, 'DD/MM/YYYY')),12))) AS MONTH,
        //            (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_KAUM AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM, 
        //            (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_KAUM  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM_PK, 
        //            (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_UGAMA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA, 
        //            (SELECT H.KOD FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_UGAMA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA_PK, 
        //            (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_JANTINA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA, 
        //            (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_JANTINA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA_PK,
        //            '1' as RESULTSET
        //            FROM AIMS.ssm_semester@" + _studsmu + " B, AIMS.sbp_peribadi@" + _studsmu + " E, AIMS.slg_mplog01@" + _studsmu + " C, AIMS.kod_fakulti@" + _studsmu + " D, " +
        //            "AIMS.SPH_PENEMPATAN@" + _studsmu + " F, AIMS.KOD_KOLEJ@" + _studsmu + " G, AIMS.SBG_PERIBADI@" + _studsmu + " H " +
        //            "WHERE B.SSM_SESISEM = C.SLG_SESISEM AND B.SSM_NOKP = F.SPH_NOKP(+) AND B.SSM_SESISEM = F.SPH_SESISEM(+) AND F.SPH_KOLEJ = G.KOD(+) AND F.SPH_DAFTAR(+) = 'Y' " +
        //            "AND F.SPH_SKELUAR(+) = 'T' AND B.SSM_NOKP = H.SBG_NOKP(+) AND C.SLG_KOD_PRO = 'AKIB' AND B.SSM_FAKULTI = D.JMF_KOD(+) AND B.SSM_NOKP=E.SPR_NOKP";

        internal static string SqlGetApelStudentInfo =
            @"SELECT
                    DISTINCT(SPR_NOKP) AS SPR_NOKP,
                    UPPER(SPR_NAMA) AS NAMA_PELAJAR,
                    SSM_MATRIK AS MATRIK,
                    SBG_HANDPHONE,
                    SBG_EMAIL_RASMI,
                    '1' as RESULTSET
            FROM
                    SENARAI.VW_BI_PELAJAR_ENROL";

        internal static string SQL_GetStudentRecord(string? _nokp)
        {
            string _SQL = SqlGetApelStudentInfo;
            if(_nokp == null) { _nokp = "~"; }

            _SQL += " WHERE SPR_NOKP = '" + _nokp + "' ";
            return _SQL;
        }


        // Get Info Asas Pelajar UTM
        internal static string SqlVwStudentEnrol =
            @"SELECT
                    (UPPER(TRIM(SPR_NAMA)) || '~' || SSM_MATRIK || '~' || SPR_NOKP)
              FROM
                    SENARAI.VW_BI_PELAJAR_ENROL
              WHERE";

        internal static string SQL_GetInfoPelajarDetails()
        {
            string _SQL = SqlVwStudentEnrol;

            _SQL += " SPR_NOKP = :NO_KP ";

            return _SQL;
        }

        //internal static string SQL_GetInfoPelajarDetails()
        //{
        //    string _SQL = "SELECT (UPPER(TRIM(E.SPR_NAMA)) || '~' || B.SSM_MATRIK || '~' || B.SSM_NOKP) " +
        //        "FROM AIMS.ssm_semester@" + _studsmu + " B, AIMS.sbp_peribadi@" + _studsmu + " E, AIMS.slg_mplog01@" + _studsmu + " C, " +
        //        "AIMS.kod_fakulti@" + _studsmu + " D, AIMS.SPH_PENEMPATAN@" + _studsmu + " F, AIMS.KOD_KOLEJ@" + _studsmu + " G, AIMS.SBG_PERIBADI@" + _studsmu + " H " +
        //        "WHERE B.SSM_SESISEM = C.SLG_SESISEM AND B.SSM_NOKP = F.SPH_NOKP(+) AND B.SSM_SESISEM = F.SPH_SESISEM(+) AND " +
        //        "F.SPH_KOLEJ = G.KOD(+) AND F.SPH_DAFTAR(+) = 'Y' AND F.SPH_SKELUAR(+) = 'T' AND B.SSM_NOKP = H.SBG_NOKP(+) AND " +
        //        "C.SLG_KOD_PRO = 'AKIB' AND B.SSM_FAKULTI = D.JMF_KOD(+) AND B.SSM_NOKP = E.SPR_NOKP ";

        //    _SQL += " AND B.SSM_NOKP = :NO_KP ";

        //    return _SQL;
        //}

        // Get Info Staf UTM
        internal static string SQL_MaklumatPeribadiVisitor =
            @"      SELECT 
                            UPPER(NAMA) AS NAMA_LAIN,
                            TKH_LAHIR AS TKH_LAHIR_LAIN, 
                            TO_CHAR(TKH_LAHIR,'DD/MM/YYYY') AS DATE_TKH_LAHIR_LAIN,
                            (SELECT TRUNC(TO_NUMBER(SYSDATE - TO_DATE(TKH_LAHIR)) / 365.25) FROM DUAL) AS YEAR,
                            (TRUNC(MOD(MONTHS_BETWEEN(SYSDATE, TO_DATE(TKH_LAHIR, 'DD/MM/YYYY')),12))) AS MONTH,
                            NO_KP_BARU AS NO_KP_BARU_LAIN
                    FROM 
                            HR_MAKLUMAT_PERIBADI 
                            ";

        internal static string SQL_GetDataVisitorByPk()
        {
            string _SQL = SQL_MaklumatPeribadiVisitor +
                @" WHERE 
                      TKH_HAPUS IS NULL ";

            _SQL += " AND MAKLUMAT_PERIBADI_PK = :MAKLUMAT_PERIBADI_FK ";

            return _SQL;
        }

        // Get Info Info Pengadu dari HR_BK_ADUAN
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
                A.ACCEPTER_FK,
                A.CATATAN_ADUAN,
                A.LOCATION_RECORD,
                A.STATUS_FK AS STATUS_FK,
                B.SIASATAN_PK AS SIASATAN_PK,
                B.TAJUK_RNGKSN,
                B.AKBT_KJDN,
                B.RMSN_AWAL,
                (SELECT U.NAMA_PARAMETER FROM SMU_PARAMETER U WHERE U.PARAM_PK = A.LANGUAGE_FK) AS BAHASA,
                (SELECT Z.NAMA_PARAMETER FROM SMU_PARAMETER Z WHERE Z.PARAM_PK = A.ZONE_FK AND Z.KUMPULAN_FK=65 AND Z.TKH_HAPUS IS NULL) AS ZON, 
                (SELECT U.NAMA_PARAMETER FROM SMU_PARAMETER U WHERE U.PARAM_PK = A.UNIT_FK) AS UNIT,
                DECODE (SUBSTR(A.REPORT_NO,1,2), 'PG','PAGOH','KL','KUALA LUMPUR','JB','JOHOR BAHRU') AS KAMPUS_DESC,
                (SELECT (UPPER(TRIM(NAMA)) || '~' || NO_KP_BARU) FROM HR_MAKLUMAT_PERIBADI WHERE MAKLUMAT_PERIBADI_PK = A.MAKLUMAT_PERIBADI_FK AND TKH_HAPUS IS NULL) AS INFO_LAIN,
                (SELECT Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.KUMPULAN_FK = 67 AND Y.KOD = A.REPORT_CATEGORY_FK AND Y.TKH_HAPUS IS NULL) AS CATEGORY_PENGADU,  
                (SELECT Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.PARAM_PK = A.REPORT_SUBCATEGORY_FK AND Y.TKH_HAPUS IS NULL) AS SUB_CATEGORY_PENGADU,
                (SELECT UPPER(S.NAMA_PARAMETER) FROM SMU_PARAMETER S WHERE S.KUMPULAN_FK = 94 AND S.PARAM_PK = B.KATEGORI_KES_FK AND S.TKH_HAPUS IS NULL) AS KATEGORI_KES_DESC
            FROM 
                HR_BK_ADUAN A
                INNER JOIN HR_INV_SIASATAN B ON B.ADUAN_FK = A.ADUAN_PK AND B.TKH_HAPUS IS NULL
             ";

        //  --(SELECT (UPPER(NAMA_PARAMETER)) FROM SMU_PARAMETER WHERE PARAM_PK = B.KATEGORI_KES_FK AND TKH_HAPUS IS NULL) AS KATEGORI_KES_DESC,

        internal static string SQL_MtdGetApel()
        {
            string _SQL = SqlGetApelPengaduInfo +
                @" WHERE
                        A.TKH_HAPUS IS NULL ";

            _SQL += @" AND A.ADUAN_PK = :ADUAN_PK ";

            return _SQL;
        }

        internal static string SQL_MtdGetInitialEvent()
        {
            string _SQL = SqlGetApelPengaduInfo +
                @" WHERE
                        A.TKH_HAPUS IS NULL ";

            _SQL += @" AND B.SIASATAN_PK = :SIASATAN_PK ";

            return _SQL;
        }

        // Get Info Staf Penyiasat dari value STAF_PP_FK - HR_INV_DAFTAR_PNYST
        internal static string SqlGetStafPenyiasat =
            @"SELECT
                A.ADUAN_PK AS ADUAN_PK,
                A.TKH_ADUAN,
                A.STATUS_FK AS STATUS_FK,
                A.REPORT_NO,
                C.SIASATAN_PK AS SIASATAN_PK,
                D.STAF_PP_FK AS STAF_PP_FK,
                D.KOD_PRNN_PNYST,
                TO_CHAR(D.TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKH_PNYST,
                TO_CHAR(D.TKH_CIPTA,'HH:MI AM') AS MASA_TKH_PNYST,
                (SELECT 
                    (UPPER(TRIM(B.NAMA)) || '~' || 
                    B.NO_PEKERJA || '~' ||
                    B.NO_KP_BARU) || '~' ||
                    C.KOD_JAWATAN || ' - '|| D.DESKRIPSI || '~' ||
                    (CASE 
                        WHEN SUBSTR(A.KOD_PTJ,1,1) = 'J' THEN 'JOHOR BAHRU' 
                        WHEN SUBSTR(A.KOD_PTJ,1,1)= 'K' THEN 'KUALA LUMPUR' 
                        WHEN SUBSTR(A.KOD_PTJ,1,1) = 'P' THEN 'PAGOH'  
                    ELSE '' END) || '~' ||
                    A.STATUS_AKTIF
                FROM 
                    HR_STAF A, HR_MAKLUMAT_PERIBADI B, HR_KOD_JAWATAN C, HR_KOD_JENIS_JAWATAN D 
                WHERE 
                    A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK AND A.TKH_HAPUS IS NULL AND B.TKH_HAPUS IS NULL 
                    AND A.KOD_JAWATAN = C.KOD_JAWATAN AND C.TKH_HAPUS IS NULL
                    AND C.KOD_JENIS_JAWATAN = D.KOD_JENIS_JAWATAN AND C.KOD_KLASIFIKASI = D.KOD_KLASIFIKASI AND D.TKH_HAPUS IS NULL
                    AND A.STAF_PK = D.STAF_PP_FK) AS CNAMA
            FROM 
                HR_BK_ADUAN A
                INNER JOIN HR_INV_SIASATAN C ON C.ADUAN_FK = A.ADUAN_PK AND C.TKH_HAPUS IS NULL
                INNER JOIN HR_INV_DAFTAR_PNYST D ON D.SIASATAN_FK = C.SIASATAN_PK AND D.TKH_HAPUS IS NULL
             ";

        internal static string SQL_MtdGetStafPenyiasatList()
        {
            string _SQL = SqlGetStafPenyiasat +
                @" WHERE
                       A.TKH_HAPUS IS NULL";

            _SQL += @" AND D.SIASATAN_FK = :SIASATAN_PK";

            _SQL += @" ORDER BY D.TKH_CIPTA DESC ";

            return _SQL;
        }

        internal static string SQL_GetMaklumatPenyiasatChat(string userAll)
        {
            string _SQL = SqlGetStafPenyiasat +
                @" WHERE
                        A.TKH_HAPUS IS NULL ";

            _SQL += @" AND D.SIASATAN_FK = :SIASATAN_PK";

            if (userAll != "ALL")
            {
                _SQL += @" AND D.STAF_PP_FK = :STAF_PK";
            }

            return _SQL;
        }

        internal static string SQL_MtdGetKodBorangPS(string? _kodPeranan)
        {
            return @"
            SELECT
                MAX(SUBSTR(KOD_BRG_PRCKPN, INSTR(KOD_BRG_PRCKPN, '.') + 1)) AS Nombor
            FROM 
                HR_INV_PERINCIAN_PRCKPN
            WHERE 
                TKH_HAPUS IS NULL AND PERIBADI_PRCKPN_FK = :PERIBADI_PRCKPN_FK
                AND KOD_BRG_PRCKPN LIKE '" + _kodPeranan + "%' ORDER BY PERINCIAN_PRCKPN_PK DESC";
        }

        internal static string SQL_GetInfoPeribadiPrckpn()
        {
            return @"
            SELECT 
                PERIBADI_PRCKPN_PK,
                KATEGORI_BRG_FK,
                KOD_PRNN_PRCKPN
            FROM 
                HR_INV_PERIBADI_PRCKPN 
            WHERE 
                TKH_HAPUS IS NULL
                AND PERIBADI_PRCKPN_PK = :PERIBADI_PRCKPN_PK ";
        }


        // Begin:
        // DDL
        internal static string SQL_ListKatAduan()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 67 AND AKTIF = 'Y'
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_ListKatPengadu()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 83 AND AKTIF = 'Y'
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_ListStsAduan()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_ListSenaraiZon()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_ListSenaraiBlok()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_ListSenaraiAras()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_ListSenaraiBilik()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_CAPAI_PENTERJEMAH_STAF_REKOD_HAPUS()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_CAPAI_PENTERJEMAH_LAIN_LAIN_REKOD_HAPUS()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 76 AND AKTIF = 'Y' AND PARAM_PK IN('378','380','381') 
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }

        public static string SqlJawatanList(string _kodjwtn)
        {
            return @"SELECT A.KOD_JAWATAN as Key, A.KOD_JAWATAN || ' - ' || B.DESKRIPSI as ViewField 
                    FROM HR_KOD_JAWATAN A, HR_KOD_JENIS_JAWATAN B 
                    WHERE A.KOD_JENIS_JAWATAN = B.KOD_JENIS_JAWATAN AND A.KOD_KLASIFIKASI = B.KOD_KLASIFIKASI 
                    AND A.KOD_JAWATAN LIKE '" + _kodjwtn + "%' AND A.TKH_HAPUS is null ORDER BY Key ";
        }

        internal static string SQL_ListStsAktif()
        {
            return @"
            SELECT 
                   KOD as Key,
                   NAMA_PARAMETER as ViewField
            FROM 
                   SMU_PARAMETER
            WHERE 
                   KUMPULAN_FK ='80'
                   ORDER BY KOD DESC ";
        }

        internal static string SQL_ListKatPrckpn()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField 
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 160 AND AKTIF = 'Y'
                AND TKH_HAPUS IS NULL
            ORDER BY PARAM_PK ";
        }
        internal static string SQL_GetPerananByKatPrckpnList()
        {
            return @"
            SELECT
                MAX(SUBSTR(KOD_PRNN_PRCKPN,2)) AS Nombor
            FROM 
                HR_INV_PERIBADI_PRCKPN 
            WHERE 
                TKH_HAPUS IS NULL AND SIASATAN_FK = :SIASATAN_FK
                AND KTGRI_PRCKPN_FK = :KTGRI_PRCKPN_FK
            ORDER BY PERIBADI_PRCKPN_PK DESC";
        }

        internal static string SQL_GetKodPeranan =
            @"SELECT 
                PERIBADI_PRCKPN_PK AS Key,
                KOD_PRNN_PRCKPN AS ViewField 
            FROM 
                HR_INV_PERIBADI_PRCKPN
             ";
        internal static string SQL_GetKodPerananAttch =
            @"SELECT 
                KOD_PRNN_PRCKPN AS Key,
                KOD_PRNN_PRCKPN AS ViewField 
            FROM 
                HR_INV_PERIBADI_PRCKPN
             ";
        internal static string SQL_GetKodPerananList(string? _kodperanan)
        {
            string _SQL = SQL_GetKodPeranan +
                @"WHERE
                     TKH_HAPUS IS NULL";

            if ((_kodperanan == "1017" || _kodperanan == "1018") && _kodperanan.Length > 0)
            {
                _SQL = SQL_GetKodPerananAttch +
                @"WHERE
                     TKH_HAPUS IS NULL";
                _SQL += @" AND SIASATAN_FK = :SIASATAN_FK ";
            }
            else
            {
                _SQL += @" AND SIASATAN_FK = :SIASATAN_FK AND KATEGORI_BRG_FK = :KATEGORI_BRG_FK";
            }

            _SQL += @" ORDER BY PERIBADI_PRCKPN_PK ASC";

            return _SQL;
        }
        internal static string SQL_GetKodPerananPerincianList()
        {
            return @"
            SELECT 
                PERINCIAN_PRCKPN_PK AS Key,
                KOD_BRG_PRCKPN AS ViewField 
            FROM 
                HR_INV_PERINCIAN_PRCKPN 
            WHERE 
                TKH_HAPUS IS NULL
                AND PERIBADI_PRCKPN_FK = :PERIBADI_PRCKPN_FK
            ORDER BY PERINCIAN_PRCKPN_PK ASC";
        }
        internal static string SQL_GetKodPerananPrckpn()
        {
            return @"
            SELECT 
                   KOD as Key,
                   NAMA_PARAMETER as ViewField
            FROM 
                   SMU_PARAMETER
            WHERE 
                   KUMPULAN_FK ='161'
                   AND PARAM_PK = :PARAM_PK
            ";
        }
        internal static string SQL_ListKampusFk()
        {
            return @"
            SELECT 
                PARAM_PK AS Key,
                NAMA_PARAMETER AS ViewField
            FROM
                SMU_PARAMETER
            WHERE
                TKH_HAPUS IS NULL AND KUMPULAN_FK = '81'
                AND PARAM_PK IN ('416','417','1510') AND AKTIF = 'Y' ";
        }
        internal static string SQL_GetKodZonList()
        {
            return @"
            SELECT 
                DISTINCT(KOD_ZON) AS Key,
                (SELECT NAMA_PARAMETER FROM SMU_PARAMETER A WHERE A.TKH_HAPUS IS NULL AND A.KUMPULAN_FK = '65' AND A.PARAM_PK = KOD_ZON) AS ViewField
            FROM 
                HR_BLOK 
            WHERE 
                TKH_HAPUS IS NULL AND KOD_KAMPUS = :KOD_KAMPUS
                ORDER BY KOD_ZON ASC ";
        }
        internal static string SQL_GetKodBlokList()
        {
            return @"
            SELECT 
                  DISTINCT(BLOK_PK) AS Key,
                  DESKRIPSI AS ViewField
            FROM 
                HR_BLOK 
            WHERE 
                TKH_HAPUS IS NULL AND BL_ID IS NOT NULL
                AND KOD_KAMPUS = :KOD_KAMPUS AND KOD_ZON = :KOD_ZON
            ORDER BY BLOK_PK ASC ";
        }
        // End: DDL

        internal static string SQL_MtdGetKodKategori()
        {
            return @"
            SELECT
                PARAM_PK AS Key,
                KOD AS ViewField
            FROM 
                SMU_PARAMETER 
            WHERE 
                KUMPULAN_FK = 161 AND AKTIF = 'Y'
                AND TKH_HAPUS IS NULL AND PARAM_PK= :PARAM_PK
            ORDER BY PARAM_PK ";
        }

        internal static string SQL_MtdGetKodKategoriSemakSediada()
        {
            return @"SELECT 
                        COUNT(KOD_PRNN_PNYST) AS Nombor
                     FROM 
                        HR_INV_DAFTAR_PNYST 
                     WHERE 
                        SIASATAN_FK = :SIASATAN_PK AND SUBSTR(KOD_PRNN_PNYST,1,1) = :KOD ";
        }

        internal static string SQL_GetHrPenyiasatInsert()
        {
            return @" INSERT INTO HR_INV_DAFTAR_PNYST 
                                 (SIASATAN_FK, STAF_PP_FK, KATEGORI_KOD_FK, KOD_PRNN_PNYST, STATUS_PNYST, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:SIASATAN_FK, :STAF_PP_FK, :KATEGORI_KOD_FK, :KOD_PRNN_PNYST, 'Y', :PENCIPTA_FK, SYSDATE) ";
        }

        internal static string SQL_MtdGetKodPerananSemakSediada()
        {
            return @"SELECT
                        PERIBADI_PRCKPN_PK AS Key, 
                        '1' AS Value
                    FROM
                        HR_INV_PERIBADI_PRCKPN
                    WHERE
                        TKH_HAPUS IS NULL AND SIASATAN_FK = :SIASATAN_FK AND KOD_PRNN_PRCKPN = :KOD_PRNN_PRCKPN ";
        }

        internal static string SQL_InsertPeribadiPrckpn()
        {
            return @" INSERT INTO HR_INV_PERIBADI_PRCKPN 
                                 (PERIBADI_PRCKPN_PK, SIASATAN_FK, KOD_PRNN_PNYST_FK, KATEGORI_BRG_FK, KTGRI_PRCKPN_FK, KOD_PRNN_PRCKPN, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PERIBADI_PRCKPN_PK, :SIASATAN_FK, :KOD_PRNN_PNYST_FK, :KATEGORI_BRG_FK, :KTGRI_PRCKPN_FK, :KOD_PRNN_PRCKPN, :PENCIPTA_FK, SYSDATE) ";
        }
        internal static string SQL_InsertPerincianPrckpn()
        {
            return @" INSERT INTO HR_INV_PERINCIAN_PRCKPN 
                                 (PERIBADI_PRCKPN_FK, KOD_PRNN_PNYST_FK, SUMPAH_AKUR_JANJI_FK, BIODATA_AWAL, SOALAN_JAWAPAN, TEKS_PENGESAHAN, STATUS_PRCKPN, KOD_BRG_PRCKPN, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PERIBADI_PRCKPN_FK, :KOD_PRNN_PNYST_FK, NULL, NULL, NULL, NULL, :STATUS_PRCKPN, :KOD_BRG_PRCKPN, :PENCIPTA_FK, SYSDATE) ";
        }
        internal static string SQL_InsertSaksiPrckpnStaf()
        {
            return @" INSERT INTO HR_INV_PRCKPN_STAF_LAIN2 
                                 (PERIBADI_PRCKPN_FK, STAF_LAIN2_SAKSI_OKT_FK, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PERIBADI_PRCKPN_FK, :STAF_LAIN2_SAKSI_OKT_FK, :PENCIPTA_FK, SYSDATE) ";
        }
        internal static string SQL_InsertSaksiPrckpnStudent()
        {
            return @" INSERT INTO HR_INV_PRCKPN_PLJR
                                 (PERIBADI_PRCKPN_FK, NO_KP, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PERIBADI_PRCKPN_FK, :NO_KP, :PENCIPTA_FK, SYSDATE) ";
        }

        internal static string DB_MtdGetDaftarPnystPk()
        {
            return @"
            SELECT 
                DAFTAR_PNYST_PK
            FROM 
                HR_INV_DAFTAR_PNYST 
            WHERE
                TKH_HAPUS IS NULL AND STATUS_PNYST = 'Y'
                AND SIASATAN_FK = :SIASATAN_FK AND STAF_PP_FK = :STAF_PP_FK
            ORDER BY DAFTAR_PNYST_PK ASC ";
        }

        internal static string Sql_PeribadiPrckpn =
           @"SELECT 
                    A.PERIBADI_PRCKPN_PK,
                    A.SIASATAN_FK,
                    A.KOD_PRNN_PRCKPN,
                    A.KATEGORI_BRG_FK,
                    A.KTGRI_PRCKPN_FK,
                    TO_CHAR(A.TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKHCIPTA_RKMN,
                    TO_CHAR(A.TKH_CIPTA,'HH:MI AM') AS MASA_TKHCIPTA_RKMN,
                    F.COMPLAINER_FK,
                    F.COMPLAINER_NO_KP,
                    F.MAKLUMAT_PERIBADI_FK,
                    (SELECT (UPPER(TRIM(B.NAMA)) || '~' || B.NO_PEKERJA || '~' || B.NO_KP_BARU) FROM HR_STAF A, HR_MAKLUMAT_PERIBADI B WHERE A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK 
                    AND A.STAF_PK = F.COMPLAINER_FK AND A.TKH_HAPUS IS NULL AND B.TKH_HAPUS IS NULL) AS INFO_PENGADU_STAF,
                    (SELECT 
                    (UPPER(TRIM(B.NAMA)) || '~' || 
                    A.NO_PEKERJA || '~' ||
                    B.NO_KP_BARU)
                FROM 
                    HR_STAF A, HR_MAKLUMAT_PERIBADI B
                WHERE 
                    A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK 
                    AND A.STAF_PK = G.STAF_LAIN2_SAKSI_OKT_FK) AS INFO_SAKSI_STAF,
                    H.NO_KP AS NO_KP_PELAJAR
             FROM
                    HR_INV_PERIBADI_PRCKPN A
                    INNER JOIN HR_INV_SIASATAN D ON D.SIASATAN_PK = A.SIASATAN_FK AND D.TKH_HAPUS IS NULL
                    INNER JOIN HR_BK_ADUAN F ON F.ADUAN_PK = D.ADUAN_FK AND F.TKH_HAPUS IS NULL
                    LEFT JOIN HR_INV_PRCKPN_STAF_LAIN2 G ON G.PERIBADI_PRCKPN_FK = A.PERIBADI_PRCKPN_PK AND G.TKH_HAPUS IS NULL
                    LEFT JOIN HR_INV_PRCKPN_PLJR H ON H.PERIBADI_PRCKPN_FK = A.PERIBADI_PRCKPN_PK AND H.TKH_HAPUS IS NULL
             ";

        internal static string SQL_MtdGetPeribadiPrckpnList()
        {
            string _SQL = Sql_PeribadiPrckpn +
                @"WHERE
                     A.TKH_HAPUS IS NULL";

            _SQL += @" AND A.SIASATAN_FK = :SIASATAN_PK AND A.KATEGORI_BRG_FK = :KATEGORI_BRG_FK";

            _SQL += @" ORDER BY A.TKH_CIPTA DESC ";

            return _SQL;
        }

        internal static string Sql_PrckpnPengaduSaksi =
            @"SELECT 
                    A.PERIBADI_PRCKPN_PK,
                    A.SIASATAN_FK,
                    A.KOD_PRNN_PRCKPN,
                    A.KATEGORI_BRG_FK,
                    A.KTGRI_PRCKPN_FK,
                    B.PERINCIAN_PRCKPN_PK,
                    B.STATUS_PRCKPN,
                    B.KOD_BRG_PRCKPN,
                    B.BIODATA_AWAL,
                    B.SUMPAH_AKUR_JANJI_FK,
                    B.TEKS_PENGESAHAN,
                    B.SOALAN_JAWAPAN,
                    B.STATUS_PRCKPN,
                    TO_CHAR(A.TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKHCIPTA_RKMN,
                    TO_CHAR(A.TKH_CIPTA,'HH:MI AM') AS MASA_TKHCIPTA_RKMN,
                    TO_CHAR(B.TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKHCIPTA_RKMN_PRNCN,
                    TO_CHAR(B.TKH_CIPTA,'HH:MI AM') AS MASA_TKHCIPTA_RKMN_PRNCN,
                    TO_CHAR(B.TKH_KEMASKINI,'DD/MM/YYYY') AS DATE_TKHKEMASKINI_RKMN_PRNCN,
                    TO_CHAR(B.TKH_KEMASKINI,'HH:MI AM') AS MASA_TKHKEMASKINI_RKMN_PRNCN,
                    C.KOD_PRNN_PNYST,
                    F.COMPLAINER_FK,
                    F.COMPLAINER_NO_KP,
                    F.MAKLUMAT_PERIBADI_FK,
                    (SELECT (UPPER(TRIM(B.NAMA)) || '~' || B.NO_PEKERJA || '~' || B.NO_KP_BARU) FROM HR_STAF A, HR_MAKLUMAT_PERIBADI B WHERE A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK 
                    AND A.STAF_PK = F.COMPLAINER_FK AND A.TKH_HAPUS IS NULL AND B.TKH_HAPUS IS NULL) AS INFO_PENGADU_STAF,
                    (SELECT 
                    (UPPER(TRIM(B.NAMA)) || '~' || 
                    B.NO_PEKERJA || '~' ||
                    B.NO_KP_BARU)
                FROM 
                    HR_STAF A, HR_MAKLUMAT_PERIBADI B
                WHERE 
                    A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK 
                    AND A.STAF_PK = G.STAF_LAIN2_SAKSI_OKT_FK) AS INFO_SAKSI_STAF,
                    H.PRCKPN_TERJEMAHAN_PK,
                    H.PENTERJEMAH_FK,
                    H.KATEGORI_PENTERJEMAH_FK,
                    H.NO_KP,
                    I.NO_KP AS NO_KP_PELAJAR
             FROM
                    HR_INV_PERIBADI_PRCKPN A
                    INNER JOIN HR_INV_PERINCIAN_PRCKPN B ON B.PERIBADI_PRCKPN_FK = A.PERIBADI_PRCKPN_PK AND B.TKH_HAPUS IS NULL
                    INNER JOIN HR_INV_DAFTAR_PNYST C ON C.DAFTAR_PNYST_PK = B.KOD_PRNN_PNYST_FK AND C.TKH_HAPUS IS NULL
                    INNER JOIN HR_INV_SIASATAN D ON D.SIASATAN_PK = A.SIASATAN_FK AND D.TKH_HAPUS IS NULL
                    INNER JOIN HR_BK_ADUAN F ON F.ADUAN_PK = D.ADUAN_FK AND F.TKH_HAPUS IS NULL
                    LEFT JOIN HR_INV_PRCKPN_STAF_LAIN2 G ON G.PERIBADI_PRCKPN_FK = A.PERIBADI_PRCKPN_PK AND G.TKH_HAPUS IS NULL
                    LEFT JOIN HR_INV_PRCKPN_TERJEMAHAN H ON H.PERINCIAN_PRCKPN_FK = B.PERINCIAN_PRCKPN_PK AND H.TKH_HAPUS IS NULL
                    LEFT JOIN HR_INV_PRCKPN_PLJR I ON I.PERIBADI_PRCKPN_FK = A.PERIBADI_PRCKPN_PK AND I.TKH_HAPUS IS NULL
             ";

        internal static string SQL_MtdGetPrckpnList(string? _kodPeranan)
        {
            string _SQL = Sql_PrckpnPengaduSaksi +
                @"WHERE
                     A.TKH_HAPUS IS NULL ";

            _SQL += @" AND A.SIASATAN_FK = :SIASATAN_PK AND A.KATEGORI_BRG_FK = :KATEGORI_BRG_FK";

            if (_kodPeranan != null && _kodPeranan.Length > 0)
            {
                _SQL += " AND A.KOD_PRNN_PRCKPN LIKE '" + _kodPeranan + "%'  ";
            }

            _SQL += @" ORDER BY A.TKH_CIPTA DESC ";

            return _SQL;
        }

        internal static string SQL_GetPerincianPrbdiDetails()
        {
            string _SQL = Sql_PrckpnPengaduSaksi +
                @"WHERE
                     A.TKH_HAPUS IS NULL ";

            _SQL += " AND B.PERINCIAN_PRCKPN_PK = :PERINCIAN_PRCKPN_PK  ";

            return _SQL;
        }

        internal static string SQL_GetSaksiList(string? _katsaksi, string? _select, string? _name)
        {
            string _SQL = "";
            string _value = "~";
            if (_name == "") { _name = _value; }
            if (_katsaksi == "421")
            {
                _SQL = @" SELECT 
                                S.STAF_PK AS Key, 
                                S.NO_PEKERJA AS ViewField, 
                                M.NAMA AS Tahun
                             FROM 
                                HR_STAF S
                                LEFT JOIN HR_MAKLUMAT_PERIBADI M ON M.MAKLUMAT_PERIBADI_PK = S.MAKLUMAT_PERIBADI_FK
                             WHERE 
                                S.TKH_HAPUS IS NULL
                                AND M.TKH_HAPUS IS NULL ";
            }

            if (_select == "Name")
            {
                _SQL += " AND (UPPER(M.NAMA) LIKE UPPER('%" + _name + "%')) ";
            }
            else
            {
                _SQL += " AND S.NO_PEKERJA = '" + _name + "' ";
            }

            _SQL += @" ORDER BY M.NAMA ";

            return _SQL;
        }

        static readonly string _VW_SENARAI_STUDENT_AKTIF =
            @" SELECT 
                    IC AS Key, 
                    NOMATRIK AS ViewField,
                    NAMA AS Tahun
                 FROM 
              SENARAI.VW_STUDENT_AKTIF_KESELURUHAN ";

        public static string SQL_GetStudentVWSenarai(string? _katsaksi, string? _select, string? _name)
        {
            string _SQL = _VW_SENARAI_STUDENT_AKTIF;
            string _value = "~";
            if (_name == "") { _name = _value; }
            if (_select == "Name")
            {
                _SQL += " WHERE (UPPER(NAMA) LIKE UPPER('%" + _name + "%')) ";
            }
            else
            {
                _SQL += " WHERE ( IC = '" + _name + "') ";
            }

            _SQL += @" ORDER BY NAMA ";
            return _SQL;
        }

        // Get Info Polis - Lampiran
        internal static string SqlGetPolisInfo =
            @"SELECT
                    A.ADUAN_FK,
                    A.SIASATAN_FK,
                    A.NO_LPRN_POLIS,
                    A.TKH_LPRN,
                    TO_CHAR(A.TKH_LPRN,'DD/MM/YYYY') AS DATE_TKH_LPRN,
                    TO_CHAR(A.TKH_LPRN,'HH:MI AM') AS MASA_TKH_LPRN,
                    (CASE TO_CHAR(TO_DATE(A.TKH_LPRN,'DD/MM/YYYY'),'D')
                        WHEN '1' THEN 'AHAD'
                        WHEN '2' THEN 'ISNIN'
                        WHEN '3' THEN 'SELASA'
                        WHEN '4' THEN 'RABU'
                        WHEN '5' THEN 'KHAMIS'
                        WHEN '6' THEN 'JUMAAT'
                        WHEN '7' THEN 'SABTU' ELSE 'TIADA DATA'
                    END) HARI_TKH_LPRN,
                    A.MASA_LPRN,
                    A.KATEGORI_KOD_FK,
                    B.FAIL,
                    (SELECT Y.KOD || ' - ' || Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.KUMPULAN_FK = 161 AND Y.PARAM_PK = A.KATEGORI_KOD_FK AND Y.TKH_HAPUS IS NULL) AS KOD_RUJUKAN,
                    '2' as RESULTSET 
              FROM 
                    HR_BK_LAPORAN_POLIS A
                    INNER JOIN HR_BK_LAMPIRAN B ON B.ADUAN_FK = A.ADUAN_FK AND TRIM(B.LAMPIRAN) = TRIM(A.NO_LPRN_POLIS) AND B.TKH_HAPUS IS NULL
             ";

        internal static string SQL_MtdGetLaporanPolis()
        {
            string _SQL = SqlGetPolisInfo +
                    @" WHERE
                        A.TKH_HAPUS IS NULL ";
            // hardcoded
            _SQL += @" AND A.ADUAN_FK = '1132' AND A.SIASATAN_FK = :SIASATAN_PK ";

            return _SQL;
        }

        internal static string SQL_UpdateKeteranganPS()
        {
            return @"UPDATE 
                         HR_INV_PERINCIAN_PRCKPN 
                     SET 
                        KOD_PRNN_PNYST_FK = :KOD_PRNN_PNYST_FK,
                        SUMPAH_AKUR_JANJI_FK  = :CHECKED, 
                        TEKS_PENGESAHAN = :CHECKED2,
                        SOALAN_JAWAPAN = :SOALAN_JAWAPAN,
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        PERINCIAN_PRCKPN_PK = :PERINCIAN_PRCKPN_PK";
        }

        internal static string SQL_InsertPerincianTerjemahStaf()
        {
            return @" INSERT INTO HR_INV_PRCKPN_TERJEMAHAN
                                 (PERINCIAN_PRCKPN_FK, PENTERJEMAH_FK, KATEGORI_PENTERJEMAH_FK, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PERINCIAN_PRCKPN_FK, :PENTERJEMAH_FK, :KATEGORI_PENTERJEMAH_FK, :PENCIPTA_FK, SYSDATE) ";
        }
        internal static string SQL_InsertPerincianTerjemahPelajar()
        {
            return @" INSERT INTO HR_INV_PRCKPN_TERJEMAHAN
                                 (PERINCIAN_PRCKPN_FK, NO_KP, KATEGORI_PENTERJEMAH_FK, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PERINCIAN_PRCKPN_FK, :NO_KP, :KATEGORI_PENTERJEMAH_FK, :PENCIPTA_FK, SYSDATE) ";
        }
        internal static string SQL_HapusPrckpnTerjemah()
        {
            return @"UPDATE 
                         HR_INV_PRCKPN_TERJEMAHAN 
                     SET 
                        TKH_HAPUS = SYSDATE,
                        PENGHAPUS_FK = :PENCIPTA_FK
                    WHERE 
                        PRCKPN_TERJEMAHAN_PK = :PRCKPN_TERJEMAHAN_PK";
        }

        internal static string SQL_MtdUpdateInitialEvent()
        {
            return @"UPDATE 
                         HR_INV_SIASATAN
                     SET 
                        TAJUK_RNGKSN  = :TAJUK_RNGKSN, 
                        AKBT_KJDN = :AKBT_KJDN,
                        RMSN_AWAL = :RMSN_AWAL,
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        SIASATAN_PK = :SIASATAN_PK";
        }

        internal static string SQL_MtdInsertLampiran()
        {
            return @" INSERT INTO HR_INV_LAMPIRAN
                      (SIASATAN_FK, KATEGORI_KOD_FK, KOD_LMPRN_RKMN, KOD_PRNN_PNYST_FK, TAJUK, PATH, URL, 
                       RAKAMAN_PRCKPN_FK, PENCIPTA_FK, TKH_CIPTA, NAMA_FAIL, CTTN_LMPRN)
                      VALUES 
                      (:SIASATAN_FK, :KATEGORI_KOD_FK, :KOD_LMPRN_RKMN, :KOD_PRNN_PNYST_FK, :TAJUK, :PATH, :URL, 
                       :RAKAMAN_PRCKPN_FK, :PENCIPTA_FK, SYSDATE, :NAMA_FAIL, :CTTN_LMPRN) ";
        }

        internal static string Sql_InvLampiran =
            @"SELECT 
                    A.INV_LAMPIRAN_PK,
                    A.SIASATAN_FK,
                    A.KOD_LMPRN_RKMN,
                    A.TAJUK,
                    A.NAMA_FAIL,
                    A.KOD_PRNN_PNYST_FK,
                    A.CTTN_LMPRN,
                    A.URL,
                    A.RAKAMAN_PRCKPN_FK,
                    A.KATEGORI_KOD_FK,
                    A.PATH,
                    TO_CHAR(A.TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKHCIPTA,
                    TO_CHAR(A.TKH_CIPTA,'HH:MI AM') AS MASA_TKHCIPTA,
                    TO_CHAR(A.TKH_KEMASKINI,'DD/MM/YYYY') AS DATE_TKHKEMASKINI,
                    TO_CHAR(A.TKH_KEMASKINI,'HH:MI AM') AS MASA_TKHKEMASKINI,
                    B.KOD_PRNN_PNYST
             FROM
                    HR_INV_LAMPIRAN A
                    INNER JOIN HR_INV_DAFTAR_PNYST B ON B.DAFTAR_PNYST_PK = A.KOD_PRNN_PNYST_FK AND B.TKH_HAPUS IS NULL
             ";

        internal static string SQL_GetLampiranList()
        {
            string _SQL = Sql_InvLampiran +
                @"WHERE
                     A.TKH_HAPUS IS NULL ";

            _SQL += @" AND A.SIASATAN_FK = :SIASATAN_PK AND A.KATEGORI_KOD_FK = :KATEGORI_KOD_FK";

            _SQL += @" ORDER BY A.TKH_CIPTA DESC ";

            return _SQL;
        }

        internal static string SQL_GetLampiran()
        {
            string _SQL = Sql_InvLampiran +
                @" WHERE
                        A.TKH_HAPUS IS NULL ";

            _SQL += @" AND A.INV_LAMPIRAN_PK = :INV_LAMPIRAN_PK ";

            return _SQL;
        }

        internal static string SQL_MtdUpdateLampiran()
        {
            return @"UPDATE 
                         HR_INV_LAMPIRAN
                     SET 
                        PATH  = :PATH, 
                        KOD_PRNN_PNYST_FK = :KOD_PRNN_PNYST_FK,
                        TAJUK = :TAJUK,
                        NAMA_FAIL = :NAMA_FAIL,
                        CTTN_LMPRN = :CTTN_LMPRN,
                        URL = :URL,
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        INV_LAMPIRAN_PK = :INV_LAMPIRAN_PK";
        }

        internal static string SQL_GetPerananOkt()
        {
            return @"
            SELECT
                MAX(SUBSTR(KOD_PRNN_PRCKPN,2)) AS Nombor
            FROM 
                HR_INV_PERIBADI_PRCKPN 
            WHERE 
                TKH_HAPUS IS NULL 
                AND SIASATAN_FK = :SIASATAN_FK AND KATEGORI_BRG_FK = :KATEGORI_BRG_FK
            ORDER BY PERIBADI_PRCKPN_PK DESC";
        }

        internal static string SQL_UpdateStatus()
        {
            return @"UPDATE 
                         HR_INV_PERINCIAN_PRCKPN 
                     SET 
                        STATUS_PRCKPN  = :STATUS_PRCKPN, 
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        PERINCIAN_PRCKPN_PK = :PERINCIAN_PRCKPN_PK";
        }
        internal static string SQL_HapusStatus()
        {
            return @"UPDATE 
                         HR_INV_PERINCIAN_PRCKPN 
                     SET 
                        STATUS_PRCKPN  = :STATUS_PRCKPN, 
                        TKH_HAPUS = SYSDATE,
                        PENGHAPUS_FK = :PENCIPTA_FK
                    WHERE 
                        PERINCIAN_PRCKPN_PK = :PERINCIAN_PRCKPN_PK";
        }

        internal static string Sql_BkLampiran =
            @"SELECT 
                    LAMPIRAN_PK,
                    ADUAN_FK,
                    TAJUK,
                    NAMA_FAIL,
                    FAIL,
                    TO_CHAR(TKH_CIPTA,'DD/MM/YYYY') AS DATE_TKHCIPTA,
                    TO_CHAR(TKH_CIPTA,'HH:MI AM') AS MASA_TKHCIPTA
             FROM
                    HR_BK_LAMPIRAN
             ";

        internal static string SQL_GetLampiranDocList()
        {
            string _SQL = Sql_BkLampiran +
                @"WHERE
                     TKH_HAPUS IS NULL ";

            _SQL += @" AND ADUAN_FK = :ADUAN_FK ";

            _SQL += @" ORDER BY TKH_CIPTA DESC ";

            return _SQL;
        }

        internal static string SQL_GetLampiranDoc()
        {
            string _SQL = Sql_BkLampiran +
                @"WHERE
                     TKH_HAPUS IS NULL ";

            _SQL += @" AND LAMPIRAN_PK = :LAMPIRAN_PK ";

            return _SQL;
        }

        internal static string SQL_GetPerananInvLampiran()
        {
            return @"
            SELECT
                MAX(SUBSTR(KOD_LMPRN_RKMN,2)) AS Nombor
            FROM 
                HR_INV_LAMPIRAN 
            WHERE 
                TKH_HAPUS IS NULL 
                AND SIASATAN_FK = :SIASATAN_FK AND KATEGORI_KOD_FK = :KATEGORI_KOD_FK
            ORDER BY INV_LAMPIRAN_PK DESC";
        }

        internal static string SQL_MtdSahWujud()
        {
            return @"
            SELECT
                INV_PENGESAHAN_PK AS Key, 
                '1' AS Value
            FROM
                HR_INV_PENGESAHAN
            WHERE
                TKH_HAPUS IS NULL 
                AND SIASATAN_FK = :SIASATAN_FK AND STATUS_FK = :STATUS_FK AND KATEGORI_PENGESAHAN_FK = :KATEGORI_PENGESAHAN_FK
                 ";
        }

        internal static string SQL_MtdUpdatePengesahan()
        {
            return @"UPDATE 
                         HR_INV_PENGESAHAN
                     SET 
                        BTRN_ULSN = :BTRN_ULSN,
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        INV_PENGESAHAN_PK = :INV_PENGESAHAN_PK";
        }

        internal static string SQL_MtdInsertPengesahan()
        {
            return @" INSERT INTO HR_INV_PENGESAHAN 
                                 (SIASATAN_FK, KOD_APPROVAL_FK, BTRN_ULSN, PENGESAH_STATUS, STATUS_FK, KATEGORI_PENGESAHAN_FK, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:SIASATAN_FK, :KOD_APPROVAL_FK, :BTRN_ULSN, :PENGESAH_STATUS, :STATUS_FK, :KATEGORI_PENGESAHAN_FK, :PENCIPTA_FK, SYSDATE) ";
        }

        internal static string SQL_GetApprovalRolePk()
        {
            return @"
            SELECT
                C.APPROVAL_ROLE_PK AS Key, 
                '1' AS Value
            FROM
                MP_MODUL A
                INNER JOIN HR_APPROVAL_MODUL B ON B.MODUL_FK = A.MODUL_PK AND B.TKH_HAPUS IS NULL
                INNER JOIN HR_APPROVAL_ROLE C ON C.KOD_APPROVAL = B.KOD_APPROVAL AND C.TKH_HAPUS IS NULL
                INNER JOIN HR_APPROVAL_STRUCTURE D ON D.KOD_APPROVAL = B.KOD_APPROVAL AND D.TKH_HAPUS IS NULL
            WHERE
                A.TKH_HAPUS IS NULL
                AND D.STAF_FK = :STAF_FK
                AND UPPER(A.NAMA_MODULE) = UPPER('apel')
                 ";
        }

        internal static string SQL_MtdInsertInitialApprove()
        {
            return @" INSERT INTO HR_INV_PENGESAHAN 
                                 (SIASATAN_FK, KOD_APPROVAL_FK, PENGESAH_STATUS, STATUS_FK, KATEGORI_PENGESAHAN_FK, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:SIASATAN_FK, :KOD_APPROVAL_FK, :PENGESAH_STATUS, :STATUS_FK, :KATEGORI_PENGESAHAN_FK, :PENCIPTA_FK, SYSDATE) ";
        }

        internal static string SQL_GetRngksnStafEmel()
        {
            return @"
            SELECT
                PENCIPTA_FK AS Key, 
                '1' AS Value
            FROM
                HR_INV_PENGESAHAN
            WHERE
                TKH_HAPUS IS NULL
                AND SIASATAN_FK = :SIASATAN_FK AND STATUS_FK = :STATUS_FK AND KATEGORI_PENGESAHAN_FK = :KATEGORI_PENGESAHAN_FK
                AND KOD_APPROVAL_FK IS NULL
                 ";
        }

        // Get Info Staf dari value PENCIPTA_FK - HR_INV_PENGESAHAN
        internal static string SqlPengesahan =
            @"SELECT
                    P.INV_PENGESAHAN_PK AS INV_PENGESAHAN_PK,
                    P.BTRN_ULSN AS BTRN_ULSN,
                    P.STATUS_FK AS STATUS_FK,
                    (SELECT 
                        (UPPER(TRIM(B.NAMA)) || '~' || 
                        B.NO_PEKERJA || '~' ||
                        B.NO_KP_BARU) || '~' ||
                        C.KOD_JAWATAN || ' - '|| D.DESKRIPSI || '~' ||
                        (CASE 
                            WHEN SUBSTR(A.KOD_PTJ,1,1) = 'J' THEN 'JOHOR BAHRU' 
                            WHEN SUBSTR(A.KOD_PTJ,1,1)= 'K' THEN 'KUALA LUMPUR' 
                            WHEN SUBSTR(A.KOD_PTJ,1,1) = 'P' THEN 'PAGOH'  
                        ELSE '' END) || '~' ||
                        A.STATUS_AKTIF
                    FROM 
                        HR_STAF A, HR_MAKLUMAT_PERIBADI B, HR_KOD_JAWATAN C, HR_KOD_JENIS_JAWATAN D 
                    WHERE 
                        A.MAKLUMAT_PERIBADI_FK = B.MAKLUMAT_PERIBADI_PK AND A.TKH_HAPUS IS NULL AND B.TKH_HAPUS IS NULL 
                        AND A.KOD_JAWATAN = C.KOD_JAWATAN AND C.TKH_HAPUS IS NULL
                        AND C.KOD_JENIS_JAWATAN = D.KOD_JENIS_JAWATAN AND C.KOD_KLASIFIKASI = D.KOD_KLASIFIKASI AND D.TKH_HAPUS IS NULL
                        AND A.STAF_PK = P.PENCIPTA_FK) AS CNAMA,
                    TO_CHAR(P.TKH_CIPTA,'DD/MM/YYYY') AS DATE_CIPTA,
                    TO_CHAR(P.TKH_CIPTA,'HH:MI AM') AS MASA_CIPTA,
                    TO_CHAR(P.TKH_KEMASKINI,'DD/MM/YYYY') AS DATE_KEMASKINI,
                    TO_CHAR(P.TKH_KEMASKINI,'HH:MI AM') AS MASA_KEMASKINI
                FROM
                    HR_INV_PENGESAHAN P
             ";

        internal static string SQL_GetPengesahanList()
        {
            string _SQL = SqlPengesahan +
                @" WHERE
                       P.TKH_HAPUS IS NULL";

            _SQL += @" AND P.SIASATAN_FK = :SIASATAN_PK AND P.KATEGORI_PENGESAHAN_FK = :KATEGORI_PENGESAHAN_FK";

            _SQL += @" ORDER BY P.TKH_CIPTA ASC ";

            return _SQL;
        }

        internal static string SQL_InsertMessage()
        {
            return @" INSERT INTO HR_INV_MINIT_SSTN 
                                 (PENGGUNA_FK, CATATAN, SIASATAN_FK, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:PENGGUNA_FK, :CATATAN, :SIASATAN_FK, :PENCIPTA_FK, SYSDATE) ";
        }

        // Get Info Staf Penyiasat dari value STAF_PP_FK - HR_INV_DAFTAR_PNYST
        internal static string SqlGetMessage =
            @"SELECT 
                    (SELECT ID_PENGGUNA FROM PENGGUNA P WHERE P.STAF_FK = A.PENGGUNA_FK) AS ID_PENGGUNA,
                    (SELECT KOD_PRNN_PNYST FROM HR_INV_DAFTAR_PNYST D WHERE D.SIASATAN_FK = A.SIASATAN_FK AND D.STAF_PP_FK = A.PENGGUNA_FK) AS KOD_PRNN_PNYST,
                    A.CATATAN,
                    TO_CHAR(A.TKH_CIPTA,'DD/MM/YYYY') AS DATE_CIPTA,
                    TO_CHAR(A.TKH_CIPTA,'HH:MI AM') AS MASA_CIPTA
              FROM
                    HR_INV_MINIT_SSTN A
             ";

        internal static string SQL_GetMessageList()
        {
            string _SQL = SqlGetMessage +
                @" WHERE
                       A.TKH_HAPUS IS NULL";

            _SQL += @" AND A.SIASATAN_FK = :SIASATAN_PK";

            _SQL += @" ORDER BY A.TKH_CIPTA ASC ";

            return _SQL;
        }

        internal static string SQL_UpdateTamatSiasatan()
        {
            return @"UPDATE 
                         HR_INV_SIASATAN
                     SET 
                        STATUS_SSTN = :STATUS_SSTN,
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        SIASATAN_PK = :SIASATAN_PK";
        }

        internal static string SQL_GetKodPrnnPnyst()
        {
            return @"
            SELECT
                 PERINCIAN_PRCKPN_PK AS Key, 
                '1' AS Value
            FROM
                HR_INV_PERINCIAN_PRCKPN
            WHERE
                TKH_HAPUS IS NULL 
                AND PERINCIAN_PRCKPN_PK = :PERINCIAN_PRCKPN_PK AND KOD_PRNN_PNYST_FK = :KOD_PRNN_PNYST_FK
                 ";
        }

        internal static string SQL_InsertCatatanSstn()
        {
            return @" INSERT INTO HR_INV_CTTN_SSTN 
                                 (SIASATAN_FK, KOD_PRNN_PNYST_FK, TKH_CTTN, MASA, BLOK_FK, CTTN_LOKASI, BUTIRAN_SSTN, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:SIASATAN_FK, :KOD_PRNN_PNYST_FK, :TKH_CTTN_DATETIME, NULL, :BLOK_FK, :CTTN_LOKASI, :BUTIRAN_SSTN, :PENCIPTA_FK, SYSDATE) ";
        }

        // Get Info Catatan Siasatan
        internal static string SQL_GetCatatanSstn =
            @" SELECT
                    SIASATAN_FK,
                    (SELECT BL.DESKRIPSI FROM HR_BLOK BL WHERE BL.BLOK_PK = BLOK_FK) AS BLOK,
                    TO_CHAR(TKH_CTTN,'DD/MM/YYYY') AS DATE_TKH_CTTN,
                    TO_CHAR(TKH_CTTN,'HH:MI AM') AS MASA_TKH_CTTN,
                    (SELECT KOD_PRNN_PNYST FROM HR_INV_DAFTAR_PNYST P WHERE P.DAFTAR_PNYST_PK = KOD_PRNN_PNYST_FK AND P.TKH_HAPUS IS NULL) AS KOD_PRNN_PNYST
                FROM
                    HR_INV_CTTN_SSTN     
               ";

        internal static string SQL_MtdGetCatatanList()
        {
            string _SQL = SQL_GetCatatanSstn +
                @"  WHERE 
                            TKH_HAPUS IS NULL ";

            _SQL += @" AND SIASATAN_FK = :SIASATAN_PK ";

            _SQL += @" ORDER BY TKH_CIPTA ASC ";

            return _SQL;
        }

        // Get Info Laporan Siasatan
        internal static string SqlGetLaporanSstnInfo =
            @" SELECT
                    LPRN_SSTN_PK,
                    SIASATAN_FK,
                    TAJUK_KES,
                    BTIRN_LPRN
                FROM
                    HR_INV_LPRN_SSTN     
               ";

        internal static string SQL_GetLaporanSstn()
        {
            string _SQL = SqlGetLaporanSstnInfo +
                @" WHERE
                        TKH_HAPUS IS NULL ";

            _SQL += @" AND SIASATAN_FK = :SIASATAN_FK ";

            return _SQL;
        }

        internal static string SQL_InsertInvLprnSstn()
		{
			return @" INSERT INTO HR_INV_LPRN_SSTN 
                                 (LPRN_SSTN_PK, SIASATAN_FK, TAJUK_KES, BTIRN_LPRN, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:LPRN_SSTN_PK, :SIASATAN_FK, :TAJUK_KES, :BTIRN_LPRN, :PENCIPTA_FK, SYSDATE) ";
		}
		internal static string SQL_InsertInvRumusan()
		{
			return @" INSERT INTO HR_INV_RUMUSAN 
                                 (LPRN_SSTN_FK, PERKARA, TARIKH, PENCIPTA_FK, TKH_CIPTA)  
                          VALUES (:LPRN_SSTN_FK, :PERKARA, :TARIKH, :PENCIPTA_FK, SYSDATE) ";
		}

        internal static string SQL_UpdateInvLprnSstn()
        {
            return @"UPDATE 
                         HR_INV_LPRN_SSTN
                     SET 
                        TAJUK_KES = :TAJUK_KES,
                        BTIRN_LPRN = :BTIRN_LPRN,
                        TKH_KEMASKINI = SYSDATE,
                        KEMASKINI_FK = :PENCIPTA_FK
                    WHERE 
                        LPRN_SSTN_PK = :LPRN_SSTN_PK";
        }

        // Get Info Rumusan
        internal static string SqlRumusan =
            @"SELECT
                    RUMUSAN_PK,
                    LPRN_SSTN_FK,
                    PERKARA,
                    TO_CHAR(TARIKH,'DD/MM/YYYY') AS TARIKH,
                    '1' as RESULTSET
                FROM
                    HR_INV_RUMUSAN
             ";

        internal static string SQL_GetRumusanList()
        {
            string _SQL = SqlRumusan +
                @" WHERE
                       TKH_HAPUS IS NULL";

            _SQL += @" AND LPRN_SSTN_FK = :LPRN_SSTN_FK";

            _SQL += @" ORDER BY TARIKH ASC ";

            return _SQL;
        }

        internal static string SQL_HapusRumusanSstn()
        {
            return @"UPDATE 
                         HR_INV_RUMUSAN
                     SET 
                        TKH_HAPUS = SYSDATE,
                        PENGHAPUS_FK = :PENCIPTA_FK
                    WHERE 
                        RUMUSAN_PK = :RUMUSAN_PK";
        }

        // RUJUKAN SQL DARI ADUAN KESELAMATAN
        internal static string SQL_CAPAI_PENTERJEMAH_PELAJAR_REKOD_HAPUS()
        {
            return @"
           SELECT  
            B.SSM_MATRIK,  
            B.SSM_KURSUS,  
            D.JMF_FACULTY,  
            E.SPR_NAMA,  
            E.SPR_NOKP,  
            F.SPH_BILIK,  
            F.SPH_BLOK,  
            G.KETERANGAN,  
            H.TRANSLATOR_CATEGORY_FK   
            FROM ~NAMA_SKEMA~.ssm_semester@smu8i B, ~NAMA_SKEMA~.slg_mplog01@smu8i C, ~NAMA_SKEMA~.kod_fakulti@smu8i D, ~NAMA_SKEMA~.sbp_peribadi@smu8i E ,  
            ~NAMA_SKEMA~.SPH_PENEMPATAN@smu8i F, ~NAMA_SKEMA~.KOD_KOLEJ@smu8i G, HR_BK_ADUAN H  
            WHERE B.SSM_SESISEM = C.SLG_SESISEM    
            AND B.SSM_NOKP = F.SPH_NOKP()  
            AND B.SSM_SESISEM = F.SPH_SESISEM()  
            AND F.SPH_KOLEJ = G.KOD()  
            AND F.SPH_DAFTAR() = 'Y'  
            AND F.SPH_SKELUAR() = 'T'  
            AND C.SLG_KOD_PRO = 'AKIB'   
            AND B.SSM_FAKULTI = D.JMF_KOD()    
            AND B.SSM_NOKP = E.SPR_NOKP    
            AND B.SSM_NOKP = H.TRANSLATOR_NO_KP  
            AND H.ADUAN_PK = ? ; ";
        }

        internal static string SQL_LAPORAN_ADUAN()
        {
            return @"
           SELECT  
            B.SSM_MATRIK,  
            B.SSM_KURSUS,  
            D.JMF_FACULTY,  
            E.SPR_NAMA,  
            E.SPR_NOKP,  
            F.SPH_BILIK,  
            F.SPH_BLOK,  
            G.KETERANGAN,  
            H.TRANSLATOR_CATEGORY_FK   
            FROM ~NAMA_SKEMA~.ssm_semester@smu8i B, ~NAMA_SKEMA~.slg_mplog01@smu8i C, ~NAMA_SKEMA~.kod_fakulti@smu8i D, ~NAMA_SKEMA~.sbp_peribadi@smu8i E ,  
            ~NAMA_SKEMA~.SPH_PENEMPATAN@smu8i F, ~NAMA_SKEMA~.KOD_KOLEJ@smu8i G, HR_BK_ADUAN H  
            WHERE B.SSM_SESISEM = C.SLG_SESISEM    
            AND B.SSM_NOKP = F.SPH_NOKP()  
            AND B.SSM_SESISEM = F.SPH_SESISEM()  
            AND F.SPH_KOLEJ = G.KOD()  
            AND F.SPH_DAFTAR() = 'Y'  
            AND F.SPH_SKELUAR() = 'T'  
            AND C.SLG_KOD_PRO = 'AKIB'   
            AND B.SSM_FAKULTI = D.JMF_KOD()    
            AND B.SSM_NOKP = E.SPR_NOKP    
            AND B.SSM_NOKP = H.TRANSLATOR_NO_KP  
            AND H.ADUAN_PK = ? ; ";
        }

        internal static string SQL_RINGKASAN_KEJADIAN_AWAL()
        {
            return @"
           SELECT  
            B.SSM_MATRIK,  
            B.SSM_KURSUS,  
            D.JMF_FACULTY,  
            E.SPR_NAMA,  
            E.SPR_NOKP,  
            F.SPH_BILIK,  
            F.SPH_BLOK,  
            G.KETERANGAN,  
            H.TRANSLATOR_CATEGORY_FK   
            FROM ~NAMA_SKEMA~.ssm_semester@smu8i B, ~NAMA_SKEMA~.slg_mplog01@smu8i C, ~NAMA_SKEMA~.kod_fakulti@smu8i D, ~NAMA_SKEMA~.sbp_peribadi@smu8i E ,  
            ~NAMA_SKEMA~.SPH_PENEMPATAN@smu8i F, ~NAMA_SKEMA~.KOD_KOLEJ@smu8i G, HR_BK_ADUAN H  
            WHERE B.SSM_SESISEM = C.SLG_SESISEM    
            AND B.SSM_NOKP = F.SPH_NOKP()  
            AND B.SSM_SESISEM = F.SPH_SESISEM()  
            AND F.SPH_KOLEJ = G.KOD()  
            AND F.SPH_DAFTAR() = 'Y'  
            AND F.SPH_SKELUAR() = 'T'  
            AND C.SLG_KOD_PRO = 'AKIB'   
            AND B.SSM_FAKULTI = D.JMF_KOD()    
            AND B.SSM_NOKP = E.SPR_NOKP    
            AND B.SSM_NOKP = H.TRANSLATOR_NO_KP  
            AND H.ADUAN_PK = ? ; ";
        }

        internal static string SQL_LAMPIRAN_LAPORAN_POLIS()
        {
            return @"
           SELECT  
            B.SSM_MATRIK,  
            B.SSM_KURSUS,  
            D.JMF_FACULTY,  
            E.SPR_NAMA,  
            E.SPR_NOKP,  
            F.SPH_BILIK,  
            F.SPH_BLOK,  
            G.KETERANGAN,  
            H.TRANSLATOR_CATEGORY_FK   
            FROM ~NAMA_SKEMA~.ssm_semester@smu8i B, ~NAMA_SKEMA~.slg_mplog01@smu8i C, ~NAMA_SKEMA~.kod_fakulti@smu8i D, ~NAMA_SKEMA~.sbp_peribadi@smu8i E ,  
            ~NAMA_SKEMA~.SPH_PENEMPATAN@smu8i F, ~NAMA_SKEMA~.KOD_KOLEJ@smu8i G, HR_BK_ADUAN H  
            WHERE B.SSM_SESISEM = C.SLG_SESISEM    
            AND B.SSM_NOKP = F.SPH_NOKP()  
            AND B.SSM_SESISEM = F.SPH_SESISEM()  
            AND F.SPH_KOLEJ = G.KOD()  
            AND F.SPH_DAFTAR() = 'Y'  
            AND F.SPH_SKELUAR() = 'T'  
            AND C.SLG_KOD_PRO = 'AKIB'   
            AND B.SSM_FAKULTI = D.JMF_KOD()    
            AND B.SSM_NOKP = E.SPR_NOKP    
            AND B.SSM_NOKP = H.TRANSLATOR_NO_KP  
            AND H.ADUAN_PK = ? ; ";
        }

        internal static string SQL_GET_MAKLUMAT_PELAJAR_SAKSI_OKT_PENTERJEMAH()
        {
            return @"
           SELECT B.SSM_MATRIK, E.SPR_NAMA, E.SPR_JANTINA, E.SPR_UGAMA, E.SPR_KAUM, B.SSM_KURSUS, 
            D.JMF_FACULTY, E.SPR_TKHLAHIR, E.SPR_NEGARA, E.SPR_TKHMASUK, 
            G.KETERANGAN, F.SPH_BLOK, F.SPH_BILIK, H.SBG_HANDPHONE, H.SBG_EMAIL_RASMI, 
            (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_KAUM AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK = 4) AS KAUM,
            (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_KAUM  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK = 4) AS KAUM_PK,
            (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_UGAMA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK = 5) AS AGAMA,
            (SELECT H.KOD FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_UGAMA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK = 5) AS AGAMA_PK,
            (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_JANTINA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK = 2) AS JANTINA,
            (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_JANTINA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK = 2) AS JANTINA_PK
            FROM AIMS.ssm_semester@smu8i B, AIMS.sbp_peribadi@smu8i E, AIMS.slg_mplog01@smu8i C, AIMS.kod_fakulti@smu8i D, 
            AIMS.SPH_PENEMPATAN@smu8i F, AIMS.KOD_KOLEJ@smu8i G, AIMS.SBG_PERIBADI@smu8i H
            WHERE B.SSM_NOKP = E.SPR_NOKP
            AND B.SSM_SESISEM = C.SLG_SESISEM
            AND B.SSM_NOKP = F.SPH_NOKP(+)
            AND B.SSM_SESISEM = F.SPH_SESISEM(+)
            AND F.SPH_KOLEJ = G.KOD(+)
            AND F.SPH_DAFTAR(+) = 'Y'
            AND F.SPH_SKELUAR(+) = 'T'
            AND B.SSM_NOKP = H.SBG_NOKP(+)
            AND C.SLG_KOD_PRO = 'AKIB'
            AND B.SSM_FAKULTI = D.JMF_KOD(+)
            AND B.SSM_NOKP = ?";
        }

        ////internal static string SQL_GET_()
        ////{
        ////    return @"
        ////   SELECT B.SSM_MATRIK, E.SPR_NAMA, E.SPR_JANTINA, E.SPR_UGAMA, E.SPR_KAUM, B.SSM_KURSUS, 
        ////    D.JMF_FACULTY, E.SPR_TKHLAHIR, E.SPR_NEGARA, E.SPR_TKHMASUK, 
        ////    G.KETERANGAN, F.SPH_BLOK, F.SPH_BILIK, H.SBG_HANDPHONE, H.SBG_EMAIL_RASMI, 
        ////    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_KAUM AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM, 
        ////    (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_KAUM  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM_PK, 
        ////    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_UGAMA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA, 
        ////    (SELECT H.KOD FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_UGAMA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA_PK, 
        ////    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_JANTINA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA, 
        ////    (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_JANTINA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA_PK 
        ////    FROM ~NAMA_SKEMA~.ssm_semester@smu8i B, ~NAMA_SKEMA~.sbp_peribadi@smu8i E, ~NAMA_SKEMA~.slg_mplog01@smu8i C, ~NAMA_SKEMA~.kod_fakulti@smu8i D, 
        ////    ~NAMA_SKEMA~.SPH_PENEMPATAN@smu8i F, ~NAMA_SKEMA~.KOD_KOLEJ@smu8i G, ~NAMA_SKEMA~.SBG_PERIBADI@smu8i H 
        ////    WHERE B.SSM_NOKP=E.SPR_NOKP 
        ////    AND B.SSM_SESISEM = C.SLG_SESISEM 
        ////    AND B.SSM_NOKP = F.SPH_NOKP(+) 
        ////    AND B.SSM_SESISEM = F.SPH_SESISEM(+) 
        ////    AND F.SPH_KOLEJ = G.KOD(+) 
        ////    AND F.SPH_DAFTAR(+) = 'Y' 
        ////    AND F.SPH_SKELUAR(+) = 'T' 
        ////    AND B.SSM_NOKP = H.SBG_NOKP(+) 
        ////    AND C.SLG_KOD_PRO = 'AKIB' 
        ////    AND B.SSM_FAKULTI = D.JMF_KOD(+) 
        ////    AND B.SSM_NOKP = ?";
        //}

        //internal static string SQL_GET_()
        //{
        //    return @"
        //   SELECT B.SSM_MATRIK, E.SPR_NAMA, E.SPR_JANTINA, E.SPR_UGAMA, E.SPR_KAUM, B.SSM_KURSUS, 
        //    D.JMF_FACULTY, E.SPR_TKHLAHIR, E.SPR_NEGARA, E.SPR_TKHMASUK, 
        //    G.KETERANGAN, F.SPH_BLOK, F.SPH_BILIK, H.SBG_HANDPHONE, H.SBG_EMAIL_RASMI, 
        //    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_KAUM AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM, 
        //    (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_KAUM  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM_PK, 
        //    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_UGAMA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA, 
        //    (SELECT H.KOD FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_UGAMA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA_PK, 
        //    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_JANTINA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA, 
        //    (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_JANTINA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA_PK 
        //    FROM ~NAMA_SKEMA~.ssm_semester@smu8i B, ~NAMA_SKEMA~.sbp_peribadi@smu8i E, ~NAMA_SKEMA~.slg_mplog01@smu8i C, ~NAMA_SKEMA~.kod_fakulti@smu8i D, 
        //    ~NAMA_SKEMA~.SPH_PENEMPATAN@smu8i F, ~NAMA_SKEMA~.KOD_KOLEJ@smu8i G, ~NAMA_SKEMA~.SBG_PERIBADI@smu8i H 
        //    WHERE B.SSM_NOKP=E.SPR_NOKP 
        //    AND B.SSM_SESISEM = C.SLG_SESISEM 
        //    AND B.SSM_NOKP = F.SPH_NOKP(+) 
        //    AND B.SSM_SESISEM = F.SPH_SESISEM(+) 
        //    AND F.SPH_KOLEJ = G.KOD(+) 
        //    AND F.SPH_DAFTAR(+) = 'Y' 
        //    AND F.SPH_SKELUAR(+) = 'T' 
        //    AND B.SSM_NOKP = H.SBG_NOKP(+) 
        //    AND C.SLG_KOD_PRO = 'AKIB' 
        //    AND B.SSM_FAKULTI = D.JMF_KOD(+) 
        //    AND B.SSM_NOKP = ?";
        //}

        internal static string SQL_CAPAI_PERINCIAN_PENGADU_PELAJAR()
        {
            return @"SELECT  
            Z.KAD_MATRIK_PELAJAR,  
            Z.KURSUS_PELAJAR,  
            Z.FAKULTI_PELAJAR,  
            Z.NAMA_PELAJAR,  
            Z.COMPLAINER_NO_KP,  
            Z.ALAMAT1_PELAJAR,  
            Z.ALAMAT2_PELAJAR,  
            Z.ALAMAT3_PELAJAR,  
            Z.EMEL_PELAJAR,  
            (SELECT J.NAMA_PARAMETER FROM HR_PARAMETER J WHERE J.PARAM_PK = Z.JANTINA_PELAJAR AND J.TKH_HAPUS IS NULL AND J.KUMPULAN_FK=2) AS JANTINA_DESKRIPSI,   
            (SELECT J.NAMA_PARAMETER FROM HR_PARAMETER J WHERE J.KOD = TO_CHAR(Z.AGAMA_PELAJAR) AND J.TKH_HAPUS IS NULL AND J.KUMPULAN_FK=5) AS AGAMA_DESKRIPSI,   
            (SELECT J.NAMA_PARAMETER FROM HR_PARAMETER J WHERE J.PARAM_PK = BANGSA_PELAJAR AND J.TKH_HAPUS IS NULL AND J.KUMPULAN_FK=4) AS BANGSA_DESKRIPSI,  
            Z.TKH_LAHIR_PELAJAR,  
            (SELECT Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.PARAM_PK = Z.KENDERAAN1_FK) AS KENDERAAN1,  
            (SELECT Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.PARAM_PK = Z.KENDERAAN2_FK) AS KENDERAAN2,  
            Z.NO_TEL_PELAJAR,  
            Z.REPORT_CATEGORY_FK,  
            Z.REPORT_SUBCATEGORY_FK,  
            Z.CATATAN_ADUAN,  
            Z.COMPLAINER_CATEGORY_FK,  
            Z.TKH_ADUAN,  
            Z.REPORT_NO,  
            TO_CHAR(Z.MASA_ADUAN, 'HH24:MI:SS'),  
            Z.KENDERAAN1_FK, Z.KENDERAAN2_FK,  
            (SELECT Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.KUMPULAN_FK = 67 AND Y.KOD = Z.REPORT_CATEGORY_FK AND Y.TKH_HAPUS IS NULL) AS CATEGORY_PENGADU,  
            (SELECT Y.NAMA_PARAMETER FROM SMU_PARAMETER Y WHERE Y.PARAM_PK = Z.REPORT_SUBCATEGORY_FK AND Y.TKH_HAPUS IS NULL) AS SUB_CATEGORY_PENGADU,  
            GET_DAY(Z.MASA_ADUAN,'dd/mm/yyy'),  
            Z.CATATAN_TINDAKAN   
            FROM HR_BK_ADUAN Z  
            WHERE Z.ADUAN_PK = ? ";
        }

        internal static string SQL_CAPAI_PERINCIAN_PENTERJEMAH_PELAJAR()
        {

            return @"SELECT  
            B.SSM_MATRIK,  
            B.SSM_KURSUS,  
            D.JMF_FACULTY,  
            E.SPR_NAMA,  
            E.SPR_NOKP,  
            F.SPH_BILIK,  
            F.SPH_BLOK,  
            G.KETERANGAN,  
            H.TRANSLATOR_CATEGORY_FK   
            FROM AIMS.ssm_semester@smu8i B, AIMS.slg_mplog01@smu8i C, AIMS.kod_fakulti@smu8i D, AIMS.sbp_peribadi@smu8i E ,  
            AIMS.SPH_PENEMPATAN@smu8i F, AIMS.KOD_KOLEJ@smu8i G, HR_BK_ADUAN H  
            WHERE B.SSM_SESISEM = C.SLG_SESISEM    
            AND B.SSM_NOKP = F.SPH_NOKP()  
            AND B.SSM_SESISEM = F.SPH_SESISEM()  
            AND F.SPH_KOLEJ = G.KOD()  
            AND F.SPH_DAFTAR() = 'Y'  
            AND F.SPH_SKELUAR() = 'T'  
            AND C.SLG_KOD_PRO = 'AKIB'   
            AND B.SSM_FAKULTI = D.JMF_KOD()    
            AND B.SSM_NOKP = E.SPR_NOKP    
            AND B.SSM_NOKP = H.TRANSLATOR_NO_KP  
            AND H.ADUAN_PK = ? ";
        }

        internal static string SQL_CAPAI_PERINCIAN_PENTERJEMAH_STAF()
        {
            return @"SELECT C.NO_PEKERJA,   
            (SELECT B.DESKRIPSI FROM HR_KOD_JAWATAN A, HR_KOD_JENIS_JAWATAN B WHERE C.KOD_JAWATAN = A.KOD_JAWATAN AND A.KOD_KLASIFIKASI = B.KOD_KLASIFIKASI   
            AND A.KOD_JENIS_JAWATAN = B.KOD_JENIS_JAWATAN) AS JAWATAN,   
            A.NAMA,   
            (SELECT D.NO_TEL_BIMBIT FROM HR_ALAMAT D WHERE D.ALAMAT_PK = A.ALAMAT_MENYURAT_FK AND D.TKH_HAPUS IS NULL) AS NO_TEL_BIMBIT ,  
            (SELECT D.EMAIL_RASMI FROM HR_ALAMAT D WHERE D.ALAMAT_PK = A.ALAMAT_PEJABAT_FK AND D.TKH_HAPUS IS NULL) AS EMAIL_RASMI,   
            (SELECT A.NAMA_PARAMETER FROM SMU_PARAMETER A WHERE A.PARAM_PK = F.ZONE_FK AND KUMPULAN_FK=65 AND F.TKH_HAPUS IS NULL) AS ZON,   
            (SELECT A.DESKRIPSI FROM HR_BLOK A WHERE A.BLOK_PK = F.BLOK_FK) AS BLOK,   
            F.LOCATION_RECORD,   
            F.LANGUAGE_FK,   
            F.ACCEPTER_FK,   
            F.TRANSLATOR_CATEGORY_FK,   
            F.COMPLAINER_CATEGORY_FK,   
            (SELECT S.NAMA_PARAMETER FROM SMU_PARAMETER S WHERE S.PARAM_PK = F.TRANSLATOR_CATEGORY_FK) AS TRANSLATOR_KATEGORI,   
            (SELECT T.NAMA_PARAMETER FROM SMU_PARAMETER T WHERE T.PARAM_PK = F.COMPLAINER_CATEGORY_FK) AS complainer_KATEGORI,   
            (SELECT U.NAMA_PARAMETER FROM SMU_PARAMETER U WHERE U.PARAM_PK = F.LANGUAGE_FK) AS BAHASA,   
            F.UNIT_FK,   
            (SELECT A.NAMA_PARAMETER FROM SMU_PARAMETER A WHERE A.PARAM_PK = F.UNIT_FK) AS UNIT,  
            (SELECT S.NAMA_PARAMETER FROM SMU_PARAMETER S WHERE S.PARAM_PK = F.KOD_KAMPUS) AS KAMPUS  
            FROM HR_BK_ADUAN F, HR_MAKLUMAT_PERIBADI A, HR_STAF C  
            WHERE F.TKH_HAPUS IS NULL  
            AND F.ACCEPTER_FK = C.STAF_PK   
            AND A.NO_PEKERJA = C.NO_PEKERJA  
            AND F.ADUAN_PK = ? ";
        }
        internal static string SQL_GET_MAKLUMAT_LAIN2_SAKSI_OKT_PENTERJEMAH()
        {
            return @"SELECT  
            VIS_NOKP,
            VIS_NOHP,
            VIS_EMAIL,
            VIS_NAMA,
            VIS_ALAMAT,
            VIS_POSKOD,
            (SELECT G.DESKRIPSI FROM ADM_NEGARA G WHERE G.KOD_NEGARA = VIS_NEGARA) AS KEWARGANEGARAAN, 
            (SELECT G.DESKRIPSI FROM ADM_NEGERI G WHERE G.KOD_NEGERI = VIS_NEGERI) AS NEGERI, 
            (SELECT J.NAMA_PARAMETER FROM HR_PARAMETER J WHERE J.PARAM_PK = VIS_JANTINA AND J.TKH_HAPUS IS NULL AND J.KUMPULAN_FK=2) AS JANTINA_DESKRIPSI,
            (SELECT J.NAMA_PARAMETER FROM HR_PARAMETER J WHERE J.PARAM_PK = VIS_BANGSA AND J.TKH_HAPUS IS NULL AND J.KUMPULAN_FK=4) AS BANGSA_DESKRIPSI
            FROM HR_GRK_VISITOR_DETAIL
            WHERE 
            VIS_NOKP = ?;";
        }

        internal static string SQL_CAPAI_PERINCIAN_ADUAN_R()//
        {
            return @"SELECT C.NO_PEKERJA,   
            (SELECT B.DESKRIPSI FROM HR_KOD_JAWATAN A, HR_KOD_JENIS_JAWATAN B WHERE C.KOD_JAWATAN = A.KOD_JAWATAN AND A.KOD_KLASIFIKASI = B.KOD_KLASIFIKASI   
            AND A.KOD_JENIS_JAWATAN = B.KOD_JENIS_JAWATAN) AS JAWATAN,   
            A.NAMA,   
            (SELECT D.NO_TEL_BIMBIT FROM HR_ALAMAT D WHERE D.ALAMAT_PK = A.ALAMAT_MENYURAT_FK AND D.TKH_HAPUS IS NULL) AS NO_TEL_BIMBIT ,  
            (SELECT D.EMAIL_RASMI FROM HR_ALAMAT D WHERE D.ALAMAT_PK = A.ALAMAT_PEJABAT_FK AND D.TKH_HAPUS IS NULL) AS EMAIL_RASMI,   
            (SELECT A.NAMA_PARAMETER FROM SMU_PARAMETER A WHERE A.PARAM_PK = F.ZONE_FK AND KUMPULAN_FK=65 AND F.TKH_HAPUS IS NULL) AS ZON,   
            (SELECT A.DESKRIPSI FROM HR_BLOK A WHERE A.BLOK_PK = F.BLOK_FK) AS BLOK,   
            F.LOCATION_RECORD,   
            F.LANGUAGE_FK,   
            F.ACCEPTER_FK,   
            F.TRANSLATOR_CATEGORY_FK,   
            F.COMPLAINER_CATEGORY_FK,   
            (SELECT S.NAMA_PARAMETER FROM SMU_PARAMETER S WHERE S.PARAM_PK = F.TRANSLATOR_CATEGORY_FK) AS TRANSLATOR_KATEGORI,   
            (SELECT T.NAMA_PARAMETER FROM SMU_PARAMETER T WHERE T.PARAM_PK = F.COMPLAINER_CATEGORY_FK) AS complainer_KATEGORI,   
            (SELECT U.NAMA_PARAMETER FROM SMU_PARAMETER U WHERE U.PARAM_PK = F.LANGUAGE_FK) AS BAHASA,   
            F.UNIT_FK,   
            (SELECT A.NAMA_PARAMETER FROM SMU_PARAMETER A WHERE A.PARAM_PK = F.UNIT_FK) AS UNIT,  
            (SELECT S.NAMA_PARAMETER FROM SMU_PARAMETER S WHERE S.PARAM_PK = F.KOD_KAMPUS) AS KAMPUS  
            FROM HR_BK_ADUAN F, HR_MAKLUMAT_PERIBADI A, HR_STAF C  
            WHERE F.TKH_HAPUS IS NULL  
            AND F.ACCEPTER_FK = C.STAF_PK   
            AND A.NO_PEKERJA = C.NO_PEKERJA  
            AND F.ADUAN_PK = ? ";
        }

        internal static string SQL_CARI_ADUAN()
        {
            return @"SELECT  
            A.ADUAN_PK,    
            A.REPORT_NO,     
            A.TKH_ADUAN,     
            TO_CHAR(MASA_ADUAN,'HH:MI:SS AM')AS MASA_ADUAN,     
            (SELECT B.NAMA_PARAMETER FROM SMU_PARAMETER B WHERE B.PARAM_PK=A.COMPLAINER_CATEGORY_FK) AS KATEGORI,   
            (SELECT C.NAMA FROM HR_MAKLUMAT_PERIBADI C, HR_STAF D WHERE C.MAKLUMAT_PERIBADI_PK = D.MAKLUMAT_PERIBADI_FK AND    
            D.STAF_PK=A.COMPLAINER_FK) AS NAMA,    
            (SELECT C.NO_PEKERJA FROM HR_MAKLUMAT_PERIBADI C, HR_STAF D WHERE C.MAKLUMAT_PERIBADI_PK = D.MAKLUMAT_PERIBADI_FK AND    
            D.STAF_PK=A.COMPLAINER_FK) AS NO_PEKERJA,  
            (SELECT C.NO_KP_BARU FROM HR_MAKLUMAT_PERIBADI C, HR_STAF D WHERE C.MAKLUMAT_PERIBADI_PK = D.MAKLUMAT_PERIBADI_FK AND    
            D.STAF_PK=A.COMPLAINER_FK) AS IC_STAF,  
            A.NAMA_PELAJAR,  
            A.COMPLAINER_NO_KP,  
            (SELECT C.NAMA FROM HR_MAKLUMAT_PERIBADI C WHERE C.MAKLUMAT_PERIBADI_PK = A.MAKLUMAT_PERIBADI_FK AND C.TKH_HAPUS IS NULL) AS NAMA_LAIN2,    
            (SELECT C.NO_KP_BARU FROM HR_MAKLUMAT_PERIBADI C WHERE C.MAKLUMAT_PERIBADI_PK = A.MAKLUMAT_PERIBADI_FK AND C.TKH_HAPUS IS NULL) AS IC_LAIN,   
            (SELECT B.NAMA_PARAMETER FROM SMU_PARAMETER B WHERE B.PARAM_PK=A.STATUS_FK) AS STATUS,  
            A.UNIT_FK,    
            A.REPORT_CATEGORY_FK,    
            A.REPORT_SUBCATEGORY_FK,    
            A.KOD_KAMPUS  
            FROM HR_BK_ADUAN A    
            WHERE A.TKH_HAPUS IS NULL "; ;
        }

        internal static string GET_MAKLUMAT_STAF_SAKSI_OKT_PENTERJEMAH()
        {
            return @"SELECT 
           A.NO_PEKERJA,  
           (SELECT D.DESKRIPSI FROM HR_KOD_JENIS_JAWATAN D, HR_KOD_JAWATAN E WHERE D.KOD_JENIS_JAWATAN=E.KOD_JENIS_JAWATAN AND D.KOD_KLASIFIKASI= E.KOD_KLASIFIKASI AND E.KOD_JAWATAN=A.KOD_JAWATAN) AS JAWATAN, 
           (SELECT B.NAMA FROM HR_MAKLUMAT_PERIBADI B WHERE B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS NAMA, 
           (SELECT C.NO_TEL_BIMBIT FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_MENYURAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS NO_HP, 
           (SELECT C.EMAIL_RASMI FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS EMEL, 
           (SELECT B.NO_KP_BARU FROM HR_MAKLUMAT_PERIBADI B WHERE B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS NO_KP_BARU, 
           (SELECT C.ALAMAT1 FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS ALAMAT1, 
           (SELECT C.ALAMAT2 FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS ALAMAT2, 
           (SELECT C.ALAMAT3 FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS ALAMAT3, 
           (SELECT C.POSKOD FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS POSKOD, 
           (SELECT E.DESKRIPSI FROM ADM_NEGERI E WHERE E.KOD_NEGERI = (SELECT C.KOD_NEGERI FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK)) AS NEGERI, 
           (SELECT F.DESKRIPSI FROM ADM_DAERAH F WHERE F.KOD_DAERAH = (SELECT C.KOD_DAERAH FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK)) AS DAERAH, 
           (SELECT D.DESKRIPSI FROM ADM_BANDAR D WHERE D.KOD_BANDAR = (SELECT C.KOD_BANDAR FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK)) AS BANDAR, 
           (SELECT G.DESKRIPSI FROM ADM_NEGARA G WHERE G.KOD_NEGARA = (SELECT C.KOD_NEGARA FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK)) AS NEGARA, 
           (SELECT I.NAMA_PARAMETER FROM HR_PARAMETER I, HR_MAKLUMAT_PERIBADI B WHERE KOD = B.AGAMA AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK AND KUMPULAN_FK=5) AS AGAMA, 
           (SELECT B.JANTINA FROM HR_MAKLUMAT_PERIBADI B WHERE B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS JANTINA, 
           (SELECT I.NAMA_PARAMETER FROM HR_PARAMETER I, HR_MAKLUMAT_PERIBADI B WHERE PARAM_PK = B.BANGSA_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS BANGSA, 
           (SELECT G.DESKRIPSI FROM ADM_NEGARA G WHERE G.KOD_NEGARA = (SELECT B.KOD_WARGANEGARA FROM HR_MAKLUMAT_PERIBADI B WHERE B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK)) AS KEWARGANEGARAAN, 
           (SELECT B.TKH_LAHIR FROM HR_MAKLUMAT_PERIBADI B WHERE B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS TARIKH_LAHIR, 
           (SELECT C.NO_TEL_PEJABAT FROM HR_ALAMAT C, HR_MAKLUMAT_PERIBADI B WHERE C.ALAMAT_PK = B.ALAMAT_PEJABAT_FK AND B.MAKLUMAT_PERIBADI_PK=A.MAKLUMAT_PERIBADI_FK) AS NO_TEL_PEJABAT, 
           (SELECT DESKRIPSI FROM HR_FAKULTI WHERE KOD_FAKULTI=SUBSTR(A.KOD_PTJ,0,3)) AS FAKULTI, 
           A.STAF_PK, 
           A.MAKLUMAT_PERIBADI_FK 
           FROM  
           HR_STAF A 
           WHERE 
           A.STAF_PK=?
           A.NO_PEKERJA=? 
           AND A.STATUS_AKTIF != 'N' 
           AND A.STATUS_ASSTATK < '45' AND A.STATUS_ASSTATK NOT IN ('O9','32','34')";
        }
    }

}
