var escolhidas = [];
var Servescolhidas = [];
var pontos = 0;
pontosatuais = 0;
$('.selectEstado').on('change', function () {
    var idEstado = parseInt($(this).val());
    if (idEstado !== null && idEstado > 0) {
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Contribua/ListarCidades',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ "id": idEstado }),
            success: function (response) {
                var html = '<option value="">Selecione Cidade</option>';
                response.forEach(function (item) {
                    html += '<option value="' + item.id + '">' + item.descricao + '</option>';
                });
                $('.selectCidade').html(html);

            },
            error: function (error) { console.log(error); }
        }).fail(function (error) { console.log(error); });
    }
    else {
        $('.selectCidade').html('');
    }
    $('.selectBairro').html('');
});
$('.selectCidade').on('change', function () {
    var idCidade = parseInt($(this).val());
    if (idCidade !== null && idCidade > 0) {
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Contribua/ListarBairros',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ "id": idCidade }),
            success: function (response) {
                var html = '<option value="">Selecione Bairro</option>';
                response.forEach(function (item) {
                    html += '<option value="' + item.id + '">' + item.descricao + '</option>';
                });
                $('.selectBairro').html(html);

            },
            error: function (error) { console.log(error); }
        }).fail(function (error) { console.log(error); });
    }
    else {
        $('.selectBairro').html('');
    }

});

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
        $('.divEspecialidades').append('<div class="chip boxEspecialidades" ref="' + ui.item.value + '"><small>' + ui.item.label + '</small><input type="hidden" name="especialidades" value="' + ui.item.value +'" /><span class="closebtn">&times;</span></div>');
        $('#txtEspecialidades').val(ui.item.label);
        $('#txtEspecialidades').val("");

        $('.closebtn').on('click', function () {
            $(this).parent().remove();
      
 
        });

        $(this).val('');
        $(this).trigger('blur');

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
});
$("#txtServicos").autocomplete({

    source: function (request, response) {

        $.ajax({
            type: 'POST',
            url: baseUrl + 'Contribua/ListarServicos',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ "nome": request.term, "escolhidas": Servescolhidas }),
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
        $('.divServicos').append('<div class="chip boxServicos" ref="' + ui.item.value + '"><small>' + ui.item.label + '</small><input type="hidden" name="servicos" value="' + ui.item.value + '" /><span class="closebtn">&times;</span></div>');
        $('#txtServicos').val(ui.item.label);
        $('#txtServicos').val("");

        $('.closebtn').on('click', function () {
            $(this).parent().remove();
        });

        $(this).val('');
        $(this).trigger('blur');

    }

}).focus(function () {
    Servescolhidas = [];

    $('.divServicos').find('.boxServicos').each(function () {
        Servescolhidas.push(parseInt($(this).attr('ref')));

    });
    $(this).autocomplete("search");
    });

$('#cadastraClinica').on('submit', function () {
    $('.dinheiro').each(function (i, item) {
        $(this).val($(this).val().replace(/\./g, ''));
    
    });
    $('#cadastraClinica').append('<input type="hidden" name="Pontos" value="' + $('.mostrapt').text() + '" />');
});

$('.addTelCadClie').on('click', function () {
    var html = '<div class="offset-md-6 col-md-5 col-10"><label>Telefone</label><input type="text" class="form-control cel" name="cel" /></div><div class="col-md-1 col-2" style="padding: 15px;"><a href="javascript:void(0)" class="semEstilo excluiCel" ><i class="fa fa-times-circle"></i></a></div>';
    $('.telefones').append(html);
    $('.cel').mask('(00) 00000-0000');
    $('.cel').blur(function () {
        if ($(this).val().length < 15) {
            $(this).mask('(00) 0000-0000');
        }
    });
    $('.excluiCel').on('click', function () {
        $(this).parent().prev().remove();
        $(this).parent().remove();
     
    });
});
$('.excluiCel').on('click', function () {
    $(this).parent().prev().remove();
    $(this).parent().remove();

});

$('.pts').blur(function () {
    if ($(this).is('input')) {
        if ($(this).hasClass('foi')) {
            if ($(this).val().length == 0) {
                pontos -= parseInt($(this).attr('pt'));
                $(this).removeClass('foi');
            }
        }
        else {
            if ($(this).val().length > 0) {
                pontos += parseInt($(this).attr('pt'));
                $(this).addClass('foi');
            }

        }
    }
    else {
        if ($(this).hasClass('foi')) {
            if ($(this).val() == 0 || $(this.val() == "")) {
                $(this).removeClass('foi');
                pontos -= parseInt($(this).attr('pt'));
            }
        }
        else {
            if ($(this).val() > 0) {
                pontos += parseInt($(this).attr('pt'));
                $(this).addClass('foi');
            }

        }
    }

    $('.mostrapt').text(pontos);
});