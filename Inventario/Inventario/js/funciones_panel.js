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
 
 

function BuscarUsuario() {

    var URL = "./admin.aspx?view=searchUsers&busqueda=" + $("#busqueda").val() + "&" + $("#rol").val() + "=" + $("#rol").val();
    $.get(URL, function (datos, estado) {
  
        $("#contenido").html(datos);
    });
}

function FiltroUsers() {
    //admin.php?view=ticketadmin&ticket=all
   
    var URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=Nombre";

    $.get(URL, function (datos, estado) {
        $("#contenido").html(datos);
    
    }
    );
}
function FiltroFecha() {
    //admin.php?view=ticketadmin&ticket=all
    var URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=Fecha";
     
    $.get(URL, function (datos, estado) {
        $("#contenido").html(datos);
    }
    );
}

function FiltroCorreo() {
    //admin.php?view=ticketadmin&ticket=all
    var URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=Correo";
  
    $.get(URL, function (datos, estado) {
        $("#contenido").html(datos);
    }
    );
}
function FiltroEstatus() {
    //admin.php?view=ticketadmin&ticket=all
    var URL = "./admin.aspx?view=searchUsers&" + $("#rol").val() + "=Estatus";
 
    $.get(URL, function (datos, estado) {
        $("#contenido").html(datos);
    }
    );
}


/*
function ActivarBoton(opcion) {
    // Activar el botón dentro del formulario
    switch (opcion) {
        case "Bloquear": {
            var boton = document.getElementById("<%= btnBloquear.ClientID %>");
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Desbloquear": {
            var boton = document.getElementById("<%= btnDesbloquear.ClientID %>");
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Resetear": {
            var boton = document.getElementById("<%= btnResetear.ClientID %>");
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Eliminar": {
            var boton = document.getElementById("<%= btnEliminar.ClientID %>");
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
            var boton = document.getElementById("<%= btnPdf.ClientID %>");
            if (boton) {
                boton.click();
            }
            break;
        }
        case "Excel": {
            var boton = document.getElementById("<%= btnExcel.ClientID %>");
            if (boton) {
                boton.click();
            }
            break;
        }
        default: "Ninguno";
    }
}*/