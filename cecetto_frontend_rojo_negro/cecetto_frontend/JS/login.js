
// Recuperamos los elementos del HTML
const formulario = document.getElementById("formLogin");
const mensaje = document.getElementById("mensajeLogin");
const boton = document.getElementById("btnIngresar");

// Escuchamos el evento submit del formulario
formulario.addEventListener("submit", async function (event) {

    // Evitamos que el formulario se envíe de la manera tradicional
    event.preventDefault();

    // Recuperamos los valores ingresados
    const usuario = document.getElementById("usuario").value;
    const contrasenia = document.getElementById("contrasena").value;

    // Preparamos los datos que enviaremos al backend
    const datosLogin = {
        usuario: usuario,
        contrasenia: contrasenia
    };

    try {

        boton.disabled = true;
        boton.textContent = "Ingresando...";

        mensaje.textContent = "";

        // Enviamos la petición al backend
        const respuesta = await fetch(
            "https://localhost:7079/api/Auth/login",
            {
                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body: JSON.stringify(datosLogin)
            }
        );

        // Convertimos la respuesta a JSON
        const datos = await respuesta.json();

        // Comprobamos si las credenciales son incorrectas
        if (!respuesta.ok) {
            throw new Error(datos.mensaje || "Usuario o contraseña incorrectos");
        }

        // Guardamos el token durante la sesión del navegador
        sessionStorage.setItem("token", datos.token);

        // Guardamos algunos datos del empleado
        sessionStorage.setItem("empleado", JSON.stringify({
            idEmpleado: datos.idEmpleado,
            nombre: datos.nombre,
            apellido: datos.apellido,
            usuario: datos.usuario
        }));

        mensaje.className = "alert alert-success";
        mensaje.textContent = "¡Inicio de sesión correcto!";

        // Redirigimos al sistema de gestión
        window.location.href = "gestion-servicios.html";

    }
    catch (error) {

        mensaje.className = "alert alert-danger";
        mensaje.textContent = error.message;

        boton.disabled = false;
        boton.textContent = "Ingresar";
    }

});
