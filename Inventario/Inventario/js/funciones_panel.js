  if ('serviceWorker' in navigator) {
     navigator.serviceWorker.register('./Inventario/sw/sw.js')
      .then(
         function (registration) {
             console.log('Reg. satisfactorio del sw en el �mbito: ', registration.scope);
         }
     ).catch(
       function (err) {
       console.log('El SW no se registr�', err);
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
    var URL; // Declara la variable URL aqu�
    // Verifica si se proporcion� un valor para el par�metro pagina
    if (typeof pagina === 'undefined') {
        // Si no se proporcion� un valor, establece el valor predeterminado
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

function FiltroUsers(ordenador) {
    var URL;
    if (typeof ordenador === 'undefined') {
        ordenador = 'Nombre'
    }
    var URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=" + ordenador;
    alert(URL);
    $.get(URL, function (datos) {
        $("#contenido").html(datos);
    });
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
    $(document).on("click", "#paginador", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un n�mero entero
        console.log(num);
        BuscarUsuario(num);
    });
    //PAGINADOR 2 BUSCAR
    $(document).on("click", "#paginador2", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un n�mero entero
        console.log(num);
        BuscarUsuario(num);
    });


    //PAGINADOR FILTRO
    $(document).on("click", "#paginador", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un n�mero entero
        console.log(num);
        BuscarUsuario(num);
    });
    //PAGINADOR 2 FILTRO
    $(document).on("click", "#paginador2", function () {
        var num = parseInt($(this).text()); // Convertir el texto a un n�mero entero
        console.log(num);
        BuscarUsuario(num);
    });

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
        e.preventDefault(); // Evita la acci�n predeterminada del enlace
        var dropdownMenu = $(this).closest(".dropdown").find(".dropdown-menu");
        dropdownMenu.toggleClass("show");
    });
    // Evento clic para cerrar el men� desplegable al hacer clic fuera de �l
    $(".nombre").click(function (e) {
        e.preventDefault(); // Evita la acci�n predeterminada del enlace
        //    var dropdownMenu = $(this).closest(".dropdown").find(".dropdown-menu");
        dropdownMenu.toggleClass("open");
    });
    // Evitar que se cierre el men� al hacer clic en un enlace interno
    $(".dropdown-menu").on("click", function (e) {
        e.stopPropagation();
    });
    $(".inventario").click(function (e) {
        e.preventDefault();
        $(".principal, .secundario, .terciario").removeClass("show");
    });
    $(".principal, .secundario, .terciario").click(function (e) {
        e.preventDefault();
        $(this).addClass("show");
    });
});