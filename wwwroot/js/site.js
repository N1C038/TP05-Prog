//Como mínimo deberán validarse mediante el uso de JS: Campos obligatorios. Longitud mínima del nombre de usuario. Longitud mínima de la contraseña. Nombre y apellido con caracteres válidos.

function CampoObligatorio(){
    let Usuario = document.getElementById("Usuario").value;
    let Contra = document.getElementById("Contraseña").value;
    let nombre = document.getElementById("Nombre").value;
    let apellido = document.getElementById("Apellido").value;
    let tipo = document.getElementById("Tipo de Usuario").value;
    if(Usuario == null && Contra == null && apellido == null && tipo == null){
    console.log("Todos los campos son obligatorios");
    }
}
function LongitudMinima(){
let Usuario = document.getElementById("Usuario").value;
let Contra = document.getElementById("Contraseña").value;
let mensaje = "";
if(Usuario.length < 5){
    mensaje += "El nombre de usuario debe tener al menos 5 caracteres. ";
}
if(Contra.length < 8){
    mensaje += "La contraseña debe tener al menos 8 caracteres. ";
}
return mensaje;
}
//hacerlo a partir de https://docs.google.com/presentation/?usp=slides_web
function CaracteresValidos(){
let nombre = document.getElementById("Nombre").value;
let apellido = document.getElementById("Apellido").value;
let mensaje = "";
let invalidos = "!@#$%^&*()_+[]{}|;':\",./<>?`~0123456789";

if(nombre.includes(invalidos)){
    mensaje += "El nombre no puede contener caracteres especiales. ";
}
if(apellido.includes(invalidos)){
    mensaje += "El apellido no puede contener caracteres especiales. ";
}
return mensaje;
}
