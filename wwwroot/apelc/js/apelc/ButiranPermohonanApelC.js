"use strict";
var LaporanSstn = function () {
    var _varInit = function () {

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


        fnRedrawPaparList();

        $('.m-length').maxlength({
            warningClass: "badge badge-secondary",
            limitReachedClass: "badge badge-success",
            separator: ' daripada ',
            postText: ' aksara yang tinggal',
            appendToParent: true
        });

        $("#m_modal_kodItem").on('hidden.bs.modal', function (event) {
            $('#idHiddenPaparCode').val('');
            $('#IdTxtAreaUlasan').val('');
        });
        $("#m_modal_kodItemPengadu").on('hidden.bs.modal', function (event) {
            $('#idHiddenPaparCodePengadu').val('');
            $('#IdTxtAreaUlasanPengadu').val('');
        });

        $('#IdKemaskiniLaporanKnn').click(function () {
            var _ok = true;
            var _laporansstnPkEnc = $('#IdLprnSstnPkEnv').val();
            var _tajukKes = $('#idTajukKes').val();
            //var _laporansstn = $('#idLaporanSstn').val();
            var _laporansstn = CKEDITOR.instances.idLaporanSstn.getData();

            //var _rowCount = document.getElementById('IdTableRumusan').tBodies[0].rows.length;

            if (_tajukKes === "" || _laporansstn === "") {
                var _msg = "Sila Masukkan Maklumat yang Berkaitan!";
                var _type = "warning";

                fnPublicMessageOnly(_msg, _type)
                _ok = false;
            }

            //Loop through the Table rows and build a JSON array.
            var customers = new Array();
            var _arr = [];
            $("#IdTableRumusan TBODY TR").each(function () {
                var row = $(this);
                var customer = {};
                customer.TARIKH = row.find("TD").eq(1).html();
                customer.PERKARA = row.find("TD").eq(2).html();
                customer.RESULTSET = row.find("TD").eq(3).html();
                customers.push(customer);

                // Tambah value Tarikh dalam Array - _arr
                _arr.push(customer.TARIKH);
            });

            if (_arr.some((e, i, arr) => arr.indexOf(e) !== i) == true) {
                var _msg = "Tarikh Rumusan Tidak boleh Sama!";
                var _type = "warning";

                fnPublicMessageOnly(_msg, _type)
                _ok = false;
            }

            //var _rowCount = document.getElementById('IdTableRumusan').tBodies[0].rows.length;

            ////Loop through the Table rows and build a JSON array.
            //var customers = new Array();
            //var _arr = [];
            //$("#IdTableRumusan TBODY TR").each(function () {
            //    var row = $(this);
            //    var customer = {};
            //    customer.TARIKH = row.find("TD").eq(1).html();
            //    customer.PERKARA = row.find("TD").eq(2).html();
            //    customer.RESULTSET = row.find("TD").eq(3).html();
            //    customers.push(customer);

            //    // Tambah value Tarikh dalam Array - _arr
            //    _arr.push(customer.TARIKH);

            //    if (_tajukKes === "" || _laporansstn === "" || (_rowCount === 1 && typeof customer.PERKARA === "undefined")) {
            //        var _msg = "Sila Masukkan Maklumat yang Berkaitan!";
            //        var _type = "warning";

            //        fnPublicMessageOnly(_msg, _type)
            //        _ok = false;
            //    }
            //});

            if (_ok) {
                Swal.fire({
                    title: "Kemaskini Butiran Laporan",
                    text: "Anda Pasti?",
                    icon: "warning",
                    showCancelButton: !0,
                    confirmButtonText: "Ya, saya pasti!",
                    cancelButtonText: "Tidak",
                    reverseButtons: !1
                }).then(function (result) {
                    if (result.value == true) {
                        $('#IdKemaskiniLaporanKnn').hide();
                        $('#IdKemaskiniLaporanKnnGray').show();
                        fnUpdateLaporan(_laporansstnPkEnc, _tajukKes, _laporansstn, customers);
                    }
                });
            }
        });

        $('#IdBtnHantarEmel').click(function () {
            var _ok = true;
            var _staffPkEnc = $('#idStafPkEnc').val();
            var _kodprocess = $('#idHiddenPaparCode').val();
            var _kod = $('#idHiddenKod').val();
            var _ulasan = $('#IdTxtAreaUlasan').val();
            const span = document.getElementById('idEmelStaf');
            const _emel = span.textContent.trim();

            if (_ulasan === "") {
                var _msg = "Sila Masukkan Ulasan Pembetulan!";
                var _type = "warning";

                fnPublicMessageOnly(_msg, _type)
                _ok = false;
            }

            if (_ok) {
                $('#IdBtnHantarEmel').hide();
                $('#IdBtnHantarEmelGray').show();
                fnInsertPengesahanLprn(_kodprocess, _kod, _emel, _ulasan, _staffPkEnc);
            }
        });

        $('#IdBtnHantarEmelPengadu').click(function () {
            var _ok = true;
            var _kodprocess = $('#idHiddenPaparCodePengadu').val();
            var _kod = $('#idHiddenKodPengadu').val();
            var _nama = $('#idNama').val();
            var _ulasan = $('#IdTxtAreaUlasanPengadu').val();
            var _nokp = $('#idNokpPengadu').val();
            var _noid = $('#idNoIDPengadu').val();
            var _noaduan = $('#idNoAduanPengadu').val();
            var _tkhaduan = $('#idTkhPengadu').val();
            var _masaaduan = $('#idMasaPengadu').val();
            var _hariaduan = $('#idHariPengadu').val();
            var _kampus = $('#idKampusPengadu').val();
            var _stspengadu = $('#idStsPengadu').val();

            var _kategorikes = $('#IdKategoriKes').val();

            const span = document.getElementById('idEmelPengadu');
            const _emel = span.textContent.trim();

            if (_ulasan === "" || _kategorikes === "") {
                var _msg = "Sila Masukkan Kategori Kes dan Ulasan Tindakan!";
                var _type = "warning";

                fnPublicMessageOnly(_msg, _type)
                _ok = false;
            }

            if (_ok) {
                $('#IdBtnHantarEmelPengadu').hide();
                $('#IdBtnHantarEmelPengaduGray').show();
                fnInsertPengesahanLprnPengadu(_kodprocess, _kod, _emel, _ulasan, _nokp, _nama, _noid, _noaduan, _tkhaduan, _masaaduan, _hariaduan, _kampus, _stspengadu, _kategorikes);
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
    LaporanSstn.init();
});

$(document).ready(function () {
    var _idRole = $('#IdRole').val();
    var _idStsLaporan = $('#IdStsLaporan').val();

    if ((_idStsLaporan == "SUBMITTED" || _idStsLaporan == "CORRECTION") && _idRole.includes("PEGAWAI-01-SST")) {
        ckeditor();
        /*summernote();*/
    } else if (_idRole.includes("PEGAWAI-01-SST") || _idRole.includes("URUSETIA-01-SST")) {
        ckeditorreadonly();
        /*$('.summernote').summernote('disable');*/
    } else if (_idStsLaporan == "CANCELLED" || _idStsLaporan == "APPROVED") {
        ckeditorreadonly();
        /* $('.summernote').summernote('disable');*/
    } else if ((_idStsLaporan == "SUBMITTED" || _idStsLaporan == "CORRECTION") && _idRole.includes("KERANI-01-SST")) {
        ckeditorreadonly();
        /*$('.summernote').summernote('disable');*/
    } else {
        ckeditor();
        /*summernote();*/
    }

    $("body").on("click", "#btnAdd", function () {
        var _ok = true;
        //Reference the Tarikh and Perkara TextBoxes.
        var txtName = $("#txtName");
        var txtCountry = $("#txtCountry");

        if (txtName.val() === "" || txtCountry.val() === "") {
            var _msg = "Sila Masukkan Maklumat yang Diperlukan!";
            var _type = "warning";

            fnPublicMessageOnly(_msg, _type)
            _ok = false;
        }
        if (_ok) {
            //Get the reference of the Table's TBODY element.
            var tBody = $("#IdTableRumusan > TBODY")[0];

            //Add Row.
            var row = tBody.insertRow(-1);

            //Add bil cell.
            var cell = $(row.insertCell(-1));
            cell.html('-');

            //Add Name cell.
            var cell = $(row.insertCell(-1));
            cell.html(txtName.val());

            //Add Country cell.
            cell = $(row.insertCell(-1));
            cell.html(txtCountry.val());

            //Add ResultSet cell.
            cell = $(row.insertCell(-1));
            cell.html(null);
            cell.hide();

            //Add Button cell.
            cell = $(row.insertCell(-1));
            var btnRemove = $("<input class='btn btn-sm btn-danger' />");
            btnRemove.attr("type", "button");
            btnRemove.attr("onclick", "Remove(this);");
            btnRemove.val("Hapus");
            cell.append(btnRemove);

            document.getElementById('IdTableRumusan').style.textAlign = "center";

            //Clear the TextBoxes.
            txtName.val("");
            txtCountry.val("");
        }
    });

    $(".form-select").select2();
    $('#m_modal_kodItemPengadu .form-select').select2({
        dropdownParent: $('#m_modal_kodItemPengadu')
    });
});

function Remove(button) {
    var _dataid = $(button).attr("data-id");

    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    /*var name = $("TD", row).eq(1).html();*/

    //Get the reference of the Table.
    var table = $("#IdTableRumusan")[0];

    //Delete the Table row using it's Index.
    table.deleteRow(row[0].rowIndex);

    fnHapusRumusan(_dataid);
};

function ckeditorreadonly() {
    var editorSummary;
    editorSummary = CKEDITOR.replace('idLaporanSstn',
        {
            on: {
                instanceReady: function () {
                    document.getElementById("idLaporanSstn");
                    this.element.hide();
                },
                change: function () {
                    this.updateElement();
                }
            },
        });

    var checkSum = "no change";

    $(document).ready(function () {
        editorSummary.on("change", function () {
            checkSum = "change";
        });
    });

}

function ckeditor() {
    var editorSummary;
    editorSummary = CKEDITOR.replace('idLaporanSstn',
        {
            on: {
                instanceReady: function () {
                    document.getElementById("idLaporanSstn");
                    this.element.show();
                },
                change: function () {
                    this.updateElement();
                }
            },

            toolbarGroups: [
                { name: 'clipboard', groups: ['clipboard', 'undo'] },
                { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
                { name: 'links' },
                { name: 'insert' },
                { name: 'forms' },
                { name: 'tools' },
                { name: 'document', groups: ['mode', 'document', 'doctools'] },
                { name: 'others' },
                '/',
                { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
                { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
                { name: 'styles' },
                { name: 'colors' },
                /*{ name: 'about' }*/
            ],

            wordcount: {
                showParagraphs: false,
                showWordCount: false,
                showCharCount: false,
                countSpacesAsChars: false,
                countHTML: false,
                //maxWordCount: 150
            },
        });

    $("#idLaporanSstn").css({
        "position": "fixed",
        "height": "1px",
        "width": "1px",
        "bottom": "0",
        "left": "0",
        "z-index": "-999",
        "display": "inline"
    });

    var checkSum = "no change";
    $(document).ready(function () {
        editorSummary.on("change", function () {
            checkSum = "change";
        });
    });

}

//function summernote() {
//    $('.summernote').summernote({
//        placeholder: 'Catatan Butiran Laporan',
//        lineHeights: ['0.5', '1.0'],
//        tabsize: 1,
//        height: 200,
//        toolbar: [
//            ['style', ['style']],
//            ['font', ['bold', 'underline', 'clear']],
//            ['fontsize', ['fontsize']],
//            ['fontname', ['fontname']], // Add fontname option
//            ['height', ['height']],
//            ['para', ['ul', 'ol', 'paragraph']],
//            ['color', ['color']],
//            ['table', ['table']],
//            ['insert', ['link']],
//            ['view', ['fullscreen']]
//        ],
//        fontNames: ['Calibri', 'Arial', 'Times New Roman', 'Verdana'], // Customize font family options
//        fontNamesDefault: 'Calibri', // Default font size
//        //callbacks: {
//        //    onKeydown: function (e) {
//        //        let characters = $('#idLaporanSstn').summernote('code').replace(/(<([^>]+)>)/ig, "");
//        //        let totalCharacters = characters.length;
//        //        $("#total-characters").text(totalCharacters + " / " + charLimit);
//        //        var t = e.currentTarget.innerText;
//        //        if (t.trim().length >= charLimit) {
//        //            if (e.keyCode != 8 && !(e.keyCode >= 37 && e.keyCode <= 40) && e.keyCode != 46 && !(e.keyCode == 88 && e.ctrlKey) && !(e.keyCode == 67 && e.ctrlKey)) e.preventDefault();
//        //        }
//        //    },
//        //    onKeyup: function (e) {
//        //        var t = e.currentTarget.innerText;
//        //        $('#idLaporanSstn').text(charLimit - t.trim().length);
//        //    },
//        //    onPaste: function (e) {
//        //        let characters = $('#idLaporanSstn').summernote('code').replace(/(<([^>]+)>)/ig, "");
//        //        let totalCharacters = characters.length;
//        //        $("#total-characters").text(totalCharacters + " / " + charLimit);
//        //        var t = e.currentTarget.innerText;
//        //        var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
//        //        e.preventDefault();
//        //        var maxPaste = bufferText.length;
//        //        if (t.length + bufferText.length > charLimit) {
//        //            maxPaste = charLimit - t.length;
//        //        }
//        //        if (maxPaste > 0) {
//        //            document.execCommand('insertText', false, bufferText.substring(0, maxPaste));
//        //        }
//        //        $('#idLaporanSstn').text(charLimit - t.length);
//        //    }
//        //}
//    });
//}

function fnRedrawPaparList() {

    var tableRumusan = $("#IdTableRumusan").DataTable({
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
            "excelHtml5",
        ],
        columnDefs: [
            {
                targets: 3,
                visible: false,
            },
        ],
        "language": {
            "sSearch": "Carian : "
        },
    });

    var table = $("#IdHasilCarianSearch").DataTable({
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
            "excelHtml5",
        ],
    });
};

function fnSubmitLaporan(_laporansstnPkEnc, _tajukKes, _laporansstn, customers, _kodprocess) {
    var _path = $('#IdPathServer').val();
    var _data = JSON.stringify({
        'LPRN_SSTN_PK_ENC': _laporansstnPkEnc,
        'TAJUK_KES': _tajukKes,
        'BTIRN_LPRN': _laporansstn,
        'ListLaporanSstn': customers,
        'KOD_PROCESS': _kodprocess
    });

    $.ajax({
        type: "POST",
        url: _path + "/Siasatan/MtdSubmitLaporan",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: _data,
        beforeSend: function () {
            Swal.fire({
                title: 'Laporan sedang dihantar. Sila tunggu......',
                allowOutsideClick: false
            });
            Swal.showLoading();
        },
        success: function (res) {
            Swal.close();
            if (res != null) {
                fnInsertPengesahanLprnResult(res);
            } else {
                toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            if (xhr.status === 410 || xhr.status === 500) {
                alert(customErrorMessage);
            } else {
                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
            }
        }
    });
}

function fnUpdateLaporan(_laporansstnPkEnc, _tajukKes, _laporansstn, customers) {
    var _path = $('#IdPathServer').val();
    var _data = JSON.stringify({
        'LPRN_SSTN_PK_ENC': _laporansstnPkEnc,
        'TAJUK_KES': _tajukKes,
        'BTIRN_LPRN': _laporansstn,
        'ListLaporanSstn': customers
    });

    $.ajax({
        type: "POST",
        url: _path + "/Siasatan/MtdUpdateLaporan",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: _data,
        success: function (res) {
            if (res != null) {
                fnUpdateLaporanResult(res);
            } else {
                toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            if (xhr.status === 410 || xhr.status === 500) {
                alert(customErrorMessage);
            } else {
                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
            }
        }
    });
}

function fnHapusRumusan(_rumusanPkEnc) {
    var _path = $('#IdPathServer').val();
    var _data = JSON.stringify({
        'RUMUSAN_PK_ENC': _rumusanPkEnc
    });
    if (_rumusanPkEnc != null) {
        $.ajax({
            type: "POST",
            url: _path + "/Siasatan/MtdHapusRumusan",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: _data,
            success: function (res) {
                if (res != null) {
                    fnHapusRumusanResult(res);
                } else {
                    toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
                }
            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                if (xhr.status === 410 || xhr.status === 500) {
                    alert(customErrorMessage);
                } else {
                    toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
                }
            }
        });
    }
}

function fnHapusRumusanResult(res) {
    if (res.RESULTSET === "2") {

        var _msg = "Berjaya Hapus!";
        var _type = "success";

        fnPublicMessageOnly(_msg, _type)
    }
    else {
        var _msg = "Tidak Berjaya Hapus!";
        var _type = "warning";

        fnPublicMessageOnly(_msg, _type)
    }
}

function fnCallModalItem(id) {
    var _ok = true;
    var _dataid = $(id).attr("data-id");
    var _text = _dataid.split("~~");
    var _kod = _text[0];
    var _status = _text[1];

    $('#IdPaparCode').html(("[" + _status + "] "));
    document.getElementById("idHiddenKod").value = _kod;
    document.getElementById("idHiddenPaparCode").value = _status;

    var _laporansstnPkEnc = $('#IdLprnSstnPkEnv').val();
    var _tajukKes = $('#idTajukKes').val();
    //var _laporansstn = $('#idLaporanSstn').val();
    var _laporansstn = CKEDITOR.instances.idLaporanSstn.getData();

    if (_tajukKes === "" || _laporansstn === "" || _laporansstnPkEnc === "") {
        var _msg = "Sila Masukkan Maklumat yang Berkaitan!";
        var _type = "warning";

        fnPublicMessageOnly(_msg, _type)
        _ok = false;
    }

    if (_ok) {
        $('#m_modal_kodItem').modal('show');

        fnGetLprnSstnStafEmel();
    }
}

function fnGetLprnSstnStafEmel() {
    var _path = $('#IdPathServer').val();

    $.ajax({
        type: "POST",
        url: _path + "/Siasatan/MtdGetLprnSstnStafEmel",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res != null) {
                fnGetLprnSstnStafEmelResults(res);
            } else {
                toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            if (xhr.status === 410 || xhr.status === 500) {
                alert(customErrorMessage);
            } else {
                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
            }
        }
    });
}

function fnGetLprnSstnStafEmelResults(res) {
    $('#idNamaStaf').html(res.NAMA);
    $('#idEmelStaf').html(res.EMAIL_RASMI);
    $('#idStafPkEnc').val(res.STAF_PK_ENC);
}

function fnCallModalItemPengadu(id) {
    var _ok = true;
    var _dataid = $(id).attr("data-id");
    var _text = _dataid.split("~~");
    var _kod = _text[0];
    var _status = _text[1];

    $('#IdPaparCodePengadu').html(("[" + _status + "] "));
    document.getElementById("idHiddenKodPengadu").value = _kod;
    document.getElementById("idHiddenPaparCodePengadu").value = _status;

    var _laporansstnPkEnc = $('#IdLprnSstnPkEnv').val();
    var _tajukKes = $('#idTajukKes').val();
    //var _laporansstn = $('#idLaporanSstn').val();
    var _laporansstn = CKEDITOR.instances.idLaporanSstn.getData();


    if (_tajukKes === "" || _laporansstn === "" || _laporansstnPkEnc === "") {
        var _msg = "Sila Masukkan Maklumat yang Berkaitan!";
        var _type = "warning";

        fnPublicMessageOnly(_msg, _type)
        _ok = false;
    }

    if (_ok) {
        $('#m_modal_kodItemPengadu').modal('show');

        fnGetLprnSstnPengaduEmel();
    }
}

function fnGetLprnSstnPengaduEmel() {
    var _path = $('#IdPathServer').val();

    $.ajax({
        type: "POST",
        url: _path + "/Siasatan/MtdGetLprnSstnPengaduEmel",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res != null) {
                fnGetLprnSstnPengaduEmelResults(res);
            } else {
                toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            if (xhr.status === 410 || xhr.status === 500) {
                alert(customErrorMessage);
            } else {
                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
            }
        }
    });
}

function fnGetLprnSstnPengaduEmelResults(res) {
    $('#idNamaPengadu').html(res.NAMA_PENGADU);
    $('#idEmelPengadu').html(res.EMAIL);
    $('#idNokpPengadu').val(res.NO_KP);
    $('#idNoIDPengadu').val(res.NO_ID);
    $('#idNoAduanPengadu').val(res.REPORT_NO);
    $('#idTkhPengadu').val(res.DATE_TKH_ADUAN);
    $('#idMasaPengadu').val(res.MASA_TKH_ADUAN);
    $('#idHariPengadu').val(res.HARI_TKH_ADUAN);
    $('#idKampusPengadu').val(res.KAMPUS_DESC);
    $('#idStsPengadu').val(res.CSTSPENGADU);
    $('#idNama').val(res.NAMA_PENGADU);
    $('#IdPaparKategori').html(res.CSTSPENGADU);

    $('#IdKategoriKes').val(res.KATEGORI_KES_FK); // Select the option with a value of '1'
    $('#IdKategoriKes').trigger('change');
}

// Insert: Pengesahan Pembetulan
function fnInsertPengesahanLprn(_kodprocess, _kod, _emel, _ulasan, _staffPkEnc) {
    var _path = $('#IdPathServer').val();
    var _data = JSON.stringify({
        'KOD_PROCESS': _kodprocess,
        'KOD': _kod,
        'EMAIL': _emel,
        'BTRN_ULSN': _ulasan,
        'STAF_PK_ENC': _staffPkEnc
    });

    $.ajax({
        type: "POST",
        url: _path + "/Siasatan/MtdInsertPengesahanLprn",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: _data,
        beforeSend: function () {
            Swal.fire({
                title: 'Laporan sedang dihantar. Sila tunggu......',
                allowOutsideClick: false
            });
            Swal.showLoading();
        },
        success: function (res) {
            Swal.close();
            if (res != null) {
                $('#IdBtnHantarEmel').show();
                $('#IdBtnHantarEmelGray').hide();
                fnInsertPengesahanLprnResult(res);
            } else {
                toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            if (xhr.status === 410 || xhr.status === 500) {
                alert(customErrorMessage);
            } else {
                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
            }
        }
    });
}

function fnInsertPengesahanLprnResult(res) {
    if (res != null) {
        if (res.RESULTSET === "2") {
            Swal.fire({
                title: "Berjaya!",
                text: "Berjaya Kemaskini",
                icon: "success",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    location.href = location.href;
                }
            });
        } else if (res.RESULTSET === "3") {
            Swal.fire({
                title: "Berjaya!",
                text: "Pembetulan dan Berjaya Emel kepada " + res.EMAIL,
                icon: "success",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    location.href = location.href;
                }
            });
        } else {
            Swal.fire({
                title: "Tidak Berjaya!",
                text: "Tidak Berjaya Kemaskini",
                icon: "warning",
                confirmButtonText: "OK"
            });
        }
    }
}

function fnUpdateLaporanResult(res) {
    if (res != null) {
        if (res.RESULTSET === "2") {
            Swal.fire({
                title: "Berjaya!",
                text: "Berjaya Kemaskini",
                icon: "success",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    location.href = location.href;
                }
            });
        } else {
            Swal.fire({
                title: "Tidak Berjaya!",
                text: "Tidak Berjaya Kemaskini",
                icon: "warning",
                confirmButtonText: "OK"
            });
        }
    }
}

// Insert: Pengesahan Selesai Batal
function fnInsertPengesahanLprnPengadu(_kodprocess, _kod, _emel, _ulasan, _nokp, _nama, _noid, _noaduan, _tkhaduan, _masaaduan, _hariaduan, _kampus, _stspengadu, _kategorikes) {
    var _path = $('#IdPathServer').val();
    var _data = JSON.stringify({
        'KOD_PROCESS': _kodprocess,
        'KOD': _kod,
        'EMAIL': _emel,
        'BTRN_ULSN': _ulasan,
        'NO_KP': _nokp,
        'NAMA': _nama,
        'NO_ID': _noid,
        'REPORT_NO': _noaduan,
        'DATE_TKH_ADUAN': _tkhaduan,
        'MASA_TKH_ADUAN': _masaaduan,
        'HARI_TKH_ADUAN': _hariaduan,
        'KAMPUS_DESC': _kampus,
        'CSTSPENGADU': _stspengadu,
        'KATEGORI_KES_FK': _kategorikes
    });

    $.ajax({
        type: "POST",
        url: _path + "/Siasatan/MtdInsertPengesahanLprnPengadu",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: _data,
        beforeSend: function () {
            Swal.fire({
                title: 'Laporan sedang dihantar. Sila tunggu......',
                allowOutsideClick: false
            });
            Swal.showLoading();
        },
        success: function (res) {
            Swal.close();
            if (res != null) {
                $('#IdBtnHantarEmelPengadu').show();
                $('#IdBtnHantarEmelPengaduGray').hide();
                fnInsertPengesahanLprnPengaduResult(res, _kodprocess);
            } else {
                toastr.warning("", "<font color='#800000'> Record Not Found.</font>");
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            if (xhr.status === 410 || xhr.status === 500) {
                alert(customErrorMessage);
            } else {
                toastr.warning("", "<font color='#800000'>Error Code " + httpStatusMessage + "</font>");
            }
        }
    });
}

function fnInsertPengesahanLprnPengaduResult(res, _kodprocess) {
    if (res != null) {
        if (res.RESULTSET === "2") {
            Swal.fire({
                title: "Berjaya!",
                text: "Berjaya Kemaskini",
                icon: "success",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    location.href = location.href;
                }
            });
        } else if (res.RESULTSET === "3") {
            Swal.fire({
                title: "Berjaya!",
                text: _kodprocess + " dan Berjaya Emel kepada " + res.EMAIL,
                icon: "success",
                confirmButtonText: "OK"
            }).then(function (result) {
                if (result.value) {
                    location.href = location.href;
                }
            });
        } else {
            Swal.fire({
                title: "Tidak Berjaya!",
                text: "Tidak Berjaya Kemaskini",
                icon: "warning",
                confirmButtonText: "OK"
            });
        }
    }
}

function fnLaporanSave(id) {
    var _ok = true;
    var _laporansstnPkEnc = $('#IdLprnSstnPkEnv').val();
    var _tajukKes = $('#idTajukKes').val();
    //var _laporansstn = $('#idLaporanSstn').val();
    var _laporansstn = CKEDITOR.instances.idLaporanSstn.getData();
    var _kodprocess = $(id).attr("data-id");
    //var _rowCount = document.getElementById('IdTableRumusan').tBodies[0].rows.length;

    if (_tajukKes === "" || _laporansstn === "") {
        var _msg = "Sila Masukkan Maklumat yang Berkaitan!";
        var _type = "warning";

        fnPublicMessageOnly(_msg, _type)
        _ok = false;
    }

    //Loop through the Table rows and build a JSON array.
    var customers = new Array();
    var _arr = [];
    $("#IdTableRumusan TBODY TR").each(function () {
        var row = $(this);
        var customer = {};
        customer.TARIKH = row.find("TD").eq(1).html();
        customer.PERKARA = row.find("TD").eq(2).html();
        customer.RESULTSET = row.find("TD").eq(3).html();
        customers.push(customer);

        // Tambah value Tarikh dalam Array - _arr
        _arr.push(customer.TARIKH);
    });

    if (_arr.some((e, i, arr) => arr.indexOf(e) !== i) == true) {
        var _msg = "Tarikh Rumusan Tidak boleh Sama!";
        var _type = "warning";

        fnPublicMessageOnly(_msg, _type)
        _ok = false;
    }

    if (_ok) {
        if (_kodprocess == "DRAF") {
            $('#IdDrafLaporan').hide();
            $('#IdDrafLaporanGray').show();
        } else {
            $('#IdHantarLaporan').hide();
            $('#IdHantarLaporanGray').show();
        }
        fnSubmitLaporan(_laporansstnPkEnc, _tajukKes, _laporansstn, customers, _kodprocess);
    }
}

function validateCarian() {

    // validate required fields
    var tajuk = $('#idTajukKes').val();
    var catatan = CKEDITOR.instances.idLaporanSstn.getData();
    //var catatan = $('#idLaporanSstn').val();

    var status = false;

    if (tajuk === "" || catatan === "") {

        var _msg = "Sila lengkapkan ruangan yang bertanda wajib (*) terlebih dahulu";
        var _type = "warning";

        fnPublicMessageOnly(_msg, _type);
        status = false;
    }
    else {
        status = true;

        $('#idHiddenTajukKes').val(tajuk);
        $('#idHiddenBtrnLprn').val(catatan);
    }

    return status;

}

//function fnGenralMessageOnly
function fnPublicMessageOnly(_msg, _type) {
    Swal.fire({
        text: _msg,
        icon: _type,
        buttonsStyling: false,
        confirmButtonText: "Ok",
        customClass: {
            confirmButton: "btn btn-secondary"
        }
    });
};