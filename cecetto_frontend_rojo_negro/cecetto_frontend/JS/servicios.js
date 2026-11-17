//NUEVOCAMIO: archivo nuevo. Tabla de servicios con busqueda, igual que la de empleados.

const tablaServicios = document.getElementById("tablaServicios");
const tipoBusquedaServicio = document.getElementById("tipoBusquedaServicio");
const txtBuscarServicio = document.getElementById("txtBuscarServicio");


let listaServicios = [];

// Trae todos los servicios del backend y los muestra
async function cargarServicios() {

    try {

        const respuesta = await fetch(`${API_URL}/ServiciosContratados`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        // El backend, cuando no hay servicios, responde con un texto en vez de una lista.
        // Por eso leemos la respuesta como texto y despues intentamos convertirla.
        const texto = await respuesta.text();

        if (!respuesta.ok) {
            throw new Error(texto || "No se pudieron cargar los servicios");
        }

        let datos;
        try {
            datos = JSON.parse(texto);
        } catch {
            throw new Error(texto || "No se pudieron cargar los servicios");
        }

        if (!Array.isArray(datos)) {
            throw new Error("No se pudieron cargar los servicios");
        }

        listaServicios = datos;
        mostrarServicios(listaServicios);

    }
    catch (error) {

        mostrarMensajeServicios(error.message, "text-danger");
        console.error(error);

    }
}

// Dibuja en la tabla la lista que le pasemos (completa o filtrada)
function mostrarServicios(servicios) {

    tablaServicios.innerHTML = "";

    if (servicios.length === 0) {
        mostrarMensajeServicios("No se encontraron servicios");
        return;
    }

    servicios.forEach(servicio => {

        const fila = document.createElement("tr");

        fila.innerHTML = `
            <td>${servicio.idServicio}</td>
            <td>${formatearFecha(servicio.fecha)}</td>
            <td>${servicio.cliente ?? "-"}</td>
            <td>${servicio.empleado ?? "-"}</td>
            <td>${servicio.sucursal ?? "-"}</td>
            <td>${servicio.tipoServicio ?? "-"}</td>
            <td>${servicio.descripcion ?? "-"}</td>
            <td>${etiquetaEstado(servicio.estado)}</td>
            <!-- NUEVOCAMIO: editar y dar de baja, igual que en empleados -->
            <td class="text-end pe-0">
                <div class="d-inline-flex gap-2">
                    <button class="btn btn-primary btn-sm d-flex align-items-center justify-content-center"
                            title="Editar" aria-label="Editar"
                            style="width: 46px; height: 32px;"
                            onclick="editarServicio(${servicio.idServicio})">
                        <i class="bi bi-pencil-square"></i>
                    </button>

                    <button class="btn btn-danger btn-sm d-flex align-items-center justify-content-center"
                            title="${estaCancelado(servicio) ? "Ya está cancelado" : "Dar de baja"}"
                            aria-label="Dar de baja"
                            style="width: 46px; height: 32px;"
                            ${estaCancelado(servicio) ? "disabled" : ""}
                            onclick="eliminarServicio(${servicio.idServicio})">
                        <i class="bi bi-trash3"></i>
                    </button>
                </div>
            </td>
        `;

        //NUEVOCAMIO: los servicios cancelados se muestran con el texto en rojo
        if (estaCancelado(servicio)) {
            fila.querySelectorAll("td").forEach(celda => celda.classList.add("text-danger"));
        }

        tablaServicios.appendChild(fila);

    });
}

// Muestra un mensaje en la tabla (sin resultados o error)
function mostrarMensajeServicios(texto, clase = "") {
    tablaServicios.innerHTML = `
        <tr>
            <td colspan="9" class="text-center ${clase}">
                ${texto}
            </td>
        </tr>
    `;
}

// El backend manda la fecha como "2026-05-10"; la mostramos como "10/05/2026"
function formatearFecha(fecha) {
    if (!fecha) return "-";
    const [anio, mes, dia] = fecha.split("-");
    return `${dia}/${mes}/${anio}`;
}

// Muestra el estado como una etiqueta de color segun lo que diga
function etiquetaEstado(estado) {

    if (!estado) return "-";

    const e = normalizarServicio(estado);
    let color = "bg-secondary";

    if (e.includes("finaliz") || e.includes("complet") || e.includes("entreg")) {
        color = "bg-success";
    } else if (e.includes("cancel") || e.includes("rechaz")) {
        color = "bg-danger";
    } else if (e.includes("proceso") || e.includes("curso")) {
        color = "bg-primary";
    } else if (e.includes("pendiente") || e.includes("espera")) {
        color = "bg-warning text-dark";
    }

    return `<span class="badge ${color}">${estado}</span>`;
}

// Pasa a minusculas y saca los acentos, asi "cordoba" encuentra a "Córdoba"
function normalizarServicio(texto) {
    return String(texto ?? "")
        .toLowerCase()
        .normalize("NFD")
        .replace(/[̀-ͯ]/g, "")
        .trim();
}

// Filtra la lista guardada segun la opcion elegida en el desplegable
function filtrarServicios() {

    const texto = normalizarServicio(txtBuscarServicio.value);

    // Que campo del servicio comparamos segun la opcion elegida
    const campos = {
        cliente: "cliente",
        empleado: "empleado",
        sucursal: "sucursal",
        estado: "estado",
        tipo: "tipoServicio"
    };

    const campo = campos[tipoBusquedaServicio.value];

    const resultado = listaServicios.filter(s =>
        normalizarServicio(s[campo]).includes(texto));

    mostrarServicios(resultado);
}

// La lupa (o Enter). Con el cuadro vacio trae todos los servicios
document.getElementById("formBuscarServicio").addEventListener("submit", async event => {

    event.preventDefault();

    if (txtBuscarServicio.value.trim() === "") {
        await cargarServicios();
        return;
    }

    if (listaServicios.length === 0) {
        await cargarServicios();   // si todavia no se cargo la lista, la traemos primero
    }

    filtrarServicios();
});

// Cambia el texto de ayuda segun la opcion elegida
tipoBusquedaServicio.addEventListener("change", () => {
    const opcion = tipoBusquedaServicio.options[tipoBusquedaServicio.selectedIndex].text;
    txtBuscarServicio.placeholder = `Ingresá ${opcion.toLowerCase()}`;
    txtBuscarServicio.value = "";
    txtBuscarServicio.focus();
});

// ===================== NUEVO SERVICIO (modal) =====================

// Llena un desplegable con una lista: valor = id, texto = lo que se muestra
function llenarCombo(idCombo, lista, campoValor, campoTexto, textoInicial) {
    const combo = document.getElementById(idCombo);
    combo.innerHTML = `<option value="">${textoInicial}</option>`;

    lista.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item[campoValor];
        opcion.textContent = item[campoTexto];
        combo.appendChild(opcion);
    });
}

