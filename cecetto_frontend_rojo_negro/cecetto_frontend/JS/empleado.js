async function cargarEmpleados() {

    try {

        const respuesta = await fetch(
            "https://localhost:7079/api/Empleados/by-EmpleadoALL",
            {
                method: "GET",
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }
        );

        // Comprobamos si ocurrió un error
        if (!respuesta.ok) {
            throw new Error("No se pudieron cargar los empleados");
        }

        // Convertimos la respuesta JSON
        const empleados = await respuesta.json();

        // Limpiamos el contenido anterior de la tabla
        tablaEmpleados.innerHTML = "";
       
        // Recorremos la lista de empleados
        empleados.forEach(empleado => {

            const fila = document.createElement("tr");

            fila.innerHTML = `
                <td>${empleado.idEmpleado}</td>
                <td>${empleado.nombre}</td>
                <td>${empleado.apellido}</td>
                <td>${empleado.usuario}</td>
                <td>${empleado.activo ? "Activo" : "Inactivo"}</td>
                <td>${empleado.documento.documento}</td>
                        <td>
            <button class="btn btn-primary btn-sm"
                    title="Editar"
                    onclick="editarEmpleado(${empleado.idEmpleado})">
                <i class="bi bi-pencil-square"></i>
            </button>

            <button class="btn btn-danger btn-sm"
                    title="Eliminar"
                    onclick="eliminarEmpleado(${empleado.idEmpleado})">
                <i class="bi bi-trash3"></i>
            </button>
        </td>
            `;

            tablaEmpleados.appendChild(fila);

        });

    }
    catch (error) {

        tablaEmpleados.innerHTML = `
            <tr>
                <td colspan="5" class="text-center text-danger">
                    ${error.message}
                </td>
            </tr>
        `;

        console.error(error);

    }
}
// Ejecutamos la función al cargar el archivo
//cargarEmpleados();

// Buscamos el botón en el HTML
const btnEmpleados = document.getElementById("btnEmpleados");

// Ejecutamos cargarEmpleados cuando se hace clic
btnEmpleados.addEventListener("click", cargarEmpleados);

// Rellenamos los comboBox
const comboCiudad = document.getElementById("ciudad");
const comboBarrio = document.getElementById("barrio");
const comboSucursal = document.getElementById("sucursal");

async function cargarCiudades() {
    try {
        const respuesta = await fetch(`${API_URL}/Empleados/By-Ciudades`);

        if (!respuesta.ok) {
            throw new Error("No se pudieron cargar las ciudades");
        }

        const ciudades = await respuesta.json();

        comboCiudad.innerHTML = '<option value="">Seleccionar ciudad</option>';

        ciudades.forEach(ciudad => {
            const opcion = document.createElement("option");

            opcion.value = ciudad.ciudad;
            opcion.textContent = ciudad.ciudad;

            comboCiudad.appendChild(opcion);
        });

    } catch (error) {
        console.error("Error ciudades:", error);
    }
}

async function cargarTiposDocumentos()
{

    const respuesta = await fetch(`${API_URL}/Empleados/By-TipoDocumento`);
    const tipos = await respuesta.json();

    const combo = document.getElementById("tipoDocumento");

    combo.innerHTML = '<option value="">Seleccionar tipo</option>';

    tipos.forEach(tipo => {
        const opcion = document.createElement("option");
        opcion.value = tipo.idTipoDocumento;
        opcion.textContent = tipo.tipos;
        combo.appendChild(opcion);
    });
}

async function cargarTiposEmpleados() {
    const respuesta = await fetch(`${API_URL}/Empleados/By-TipoEmpleado`);

    if (!respuesta.ok) {
        throw new Error(`Error HTTP: ${respuesta.status}`);
    }

    const tipos = await respuesta.json();

    const combo = document.getElementById("tipoEmpleado");

    combo.innerHTML = '<option value="">Seleccionar tipo</option>';

    tipos.forEach(tipo => {
        const opcion = document.createElement("option");

        opcion.value = tipo.idTipoEmpleado;
        opcion.textContent = tipo.tipoEmpleado;

        combo.appendChild(opcion);
    });
}
// CARGAR SUCURSALES
async function cargarSucursales() {
    try {
        const respuesta = await fetch(`${API_URL}/Empleados/By-Sucursal`);

        if (!respuesta.ok) {
            throw new Error("No se pudieron cargar las sucursales");
        }

        const sucursales = await respuesta.json();

        comboSucursal.innerHTML = '<option value="">Seleccionar sucursal</option>';

        sucursales.forEach(sucursal => {
            const opcion = document.createElement("option");

            opcion.value = sucursal.idSucursal;
            opcion.textContent = sucursal.sucursal;

            comboSucursal.appendChild(opcion);
        });

    } catch (error) {
        console.error("Error sucursales:", error);
    }
}


