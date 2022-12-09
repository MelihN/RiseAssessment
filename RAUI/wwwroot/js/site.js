function RefreshDataTable(tableId, newData) {
    var dt = $(tableId).dataTable();
    oSettings = dt.fnSettings();
    dt.fnClearTable(this);

    for (var i = 0; i < newData.length; i++) {
        dt.oApi._fnAddData(oSettings, newData[i]);
    }

    oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
    dt.fnDraw();
    return true;
}
function RefreshListDataTable(tableId) {
    var dt = $(tableId).dataTable();
    oSettings = dt.fnSettings();
    dt.fnClearTable(this);
    return true;
}
function PostAjaxWithData(options, callBack, sender) {
    var settings = $.extend({
        url: "",
        data: {}
    }, options);
    var result;
    $.ajax({
        type: "POST",
        url: settings.url,
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded;charset=UTF-8", //ISO-8859-15",
        data: settings.data,
        success: function (data) {
            if (sender != null) {
                callBack(sender, data);
            }
            else {
                callBack(data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            const data = { xhr, textStatus, errorThrown, RequestState: false, ErrorMessage: "Veri iletilemedi." };
            if (sender != null) {
                callBack(sender, data);
            }
            else {
                callBack(data);
            }
        }
    });
    return result;
}

function ShowErrorMessage(options) {
    var settings = $.extend({
        // These are the defaults.
        text: "İşleminizde hata oluştu. Lütfen sistem yöneticisi ile iletişime geçiniz."
        //onSuccess: ""
    }, options);
    var span = document.createElement("span");
    span.innerHTML = settings.text;
    swal({
        title: "Hata!",
        content: span,
        icon: "error",
        buttons: [false, "Tamam"],
        dangerMode: true
    }
        //, settings.onSuccess
    );
}

function ShowWarningMessage(options, callback) {
    var settings = $.extend({
        // These are the defaults. cancelButton: "Hayır" yazılırsa cancel button text i Hayır olur
        title: "Uyarı!",
        cancelButton: false,
        deleteUrl: "",
        deleteData: {},
        deleteDatatable: null,
        text: "İşleminizde hata oluştu. Lütfen tekrar deneyin.",
        successButton: "Tamam",
        dangerMode: true
        //onSuccess: ""
    }, options);
    //HTML mesaj oluşturmak için span kullanıyoruz.
    var span = document.createElement("span");
    span.innerHTML = settings.text;
    var result = false;
    swal({
        title: settings.title,
        content: span,
        icon: "warning",
        buttons: [settings.cancelButton, settings.successButton],
        dangerMode: settings.dangerMode
    }).then((value) => {
        if (settings.deleteUrl == "" && settings.deleteDatatable == null && callback) {
            callback(value);
        }
        else
            DeleteRecord(value, settings);
    });
}

function DeleteRecord(isDelete, settings) {
    if (isDelete) {// { "__RequestVerificationToken": frmToken, Id: row_id }
        var dt = settings.deleteDatatable;
        if (dt != null)
            PostAjaxWithData({ url: settings.deleteUrl, data: settings.deleteData }, function (data) {
                if (data.RequestState) {
                    dt.ajax.reload();
                }
                else {
                    ShowWarningMessage({ text: data.ErrorMessage });
                }
            });
    }
}

function ShowInfoMessage(options) {
    var settings = $.extend({
        // These are the defaults.
        text: "İşleminizde hata oluştu. Lütfen tekrar deneyin."
        //onSuccess: ""
    }, options);
    var span = document.createElement("span");
    span.innerHTML = settings.text;

    swal({
        title: "Bilgilendirme",
        content: span,
        icon: "info",
        buttons: [false, "Tamam"]
    }
        //, settings.onSuccess
    );
}
function ShowSuccessMessage(options) {
    var settings = $.extend({
        // These are the defaults.
        text: (options == null ? "İşleminizde başarıyla tamamlandı. \n Liste sayfasına geçmek istiyor musunuz?" : options.text ?? "İşleminizde başarıyla tamamlandı. \n Liste sayfasına geçmek istiyor musunuz?"),
        url: (options == null ? '' : options.url ?? undefined)
    }, options);
    var span = document.createElement("span");
    span.innerHTML = settings.text;
    var msg = swal({
        title: "Başarılı!",
        content: span,
        icon: "success",
        buttons: ["İptal", "Tamam"]
    });

    msg.then((value) => {

        if (settings.url != null && settings.url != undefined && settings.url != '') {
            ShowSuccessMessageCallBack(value, settings.url);
        }
    });

}
function ShowSuccessMessageCallBack(isConfirm, url) {
    if (isConfirm) {
        window.location.replace(url);
    }
    else {
        window.location.reload();
    }
}