
// Comprobamos si existe un token de sesión
const token = sessionStorage.getItem("token");

// Buscamos los elementos del navbar
const opcionesEmpleado = document.querySelectorAll(
    '[data-auth="employee"]'
);

const opcionesVisitante = document.querySelectorAll(
    '[data-auth="guest"]'
);

// Buscamos las opciones exclusivas de administrativo, gerente y supervisor
const opcionesAdmin = document.querySelectorAll(
    '[data-auth="employee-admin"]'
);

// Variable para almacenar el rol
let rol = "";

// Comprobamos si existe un token
if (token) {

    try {

        // Separamos las partes del JWT
        const partesToken = token.split(".");

        // Recuperamos el payload
        let payload = partesToken[1];

        // Adaptamos Base64 URL a Base64
        payload = payload.replace(/-/g, "+").replace(/_/g, "/");

        // Agregamos el padding necesario
        while (payload.length % 4) {
            payload += "=";
        }

        // Convertimos el payload a un objeto JavaScript
        const datosToken = JSON.parse(atob(payload));

        // Comprobamos que el token no esté vencido
        if (datosToken.exp > Date.now() / 1000) {

            // Recuperamos el rol del empleado
            rol = datosToken.role ||
                  datosToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ||
                  "";

            rol = rol.toLowerCase();

        } else {

            // Si el token venció, eliminamos la sesión
            sessionStorage.removeItem("token");
            sessionStorage.removeItem("empleado");
        }

    } catch (error) {

        // Si el token no es válido, eliminamos la sesión
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("empleado");
    }
}

// Comprobamos si hay un empleado autenticado
const empleadoAutenticado = rol !== "";

// Comprobamos si el empleado puede gestionar empleados
const puedeGestionarEmpleados = [
    "administrativo",
    "gerente",
    "supervisor"
].includes(rol);

// Mostramos u ocultamos las opciones del navbar
opcionesEmpleado.forEach(opcion => {
    opcion.classList.toggle("d-none", !empleadoAutenticado);
});

opcionesVisitante.forEach(opcion => {
    opcion.classList.toggle("d-none", empleadoAutenticado);
});

opcionesAdmin.forEach(opcion => {
    opcion.classList.toggle("d-none", !puedeGestionarEmpleados);
});

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
}

// Comprobamos si la página requiere autenticación
const paginaProtegida = document.body.dataset.requiresAuth;

if (paginaProtegida === "true" && !empleadoAutenticado) {

    // Si no hay sesión, volvemos al login
    window.location.href = "login.html";
}
// Comprobamos si la página requiere permisos administrativos
const paginaSoloAdmin = document.body.dataset.requiresAdmin;

if (paginaSoloAdmin === "true" && empleadoAutenticado && !puedeGestionarEmpleados) {

    // Si el empleado no tiene permisos, volvemos a servicios
    window.location.href = "gestion-servicios.html";

}
