function MostarDatos() {

   
    var pos = $("select option:selected").val();



    $.ajax({
        type: "POST",
        url: '/Usuario/consultarUsuarioAjax',
        data: {
            pos: pos,
        },
        dateType: 'json',
        success: function (response) {
            console.log(response);
            $('#direccion').val(response.direccion);
            $('#cedula').val(response.cedula);
            $('#telefono').val(response.telefono);
        },
        error: function (response) {
            console.log("nah")

        }
    });
}