// Trae una lista del backend (con el token por si el endpoint lo pide)
async function traerLista(url) {
    const respuesta = await fetch(url, {
        headers: { "Authorization": `Bearer ${token}` }
    });

    if (!respuesta.ok) {
        throw new Error(`No se pudo cargar ${url}`);
    }

    return await respuesta.json();
}

// Carga los 4 desplegables del formulario.
// Cada uno se carga por separado: si falla uno, los demas se llenan igual.
async function cargarCombosServicio() {

    async function cargarUno(idCombo, url, armar) {
        try {
            const lista = await traerLista(url);
            armar(lista);
        } catch (error) {
            // Si falla, lo avisamos dentro del mismo desplegable
            document.getElementById(idCombo).innerHTML =
                '<option value="">No se pudo cargar (¿backend actualizado?)</option>';
            console.error(`Error al cargar ${idCombo}:`, error);
        }
    }

    await Promise.all([
        cargarUno("clienteServicio", `${API_URL}/ServiciosContratados/Clientes`, clientes =>
            llenarCombo("clienteServicio", clientes, "idCliente", "cliente", "Seleccionar cliente")),

        cargarUno("empleadoServicio", `${API_URL}/Empleados/by-EmpleadoALL`, empleados => {
            // Solo empleados activos, con nombre y apellido
            const activos = empleados
                .filter(e => e.activo)
                .map(e => ({ idEmpleado: e.idEmpleado, texto: `${e.nombre} ${e.apellido}` }));
            llenarCombo("empleadoServicio", activos, "idEmpleado", "texto", "Seleccionar empleado");
        }),

        cargarUno("sucursalServicio", `${API_URL}/Empleados/By-Sucursal`, sucursales =>
            llenarCombo("sucursalServicio", sucursales, "idSucursal", "sucursal", "Seleccionar sucursal")),

        cargarUno("tipoServicio", `${API_URL}/ServiciosContratados/TiposServicio`, tipos =>
            llenarCombo("tipoServicio", tipos, "idTipoServicio", "descripcion", "Seleccionar tipo"))
    ]);
}

