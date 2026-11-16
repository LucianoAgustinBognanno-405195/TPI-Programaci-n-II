// Comprobamos si existe un token de sesión
const token = sessionStorage.getItem("token");

// Buscamos los elementos del navbar para empleados
const opcionesEmpleado = document.querySelectorAll(
    '[data-auth="employee"]'
);

// Buscamos los elementos del navbar para visitantes
const opcionesVisitante = document.querySelectorAll(
    '[data-auth="guest"]'
);

// Si existe un token, el empleado inició sesión
if (token) {

    // Mostramos las opciones de empleados
    opcionesEmpleado.forEach(opcion => {
        opcion.classList.remove("d-none");
    });

    // Ocultamos la opción de iniciar sesión
    opcionesVisitante.forEach(opcion => {
        opcion.classList.add("d-none");
    });

} else {

    // Ocultamos las opciones de empleados
    opcionesEmpleado.forEach(opcion => {
        opcion.classList.add("d-none");
    });

    // Mostramos la opción de iniciar sesión
    opcionesVisitante.forEach(opcion => {
        opcion.classList.remove("d-none");
    });

}

// Configuramos el botón de cerrar sesión
const botonCerrarSesion = document.getElementById("btnCerrarSesion");

if (botonCerrarSesion) {

    botonCerrarSesion.addEventListener("click", function (event) {

        event.preventDefault();

        // Eliminamos los datos de la sesión
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("empleado");

        // Volvemos al inicio
        window.location.href = "index.html";

    });

}// Comprobamos si la página requiere autenticación
const paginaProtegida = document.body.dataset.requiresAuth;

if (paginaProtegida === "true" && !token) {

    // Si no hay sesión, volvemos al login
    window.location.href = "login.html";

}