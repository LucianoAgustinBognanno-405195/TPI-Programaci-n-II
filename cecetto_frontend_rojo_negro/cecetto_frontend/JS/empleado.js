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


// AL CARGAR LA PÁGINA
document.addEventListener("DOMContentLoaded", function () {

    cargarCiudades();
    cargarSucursales();
    cargarTiposDocumentos();
    cargarTiposEmpleados();

});
