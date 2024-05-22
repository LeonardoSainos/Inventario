$(document).ready(function () {
    //// BUSCAR USUARIO
    $(document).on("click", "#mt", function () {
        BuscarInformacion("searchUsers");
    });

    // ORDENAR POR NOMBRE
    $(document).on("click", "#nombree", function () {
        FiltroUsers();
    });
    $("#nombree").click(function () {
        FiltroUsers();
    });
    // ORDENAR POR FECHA
    $(document).on("click", "#fechaa", function () {
        FiltroUsers('Fecha');
    });
    $("#fechaa").click(function () {
        FiltroUsers('Fecha');
    });
    // ORDENAR POR CORREO
    $(document).on("click", "#correoo", function () {
        FiltroUsers('Correo');
    });
    $("#correoo").click(function () {
        FiltroUsers('Correo');
    });
    // ORDENAR POR ESTATUS
    $(document).on("click", "#estatuss", function () {
        FiltroUsers('Estatus');

    });
    $("#estatuss").click(function () {
        FiltroUsers('Estatus');
    });

}