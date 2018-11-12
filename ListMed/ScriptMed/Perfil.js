function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagemTroca').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#fotoPerfil").change(function () {
    readURL(this);
    $('#imagemTroca').show();
    $('#imgPadrao').hide();

});