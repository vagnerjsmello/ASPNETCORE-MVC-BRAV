$(document).ready(function () {
    getStudents();
});

function getStudents(filter) {
    let url = "/Student/GetStudents/";

    if (filter !== "" && filter !== undefined) {
        url = url + "?filter=" + filter;
    }

    const obj = {
        url: url,
        type: "GET",
        dataType: "html",
        contentType: "application/json; charset=utf-8"        
    };

    $(".spinner").show();
    $("#students-table").hide();

    ajaxComponent.getPartialViewWithData(obj)
        .then(function (data, textStatus, jqXHR) {
            $(".spinner").hide();
            $("#students-table").html(data).show();
        });
}

function filter() {
    const filter = $("input[name='search']").val();
    getStudents(filter);
}


function openCreateForm() {
    const obj = {
        modal: $("#modalBootstrap"),
        url: "/Student/Create",
        type: "GET",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        data: null,
        formName: "#formCreate"
    };

    modalComponent.open({ modal: obj.modal, title: "Create Student", size: "modal-lg", });

    ajaxComponent.getPartialViewWithData(obj)
        .then(function (data, textStatus, jqXHR) {
            modalComponent.content({modal: obj.modal, htmlData: data });
            enableFormValidationAfterAjaxPage(obj.formName);
        });
};


function openDetailsForm(id) {
    const obj = {
        modal: $("#modalBootstrap"),
        url: "/Student/Details/" + id,
        type: "GET",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        data: null,
        formName: "#formDetail"
    };

    modalComponent.open({ modal: obj.modal, title: "Details Student", size: "modal-lg", });

    ajaxComponent.getPartialViewWithData(obj)
        .then(function (data, textStatus, jqXHR) {
            modalComponent.content({ modal: obj.modal, htmlData: data });
            enableFormValidationAfterAjaxPage(obj.formName);
        });
};

function openEditForm(id) {

    const obj = {
        modal: $("#modalBootstrap"),
        url: "/Student/Edit/" + id,
        type: "GET",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        data: null,
        formName: "#formEdit"
    };

    modalComponent.open({ modal: obj.modal, title: "Edit Student", size: "modal-lg", });

    ajaxComponent.getPartialViewWithData(obj)
        .then(function (data, textStatus, jqXHR) {
            modalComponent.content({ modal: obj.modal, htmlData: data });
            enableFormValidationAfterAjaxPage(obj.formName);
        });
};

function openDeleteForm(id) {

    const obj = {
        modal: $("#modalBootstrap"),
        url: "/Student/Delete/" + id,
        type: "GET",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        data: null,
        formName: "#formDelete"
    };

    modalComponent.open({ modal: obj.modal, title: "Delete Student", size: "modal-sm", });

    ajaxComponent.getPartialViewWithData(obj)
        .then(function (data, textStatus, jqXHR) {
            modalComponent.content({ modal: obj.modal, htmlData: data });
            enableFormValidationAfterAjaxPage(obj.formName);
        }); 
};


//Ajax Forms Events
function onBegin() {
};

function onSuccess(data) {
    alertMessage("<strong>Success!</strong> Operation successful!", "alert-success");
};

function onFailure(data) {
    alertMessage("<strong>Error!</strong> Operation error.", "alert-danger");
};

function onComplete() {
    modalComponent.close({ modal: $("#modalBootstrap") });
    getStudents();
};

function alertMessage(message, alertClass) {
    $('.alert').removeClass("alert-success alert-info alert-warning alert-danger");
    $('.alert').addClass(alertClass || "alert-success").html(message || "<strong>Success!</strong> Operation successful!").fadeIn(150).delay(3000).fadeOut(150);
}

function enableFormValidationAfterAjaxPage(elementSelector) {
    const validator = $(elementSelector).validate();
    if (validator) {
        validator.destroy();
        $.validator.unobtrusive.parse(elementSelector);
    }
};


// Ajax Component
const ajaxComponent = (function () {
    function getPartialViewWithData(obj) {
        if (!obj.type) obj.type = "GET";
        if (!obj.dataType) obj.dataType = "html";
        if (!obj.contentType) obj.dataType = "application/json; charset=utf-8";
        if (obj.model) obj.model = JSON.stringify(obj.model);

        return $.when($.ajax({
            cache: false,
            type: obj.type,
            url: obj.url,
            dataType: obj.dataType,
            contentType: obj.contentType,
            data: obj.model
        }));
    };

    return {
        getPartialViewWithData: getPartialViewWithData
    };
})();

// Modal Boostrap Component 
const modalComponent = (function () {
    function open(obj) {
        const modalBootstrap = obj.modal;
        modalBootstrap.find('.modal-body-content').html("");
        modalBootstrap.find('.modal-title').text(obj.title);
        modalBootstrap.find('.modal-dialog').removeClass(['modal-xl', 'modal-lg', 'modal-sm']).addClass(obj.size || 'modal-lg'); //size sm lg xl
        modalBootstrap.modal("show");
        modalBootstrap.find('.modal-spinner').show();
    };

    function close(obj) {
        const modalBootstrap = obj.modal;
        modalBootstrap.find('.modal-body-content').html("");
        modalBootstrap.find('.modal-title').text("");
        modalBootstrap.modal("hide");
    };

    function content(obj) {
        const modalBootstrap = obj.modal;
        modalBootstrap.find('.modal-spinner').hide();
        modalBootstrap.find('.modal-body-content').html(obj.htmlData);
    };


    return {
        open: open,
        close: close,
        content: content
    };
})();