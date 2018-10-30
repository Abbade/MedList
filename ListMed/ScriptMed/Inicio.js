
$('#vaiEstados').on('click', function () {
    console.log($('#estados').val());
    var estados = JSON.parse($('#estados').val());
    $.ajax({
        type: 'POST',
        url: baseUrl + 'Inicio/AddEstado',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ "estados": estados }),
        success: function (data) {
            console.log(data);

        },
        error: function (error) { console.log(error); }
    }).fail(function (error) { console.log(error); });
});
