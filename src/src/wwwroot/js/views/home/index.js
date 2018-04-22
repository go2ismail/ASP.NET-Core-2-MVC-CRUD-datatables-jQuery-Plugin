var popup, dataTable;

$(document).ready(function () {
    dataTable = $('#gridTodo').DataTable({
        "ajax": {
            "url": "/api/todo",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "todoName" },
            {
                "data": "todoId",
                "render": function (data) {
                    return "<a class='btn btn-default btn-sm' onclick=ShowPopup('/Home/AddEditTodo/"+data+"')><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> Delete</a>";
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        }
    });
});

function ShowPopup(url) {
    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            popup = formDiv.dialog({
                autoOpen: true,
                resizeable: false,
                title: 'Add or Edit Data',
                height: 400,
                width: 500,
                close: function () {
                    popup.dialog('destroy').remove();
                }
            });
        });
}


function SubmitAddEdit(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/api/todo',
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.dialog('close');
                    dataTable.ajax.reload();
                }
            }
        });
        
    }
    return false;
}

function Delete(id) {
    $.ajax({
        type: 'DELETE',
        url: '/api/todo/' + id,
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
            }
        }
    });
}

