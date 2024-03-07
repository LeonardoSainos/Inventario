
function ActivarBoton(opcion) {
    // Activar el botón dentro del formulario
    switch (opcion) {
        case "Bloquear": {
            var boton = document.querySelector('.btnBloquearClass');
            alert(boton);
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Desbloquear": {
            var boton = document.querySelector('.btnDesbloquearClass');
            alert(boton);
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Resetear": {
            var boton = document.querySelector('.btnResetearClass');
            alert(boton);
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Eliminar": {
            var boton = document.querySelector('.btnEliminarClass');
            alert(boton);
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Nuevo": {
            var boton = document.getElementById("btnNuevo");
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Pdf": {
            var boton = document.querySelector('.btnPdfClass');
            alert(boton);
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Excel": {
            var boton = document.querySelector('.btnExcelClass');
            alert(boton);
            if (boton) {
                boton.click();
            }
            break;
        }
        default: "Ninguno";
    }
}
function BuscarUsuario(pagina) {
    var URL; // Declara la variable URL aquí
    // Verifica si se proporcionó un valor para el parámetro pagina
    if (typeof pagina === 'undefined') {
        // Si no se proporcionó un valor, establece el valor predeterminado
        pagina = 1;
    }
    if (pagina <= 1) {
        URL = "./admin.aspx?view=searchUsers&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val();
    } else if (pagina > 1) {
        URL = "./admin.aspx?view=searchUsers&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val() + "&pagina=" + pagina;
    }
    $.get(URL, function (datos) {
        $("#contenido").html(datos);

    });
}

function FiltroUsers(ordenador, pagina) {
    var URL;
    if (typeof ordenador === 'undefined') {
        ordenador = 'Nombre'
    }
    if (typeof pagina === 'undefined') {
        pagina = 1;
    }

    if (pagina <= 1) {
        URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=" + ordenador;
    }
    else if (pagina > 1) {
        URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=" + ordenador + "&pagina=" + pagina;
    }


    $.get(URL, function (datos) {
        $("#contenido").html(datos);
        ValidaId();
    });
}
function ValidaId() {
    var retrocrede = document.getElementById("retrocede");
    var incremento = document.getElementById("incremento");
    // Verificar si el elemento existe
    if (retrocrede) {
        retrocrede.id = "retrocedeOrder";
    }
    if (incremento) {
        incremento.id = "incrementoOrder";
    }
}

$(document).ready(function () {
    //// BUSCAR USUARIO
    $(document).on("click", "#mt", function () {
        BuscarUsuario();
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

    //PAGINADOR 2 FILTRO
    $(document).on("click", "[id^='paginador']", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un número entero
        var tipoOrden = document.getElementById("tipoBusqueda");
        tipoOrden = tipoOrden.value;
        if ($("#busqueda").val() == null || $("#busqueda").val() == "") {

            //alert(num + tipoOrden);
            FiltroUsers(tipoOrden, num);
        }
        else {
            BuscarUsuario(num);
        }

    });
    // INCREMENTOS 
    $("#retrocede").click(function () {
        var pagina = document.getElementById("paginaActual").value;
        BuscarUsuario(pagina - 1);
    });
    $(document).on("click", "#retrocede", function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarUsuario(pagina - 1);
    });

    $("#incremento").click(function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarUsuario(pagina + 1);
    });
    $(document).on("click", "#incremento", function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarUsuario(pagina + 1);
    });

    //INCREMENTOS ORDER 
    $("#retrocedeOrder").click(function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        var tipoBusqueda = document.getElementById("tipoBusqueda").value;
        FiltroUsers(tipoBusqueda, pagina - 1);

    });
    $(document).on("click", "#retrocedeOrder", function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        var tipoBusqueda = document.getElementById("tipoBusqueda").value;
        FiltroUsers(tipoBusqueda, pagina - 1);

    });
    $("#incrementoOrder").click(function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        var tipoBusqueda = document.getElementById("tipoBusqueda").value;
        FiltroUsers(tipoBusqueda, pagina + 1);
    });
    $(document).on("click", "#incrementoOrder", function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        var tipoBusqueda = document.getElementById("tipoBusqueda").value;
        FiltroUsers(tipoBusqueda, pagina + 1);
    });
});