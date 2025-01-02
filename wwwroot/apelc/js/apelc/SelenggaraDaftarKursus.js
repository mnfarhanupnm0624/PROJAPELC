"use strict";
var KatAduan = function () {
    var _varInit = function () {

        fnRedrawPaparList();
        $('.form-select').select2();
        $("#m_modal_KatAduan").on('hidden.bs.modal', function (event) {
            $('#IdTxtKatAduan').val('');
            $('#IdTxtKatAduanEn').val('');
        });

        $("input[name='SwitchAktif']").change(function () {
            var _status = "5063";
            if ($(this).prop("checked")) {
                _status = "5062";
            }

            fnSwitchStatus($(this), _status);
        });

        $('#IdBtnTambahKatAduan').click(function (e) {
            var _ok = true;
            var _status = $(this).data("id");
            var _cAduanPkEnc = $('#IdAduanPkEnc').val();
            var _stafFkEnc = $('#IdStafFkEnc').val();

            var _katAduan = $('#IdTxtKatAduan').val();
            var _katAduanEn = $('#IdTxt_katAduanEn').val();

            var formData = new FormData();
            //formData.append('file', $('#IdFile')[0].files[0]); // myFile is the input type="file" control
            formData.append('ADUAN_PK_ENC', _cAduanPkEnc);
            formData.append('KAT_ADUAN_DESC', _katAduan);
            formData.append('KAT_ADUAN_DESC_EN', _katAduanEn);

            if (_katAduan.length == 0 || _katAduanEn.length == 0) {

                var _msg = "Sila Isi Maklumat Kategori Aduan!";
                var _type = "warning";

                fnPublicMessageOnly(_msg, _type);
                _ok = false;
            }

            if (_ok) {
                $('#IdBtnTambahKatAduan').hide();
                $('#IdBtnTambahKatAduanGray').show();
                fnUploadAttachDocLain(formData, _status);
            }
        });

        $('#IdBtnKemaskiniKatAduan').click(function (e) {
            var _ok = true;
            var _status = $(this).data("id");
            var _cAduanPkEnc = $('#IdAduanPkEnc').val();
            var _stafFkEnc = $('#IdStafFkEnc').val();

            var _katAduan = $('#IdTxtKatAduan').val();
            var _katAduanEn = $('#IdTxt_katAduanEn').val();

            var formData = new FormData();
            formData.append('file', $('#IdFile')[0].files[0]); // myFile is the input type="file" control
            formData.append('ADUAN_PK_ENC', _cAduanPkEnc);
            formData.append('KAT_ADUAN_DESC', _katAduan);
            formData.append('KAT_ADUAN_DESC_EN', _katAduanEn);

            if (_ok) {
                $('#IdBtnKemaskiniKatAduan').hide();
                $('#IdBtnKemaskiniKatAduanGray').show();
                fnUploadAttachDocLainUpdate(formData, _status);
            }
        });
    }
    return {
        init: function () {
            _varInit();
        }
    };
}();


jQuery(document).ready(function () {
    KatAduan.init();
});

function fnRedrawPaparList() {
    var table = $("#IdTableKatAduanList").DataTable({
        "language": {
            "lengthMenu": "Show _MENU_",
        },
        "dom":
            "<'row'" +
            "<'col-sm-6 d-flex align-items-center justify-conten-start'f>" +
            "<'col-sm-6 d-flex align-items-center justify-content-end'B>" +
            ">" +
            "<'table-responsive'tr>" +
            "<'row'" +
            "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'li>" +
            "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
            ">",
        "buttons": [
            {
                extend: 'print'
            },
            "pdfHtml5",
            "excelHtml5"
        ],
    });

};

