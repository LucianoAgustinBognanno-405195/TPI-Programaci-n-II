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
