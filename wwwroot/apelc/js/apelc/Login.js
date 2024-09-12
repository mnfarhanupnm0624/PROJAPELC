﻿
"use strict";
var Index = function () {
    var _dataTableInit = function () {
        //  $('#kt_modal_log_masuk_submit').trigger("click",function (e) {
        //    swal({
        //        title: "Berjaya!",
        //        text: "Berjaya Log Masuk",
        //        type: "success",
        //        showCancelButton: false,
        //        closeOnConfirm: false,
        //        showLoaderOnConfirm: true
        //    }, function () {
        //        setTimeout(function () {
        //            $('#IdLayout_ImageKiri').trigger("click");
        //        }, 500);
        //    });
        //});

        $('#kt_modal_log_masuk_submit').click(function (e) {
            var _ok = true;
            var _path01 = $('#IdPathServerMain').val();
            var _userId = $('#Idusername').val().toLowerCase().trim();
            var _password = $('#Idpassword').val();

            var _loginpath = $('#id_loginPath').val();

            if (_userId == null || _userId.length == 0) {
                toastr.warning("<font color='#800000'> Please Fill-In User ID.</font>");
                _ok = false;
            }
            if (_password == null || _password.length == 0) {
                toastr.warning("<font color='#800000'> Please Fill-In Password.</font>");
                _ok = false;
            }
            if (_ok) {
                $('#kt_modal_log_masuk_submitGray').show();
                $('#kt_modal_log_masuk_submit').hide();
                var event = _path01 + '/Login/MtdGetVerifyUserIdPasswordAjax';
                var _data = JSON.stringify({
                    'NO_PEKERJA': _nopekerja, 'NO_MATRIK': _nomatrik, 'NO_PEKERJA': _nopekerja, 'KATA_LALUAN_PENGGUNA': _password, 'JENIS_PERANAN_FK': _role
                });
                //if (_userId == 'zaman') {
                //    $('#IdModalLoginLable').html(event);
                //}
                $.ajax({
                    type: "POST",
                    url: event,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: _data,
                    success: function (res) {
                        if (res.RESULTSET == "1") {
                            $('#IdModalLoginLable').html('');
                            $('#IdButtonClose').click();
                            //swal("Success!", "Successfully Login", "success");
                            swal({
                                title: "Berjaya Log Masuk!",
                                text: "Klik OK untuk teruskan.",
                                type: "success",
                                showCancelButton: false,
                                closeOnConfirm: false,
                                showLoaderOnConfirm: true
                            }, function () {
                                setTimeout(function () {
                                    if (_loginpath == "apelc") {
                                        location.href = _path01 + "/Login/LoginPageApelC";
                                    }
                                    else {
                                        if (res.NAMA_PERANAN == "PENGGUNA SUPER" || res.NAMA_PERANAN == "PENTADBIR/URUSETIA(APEL)" || 
                                        res.NAMA_PERANAN == "BENDAHARI" || res.NAMA_PERANAN == "PEMOHON"||
                                        res.NAMA_PERANAN == "PENGAWAS UJIAN CABARAN" || res.NAMA_PERANAN == "PANEL PENILAI" ||
                                        res.NAMA_PERANAN == "MODERATOR" || res.NAMA_PERANAN == "PENASIHAT AKADEMIK" ||
                                        res.NAMA_PERANAN == "PENGGUBAL DOKUMEN" || res.NAMA_PERANAN == "PENILAI INSTRUMEN" ||
                                        res.NAMA_PERANAN == "JK FAKULTI(TIMBALAN DEKAN)" || res.NAMA_PERANAN == "SENAT(DEKAN)" ||
                                        res.NAMA_PERANAN == "JK FAKULTI(KERANI JABATAN)") {
                                            document.getElementById("myform").submit();
                                        } else {
                                            $('#IdLayout_ImageKiri').onclick();
                                        }
                                    }
                              }, res.SESSION_TIMEOUT);
                            });
                        } else {
                            if (res.NAMA_PERANAN == "TIADA") {
                                swal({
                                    title: "Tiada Kebenaran Akses Sistem. Sila Hubungi Pentadbir Sistem atau daftar akaun baru",
                                    text: "Klik OK untuk teruskan.",
                                    type: "warning",
                                    showCancelButton: false,
                                    closeOnConfirm: false,
                                    showLoaderOnConfirm: true
                                });
                            } else {
                                swal({
                                    title: res.RESULTSET_TEXT,
                                    text: "Klik OK untuk teruskan.",
                                    type: "error",
                                    showCancelButton: false,
                                    closeOnConfirm: false,
                                    showLoaderOnConfirm: true
                                });
                            }
                            $('#kt_modal_log_masuk_submitGray').hide();
                            $('#kt_modal_log_masuk_submit').show();
                        }
                    },
                    error: function (xhr, httpStatusMessage, customErrorMessage) {
                        if (xhr.status === 410 || xhr.status === 500) {
                            alert('Error - ' + customErrorMessage);
                        } else {
                            toastr.warning("<font color='#800000'> Error other than 410-500.</font>");
                        }
                    }
                });
                $('#overlay2').hide();
            }
        });

        $('#log_masuk_form').trigger("click",function (e) {
            var _ok = true;
            var _path01 = $('#IdPathServerMain').val();
            var _userId = $('#Idusername').val().toLowerCase().trim();
            var _password = $('#Idpassword').val();
            var _jenisApel = $('#Idjenis_APEL_param').val();

            $('.form-select form-select-solid').select2();

            $(document).ready(function () {
                var a = 0;
            });

            function loadJenisAPEL(obj) {
                var value = obj.value;
                var url = "@baseurl";

                $.post(url + "Login/LoginPageApelC", { _jenisApel: value }, function (data) {
                    debugger;
                    PopulateDropDown("#Idjenis_APEL_param", data);
                });
            }

            function PopulateDropDown(_jenisApel, list, selectedId) {
                $(_jenisApel).empty();
                $(_jenisApel).append("<option>--Sila Pilih--</option>")
                $.each(list, function (index, row) {
                    $(_jenisApel).append("<option value='" + row.id + "'>" + row.name + "</option>")
                });
            }
            var _loginpath = $('#id_loginPath').val();

            if (_userId == null || _userId.length == 0) {
                toastr.warning("<font color='#800000'> Sila masukkan ID Pengguna.</font>");
                _ok = false;
            }
            if (_password == null || _password.length == 0) {
                toastr.warning("<font color='#800000'> Sila masukkan Kata Laluan.</font>");
                _ok = false;
            }
            if (_jenisApel == null) {
                toastr.warning("<font color='#800000'> Sila pilih Jenis APEL.</font>");
                _ok = false;
            }          
            if (_ok) {
                $('#kt_modal_log_masuk_submitGray').show();
                $('#kt_modal_log_masuk_submit').hide();
                var event = _path01 + '/Login/MtdGetVerifyUserIdPasswordAjax';
                var _data = JSON.stringify({
                    'USERNAME': _userId, 'KATALALUAN': _password
                });
                if (_userId == 'superuser') {
                    $('#IdModalLoginLable').html(event);
                }
                $.ajax({
                    type: "POST",
                    url: event,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: _data,
                    success: function (res) {
                        if (res.RESULTSET == "1") {
                            $('#IdModalLoginLable').html('');
                            $('#IdButtonClose').trigger("click");
                            //swal("Success!", "Successfully Login", "success");
                            swal({
                                title: "Berjaya Log Masuk!",
                                text: "Klik OK untuk teruskan.",
                                type: "success",
                                showCancelButton: false,
                                closeOnConfirm: false,
                                showLoaderOnConfirm: true
                            }, function () {
                                setTimeout(function () {
                                    if (_loginpath == "apelc") {
                                        location.href = _path01 + "/Login/LoginPageApelC";
                                    }
                                    else {
                                        if (res.NAMA_PERANAN == "SUPERUSER" || res.NAMA_PERANAN == "PENTADBIR APEL" || res.NAMA_PERANAN == "PEMOHON" || res.NAMA_PERANAN == "PENASIHAT AKADEMIK" || res.NAMA_PERANAN == "PENGAWAS UJIAN CABARAN" || res.NAMA_PERANAN == "PANEL PENILAI" || res.NAMA_PERANAN == "MODERATOR" || res.NAMA_PERANAN == "PENGGUBAL" || res.NAMA_PERANAN == "PENILAI INSTRUMEN" || res.NAMA_PERANAN == "JK FAKULTI" || res.NAMA_PERANAN == "SENAT" || res.NAMA_PERANAN == "BENDAHARI") {
                                            document.getElementById("myform").submit();
                                        } else {
                                            $('#IdLayout_ImageKiri').trigger("click");
                                        }
                                    }
                                }, 300);
                            });
                        } else {
                            if (res.NAMA_PERANAN == "TIADA") {
                                swal({
                                    title: "Semakan mendapati anda tiada kebenaran untuk mengakses sistem. Sila Hubungi Urusetia Sistem ApelC atau daftar akaun baru",
                                    text: "Click OK to continue.",
                                    type: "warning",
                                    showCancelButton: false,
                                    closeOnConfirm: false,
                                    showLoaderOnConfirm: true
                                });
                            } else {
                                swal({
                                    title: res.RESULTSET_TEXT,
                                    text: "Click OK to continue.",
                                    type: "error",
                                    showCancelButton: false,
                                    closeOnConfirm: false,
                                    showLoaderOnConfirm: true
                                });
                            }
                            $('#kt_modal_log_masuk_submitGray').hide();
                            $('#kt_modal_log_masuk_submit').show();
                        }
                    },
                    error: function (xhr, httpStatusMessage, customErrorMessage) {
                        if (xhr.status === 410 || xhr.status === 500) {
                            alert('Error - ' + customErrorMessage);
                        } else {
                            toastr.warning("<font color='#800000'> Error other than 410-500.</font>");
                        }
                    }
                });
                $('#overlay2').hide();
            }
        });


    }

    return {
        init: function () {
            _dataTableInit();
        }
    };
}();