// Deja el formulario listo: fecha de hoy y el empleado que inició sesión
function prepararFormularioServicio() {
    const hoy = new Date();
    const fechaLocal = new Date(hoy.getTime() - hoy.getTimezoneOffset() * 60000)
        .toISOString().slice(0, 10);
    document.getElementById("fechaServicio").value = fechaLocal;

    try {
        const empleadoSesion = JSON.parse(sessionStorage.getItem("empleado"));
        if (empleadoSesion?.idEmpleado) {
            document.getElementById("empleadoServicio").value = empleadoSesion.idEmpleado;
        }
    } catch { /* si no hay datos de sesión, queda sin elegir */ }
}

//NUEVOCAMIO: el mismo modal sirve para crear y para editar.
// Si es null estamos creando; si tiene un numero, estamos editando ese servicio
let idServicioEditando = null;

// Cuando el modal se abre con el boton +, lo dejamos en modo "nuevo"
// (si se abre desde el lapiz, editarServicio ya lo preparo)
document.getElementById("modalNuevoServicio").addEventListener("show.bs.modal", event => {
    if (!event.relatedTarget) return;

    idServicioEditando = null;
    document.getElementById("formNuevoServicio").reset();
    document.getElementById("tituloNuevoServicio").textContent = "Nuevo servicio";
    document.getElementById("btnGuardarServicio").textContent = "Guardar servicio";
    prepararFormularioServicio();
});

// Guardar el nuevo servicio
document.getElementById("formNuevoServicio").addEventListener("submit", async event => {

    event.preventDefault();

    const servicio = {
        fechaServicio: document.getElementById("fechaServicio").value,
        idCliente: Number(document.getElementById("clienteServicio").value),
        idEmpleado: Number(document.getElementById("empleadoServicio").value),
        idSucursal: Number(document.getElementById("sucursalServicio").value),
        descripcion: document.getElementById("descripcionServicio").value.trim(),
        idTipoServicio: Number(document.getElementById("tipoServicio").value),
        detalles: []
    };

    //NUEVOCAMIO: si estamos editando, mandamos PUT al servicio; si no, POST para crear
    const editando = idServicioEditando !== null;
    if (editando) {
        delete servicio.detalles;
    }

    try {
        const respuesta = await fetch(
            editando ? `${API_URL}/ServiciosContratados/${idServicioEditando}` : `${API_URL}/ServiciosContratados`, {
            method: editando ? "PUT" : "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(servicio)
        });

        if (!respuesta.ok) {
            // El backend puede responder { mensaje } o la lista de errores de validación
            const texto = await respuesta.text();
            let mensaje = texto;
            try {
                const datos = JSON.parse(texto);
                mensaje = datos.mensaje
                    ?? (datos.errors ? Object.values(datos.errors).flat().join("\n") : texto);
            } catch { }
            throw new Error(mensaje || "No se pudo guardar el servicio");
        }

        const datos = await respuesta.json();

        //NUEVOCAMIO: armamos el aviso antes de limpiar el numero del servicio editado
        const tituloAviso = editando ? "Servicio modificado" : "Servicio registrado";
        const textoAviso = editando
            ? `Los cambios del servicio N° ${idServicioEditando} se guardaron correctamente.`
            : `El servicio se registró correctamente con el N° ${datos.idServicio}.`;

        idServicioEditando = null;

        document.getElementById("formNuevoServicio").reset();
        bootstrap.Modal.getInstance(document.getElementById("modalNuevoServicio")).hide();

        // Actualizamos el listado para que aparezca el nuevo
        txtBuscarServicio.value = "";
        await cargarServicios();

        //NUEVOCAMIO: aviso prolijo en vez del alert del navegador
        mostrarAviso("ok", tituloAviso, textoAviso);
    }
    catch (error) {
        console.error("Error al registrar el servicio:", error);
        //NUEVOCAMIO: aviso prolijo en vez del alert del navegador
        mostrarAviso("error", "No se pudo guardar el servicio", error.message);
    }
});

// ===================== NUEVOCAMIO: EDITAR SERVICIO =====================

// Se llama desde el lapiz de cada fila: trae el servicio y abre el modal con sus datos
async function editarServicio(id) {

    try {
        const respuesta = await fetch(`${API_URL}/ServiciosContratados/${id}`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!respuesta.ok) {
            throw new Error(await respuesta.text() || "No se pudo traer el servicio");
        }

        const servicio = await respuesta.json();

        idServicioEditando = id;

        document.getElementById("tituloNuevoServicio").textContent = `Editar servicio N° ${id}`;
        document.getElementById("btnGuardarServicio").textContent = "Guardar cambios";

        document.getElementById("fechaServicio").value = servicio.fechaServicio ?? "";
        document.getElementById("tipoServicio").value = servicio.idTipoServicio ?? "";
        document.getElementById("descripcionServicio").value = servicio.descripcion ?? "";
        document.getElementById("clienteServicio").value = servicio.idCliente ?? "";
        document.getElementById("empleadoServicio").value = servicio.idEmpleado ?? "";
        document.getElementById("sucursalServicio").value = servicio.idSucursal ?? "";

        bootstrap.Modal.getOrCreateInstance(document.getElementById("modalNuevoServicio")).show();
    }
    catch (error) {
        console.error("Error al abrir la edicion:", error);
        //NUEVOCAMIO: aviso prolijo en vez del alert del navegador
        mostrarAviso("error", "No se pudo abrir el servicio", error.message);
    }
}

