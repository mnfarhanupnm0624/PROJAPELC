"use strict";
function BarApplicationChem() {

    //alert("masuk Bar Function");

    var BulanKGT = $("#PVM_CBULAN_KGT").val();
    var TahunKGT = $("#PVM_CTAHUN_KGT").val();
    var KodKGT = $("#PVM_CKOD_KGT").val();

    var type = [];
    var ctype = [];

    var obj = { BulanKGT: BulanKGT, TahunKGT: TahunKGT, KodKGT: KodKGT };

    $.ajax({
        type: "POST",
        url: "ChartKgt",
        data: obj,
        success: function (response) {

            if (response.length > 0) {

                for (var i = 0; i < response.length; i++) {


                    type[i] = response[i].PTJ_CHART;
                    ctype[i] = response[i].TOTAL_AMOUNT_CHART;

                    var options = {
                        series: [{
                            name: 'Amaun KGT (RM)',
                            data: ctype,
                            color: '#f9ce1d',
                        }],
                        chart: {
                            type: 'bar',
                            height: 350
                        },
                        plotOptions: {
                            bar: {
                                horizontal: false,
                                columnWidth: '70%',
                                endingShape: 'rounded'
                            },
                        },
                        dataLabels: {
                            enabled: false
                        },
                        stroke: {
                            show: true,
                            width: 2,
                            colors: ['transparent'],
                            lineCap: 'round'
                        },
                        xaxis: {
                            categories: type,
                        },
                        yaxis: {
                            title: {
                                text: 'Amaun KGT (RM)'
                            },
                            labels: {
                                rotate: 0,
                                rotateAlways: false,
                                formatter: function (val) {
                                    return val.toFixed(0);
                                }
                            },
                            decimalsInFloat: 0,
                        },
                        fill: {
                            opacity: 1
                        },
                        tooltip: {
                            y: {
                                formatter: function (val) {
                                    return val
                                }
                            }
                        }
                    };

                    var chart = new ApexCharts(document.querySelector("#column_chart_basic"), options);
                    chart.render();

                }

            }
            else {
                //alert("entah ler");
            }

        }
    });

}

$('#idonchangedashboard').on('change', function (e) {

    var _path = $('#IdPathServer').val()
    var _tempoh = $('#idonchangedashboard').val();
    var _url = _path + "/Home/Dashboard";

    var form = document.createElement('form');
    form.method = 'POST';
    form.action = _url;

    var input = document.createElement('input');
    input.name = "idtempoh";
    input.value = _tempoh;
    input.type = 'hidden';

    form.appendChild(input)
    document.body.appendChild(form);
    form.submit();
});



