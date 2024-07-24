/* globals Chart:false, feather:false */

(function () {
    'use strict'

    feather.replace({ 'aria-hidden': 'true' })

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
//        //            _html = '<option value="' + res[u].Key + '">' + res[u].ViewField + '</option>';
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