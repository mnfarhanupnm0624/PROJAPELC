namespace APELC.LocalServices.Aduan
{
    public class StatistikSQL
    {
        internal static string DB_MtdGetMaklumatAsas()
        {
            return @" SELECT 
                            A.NO_PEKERJA, 
                            UPPER(B.NAMA) AS NAMA, 
                            C.KOD_JAWATAN || ' - '|| D.DESKRIPSI AS JAWATAN_DESC,
                            SUBSTR(A.KOD_PTJ,0,3) || ' - '|| E.DESKRIPSI AS FAKULTI,
                            B.TKH_LAHIR, 
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
                            M.NO_TEL_BIMBIT
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
                    WHERE 
                            A.STAF_PK= :STAF_PK AND A.TKH_HAPUS IS NULL ";
        }

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
                (SELECT (UPPER(NAMA_PARAMETER)) FROM SMU_PARAMETER WHERE PARAM_PK = B.KATEGORI_KES_FK AND TKH_HAPUS IS NULL) AS KATEGORI_KES_DESC,
                DECODE (SUBSTR(A.REPORT_NO,1,2), 'PG','PAGOH','KL','KUALA LUMPUR','JB','JOHOR BAHRU') AS KAMPUS_DESC,
                (SELECT (UPPER(TRIM(NAMA)) || '~' || NO_KP_BARU) FROM HR_MAKLUMAT_PERIBADI WHERE MAKLUMAT_PERIBADI_PK = A.MAKLUMAT_PERIBADI_FK AND TKH_HAPUS IS NULL) AS INFO_LAIN,
                C.Aduan_PK AS Aduan_PK,
                A.STATUS_FK AS STATUS_FK
            FROM 
                HR_BK_ADUAN A
                LEFT JOIN HR_BK_TINDAKAN B ON B.ADUAN_FK = A.ADUAN_PK AND B.TKH_HAPUS IS NULL
                INNER JOIN HR_INV_SIASATAN C ON C.TINDAKAN_FK = B.TINDAKAN_PK AND C.TKH_HAPUS IS NULL
             ";

        internal static string SQL_MtdGetApelPengadu()
        {
            string _SQL = SqlGetApelPengaduInfo +
                @" WHERE
                        A.TKH_HAPUS IS NULL
                        AND B.STATUS_FK IN ('378') ";

            _SQL += @" AND A.ADUAN_PK = :ADUAN_PK ";

            _SQL += @" ORDER BY A.TKH_ADUAN DESC ";

            return _SQL;
        }

        internal static string SqlGetStafPenyiasat =
            @"SELECT
                A.ADUAN_PK AS ADUAN_PK,
                A.TKH_ADUAN,
                A.STATUS_FK AS STATUS_FK,
                C.ApelC_PK AS SIASATAN_PK,
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
                LEFT JOIN HR_BK_TINDAKAN B ON B.ADUAN_FK = A.ADUAN_PK AND B.TKH_HAPUS IS NULL
                INNER JOIN HR_INV_SIASATAN C ON C.TINDAKAN_FK = B.TINDAKAN_PK AND C.TKH_HAPUS IS NULL
                INNER JOIN HR_INV_DAFTAR_PNYST D ON D.ApelC_FK = C.ApelC_PK AND D.TKH_HAPUS IS NULL
             ";

        internal static string SQL_MtdGetStafPenyiasatList()
        {
            string _SQL = SqlGetStafPenyiasat +
                @" WHERE
                       A.TKH_HAPUS IS NULL
                       AND B.STATUS_FK IN ('378') ";

            _SQL += @" AND D.ApelC_FK = :SIASATAN_PK";

            _SQL += @" ORDER BY A.TKH_ADUAN DESC ";

            return _SQL;
        }

        internal static string SqlGetApelStudentInfo =
            @"SELECT 
                    B.SSM_MATRIK AS MATRIK, 
                    B.SSM_NOKP AS SPR_NOKP, 
                    E.SPR_NAMA AS NAMA_PELAJAR, 
                    E.SPR_JANTINA, 
                    E.SPR_UGAMA, 
                    E.SPR_KAUM, 
                    B.SSM_KURSUS, 
                    D.JMF_FACULTY, 
                    E.SPR_TKHLAHIR, 
                    E.SPR_NEGARA, 
                    E.SPR_TKHMASUK, 
                    G.KETERANGAN, 
                    F.SPH_BLOK, 
                    F.SPH_BILIK, 
                    H.SBG_HANDPHONE, 
                    H.SBG_EMAIL_RASMI, 
                    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_KAUM AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM, 
                    (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_KAUM  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=4) AS KAUM_PK, 
                    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_UGAMA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA, 
                    (SELECT H.KOD FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_UGAMA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=5) AS AGAMA_PK, 
                    (SELECT H.NAMA_PARAMETER FROM HR_PARAMETER H  WHERE H.KOD_KESETARAAN = E.SPR_JANTINA AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA, 
                    (SELECT H.PARAM_PK FROM HR_PARAMETER H WHERE H.KOD_KESETARAAN = E.SPR_JANTINA  AND H.TKH_HAPUS IS NULL AND H.KUMPULAN_FK=2) AS JANTINA_PK,
                    '1' as RESULTSET
                    FROM AIMS.ssm_semester@smu8i B, AIMS.sbp_peribadi@smu8i E, AIMS.slg_mplog01@smu8i C, AIMS.kod_fakulti@smu8i D, 
                    AIMS.SPH_PENEMPATAN@smu8i F, AIMS.KOD_KOLEJ@smu8i G, AIMS.SBG_PERIBADI@smu8i H
                WHERE 
                    B.SSM_SESISEM = C.SLG_SESISEM 
                    AND B.SSM_NOKP = F.SPH_NOKP(+) 
                    AND B.SSM_SESISEM = F.SPH_SESISEM(+) 
                    AND F.SPH_KOLEJ = G.KOD(+) 
                    AND F.SPH_DAFTAR(+) = 'Y' 
                    AND F.SPH_SKELUAR(+) = 'T' 
                    AND B.SSM_NOKP = H.SBG_NOKP(+) 
                    AND C.SLG_KOD_PRO = 'AKIB' 
                    AND B.SSM_FAKULTI = D.JMF_KOD(+)
                    AND B.SSM_NOKP=E.SPR_NOKP
             ";

        internal static string SQL_GetStudentRecord(string _nokp)
        {
            string _SQL = SqlGetApelStudentInfo;

            _SQL += " AND B.SSM_NOKP = '" + _nokp + "' ";
            return _SQL;
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

        // End: 
        // DDL

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