// CARGAR BARRIOS SEGÚN LA CIUDAD
async function cargarBarrios(ciudad) {
    try {
        const respuesta = await fetch(
            `${API_URL}/Empleados/By-Barrios?Ciudad=${encodeURIComponent(ciudad)}`
        );

        if (!respuesta.ok) {
            throw new Error("No se pudieron cargar los barrios");
        }

        const barrios = await respuesta.json();

        comboBarrio.innerHTML = '<option value="">Seleccionar barrio</option>';

        barrios.forEach(barrio => {
            const opcion = document.createElement("option");

            opcion.value = barrio.idBarrio;
            opcion.textContent = barrio.barrio;

            comboBarrio.appendChild(opcion);
        });

        comboBarrio.disabled = false;

    } catch (error) {
        console.error("Error barrios:", error);
    }
}


// CUANDO CAMBIA LA CIUDAD
comboCiudad.addEventListener("change", function () {

    const ciudadSeleccionada = comboCiudad.value;

    comboBarrio.innerHTML = '<option value="">Seleccionar barrio</option>';
    comboBarrio.disabled = true;

    if (ciudadSeleccionada !== "") {
        cargarBarrios(ciudadSeleccionada);
    }
});

const formNuevoEmpleado = document.getElementById("formNuevoEmpleado");

formNuevoEmpleado.addEventListener("submit", async function (event) {

    // Evitamos que el formulario recargue la página
    event.preventDefault();

    try {

        // Recuperamos los datos del formulario
        const empleado = {

            nombre: document.getElementById("nombre").value,

            apellido: document.getElementById("apellido").value,

            documento: {
                documento: Number(document.getElementById("documento").value),
                idTipoDocumento: Number(document.getElementById("tipoDocumento").value)
            },

            direccion: {
                calle: document.getElementById("calle").value,
                altura: Number(document.getElementById("altura").value),
                idBarrio: Number(document.getElementById("barrio").value)
            },

            idSucursal: Number(document.getElementById("sucursal").value),

            idTipoEmpleado: Number(document.getElementById("tipoEmpleado").value),

            usuario: document.getElementById("usuario").value,

            contrasenia: document.getElementById("contrasenia").value
        };

        // Recuperamos el token de autenticación
        const token = sessionStorage.getItem("token");

        // Enviamos los datos a la API
        const respuesta = await fetch(`${API_URL}/Empleados/Nuevo-Empleado`, {

            method: "POST",

            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },

            body: JSON.stringify(empleado)

        });

        // Comprobamos si la API devolvió un error
        if (!respuesta.ok) {

            const mensaje = await respuesta.text();

            throw new Error(mensaje || "No se pudo registrar el empleado");

        }

        // Si llegamos hasta acá, el empleado se registró correctamente
        alert("¡Empleado registrado correctamente!");

        // Limpiamos el formulario
        formNuevoEmpleado.reset();

        // Cerramos el modal
        const modal = document.getElementById("modalNuevoEmpleado");
        const instanciaModal = bootstrap.Modal.getInstance(modal);

        instanciaModal.hide();

        // Actualizamos el listado
        await cargarEmpleados();

    }
    catch (error) {

        console.error("Error al registrar empleado:", error);

        alert("No se pudo registrar el empleado: " + error.message);

    }

});

async function eliminarEmpleado(id) {

    
    const confirmar = confirm(
        "¿Estás seguro de que querés dar de baja a este empleado?"
    );

    
    if (!confirmar) {
        return;
    }

    try {

        const token = sessionStorage.getItem("token");

        // Enviamos la solicitud al backend
        const respuesta = await fetch(
            `${API_URL}/Empleados/Baja-Logica/${id}`,
            {
                method: "PUT",
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }
        );

        if (!respuesta.ok) {
            const mensaje = await respuesta.text();
            throw new Error(mensaje);
        }

        alert("Empleado dado de baja correctamente");

        
        await cargarEmpleados();

    }
    catch (error) {

        console.error("Error al dar de baja:", error);

        alert("No se pudo dar de baja al empleado: " + error.message);

    }
}



document.addEventListener("DOMContentLoaded", function () {

    cargarCiudades();
    cargarSucursales();
    cargarTiposDocumentos();
    cargarTiposEmpleados();

});
