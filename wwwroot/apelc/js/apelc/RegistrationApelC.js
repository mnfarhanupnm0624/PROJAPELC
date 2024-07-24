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

    function validate_login() {
        form = document.form1;
        var selected = document.getElementById("dd_jnsApel").value;

        if (selected == "") {
            alert("Sila pilih Jenis APEL");
            return;
        } else {
            form.submit();
        }
    }

    function get_jnsApel() {
        var selected = document.getElementById("dd_jnsApel").value;
        document.getElementById("txt_jnsApel").value = selected;
    }