function pegarParametro() {
    var result = {}, keyValuePairs = location.search.slice(1).split("&");
    keyValuePairs.forEach(function (keyValuePair) {
        keyValuePair = keyValuePair.split('=');
        result[decodeURIComponent(keyValuePair[0])] = decodeURIComponent(keyValuePair[1]) || '';
    });
    return result;
}
function botaEspaco(str) {
    return str.replace(/\+/g, " ");
}
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