jQuery(function () {
    Index.init();
    if ($('#IdViewBagRes').val() == '7') {
        var _msg = $('#IdViewBagTxt').val();
        fnGenralMessage(_msg);
    } });
//jQuery(document).ready(function () {
//    Index.init();
//    if ($('#IdViewBagRes').val() == '7') {
//        var _msg = $('#IdViewBagTxt').val();
//        fnGenralMessage(_msg);
//    }
//});

//function fnGenralMessage(_msg)


$(document).ready(function () {
    $('#modaldaftarAkaun').on('shown.bs.modal', function () {
        var fileToRead = document.getElementById("IdFile");
        //fnsemakfile(fileToRead);
    });

});






function fnCallModalDaftarAkaunBaru(id) {

    var _dataid = $(id).attr("data-id");
    //alert(_dataid); // 0~~SIMPAN
    var _text = _dataid.split("~~");
    var _pkenc = _text[0]; //answer= 0
    var _kod = _text[1]; ////answer=SIMPAN

    var _kodProcess = $('#IdModalItemHiddenKodProcessModal').val();
    var NoMatrikEnc = $('#IdNo_MatrikPKEncModal').val();

    // get values from input field
    var NO_MATRIK = $('#IdDdNo_Matrik').val();
    var NoMatrikEnc = $('#IdDdNo_MatrikEn').val();

    // validate mandatory fields
    if (NO_MATRIK == "" || NoMatrikEnc == "") {
        Swal.fire(
            "Sila lengkapkan ruangan yang bertanda wajib (*) terlebih dahulu.",
            "",
            "warning"
        );
        _ok = false;
    }




    var _path = $('#IdPathServer').val();
    var event = _path + "/Login/CRUDDaftarAkaun";
    //alert(KatAduan);
    //return false;
    var _data = JSON.stringify({

        'PENGGUNA_UPNM_PK_ENC': _PenggunaUpnmPkEnc,
        'ID_PENGGUNA': _IdPengguna,
        'KATA_LALUAN_PENGGUNA': _KataLaluanPengguna,
        'JENIS_MODUL_PENGGUNA_UPNM_FK': _JenisModulPenggunaUpnm,
        'SESSION_TIMEOUT': _SessionTimeout,
        'STATUS_AKTIF_PENGGUNA_UPNM_FK': '90',


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
                        location.href = _path + "/Login/NotifikasiEmel";
                        //$('#IdBtnKodItemRefreshScreen').click().delay(8500);
                    });
                }
                else {
                    Swal.fire(
                        "Gagal!",
                        "Rekod tidak berjaya disimpan",
                        "error"
                    );
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {

                if (xhr.status === 410 || xhr.status === 500) {
                    Swal.fire(
                        "Gagal!",
                        "Rekod tidak berjaya disimpan",
                        "error"
                    );
                } else {
                    Swal.fire(
                        "Gagal!",
                        "Rekod gagal dicapai",
                        "error"
                    );
                }

            }





        })

    }



    //alert(_pkenc);
    //return false;
    //untuk KEMASKINI
    var _path = $('#IdPathServer').val();
    var event = _path + '/Login/MtdGetMaklumatPengguna';
    var _data = JSON.stringify({
        'PERANAN_PENGGUNA_PK_ENC': _pkenc

    });

    //alert(_pkenc.length);
    //console.log(event);
    //console.log(_pkenc);
    //console.log(_data);
    //return false;


    if (_pkenc.length == 0) {
        // then load the selection value
        $('#modaldaftarAkaun').modal('show');
        var _textNya = "Daftar";
        if (_kod == " DAFTAR") {
            _textNya = "Daftar ";
        }

        $('#IdButangActionModal').html(_textNya);
        $('#IdPaparCodeModal').html(_kod + " REKOD");

        //$('#IdPaparCodeModal').html(" (" + _kod + ") ");
        //$('#IdBtnModalSaveData').val(_kod);

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
                    var _textNya = "Daftar";
                    if (_kod == " DAFTAR") {
                        _textNya = "Daftar ";
                    }
                    $('#IdButangActionModal').html(_textNya);
                    $('#IdPaparCodeModal').html(_kod + " REKOD");
                    $('#IdPerananPenggunaPKEncModal').val(_pkenc);
                    $("#IdDdPerananPengguna").val(res.PERANAN_PENGGUNA_DESC);
                    $("#IdDdPerananPenggunEn").val(res.PERANAN_PENGGUNA_DESC_EN);





                    $('#modaldaftarAkaun').modal('show');
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
        $('#IdButangActionModal').html(_textNya);
        $('#IdPaparCodeModal').html(_kod + " REKOD");
        $("#IdDdKatAduan").val('');
        $("#IdDdKatAduanEn").val('');
        $('#IdModalItemHiddenKodProcessModal').val('SIMPAN');
        $('#IdKatAduanPKEncModal').val('');


        var myModal = new bootstrap.Modal(document.getElementById('modaldaftarAkaun'), {
            keyboard: false
        })

        myModal.show();

    }


}

