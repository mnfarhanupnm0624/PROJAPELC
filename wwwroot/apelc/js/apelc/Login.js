/* globals Chart:false, feather:false */

"use strict";
var Index = function () {
    var _dataTableInit = function () {


        $('#IdButtonTest').click(function (e) {
            swal({
                title: "Berjaya!",
                text: "Berjaya Log Masuk",
                type: "success",
                showCancelButton: false,
                closeOnConfirm: false,
                showLoaderOnConfirm: true
            }, function () {
                setTimeout(function () {
                    $('#IdLayout_ImageKiri').click();
                }, 500);
            });
        });

        $('#IdButtonSubmit').click(function (e) {
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
                $('#IdButtonSubmitGray').show();
                $('#IdButtonSubmit').hide();
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
                                        location.href = _path01 + "/Home/Dashboard";
                                    }
                                    else {
                                        if (res.NAMA_PERANAN == "SUPERUSER" || res.NAMA_PERANAN == "PENTADBIR APEL" || res.NAMA_PERANAN == "PEMOHON" || res.NAMA_PERANAN == "PENASIHAT AKADEMIK" || res.NAMA_PERANAN == "PENGAWAS UJIAN CABARAN" || res.NAMA_PERANAN == "PANEL PENILAI" || res.NAMA_PERANAN == "MODERATOR" || res.NAMA_PERANAN == "PENGGUBAL" || res.NAMA_PERANAN == "PENILAI INSTRUMEN" || res.NAMA_PERANAN == "JK FAKULTI" || res.NAMA_PERANAN == "SENAT" || res.NAMA_PERANAN == "BENDAHARI") {
                                            document.getElementById("myform").submit();
                                        } else {
                                            $('#IdLayout_ImageKiri').click();
                                        }
                                    }
                                }, 300);
                            });
                        } else {
                            if (res.NAMA_PERANAN == "TIADA") {
                                swal({
                                    title: "Tiada Kebenaran Akses Sistem. Sila Hubungi Urusetia Sistem ApelC",
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
                            $('#IdButtonSubmitGray').hide();
                            $('#IdButtonSubmit').show();
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
