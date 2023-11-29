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
   