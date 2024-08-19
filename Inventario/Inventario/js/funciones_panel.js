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

function PasarValor() {
    document.getElementById("fecha").value = document.getElementById("fech").value;
}
//pasar valor checkbox
const MarcarCheckBox = (validar) => {
    var checkBox = document.getElementsByTagName('input');
    for (i = 0; i <= checkBox.length; i++) {
        if (checkBox[i].type == "checkbox") {
            checkBox[i].checked = validar.checked;
        }
    }
}
//reiniciar formulario
function funcion_reiniciar(x) {
    document.getElementById(x).reset();
}

var container = document.getElementById('container');
setTimeout(function () {
    container.classList.add('cerrar');
}, 9000);

var loadFile = function (event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('output');
        output.src = reader.result;
    };
    reader.readAsDataURL(event.target.files[0]);
};

//quitar carpeta en url
function ModificaUrl() {
    let currentUrl = window.location.href;
    let newUrl;
    if (currentUrl.includes("TipoVehiculos/")) {
        newUrl = currentUrl.replace("TipoVehiculos/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("Marcas/")) {
        newUrl = currentUrl.replace("Marcas/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("ModeloVehiculos/")) {
        newUrl = currentUrl.replace("ModeloVehiculos/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("Vehiculos/")) {
        newUrl = currentUrl.replace("Vehiculos/", "");
        window.history.replaceState({}, document.title, newUrl);
    } else if (currentUrl.includes("Configuracion/", "")) {
        newUrl = currentUrl.replace("Configuracion/", "");
        window.history.replaceState({}, document.title, newUrl);
    }
    else {
        console.log("URL no modificada:", currentUrl);
    }
}


//nombre de la pagina actual
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
    $(".inventario").click(function (e) {
        e.preventDefault();
        $(".principal, .secundario, .terciario").removeClass("show");
    });
    $(".principal, .secundario, .terciario").click(function (e) {
     
        $(this).addClass("show");
    });
});