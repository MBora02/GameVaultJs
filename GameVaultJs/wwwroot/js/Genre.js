$(document).ready(function () {
    ShowGenreData();
});

function ShowGenreData() {
    var url = $('#urlGenreData').val();

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
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.description + '</td>';
                object += '<td>' + item.gameCount + '</td>';
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

$('#btnAddGenre').click(function () {

    ClearTextBox();

    $('#GenreMadal').modal('show');
    $('#genreId').hide();

    $('#AddGenre').css('display', 'block');
    $('#btnUpdate').css('display', 'none');

    $('#GenreHeading').text('Add Genre');
});

function AddGenre() {

    var objData = {
        Name: $('#Name').val(),
        Description: $('#Description').val(),
        GameCount: $('#GameCount').val()
    };

    $.ajax({
        url: '/Genre/AddGenre',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        dataType: 'json',

        success: function () {
            alert('Data Saved');
            ClearTextBox();
            ShowGenreData();
            HideModalPopUp();
        },
        error: function () {
            alert("Data can't Saved!");
        }
    });
}

function ClearTextBox() {

    $('#GenreId').val('');
    $('#Name').val('');
    $('#Description').val('');
    $('#GameCount').val('');
}

function HideModalPopUp() {
    $('#GenreMadal').modal('hide');
}

function Delete(id) {

    if (confirm('Are you sure?')) {

        $.ajax({
            url: '/Genre/Delete?id=' + id,
            success: function () {
                alert('Record Deleted');
                ShowGenreData();
            },
            error: function () {
                alert("Data can't be deleted!");
            }
        });
    }
}

function Edit(id) {

    $.ajax({
        url: '/Genre/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',

        success: function (response) {

            $('#GenreMadal').modal('show');

            $('#GenreId').val(response.id);
            $('#Name').val(response.name);
            $('#Description').val(response.description);
            $('#GameCount').val(response.gameCount);

            $('#AddGenre').css('display', 'none');
            $('#btnUpdate').css('display', 'block');

            $('#GenreHeading').text('Update Record');
        },
        error: function () {
            alert('Data not found');
        }
    });
}

function UpdateGenre() {

    var objData = {
        Id: $('#GenreId').val(),
        Name: $('#Name').val(),
        Description: $('#Description').val(),
        GameCount: $('#GameCount').val()
    };

    $.ajax({
        url: '/Genre/Update',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        dataType: 'json',

        success: function () {

            alert('Data Updated');

            HideModalPopUp();
            ShowGenreData();
            ClearTextBox();
        },
        error: function () {
            alert("Data can't Saved!");
        }
    });
}