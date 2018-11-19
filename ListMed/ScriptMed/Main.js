function pegarParametro() {
    var result = {}, keyValuePairs = location.search.slice(1).split("&");
    keyValuePairs.forEach(function (keyValuePair) {
        keyValuePair = keyValuePair.split('=');
        result[decodeURIComponent(keyValuePair[0])] = decodeURIComponent(keyValuePair[1]) || '';
    });
    return result;
}
function botaEspaco(str) {
    if (str === null || str === undefined)
        return null;
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
$('.cel').mask('(00) 00000-0000');
$('.cel').blur(function () {
    if ($(this).val().length < 15) {
        $(this).mask('(00) 0000-0000');
    }
});

$('.cep').mask('00000-000');
//$('.hora').mask('00:00');
$('.hora').mask('Hh:Mm', {
    'translation': {
        H: { pattern: /[0-2]/ },
        h: { pattern: /[01]?\d|2[0-3]/ },
        M: { pattern: /[0-5]/ },
        m: { pattern: /[0-9]/ }
    }
});