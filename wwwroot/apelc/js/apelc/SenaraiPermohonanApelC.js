/* globals Chart:false, feather:false */

(function () {
    'use strict'

    feather.replace({ 'aria-hidden': 'true' })

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
                url: _path + '/Login/checkSession',
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
                title: 'Anda log masuk terlalu lama!',
                html: 'Anda akan log keluar dalam <b></b> saat.<br> <strong>Teruskan?</strong>',
                type: "warning",
                confirmButtonText: 'Ya, kekal log masuk!',
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
                        title: 'Log Keluar',
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

    //// Graphs
    var ctx = document.getElementById('myChart')
    //// eslint-disable-next-line no-unused-vars
    //var myChart = new Chart(ctx, {
    //  type: 'line',
    //  data: {
    //    labels: [
    //      'Sunday',
    //      'Monday',
    //      'Tuesday',
    //      'Wednesday',
    //      'Thursday',
    //      'Friday',
    //      'Saturday'
    //    ],
    //    datasets: [{
    //      data: [
    //        15339,
    //        21345,
    //        18483,
    //        24003,
    //        27489,
    //        24092,
    //        12034
    //      ],
    //      lineTension: 0,
    //      backgroundColor: 'transparent',
    //      borderColor: '#007bff',
    //      borderWidth: 4,
    //      pointBackgroundColor: '#007bff'
    //    }]
    //  },
    //  options: {
    //    scales: {
    //      yAxes: [{
    //        ticks: {
    //          beginAtZero: false
    //        }
    //      }]
    //    },
    //    legend: {
    //      display: false
    //    }
    //  }
    //})
    //--------------------------------------------------------------------------

    var xValues = ["Submitted", "Approved(PTJ) ", "PickUp(PTJ)", "Delivered(PTJ)", "Received", ""];
    var jumlahsubmitted = $('#idSubmittedGraph').val();
    var jumlahapproved = $('#idApprovedPtjGraph').val();
    var jumlahpickup = $('#idPickupPtjGraph').val();
    var jumlahdelivered = $('#idDeliveredPtjGraph').val();
    var jumlahreceived = $('#idReceivedGraph').val();
    var dateYear = $('#IdDateYear').val();

    var yValues = [jumlahsubmitted, jumlahapproved, jumlahpickup, jumlahdelivered, jumlahreceived, 0];
    var barColors = ["yellow", "red", "green", "purple", "blue"];


    new Chart("myChart", {
        type: "bar",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
            //    title: {
            //        display: true,
            //        text: "Statistik Sysmel " + dateYear
            //    }
        }
    });




})()


$('#idonchangedashboard').on('change', function (e) {
    var _tempoh = $('#idonchangedashboard').val();
    //alert("idonchangedashboard:" + _tempoh);
    $('#idtempoh').val(_tempoh);

    document.getElementById("myform").submit();
    //var _path = $('#IdPathServer').val();
    //var _data = JSON.stringify({
    //    'TEMPOH': _tempoh
    //});

    //$.ajax({
    //    type: "POST",
    //    url: _path + "/SysMel/Dashboard",
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    data: _data,

    //    success: function (res) {
    //        alert("disini");

    //    },
    //    error: function (xhr, httpstatusmessage, customerrormessage) {
    //        if (xhr.status === 410 || xhr.status === 500) {
    //            alert(customerrormessage);
    //        } else {
    //            toastr.warning("", "<font color='#800000'>error code " + httpstatusmessage + "</font>");
    //        }
    //    }
    //});
});



//    //var _kampus = $('#IdDdCKampus').val();
//    //var _zonPk = $('#IdDdCKodZon').val();

//    //var _path = $('#IdPathServer').val();
//    //var _html = "";
//    //var _data = JSON.stringify({
//    //    'KAMPUS': _kampus,
//    //    'ZONE_PK': _zonPk
//    //});

//    //$.ajax({
//    //    type: "POST",
//    //    url: _path + "/Aduan/MtdGetKodBlokAjax",
//    //    dataType: "json",
//    //    contentType: "application/json; charset=utf-8",
//    //    data: _data,


//        /*success: function (res) {*/
//        //    if (res.length > 0) {
//        //        $('#IdDdCKodBlokAfm').html(_html);
//        //        $('#IdDdCKodBlokAfm').append('<option value="">-- Sila Pilih --</option>');
//        //        $.each(res, function (u) {
//        //            _html = '<option value="' + res[u].KOD + '">' + res[u].NAMA_PARAMETER + '</option>';
//        //            $('#IdDdCKodBlokAfm').append(_html);
//        //        });

//        //        $('#IdDdLaluanJalan').val(null).trigger('change');
//        //        $('#IdDdLaluanJalan').html("");
//        //        $('#IdDdLaluanJalan').append('<option value="">-- Sila Pilih --</option>');

//        //        document.getElementById('IdDdPoskod').value = "TIADA";
//        //        document.getElementById('IdDdBandar').value = "TIADA";
//        //        document.getElementById('IdDdNegeri').value = "TIADA";
//        //        document.getElementById('IdDdNegara').value = "TIADA";

//        //        $('#IdDdCArasAfm').val(null).trigger('change');
//        //        $('#IdDdCArasAfm').html("");
//        //        $('#IdDdCArasAfm').append('<option value="">-- Sila Pilih --</option>');


//        //    } else {
//        //        $('#IdDdCKodBlokAfm').val(null).trigger('change');
//        //        $('#IdDdCKodBlokAfm').html(_html);
//        //        $('#IdDdCKodBlokAfm').append('<option value="">-- Sila Pilih --</option>');
//        //    }
//        //},
//        //error: function (xhr, httpStatusMessage, customErrorMessage) {
//        //    if (xhr.status === 410 || xhr.status === 500) {
//        //        alert(customErrorMessage);
//        //    } else {
//        //        toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
//        //    }
//        //}
//    });

//});


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