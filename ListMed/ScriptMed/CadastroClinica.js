var escolhidas = [];
var Servescolhidas = [];
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
    listarClinicas();
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
        $('.divServicos').append('<div class="chip boxServicos" ref="' + ui.item.value + '"><small>' + ui.item.label + '</small><input type="hidden" name="servicos" value="' + ui.item.value  +'" /><span class="closebtn">&times;</span></div>');
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
    //var cadServicos = [];
    //var cadEspecialidades = [];
    //$('.divServicos').find('.boxServicos').each(function () {
    //    cadServicos.push(parseInt($(this).attr('ref')));
    //});
    //$('.divEspecialidades').find('.boxEspecialidades').each(function () {
    //    cadEspecialidades.push(parseInt($(this).attr('ref')));
    //});
    //console.log("Servicos: " + cadServicos + "Especidadades: " + cadEspecialidades);
    //$('#servicos').val(cadServicos);
    //$('#especialidades').val(cadEspecialidades);

});