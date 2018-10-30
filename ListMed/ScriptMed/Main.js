$("#procura_clinica").autocomplete({
    source: function (request, response) {
        console.log(request.term);
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Inicio/ListarLocalidades',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ "nome": request.term }),
            success: function (data) {
                response(data);

            },
            error: function (error) { console.log(error); }
        }).fail(function (error) { console.log(error); });
    },
    autoFocus: true,
    select: function (event, ui) {
        //event.preventDefault();


    }
});
$('#procura_clinica').keypress(function (e) {
    if (e.which === 13) {
        $('.class="form_consultaClinica"').submit();
    }
});