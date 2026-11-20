const tablaEmpleados = document.getElementById("tablaEmpleados");
const tipoBusqueda = document.getElementById("tipoBusqueda");
const txtBuscar = document.getElementById("txtBuscar");

let listaEmpleados = [];

async function cargarEmpleados() {

    try {

        const respuesta = await fetch(
            "https://localhost:7079/api/Empleados/by-EmpleadoNav",
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

        listaEmpleados = await respuesta.json();
        mostrarEmpleados(listaEmpleados);

    }
    catch (error) {

        mostrarMensaje(error.message, "text-danger");

        console.error(error);

    }
}

function mostrarEmpleados(empleados) {

    // Limpiamos el contenido anterior de la tabla
    tablaEmpleados.innerHTML = "";

    if (empleados.length === 0) {
        mostrarMensaje("No se encontraron empleados");
        return;
    }

    // Recorremos la lista de empleados
    empleados.forEach(empleado => {

        const fila = document.createElement("tr");

        fila.innerHTML = `
            <td>${empleado.idEmpleado}</td>
            <td>${empleado.nombre}</td>
            <td>${empleado.apellido}</td>
            <td>${empleado.usuario}</td>
            <td>${empleado.activo ? "Activo" : "Inactivo"}</td>
            <td>${empleado.documento?.documento ?? "-"}</td>
            <td class="text-end pe-0">
                <div class="d-inline-flex gap-2">
                    <button class="btn btn-primary btn-sm d-flex align-items-center justify-content-center"
                            title="Editar"
                            aria-label="Editar"
                            style="width: 46px; height: 32px;"
                            onclick="editarEmpleado(${empleado.idEmpleado})">
                        <i class="bi bi-pencil-square"></i>
                    </button>

                    ${empleado.activo ? `
                    <button class="btn btn-danger btn-sm d-flex align-items-center justify-content-center"
                            title="Dar de baja"
                            aria-label="Dar de baja"
                            style="width: 46px; height: 32px;"
                            onclick="eliminarEmpleado(${empleado.idEmpleado})">
                        <i class="bi bi-trash3"></i>
                    </button>` : `
                    <button class="btn btn-success btn-sm d-flex align-items-center justify-content-center"
                            title="Dar de alta"
                            aria-label="Dar de alta"
                            style="width: 46px; height: 32px;"
                            onclick="altaEmpleado(${empleado.idEmpleado})">
                        <i class="bi bi-person-check"></i>
                    </button>`}
                </div>
            </td>
        `;

        if (!empleado.activo) {
            fila.querySelectorAll("td").forEach(celda => celda.classList.add("text-danger"));
        }

        tablaEmpleados.appendChild(fila);

    });
}

function mostrarMensaje(texto, clase = "") {
    tablaEmpleados.innerHTML = `
        <tr>
            <td colspan="7" class="text-center ${clase}">
                ${texto}
            </td>
        </tr>
    `;
}

function normalizar(texto) {
    return String(texto ?? "")
        .toLowerCase()
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .trim();
}

function filtrarEmpleados() {

    const texto = normalizar(txtBuscar.value);
    let resultado;

    if (tipoBusqueda.value === "documento") {
        resultado = listaEmpleados.filter(e =>
            String(e.documento?.documento ?? "").startsWith(texto));
    } else {
        resultado = listaEmpleados.filter(e => {
            const nombreApellido = normalizar(`${e.nombre} ${e.apellido}`);
            const apellidoNombre = normalizar(`${e.apellido} ${e.nombre}`);
            return nombreApellido.startsWith(texto) || apellidoNombre.startsWith(texto);
        });
    }

    mostrarEmpleados(resultado);
}

document.getElementById("formBuscar").addEventListener("submit", async event => {

    event.preventDefault();

    if (txtBuscar.value.trim() === "") {
        await cargarEmpleados();
        return;
    }

    if (listaEmpleados.length === 0) {
        await cargarEmpleados();
    }

    filtrarEmpleados();
});

tipoBusqueda.addEventListener("change", () => {
    txtBuscar.placeholder = tipoBusqueda.value === "documento"
        ? "Ingresá el documento"
        : "Ingresá nombre y/o apellido";
    txtBuscar.value = "";
    txtBuscar.focus();
});

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

        // Limpiamos el formulario
        formNuevoEmpleado.reset();

        // Cerramos el modal
        const modal = document.getElementById("modalNuevoEmpleado");
        const instanciaModal = bootstrap.Modal.getInstance(modal);

        instanciaModal.hide();

        // Actualizamos el listado
        await cargarEmpleados();

        mostrarAviso("ok", "Empleado registrado", "El empleado se registró correctamente.");

    }
    catch (error) {

        console.error("Error al registrar empleado:", error);

        mostrarAviso("error", "No se pudo registrar el empleado", error.message);

    }

});

