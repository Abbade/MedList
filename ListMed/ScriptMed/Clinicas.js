
$(function () {
    
});

function recuperarFiltros() {
    var parametrosClinica = pegarParametro();

    var localidade = botaEspaco(parametrosClinica['localidade']);
 
    var preco = $('#myRange').val().toString().replace(".", ",");
    var servico = $('.slcServicos').val();
    var especialidades = [];
    $('.divEspecialidades').find('.boxEspecialidades').each(function () {
        especialidades.push(parseInt($(this).attr('ref')));
    });
    var filtro = {
        localidade: localidade,
        preco: preco,
        servico: servico,
        especialidades : especialidades
    };
 
    return filtro;
}
$('#myRange').on('change', function () {
    listarClinicas();
});
$('.slcServicos').on('change', function () {
    listarClinicas();
});
function listarClinicas() {

    var filtros = recuperarFiltros();
    console.log(filtros);
    $.ajax({
        type: 'POST',
        url: baseUrl + 'Clinicas/listarClinicas',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ "filtros": filtros }),
 
        success: function (response) {
            console.log("coe");
           // refazMapa();

        },
        error: function (error) { console.log(error); }
    }).fail(function (error) { console.log(error); })
        .done(function (partialViewResult) {
            $("#clinicas").html(partialViewResult);
        });
}
$("#txtEspecialidades").autocomplete({

    source: function (request, response) {

        $.ajax({
            type: 'POST',
            url: baseUrl + 'Clinicas/listarEspecialidades',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ "nome": request.term, "escolhidas": escolhidas }),
            success: function (data) {
                response(data);

            },
            error: function (error) { console.log(error); }
        }).fail(function (error) { console.log(error); });
    },
    minLength: 0,
    appendTo: "#autoEspec",
    autoFocus: true,
    select: function (event, ui) {
        event.preventDefault();
        $('.divEspecialidades').append('<div class="chip boxEspecialidades" ref="' + ui.item.value + '"><small>' + ui.item.label + '</small><span class="closebtn">&times;</span></div>');
        $('#txtEspecialidades').val(ui.item.label);
        $('#txtEspecialidades').val("");

        $('.closebtn').on('click', function () {
            $(this).parent().remove();
            listarClinicas();
        });

        $(this).val('');
        $(this).trigger('blur');
        listarClinicas();
    }

}).focus(function () {
    escolhidas = [];
    $('.divEspecialidades').find('.boxEspecialidades').each(function () {
        escolhidas.push(parseInt($(this).attr('ref')));
    });
    $(this).autocomplete("search");
});
$('.closebtn').on('click', function () {
    $(this).parent().remove();
    listarClinicas();
});


$(".avalia").on({
    mouseenter: function () {
       
        for (var i = 0; i <= this.id; i++) {
            $('#' + i).addClass('checado');
        }

    
    },
    mouseleave: function () {
        for (var i = 0; i <= this.id; i++) {
            $('#' + i).removeClass('checado');
        }
    },
    click: function () {
        $('.check').removeClass('check');
        for (var i = 0; i <= this.id; i++) {
            $('#' + i).addClass('check');
        }
    }
});
$('.coracao').on('click', function () {
    var id = parseInt($(this).parent().attr('href').substr(1));
 
    if ($(this).hasClass('cor-coracao')) {
        $(this).removeClass('cor-coracao');
    }
    else {
     
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Clinicas/FavoritarClinica',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ "id": id }),
            success: function (response) {
                if (response)
                    $(this).addClass('cor-coracao');

            },
            error: function (error) { console.log(error); }
        }).fail(function (error) { console.log(error); });
    }
   

});