// ===================== NUEVOCAMIO: DAR DE BAJA SERVICIO =====================

// Id del estado "cancelado" en la tabla Estados_Servicio (ver InsertsCechetto.sql)
const ESTADO_CANCELADO = 4;

function estaCancelado(servicio) {
    return normalizarServicio(servicio.estado).includes("cancel");
}

// Cambia lo que muestra la ventana de baja: "confirmar", "cargando", "ok" o "error"
function estadoVentanaBajaServicio(estado, mensaje = "") {

    const icono = document.getElementById("iconoBajaServicio");
    const titulo = document.getElementById("tituloBajaServicio");
    const texto = document.getElementById("textoBajaServicio");
    const btnCancelar = document.getElementById("btnCancelarBajaServicio");
    const btnConfirmar = document.getElementById("btnConfirmarBajaServicio");
    const btnAceptar = document.getElementById("btnAceptarBajaServicio");

    const terminado = estado === "ok" || estado === "error";
    const color = estado === "ok" ? "success" : "danger";
    const iconos = { confirmar: "bi-trash3", cargando: "bi-trash3", ok: "bi-check-lg", error: "bi-x-lg" };

    icono.className = `d-inline-flex align-items-center justify-content-center rounded-circle bg-${color} bg-opacity-10 text-${color} mb-3`;
    icono.innerHTML = `<i class="bi ${iconos[estado]} fs-3"></i>`;

    btnCancelar.classList.toggle("d-none", terminado);
    btnConfirmar.classList.toggle("d-none", terminado);
    btnAceptar.className = `btn btn-${estado === "ok" ? "success" : "secondary"} px-4` + (terminado ? "" : " d-none");

    btnCancelar.disabled = estado === "cargando";
    btnConfirmar.disabled = estado === "cargando";
    btnConfirmar.innerHTML = estado === "cargando"
        ? '<span class="spinner-border spinner-border-sm me-2"></span>Dando de baja...'
        : "Sí, dar de baja";

    if (estado === "ok") {
        titulo.textContent = "Servicio dado de baja";
        texto.textContent = "El servicio quedó en estado cancelado.";
    } else if (estado === "error") {
        titulo.textContent = "No se pudo dar de baja";
        texto.textContent = mensaje;
    } else {
        titulo.textContent = "¿Dar de baja este servicio?";
        texto.textContent = "El servicio va a pasar a estado cancelado.";
    }
}

// Se llama desde el tacho de cada fila
function eliminarServicio(id) {

    const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("modalBajaServicio"));

    // Mostramos de que servicio se trata
    const servicio = listaServicios.find(s => s.idServicio === id);
    document.getElementById("datosServicioBaja").textContent = servicio
        ? `N° ${servicio.idServicio} · ${servicio.cliente ?? ""} · ${formatearFecha(servicio.fecha)}`
        : `N° ${id}`;

    estadoVentanaBajaServicio("confirmar");

    document.getElementById("btnConfirmarBajaServicio").onclick = async () => {

        estadoVentanaBajaServicio("cargando");

        try {
            const respuesta = await fetch(`${API_URL}/ServiciosContratados/${id}/estado`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({ nuevoEstado: ESTADO_CANCELADO })
            });

            if (!respuesta.ok) {
                const texto = await respuesta.text();
                let mensaje = texto;
                try { mensaje = JSON.parse(texto).mensaje ?? texto; } catch { }
                throw new Error(mensaje || "Ocurrió un error inesperado.");
            }

            estadoVentanaBajaServicio("ok");

            // Actualizamos la tabla de fondo
            await cargarServicios();
        }
        catch (error) {
            console.error("Error al dar de baja el servicio:", error);
            estadoVentanaBajaServicio("error", error.message);
        }
    };

    modal.show();
}

// Al abrir la pagina mostramos todos los servicios y cargamos los desplegables
document.addEventListener("DOMContentLoaded", () => {
    cargarServicios();
    cargarCombosServicio();
});