async function cargarBarriosEdicion(ciudad, idBarrioElegido = "") {

    const combo = document.getElementById("editBarrio");
    combo.innerHTML = '<option value="">Seleccionar barrio</option>';
    combo.disabled = true;

    if (!ciudad) return;

    const respuesta = await fetch(
        `${API_URL}/Empleados/By-Barrios?Ciudad=${encodeURIComponent(ciudad)}`
    );

    if (!respuesta.ok) {
        throw new Error("No se pudieron cargar los barrios");
    }

    const barrios = await respuesta.json();

    barrios.forEach(barrio => {
        const opcion = document.createElement("option");
        opcion.value = barrio.idBarrio;
        opcion.textContent = barrio.barrio;
        combo.appendChild(opcion);
    });

    combo.disabled = false;
    combo.value = idBarrioElegido;
}

async function editarEmpleado(id) {

    try {

        const respuesta = await fetch(`${API_URL}/Empleados/By-EmpleadoById${id}`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!respuesta.ok) {
            throw new Error(await respuesta.text() || "No se pudo traer el empleado");
        }

        const empleado = await respuesta.json();

        document.getElementById("editTipoEmpleado").innerHTML =
            document.getElementById("tipoEmpleado").innerHTML;
        document.getElementById("editCiudad").innerHTML =
            document.getElementById("ciudad").innerHTML;

        document.getElementById("editIdEmpleado").value = empleado.idEmpleado;
        document.getElementById("nombreEmpleadoEditar").textContent =
            `${empleado.nombre} ${empleado.apellido}` +
            (empleado.documento?.documento ? ` · DNI ${empleado.documento.documento}` : "");

        document.getElementById("editTipoEmpleado").value = empleado.idTipoEmpleado ?? "";
        document.getElementById("editCiudad").value = empleado.ciudad ?? "";
        document.getElementById("editCalle").value = empleado.direccion?.calle ?? "";
        document.getElementById("editAltura").value = empleado.direccion?.altura ?? "";

        await cargarBarriosEdicion(empleado.ciudad, empleado.direccion?.idBarrio ?? "");

        bootstrap.Modal.getOrCreateInstance(document.getElementById("modalEditarEmpleado")).show();

    }
    catch (error) {
        console.error("Error al abrir la edicion:", error);
        mostrarAviso("error", "No se pudo abrir el empleado", error.message);
    }
}

document.getElementById("editCiudad").addEventListener("change", function () {
    cargarBarriosEdicion(this.value).catch(error => console.error(error));
});

