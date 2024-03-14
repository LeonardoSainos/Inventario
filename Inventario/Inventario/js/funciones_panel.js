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


function ModificaUrl() {
    let currentUrl = window.location.href;

    if (currentUrl.includes("TipoVehiculos/")) {
        var newUrl = currentUrl.replace("TipoVehiculos/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("Marcas/")) {
        var newUrl = currentUrl.replace("Marcas/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("ModeloVehiculos/")) {
        var newUrl = currentUrl.replace("ModeloVehiculos/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("Vehiculos/")) {
        var newUrl = currentUrl.replace("Vehiculos/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else {
        console.log("URL no modificada:", currentUrl);
    }
}



function PaginaActual() {
    var pagina = document.getElementById("paginaActual").value;
    return pagina;
}
$(document).ready(function () {
    // /////////////////////////////////Carousel

    ModificaUrl();

    
        $("#carousel-example-generic").carousel({
            interval: 2500,
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