function fnTResult(res, _status) {
    var _path = $('#IdPathServer').val();
    if (res != null) {

        if (_status === "SIMPAN") {
            _status = "Simpan";
            $('#IdBtnTambahKatAduan').show();
            $('#IdBtnTambahKatAduanGray').hide();
        } else {
            _status = "Kemaskini";
            $('#IdBtnKemaskiniKatAduan').show();
            $('#IdBtnKemaskiniKatAduanGray').hide();
        }

        if (res.RESULTSET == "2") {
            Swal.fire({
                title: "Berjaya!",
                text: "Maklumat berjaya " + _status,
                icon: "success",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    window.location.href = _path + '/SelenggaraAduan/SelenggaraKatAduanList';
                }
            });
        }
        else {
            Swal.fire({
                title: "Tidak Berjaya!",
                text: "Maklumat Tidak Berjaya " + _status,
                icon: "warning",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    window.location.href = _path + '/SelenggaraAduan/SelenggaraKatAduanList';
                }
            });
        }
    }
}
function fnSwitchStatus(id, status) {

    //console.log(status);
    //return false;

    var _val = $(id).val();
    var _text = _val.split("~~");
    var _pkencnya = _text[0];
    //var _status = _text[1];

    var _path = $('#IdPathServer').val();
    var event = _path + '/SelenggaraAduan/MtdKemaskiniStatusAktifKatAduan';
    var _data = JSON.stringify({
        'KAT_ADUAN_PK_ENC': _pkencnya,
        'STATUS_AKTIF': status
    });

    $(function () {
        // load list Modul first
        //var _path = $('#IdPathServer').val();
        //var eventDDL = _path + '/Menu/MtdCapaiDDLKumpulanDataAjax';

        $.ajax({
            type: "POST",
            url: event,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: _data,
            success: function (res) {
                var _res = res.RESULTSET;
                if (_res == "2") {
                    Swal.fire(
                        "Maklumat telah berjaya dikemaskini!",
                        "",
                        "success"
                    ).then(function () {
                        var btn_kemaskini = $(id).parent().parent().closest('td').next().find(".btn_kemaskini");
                        var btn_hapus = $(id).parent().parent().closest('td').next().find(".btn_hapus");
                        var _stringData = res.PT_MODUL_PK_ENC + "~~KEMASKINI";
                        console.log(btn_kemaskini[0]);

                        if ($(id).prop("checked")) {
                            btn_kemaskini.removeClass("disabled");
                            btn_hapus.removeClass("disabled");
                            btn_kemaskini.attr("data-id", _stringData);
                            btn_hapus.attr("data-id", _stringData);

                        }
                        else {
                            btn_kemaskini.addClass("disabled");
                            btn_hapus.addClass("disabled");
                            btn_kemaskini.removeAttr("data-id");
                            btn_hapus.removeAttr("data-id");

                        }

                    });

                    //console.log($(id).parent().parent());

                } else {
                    Swal.fire(
                        "Maklumat tidak berjaya dikemaskini!",
                        "",
                        "error"
                    );
                }
            },
            error: function (xhr, _httpStatusMessage, customErrorMessage) {
                if (xhr.status === 410 || xhr.status === 500) {
                    alert(customErrorMessage);
                } else {
                    toastr.warning("", "<font color='#800000'>Error Code</font>");
                }
            }
        });
    });

}


