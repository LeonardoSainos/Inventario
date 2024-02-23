  if ('serviceWorker' in navigator) {
     navigator.serviceWorker.register('./Inventario/sw/sw.js')
      .then(
         function (registration) {
             console.log('Reg. satisfactorio del sw en el ámbito: ', registration.scope);
         }
     ).catch(
       function (err) {
       console.log('El SW no se registró', err);
         }
     );
  }
// ELIMINAR MODAL DEL LOGIN
/*
function EliminarModal() {
    var modalElement = document.getElementById('modalLog');
    if (modalElement) {
        modalElement.parentNode.removeChild(modalElement);
        console.log("elemento eliminado");
    }
}
*/                                  
function PasarValor()
{
   document.getElementById("fecha").value = document.getElementById("fech").value;
}
const MarcarCheckBox = (validar) =>{
  var checkBox = document.getElementsByTagName('input');
  for( i=0;i<=checkBox.length; i++)
    {
        if(checkBox[i].type == "checkbox"){
            checkBox[i].checked= validar.checked;
        }
    }
}

function funcion_reiniciar( x)
{
	document.getElementById(x).reset();
}

var container= document.getElementById('container');
setTimeout(function(){
container.classList.add('cerrar');
},9000);

var loadFile = function(event) {
    var reader = new FileReader();
    reader.onload = function(){
      var output = document.getElementById('output');
      output.src = reader.result;
    };
    reader.readAsDataURL(event.target.files[0]);
};

function BuscarUsuario(pagina) {
    var URL; // Declara la variable URL aquí
    // Verifica si se proporcionó un valor para el parámetro pagina
    if (typeof pagina === 'undefined') {
        // Si no se proporcionó un valor, establece el valor predeterminado
        pagina = 1;
    }
    if(pagina <= 1) {
        URL="./admin.aspx?view=searchUsers&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val();
    } else if(pagina>1) {
        URL = "./admin.aspx?view=searchUsers&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val() + "&pagina=" + pagina;
    }
    $.get(URL, function (datos) {
        $("#contenido").html(datos);

    });
}

function FiltroUsers(ordenador,pagina) {
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
    var paginador = document.getElementById("paginador");
    var paginador2 = document.getElementById("paginador2");
    // Verificar si el elemento existe
    if (retrocrede) {
        retrocrede.id = "retrocedeOrder";
    }
    if (incremento) {
        incremento.id = "incrementoOrder";
    }
    if (paginador) {
        paginador.id = "paginadorOrder";
    }
    if (paginador2) {
        paginador2.id = "paginadorOrder2";
    }
}

function PaginaActual() {
    var pagina = document.getElementById("paginaActual").value;
    return pagina;
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
    //PAGINADOR BUSCAR
   /* $(document).on("click", "#paginador", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un número entero
        //console.log(num);
        BuscarUsuario(num);
    });*/
    //PAGINADOR 2 BUSCAR
    $(document).on("click", "[id^='paginador']", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un número entero
       // console.log(num);
        BuscarUsuario(num);
    });
    //PAGINADOR FILTRO pendiente sabado 2:04 pm
    /*$(document).on("click", "#paginadorOrder", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un número entero
        var tipoOrden = document.getElementById("tipoBusqueda");
        tipoOrden = tipoOrden.value;
        //alert(num + tipoOrden);
        FiltroUsers(tipoOrden,num);
    });*/
    //PAGINADOR 2 FILTRO
    $(document).on("click", "[id^='paginadorOrder']", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un número entero
        var tipoOrden = document.getElementById("tipoBusqueda");
        tipoOrden = tipoOrden.value;
        //alert(num + tipoOrden);
        FiltroUsers(tipoOrden,num);
    });


    // INCREMENTOS 
    $("#retrocede").click(function () {
        var pagina = document.getElementById("paginaActual").value;
         
        BuscarUsuario(pagina-1);
    });
    $(document).on("click", "#retrocede", function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarUsuario(pagina - 1);
    });
    
    $("#incremento").click(function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        BuscarUsuario(pagina+1);
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
        FiltroUsers(tipoBusqueda, pagina-1);
       
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
        FiltroUsers(tipoBusqueda, pagina+1);
    });
    $(document).on("click", "#incrementoOrder", function () {
        var pagina = document.getElementById("paginaActual").value;
        pagina = parseInt(pagina);
        var tipoBusqueda = document.getElementById("tipoBusqueda").value;
        FiltroUsers(tipoBusqueda, pagina + 1);
    });

    // Carousel
    $(document).ready(function () {
        $("#carousel-example-generic").carousel({
            interval: 2500,
        });
    });
    ////////////////////////////////////////// FUNCIONES PARA VALIDAR ADMIN 
    $("#input_user").keyup(function () {
        $.ajax({
            url: "./process/val_admin.aspx?id=" + $(this).val(),
            success: function (data) {
                $("#com_form").html(data);
            }
        });
    });
    $("#input_user2").keyup(function () {
        $.ajax({
            url: "./process/val_admin.aspx?id=" + $(this).val(),
            success: function (data) {
                $("#com_form2").html(data);
            }
        });
    });

    $(".configuracion-link").click(function (e) {
        e.preventDefault(); // Evita la acción predeterminada del enlace
        var dropdownMenu = $(this).closest(".dropdown").find(".dropdown-menu");
        dropdownMenu.toggleClass("show");
    });
    // Evento clic para cerrar el menú desplegable al hacer clic fuera de él
    $(".nombre").click(function (e) {
        e.preventDefault(); // Evita la acción predeterminada del enlace
        //    var dropdownMenu = $(this).closest(".dropdown").find(".dropdown-menu");
        dropdownMenu.toggleClass("open");
    });
    // Evitar que se cierre el menú al hacer clic en un enlace interno
    $(".dropdown-menu").on("click", function (e) {
        e.stopPropagation();
    });
    $(".inventario").click(function(e) {
        e.preventDefault();
        $(".principal, .secundario, .terciario").removeClass("show");
    });
    $(".principal, .secundario, .terciario").click(function (e) {
        e.preventDefault();
        $(this).addClass("show");
    });
});