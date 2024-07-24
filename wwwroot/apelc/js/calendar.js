// Class definition

var KTBootstrapDatepicker = function () {

    var arrows;
    if (KTUtil.isRTL()) {
        arrows = {
            leftArrow: '<i class="la la-angle-right"></i>',
            rightArrow: '<i class="la la-angle-left"></i>'
        }
    } else {
        arrows = {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    }

    // Private functions
    var demos = function () {

        $(".datepicker").flatpickr({
            enableTime: true,
            dateFormat: "d/m/Y H:i K"
        });

        $(".datepickeronly").flatpickr({
            enableTime: false,
            dateFormat: "d/m/Y"
        });

        $('.timepicker').flatpickr({
            enableTime: true,
            noCalendar: true,
            dateFormat: 'h:i K',
            /*mode: "range"*/
        });

        $("#kt_daterangepicker_5").daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: "DD/MM/YYYY"
            },
            minYear: 1901,
            maxYear: parseInt(moment().format("YYYY"), 10)
        }, function (start, end, label) {
            alert(start);
        }
        );

        //$("#kt_datepicker_1").flatpickr({
        //    dateFormat: "d/m/Y"
        //});

    }

    return {
        // public functions
        init: function () {
            demos();
        }
    };
}();

jQuery(document).ready(function () {
    KTBootstrapDatepicker.init();
});