function validate(btn) {
    form = document.frmForgot;
    var usrname = document.getElementById("user-login-name").value;
    var usrpwd = document.getElementById("user-password").value;
    var usrrepwd = document.getElementById("user-repassword").value;
    var n = usrpwd.length;
			< !--var usrmail = document.getElementById("user-email").value; -->
			
			if (btn.name == 'forgot-password') {
        if (usrname == '' || usrpwd == '' || usrrepwd == '') {
            alert("Sila isi ID Pengguna, Kata Laluan dan Pengesahan Kata Laluan Baru!");
            document.getElementById("user-login-name").focus();
            return false;
        } else if (n < 6) {
            alert("Pastikan Kata Laluan tidak kurang dari 6 digit.");
            document.getElementById("user-password").focus();
            return false;
        } else if (usrpwd != usrrepwd) {
            alert("Kata Laluan tidak sepadan !");
            document.getElementById("user-password").focus();
            return false;
        } else {
            form.submit();
        }
    }
}

function validate_login() {
    form = document.form1;
    var selected = document.getElementById("dd_jnsApel").value;

    if (selected == "") {
        alert("Sila pilih Jenis Apel.");
        return;
    } else {
        form.submit();
    }
}

function get_jnsApel() {
    var selected = document.getElementById("dd_jnsApel").value;
    document.getElementById("txt_jnsApel").value = selected;
}
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
            $('#IdLayout_ImageKiri').trigger("click");
        }, 200);
    });
};

function fnGenralMessageOnly
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
