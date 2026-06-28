$(document).ready(function () {
    ShowGameData();
});


function ShowGameData() {
    var url = $('#urlGameData').val();
    $.ajax({
        url: url,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, statu, xhr) {
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.id + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.developer + '</td>';
                object += '<td>' + item.platform + '</td>';
                object += '<td>' + new Date(item.releaseDate).toLocaleDateString('tr-TR') + '</td>';
                object += '<td>' + item.price + '</td>';
                object += '<td>' + item.description + '</td>';
                object += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger" onclick="Delete(' + item.id + ');">Delete</a></td>';
                object += '</tr>';
            });
            $('#table_data').html(object);
        },
        error: function () {
            alert("Data can't get");
        }
    });
};


$('#btnAddGame').click(function () {
    ClearTextBox();
    $('#GameMadal').modal('show');
    $('#gameId').hide();
    $('#AddGame').css('display', 'block');
    $('#btnUpdate').css('display', 'none');
    $('#GameHeading').text('Add Game');
})

function AddGame() {
    var objData = {
        Name: $('#Name').val(),
        Developer: $('#Developer').val(),
        Platform: $('#Platform').val(),
        ReleaseDate: $('#ReleaseDate').val(),
        Price: $('#Price').val(),
        Description: $('#Description').val()
    }
    $.ajax({
        url: '/Game/AddGame',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        dataType: 'json',
        success: function () {
            alert('Data Saved');
            ClearTextBox();
            ShowGameData();
            HideModalPopUp();
        },
        error: function () {
            alert("Data can't Saved!");
        }

    });
}


function ClearTextBox() {
    $('#Name').val('');
    $('#Developer').val('');
    $('#Platform').val('');
    $('#ReleaseDate').val('');
    $('#Price').val('');
    $('#Description').val('');
    $('#GameId').val('');
}
function HideModalPopUp() {
    $('#GameMadal').modal('hide');
}

function Delete(id) {
    if (confirm('Are you sure, You want to delete this record?')) {
        $.ajax({
            url: '/Game/Delete?id=' + id,
            success: function () {
                alert('Record Deleted!');
                ShowGameData();
            },
            error: function () {
                alert("Data can't be deleted!");
            }
        })
    }
}


function Edit(id) {

    $.ajax({
        url: '/Game/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $('#GameMadal').modal('show');
            $('#GameId').val(response.id);
            $('#Name').val(response.name);
            $('#Developer').val(response.developer);
            $('#Platform').val(response.platform);
            $('#ReleaseDate').val(response.releaseDate.split('T')[0]);
            $('#Price').val(response.price);
            $('#Description').val(response.description);
            $('#AddGame').css('display', 'none');
            $('#btnUpdate').css('display', 'block');
            $('#GameHeading').text('Update Record');
        },
        error: function () {
            alert('Data not found');
        }
    })
}

function UpdateGame() {
    var objData = {
        Id: $('#GameId').val(),
        Name: $('#Name').val(),
        Developer: $('#Developer').val(),
        Platform: $('#Platform').val(),
        ReleaseDate: $('#ReleaseDate').val(),
        Price: $('#Price').val(),
        Description: $('#Description').val()
    }
    $.ajax({
        url: '/Game/Update',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        dataType: 'json',
        success: function () {
            alert('Data Updated');
            HideModalPopUp();
            ShowGameData();
            ClearTextBox();
        },
        error: function () {
            alert("Data can't Saved!");
        }
    })
}



