 class GlobalVista{
    constructor(vista) {
        this.vista = vista;   
    }
    set setVista(newVista) {
        this.vista = newVista;
    }
    get getVista() {
        return this.vista;
    }
}
var vistaGlobal = new GlobalVista("");
function NombrePaginaActual() {
    let nombre = document.getElementById("NombrePaginaActual").value;
    return nombre ? nombre : "No";
}
function BuscarInformacion(vista, pagina) {
    var URL; // Declara la variable URL aquí
    // Verifica si se proporcionó un valor para el parámetro pagina
    if (typeof pagina === 'undefined') {
        // Si no se proporcionó un valor, establece el valor predeterminado
        pagina = 1;
    }
    vistaGlobal.setVista = vista;;
    var nueva = vistaGlobal.getVista;
    //vistaGlobal = vista;
    if (pagina <= 1) {
        URL = "./admin.aspx?view=" + nueva + "&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val();
    } else if (pagina > 1) {
        URL = "./admin.aspx?view=" + nueva + "&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val() + "&pagina=" + pagina;
    }
    $.get(URL, function (datos) {
        $("#contenido").html(datos);
    });
}
function FiltroUsers(ordenador, pagina) {
    var URL;
    if (typeof ordenador === 'undefined') {
        ordenador = 'Nombre';
    }
    if (typeof pagina === 'undefined') {
        pagina = 1;
    }

    let nombrePagina = NombrePaginaActual();
    alert(nombrePagina);
    if (pagina <= 1) {
        URL = "./admin.aspx?view=" + nombrePagina + "&" + $("#rol").val() + "=" + ordenador;
       // alert(URL);
    }
    else if (pagina > 1) {
        URL = "./admin.aspx?view=" + nombrePagina + "&" + $("#rol").val() + "=" + ordenador + "&pagina=" + pagina;
      //  alert(URL);
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
function ActivarBoton(opcion) {
    // Activar el botón dentro del formulario
    switch (opcion) {
        case "Bloquear": {
            var boton = document.querySelector('.btnBloquearClass');
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Desbloquear": {
            var boton = document.querySelector('.btnDesbloquearClass');
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Resetear": {
            var boton = document.querySelector('.btnResetearClass');
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Eliminar": {
            var boton = document.querySelector('.btnEliminarClass');
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
         
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Excel": {
            var boton = document.querySelector('.btnExcelClass');
    
            if (boton) {
                boton.click();
            }
            break;
        }
        default: "Ninguno";
    }
}
$(document).ready(function () {
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
            BuscarInformacion(vistaGlobal.getVista, num);
        }
    });
    // INCREMENTOS 
    $("#retrocede").click(function () {

        var pagina = document.getElementById("paginaActual").value;
        BuscarInformacion(vistaGlobal.getVista, pagina - 1);
    });
    $(document).on("click", "#retrocede", function () {

        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarInformacion(vistaGlobal.getVista, pagina - 1);
    });

    $("#incremento").click(function () {

        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarInformacion(vistaGlobal.getVista, pagina + 1);
    });
    $(document).on("click", "#incremento", function () {

        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarInformacion(vistaGlobal.getVista, pagina + 1);
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
    //////////////////////////////////////////////////// BUSCAR Y FILTROS DE USUARIOS /////////////////////////////////////////
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
    //////////////////////////////////////////////////// BUSCAR Y FILTROS DE TIPOS /////////////////////////////////////////
    $(document).on("click", "#st", function () {
        BuscarInformacion("searchTypes");
    });
    $(document).on("click", "#Typenombree", function () {
        FiltroUsers();
    });
    $("#Typenombree").click(function () {
        FiltroUsers();
    });
    $(document).on("click", "#Typefechaa", function () {
        FiltroUsers('Fecha');
    });
    $("#Typefechaa").click(function () {
        FiltroUsers('Fecha');
    });
    //////////////////////////////////////////////////// BUSCAR Y FILTROS DE MODELOS /////////////////////////////////////////
    $(document).on("click", "#sm", function () {
        BuscarInformacion("searchModels");
    });
    $(document).on("click", "#Modelnombree", function () {
        FiltroUsers();
    });
    $("#Modelnombree").click(function () {
        FiltroUsers();
    });
    $(document).on("click", "#Modelfechaa", function () {
        FiltroUsers('Fecha');
    });
    $("#Modelfechaa").click(function () {
        FiltroUsers('Fecha');
    });
    $(document).on("click", "#ModelAño", function () {
        FiltroUsers('Fecha');
    });
    $("#ModelAño").click(function () {
        FiltroUsers('Año');
    });
    //////////////////////////////////////////////////// BUSCAR Y FILTROS DE MARCAS /////////////////////////////////////////
    $(document).on("click", "#sb", function () {
        BuscarInformacion("searchBrands");
    });
    $(document).on("click", "#Brandnombree", function () {
        FiltroUsers();
    });
    $("#Brandnombree").click(function () {
        FiltroUsers();
    });
    $(document).on("click", "#Brandfechaa", function () {
        FiltroUsers('Fecha');
    });
    $("#Brandfechaa").click(function () {
        FiltroUsers('Fecha');
    });
   /*
    $(function () {
        $("#tipoo").on('change', function () {
            var liga = "../admin/Actions/";
            var id_tipo = $("#tipoo").val();
            var url = liga + 'AddCar.aspx';
            $.ajax({
                type: 'POST',
                url: url,
                data: 'id_tipo=' + id_tipo,
                success: function (data) {
                    $("#economico input").remove();
                    $("#economico").append(data);
                }
            });
            return false;
        });
    });*/
});