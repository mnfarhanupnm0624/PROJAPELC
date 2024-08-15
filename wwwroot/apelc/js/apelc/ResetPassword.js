/* globals Chart:false, feather:false */

function validate(btn) {
    form = document.frm_register;
    var idno = document.getElementById("txt_iddesc").value;
    var name = document.getElementById("txt_fullname").value;
    var email = document.getElementById("txt_email").value;
    var pwd1 = document.getElementById("txt_password").value;
    var pwd2 = document.getElementById("txt_repassword").value;

    var n = pwd1.length;

    if (btn.name == 'btn_register') {
        if (idno == '') {
            alert("Sila isi No Kad Pengenalan anda !");
            document.getElementById("txt_iddesc").focus();
            return false;
        } else if (name == '') {
            alert("Sila isi Nama !");
            document.getElementById("txt_fullname").focus();
            return false;
        } else if (!filter.test(email)) {
            alert("Sila isi alamat emel yang betul !");
            document.getElementById("txt_email").focus();
            return false;
        } else if (pwd1 == '') {
            alert("Sila isi Kata Laluan !");
            document.getElementById("txt_password").focus();
            return false;
        } else if (n < 6) {
            alert("Pastikan Kata Laluan tidak kurang dari 6 digit.");
            document.getElementById("txt_password").focus();
            return false;
        } else if (pwd1 != pwd2) {
            alert("Kata Laluan tidak sepadan !");
            document.getElementById("txt_password").focus();
            return false;
        } else {
            if (!confirm("Sila pastikan maklumat yang diisi adalah tepat. Anda pasti untuk menyimpan rekod ini?")) {
                return false;
            } else {
                form.submit();
            }
        }
    }
}



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
})


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