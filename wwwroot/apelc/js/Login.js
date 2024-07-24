"use strict";
var Index = function () {
    var _dataTableInit = function () {
        var _path = $('#IdPathServerMain').val();

        //alert(_path);
        //check session
        var checkSessionTime = function () {
            var time;
            window.onload = resetTimer();

            var timeIdle;
            window.onload = resetIdleTimer;
            // DOM Events
            window.onload = resetIdleTimer;
            window.onmousemove = resetIdleTimer;
            window.onmousedown = resetIdleTimer;  // catches touchscreen presses as well
            window.ontouchstart = resetIdleTimer; // catches touchscreen swipes as well
            window.onclick = resetIdleTimer;      // catches touchpad clicks as well
            window.onkeypress = resetIdleTimer;
            window.addEventListener('scroll', resetIdleTimer, true); // improved; see comments

            //check session
            function checkLogout() {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: _path + '/Home/checkSession',
                    data: '{}',
                    success: function (data) {
                        if (data.sessionValue == true) {
                            resetTimer();
                        }
                        else {
                            //alert(1);
                            location.href = _path + '/Login/Logout';
                        }
                    },
                    error: function (xhr) {
                        //alert(2);
                        location.href = _path + '/Login/Logout';
                    }
                });
            }

            //check session
            function resetTimer() {
                clearTimeout(time);
                time = setTimeout(checkLogout, 900000) // 15 minit
                // 1000 milliseconds = 1 second
            }

            //check idle
            function logout() {
                var url_back = window.location.href;

                let timerInterval
                Swal.fire({
                    title: 'You have been idle for too long!',
                    html: 'You will be logout in <b></b> seconds.<br> <strong>Continue?</strong>',
                    type: "warning",
                    confirmButtonText: 'Yes, stay login!',
                    timer: 10000,
                    timerProgressBar: true,
                    onBeforeOpen: () => {
                        //Swal.showLoading()
                        timerInterval = setInterval(() => {
                            Swal.getContent().querySelector('b')
                                .textContent = Swal.getTimerLeft() / 1000
                        }, 100)
                    },
                    onClose: () => {
                        clearInterval(timerInterval)
                    }
                }).then((result) => {
                    if (result.dismiss === Swal.DismissReason.timer) {
                        Swal.fire({
                            title: 'Logging Out',
                            text: 'Redirecting...',
                            type: 'info',
                            showConfirmButton: false
                        });
                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json',
                            //url:  _path + '/Home/setSession',
                            url: _path + '/Login/Logout',
                            data: '{}',
                            success: function () {
                            },
                            error: function () {
                            }
                        });
                        //location.href = '/Login/Logout/'
                        //alert(123);
                        //location.href =  _path + '/Login/Logout/?service=' + url_back;
                        location.href = _path + '/Login/Logout';
                    }
                })
            }

            //check idle
            function resetIdleTimer() {
                clearTimeout(timeIdle);
                timeIdle = setTimeout(logout, 900000) // 15 minit
                //timeIdle = setTimeout(logout, 30000)
            }
        };

        window.onload = function () {
            checkSessionTime();
        }

        $('#IdButtonTest').click(function (e) {
            swal({
                title: "BERJAYA!",
                text: "BERJAYA LOG MASUK",
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
                if (_userId == 'zaman') {
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
                                        location.href = _path01 + "/Senarai/SenaraiPermohonanApelC";
                                    }
                                    else {
                                        if (res.NAMA_PERANAN == "PENTADBIR APEL") {
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
                                    title: "Tiada Kebenaran Akses Sistem. Sila Hubungi Admin Sistem",
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