$("#IdBtnModalSaveData").click(function (e) {
    var _ok = true;

    var _kodProcess = $('#IdModalItemHiddenKodProcessModal').val();
    var kataduanEnc = $('#IdKatAduanPKEncModal').val();

    // get values from input field
    var KatAduan = $('#IdDdKatAduan').val();
    var KatAduanEn = $('#IdDdKatAduanEn').val();

    // validate mandatory fields
    if (KatAduan == "" || KatAduanEn == "") {
        Swal.fire(
            "Sila lengkapkan ruangan yang bertanda wajib (*) terlebih dahulu.",
            "",
            "warning"
        );
        _ok = false;
    }




    var _path = $('#IdPathServer').val();
    var event = _path + "/SelenggaraAduan/CRUDSelenggaraKatAduan";
    //alert(KatAduan);
    //return false;
    var _data = JSON.stringify({

        'KODPROSES': _kodProcess,
        'KAT_ADUAN_PK_ENC': kataduanEnc,
        'STATUS_AKTIF': '5062',
        'KAT_ADUAN_DESC': KatAduan,
        'KUMPULAN_FK': '67',
        'KAT_ADUAN_DESC_EN': KatAduanEn,



    });

    if (_ok == true) {
        $.ajax({
            type: "POST",
            url: event,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: _data,
            success: function (res) {

                if (res.RESULTSET == "1") {
                    Swal.fire(
                        "Rekod Berjaya Di Simpan",
                        "",
                        "success"
                    ).then(function () {
                        location.href = _path + "/SelenggaraAduan/SelenggaraKatAduanList";
                        //$('#IdBtnKodItemRefreshScreen').click().delay(8500);
                    });
                }
                else {
                    Swal.fire(
                        "Failed!",
                        "Rekod tidak berjaya disimpan",
                        "error"
                    );
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {

                if (xhr.status === 410 || xhr.status === 500) {
                    Swal.fire(
                        "Failed!",
                        "Rekod tidak berjaya disimpan",
                        "error"
                    );
                } else {
                    Swal.fire(
                        "Failed!",
                        "Rekod gagal dicapai",
                        "error"
                    );
                }

            }





        })

    }


});

$(document).ready(function () {
    $('#m_modal_KatAduan').on('shown.bs.modal', function () {
        var fileToRead = document.getElementById("IdFile");
        //fnsemakfile(fileToRead);
    });

    $('#m_modal_KatAduanUpdate').on('shown.bs.modal', function () {
        var fileToRead = document.getElementById("IdFileUpdate");
        //fnsemakfile(fileToRead);
    });
});


//function fnCallModalTambahKatAduan(id)
//{
//    var _ok = true;
//    /*var _dataid = $(id).attr("data-id");*/
//    /* var _text = _dataid.split("~~");*/
//    //var _path = $('#IdPathServer').val();
//    //var _data = JSON.stringify({
//    //    'KAT_ADUAN_PK_ENC': _aduanPkEnc,
//    //});


//    //var _KatAduanPkEnc = $('#Id_KatAduanPkEnv').val();
//    //var _KatAduan = $('#idKatAduan').val();
//    //var _KatAduanEn = $('#idKatAduanEn').val();
//   /* var _SubkatAduan = $('#idSubkatAduan').val();*/

//    //if (_KatAduan === "" || _KatAduanEn === "" || _KatAduanPkEnc === "") {
//    //    var _msg = "Sila Masukkan Maklumat yang Berkaitan!";
//    //    var _type = "warning";

//    //    fnPublicMessageOnly(_msg, _type)
//    //    _ok = false;
//    //}
//    //$.ajax({
//    //    type: "POST",
//    //    url: _path + "/SelenggaraAduan/MtdGetPerananInvLampiranAjax",
//    //    dataType: "json",
//    //    contentType: "application/json; charset=utf-8",
//    //    data: _data,
//    //    success: function (res) {
//    //        var _html = "";
//    //        if (res != null) {
//    //            $('#IdDdKategoriKod').html(_html);
//    //            $('#IdDdKategoriKod').append('<option value="">-- Sila Pilih --</option>');
//    //            _html = '<option value="' + res.KOD + '">' + res.NAMA_PARAMETER + '</option>';
//    //            $('#IdDdKategoriKod').append(_html);
//    //            $('#IdPerananDoc').val(res.NAMA_PARAMETER);
//    //        }
//    //    },
//    //    error: function (xhr, httpStatusMessage, customErrorMessage) {
//    //        if (xhr.status === 410 || xhr.status === 500) {
//    //            alert(customErrorMessage);
//    //        } else {
//    //            toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
//    //        }
//    //    }
//    //});

//    if (_ok) {
//        $('#m_modal_KatAduan').modal('show');
//    }
//}

//function fnCallModalItem(id) {
//    var _siasatanPkEnc = $('#IdSiasatanPkEnc').val();
//    var _kodBrg = $('#IdKategoriKod').val();

//    var _path = $('#IdPathServer').val();
//    var _data = JSON.stringify({
//        'SIASATAN_PK_ENC': _siasatanPkEnc,
//        'KATEGORI_KOD_FK': _kodBrg
//    });

//    $.ajax({
//        type: "POST",
//        url: _path + "/Siasatan/MtdGetPerananInvLampiranAjax",
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        data: _data,
//        success: function (res) {
//            var _html = "";
//            if (res != null) {
//                $('#IdDdKategoriKod').html(_html);
//                $('#IdDdKategoriKod').append('<option value="">-- Sila Pilih --</option>');
//                _html = '<option value="' + res.KOD + '">' + res.NAMA_PARAMETER + '</option>';
//                $('#IdDdKategoriKod').append(_html);
//                $('#IdPerananDoc').val(res.NAMA_PARAMETER);
//            }
//        },
//        error: function (xhr, httpStatusMessage, customErrorMessage) {
//            if (xhr.status === 410 || xhr.status === 500) {
//                alert(customErrorMessage);
//            } else {
//                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
//            }
//        }
//    });

//    $('#m_modal_kodItem').modal('show');
//}



function fnCallModalSelenggaraKatAduan(id) {

    var _dataid = $(id).attr("data-id");
    //alert(_dataid); // 0~~SIMPAN
    var _text = _dataid.split("~~");
    var _pkenc = _text[0]; //answer= 0
    var _kod = _text[1]; ////answer=SIMPAN





    //alert(_pkenc);
    //return false;
    //untuk KEMASKINI
    var _path = $('#IdPathServer').val();
    var event = _path + '/SelenggaraAduan/MtdGetSelenggaraKatAduan';
    var _data = JSON.stringify({
        'KAT_ADUAN_PK_ENC': _pkenc

    });

    //alert(_pkenc.length);
    //console.log(event);
    //console.log(_pkenc);
    //console.log(_data);
    //return false;


    if (_pkenc.length > 1) {
        // then load the selection value
        $('#m_modal_KatAduan').modal('show');
        var _textNya = "Kemaskini";
        if (_kod == " KEMASKINI") {
            _textNya = "Kemaskini ";
        }

        $('#IdButangActionModal').html(_textNya);
        $('#IdPaparCodeModal').html(_kod + " REKOD");

        //$('#IdPaparCodeModal').html(" (" + _kod + ") ");
        //$('#IdBtnModalSaveData').val(_kod);

        //return false;

        $.ajax({
            type: "POST",
            url: event,
            dataType: "json",
            contentType: "application/json; charset=utf-8",

            data: _data,
            success: function (res) {
                /*Swal.close();*/

                console.log(res);
                var _res = res.RESULTSET;
                if (_res == "1") {
                    var _textNya = "Kemaskini";
                    if (_kod == " KEMASKINI") {
                        _textNya = "Kemaskini ";
                    }

                    /* $('#IdModalItemHiddenKodProcessModal').val('KEMASKINI');*/
                    $('#IdButangActionModal').html(_textNya);
                    $('#IdPaparCodeModal').html(_kod + " REKOD");
                    $('#IdModalItemHiddenKodProcessModal').val(_kod);
                    $('#IdKatAduanPKEncModal').val(_pkenc);
                    //$('#IdTahunAnugerah').val(res.TAHUN);
                    $("#IdDdKatAduan").val(res.KAT_ADUAN_DESC);
                    //$('#IdNamaAnugerah').val(res.ANUGERAH_PK);
                    $("#IdDdKatAduanEn").val(res.KAT_ADUAN_DESC_EN);


                    //$('#IdKodKompetensi').val(res.TAHUN);
                    //$('#IdNamaKompetensi').val(res.TKH_BYR_ROC);



                    $('#m_modal_KatAduan').modal('show');
                } else {
                    toastr.warning("", "<font color='#800000'>TIADA REKOD UNTUK DIKEMASKINI</font>");
                }
            },
            error: function (xhr, _httpStatusMessage, customErrorMessage) {
                if (xhr.status === 410 || xhr.status === 500) {
                    alert(customErrorMessage);
                } else {
                    toastr.warning("", "<font color='#800000'>Error Code</font>");
                }
            }
        });
    } else {

        var _textNya = "Simpan";
        if (_kod == "SIMPAN") {
            _textNya = "Simpan";
        }
        /* $('#IdModalItemHiddenKodProcessModal').val('KEMASKINI');*/
        $('#IdButangActionModal').html(_textNya);
        $('#IdPaparCodeModal').html(_kod + " REKOD");
        //$('#IdTahunAnugerah').val(res.TAHUN);
        $("#IdDdKatAduan").val('');
        //$('#IdNamaAnugerah').val(res.ANUGERAH_PK);
        $("#IdDdKatAduanEn").val('');
        //$('#IdTahunGaji').val($("#DdlTahun").val());
        $('#IdModalItemHiddenKodProcessModal').val('SIMPAN');
        $('#IdKatAduanPKEncModal').val('');
        /*$('#IdKlusterKompetensiPKEncModal').val(KOMPETENSI_UTAMA_PK_ENC);*/
        //$('#IdKodKompetensi').val(CKOD_KOMPETENSI_UTAMA);
        //$('#IdNamaKompetensi').val(CNAMA_KOMPETENSI_UTAMA);


        var myModal = new bootstrap.Modal(document.getElementById('m_modal_KatAduan'), {
            keyboard: false
        })

        myModal.show();

    }


}


jQuery(document).ready(function () {
    Index.init();
    if ($('#IdViewBagRes').val() == '7') {
        var _msg = $('#IdViewBagTxt').val();
        fnGenralMessage(_msg);
    }
});

//function fnGenralMessage(_msg)
function fnPublicMessageTimeout(_msg) {
    swal({
        title: "Alert!",
        text: _msg,
        type: "info",
        showCancelButton: false,
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    }, function () {
        setTimeout(function () {
            $('#IdLayout_ImageKiri').click();
        }, 200);
    });
};

//function fnGenralMessageOnly
function fnPublicMessageOnly(_title, _msg, _type) {
    swal({
        title: _title,
        text: _msg,
        type: _type,
        showCancelButton: false,
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    });
};


function fnGetCurrentDateTime() {
    var d = new Date();
    return d.getFullYear + "-" + d.getMonth + "-" + d.getDay + "T" + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
};