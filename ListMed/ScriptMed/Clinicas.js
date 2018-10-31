
$(function () {
    
});

function recuperarFiltros() {
    var parametrosClinica = pegarParametro();

    var localidade = botaEspaco(parametrosClinica['localidade']);
    var preco = $('#myRange').val();
    var servico = $('.slcServicos').val();
    var filtro = {
        localidade: localidade,
        preco: preco,
        servico: servico
    };
    console.log(filtro);
    return filtro;
}
$('#myRange').on('change', function () {
    recuperarFiltros();
});