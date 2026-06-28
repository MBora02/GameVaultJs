$(document).ready(function () {
    ShowNewsData();
});

function ShowNewsData() {

    var url = $('#urlNewsData').val();

    $.ajax({
        url: url,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',

        success: function (result) {

            var object = '';

            $.each(result, function (index, item) {

                object += '<tr>';
                object += '<td>' + item.id + '</td>';
                object += '<td>' + item.title + '</td>';
                object += '<td>' + item.content + '</td>';
                object += '<td>' + new Date(item.publishDate).toLocaleDateString('tr-TR') + '</td>';
                object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger" onclick="Delete(' + item.id + ')">Delete</a></td>';
                object += '</tr>';

            });

            $('#table_data').html(object);
        },
        error: function () {
            alert("Data can't get");
        }
    });
}

$('#btnAddNews').click(function () {

    ClearTextBox();

    $('#NewsMadal').modal('show');
    $('#newsId').hide();

    $('#AddNews').css('display', 'block');
    $('#btnUpdate').css('display', 'none');

    $('#NewsHeading').text('Add News');
});

function AddNews() {

    var objData = {
        Title: $('#Title').val(),
        Content: $('#Content').val(),
        publishDate: $('#PublishDate').val()
    };

    $.ajax({
        url: '/News/AddNews',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        dataType: 'json',

        success: function () {

            alert('Data Saved');

            ClearTextBox();
            ShowNewsData();
            HideModalPopUp();
        },
        error: function () {
            alert("Data can't Saved!");
        }
    });
}

function ClearTextBox() {

    $('#NewsId').val('');
    $('#Title').val('');
    $('#Content').val('');
    $('#PublishDate').val('');
}

function HideModalPopUp() {
    $('#NewsMadal').modal('hide');
}

function Delete(id) {

    if (confirm('Are you sure?')) {

        $.ajax({
            url: '/News/Delete?id=' + id,
            success: function () {
                alert('Record Deleted');
                ShowNewsData();
            },
            error: function () {
                alert("Data can't be deleted!");
            }
        });
    }
}

function Edit(id) {

    $.ajax({
        url: '/News/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',

        success: function (response) {

            $('#NewsMadal').modal('show');

            $('#NewsId').val(response.id);
            $('#Title').val(response.title);
            $('#Content').val(response.content);
            $('#PublishDate').val(response.publishDate.split('T')[0]);

            $('#AddNews').css('display', 'none');
            $('#btnUpdate').css('display', 'block');

            $('#NewsHeading').text('Update Record');
        },
        error: function () {
            alert('Data not found');
        }
    });
}

function UpdateNews() {

    var objData = {
        Id: $('#NewsId').val(),
        Title: $('#Title').val(),
        Content: $('#Content').val(),
        PublishDate: $('#PublishDate').val()
    };

    $.ajax({
        url: '/News/Update',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        dataType: 'json',

        success: function () {

            alert('Data Updated');

            HideModalPopUp();
            ShowNewsData();
            ClearTextBox();
        },
        error: function () {
            alert("Data can't Saved!");
        }
    });
}