document.getElementById("formEditarEmpleado").addEventListener("submit", async function (event) {

    event.preventDefault();

    const id = document.getElementById("editIdEmpleado").value;

    const cambios = {
        direccion: {
            calle: document.getElementById("editCalle").value.trim(),
            altura: Number(document.getElementById("editAltura").value),
            idBarrio: Number(document.getElementById("editBarrio").value)
        },
        idTipoEmpleado: Number(document.getElementById("editTipoEmpleado").value)
    };

    try {

        const respuesta = await fetch(`${API_URL}/Empleados/Direccion-TipoEmpleado/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(cambios)
        });

        if (!respuesta.ok) {
            throw new Error(await respuesta.text() || "No se pudo modificar el empleado");
        }

        bootstrap.Modal.getInstance(document.getElementById("modalEditarEmpleado")).hide();

        await cargarEmpleados();

        mostrarAviso("ok", "Empleado modificado", "Los cambios se guardaron correctamente.");

    }
    catch (error) {
        console.error("Error al editar:", error);
        mostrarAviso("error", "No se pudo modificar el empleado", error.message);
    }
});

const MODOS_VENTANA_EMPLEADO = {
    baja: {
        color: "danger",
        icono: "bi-trash3",
        boton: "Sí, dar de baja",
        cargando: "Dando de baja...",
        pregunta: "¿Dar de baja a este empleado?",
        aclaracion: "El empleado va a quedar inactivo y no va a poder ingresar al sistema.",
        tituloOk: "Empleado dado de baja",
        textoOk: "El empleado quedó inactivo.",
        tituloError: "No se pudo dar de baja",
        endpoint: "Baja-Logica"
    },
    alta: {
        color: "success",
        icono: "bi-person-check",
        boton: "Sí, dar de alta",
        cargando: "Dando de alta...",
        pregunta: "¿Dar de alta a este empleado?",
        aclaracion: "El empleado va a volver a estar activo y va a poder ingresar al sistema.",
        tituloOk: "Empleado dado de alta",
        textoOk: "El empleado volvió a estar activo.",
        tituloError: "No se pudo dar de alta",
        endpoint: "Alta-Logica"
    }
};

let modoVentanaEmpleado = "baja";

function estadoVentanaBaja(estado, mensaje = "") {

    const modo = MODOS_VENTANA_EMPLEADO[modoVentanaEmpleado];

    const icono = document.getElementById("iconoBaja");
    const titulo = document.getElementById("tituloConfirmarBaja");
    const texto = document.getElementById("textoBaja");
    const btnCancelar = document.getElementById("btnCancelarBaja");
    const btnConfirmar = document.getElementById("btnConfirmarBaja");
    const btnAceptar = document.getElementById("btnAceptarBaja");

    const terminado = estado === "ok" || estado === "error";

    const color = estado === "ok" ? "success" : estado === "error" ? "danger" : modo.color;
    const iconoClase = estado === "ok" ? "bi-check-lg" : estado === "error" ? "bi-x-lg" : modo.icono;

    icono.className = `d-inline-flex align-items-center justify-content-center rounded-circle bg-${color} bg-opacity-10 text-${color} mb-3`;
    icono.innerHTML = `<i class="bi ${iconoClase} fs-3"></i>`;

    btnCancelar.classList.toggle("d-none", terminado);
    btnConfirmar.className = `btn btn-${modo.color} px-4` + (terminado ? " d-none" : "");
    btnAceptar.className = `btn btn-${estado === "ok" ? "success" : "secondary"} px-4` + (terminado ? "" : " d-none");

    btnCancelar.disabled = estado === "cargando";
    btnConfirmar.disabled = estado === "cargando";
    btnConfirmar.innerHTML = estado === "cargando"
        ? `<span class="spinner-border spinner-border-sm me-2"></span>${modo.cargando}`
        : modo.boton;

    if (estado === "ok") {
        titulo.textContent = modo.tituloOk;
        texto.textContent = modo.textoOk;
    } else if (estado === "error") {
        titulo.textContent = modo.tituloError;
        texto.textContent = mensaje;
    } else {
        titulo.textContent = modo.pregunta;
        texto.textContent = modo.aclaracion;
    }
}

function abrirVentanaEstadoEmpleado(id, modoElegido) {

    modoVentanaEmpleado = modoElegido;
    const modo = MODOS_VENTANA_EMPLEADO[modoElegido];

    const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("modalConfirmarBaja"));

    const empleado = listaEmpleados.find(e => e.idEmpleado === id);
    document.getElementById("nombreEmpleadoBaja").textContent = empleado
        ? `${empleado.nombre} ${empleado.apellido}` +
          (empleado.documento?.documento ? ` · DNI ${empleado.documento.documento}` : "")
        : "";

    estadoVentanaBaja("confirmar");

    document.getElementById("btnConfirmarBaja").onclick = async () => {

        estadoVentanaBaja("cargando");

        try {
            const token = sessionStorage.getItem("token");

            const respuesta = await fetch(`${API_URL}/Empleados/${modo.endpoint}/${id}`, {
                method: "PUT",
                headers: { "Authorization": `Bearer ${token}` }
            });

            if (!respuesta.ok) {
                if (respuesta.status === 401 || respuesta.status === 403) {
                    throw new Error("No tenés permiso para hacer esta operación.");
                }
                throw new Error(await respuesta.text() || "Ocurrió un error inesperado.");
            }

            estadoVentanaBaja("ok");

            await cargarEmpleados();
        }
        catch (error) {
            console.error(`Error al dar de ${modoElegido}:`, error);
            estadoVentanaBaja("error", error.message);
        }
    };

    modal.show();
}

function eliminarEmpleado(id) {
    abrirVentanaEstadoEmpleado(id, "baja");
}

function altaEmpleado(id) {
    abrirVentanaEstadoEmpleado(id, "alta");
}

document.addEventListener("DOMContentLoaded", function () {

    cargarEmpleados();

    cargarCiudades();
    cargarSucursales();
    cargarTiposDocumentos();
    cargarTiposEmpleados();

});
