
const tablaServicios = document.getElementById("tablaServicios");
const tipoBusquedaServicio = document.getElementById("tipoBusquedaServicio");
const txtBuscarServicio = document.getElementById("txtBuscarServicio");

let listaServicios = [];
let listaClientes = [];
let listaBaterias = [];
let listaVehiculos = [];
let listaSucursales = [];

const listaEstados = [
    { idEstado: 1, estado: "pendiente" },
    { idEstado: 2, estado: "en proceso" },
    { idEstado: 3, estado: "finalizado" },
    { idEstado: 4, estado: "cancelado" }
];

function idEstadoDe(servicio) {
    return listaEstados.find(e => normalizarServicio(e.estado) === normalizarServicio(servicio?.estado))?.idEstado;
}

function normalizarServicio(texto) {
    return String(texto ?? "")
        .toLowerCase()
        .normalize("NFD")
        .replace(/[̀-ͯ]/g, "")
        .trim();
}

function empiezaCon(valor, buscado) {
    const q = normalizarServicio(buscado);
    if (q === "") return true;

    return normalizarServicio(valor).startsWith(q);
}

function dniEmpiezaCon(documento, buscado) {
    const q = String(buscado ?? "").replace(/\D/g, "");
    return q !== "" && String(documento ?? "").startsWith(q);
}

function formatearFecha(fecha) {
    if (!fecha) return "-";
    const [anio, mes, dia] = fecha.split("-");
    return `${dia}/${mes}/${anio}`;
}

async function leerError(respuesta, porDefecto) {
    if (respuesta.status === 401 || respuesta.status === 403) {
        return "No tenés permiso para hacer esta operación.";
    }
    const texto = await respuesta.text();
    try {
        const datos = JSON.parse(texto);
        return datos.mensaje
            ?? (datos.errors ? Object.values(datos.errors).flat().join("\n") : null)
            ?? texto ?? porDefecto;
    } catch {
        return texto || porDefecto;
    }
}

async function traerLista(url) {
    const respuesta = await fetch(url, {
        headers: { "Authorization": `Bearer ${token}` }
    });

    if (!respuesta.ok) {
        throw new Error(`No se pudo cargar ${url}`);
    }

    return await respuesta.json();
}

function llenarCombo(combo, lista, campoValor, campoTexto, textoInicial) {
    combo.innerHTML = `<option value="">${textoInicial}</option>`;

    lista.forEach(item => {
        const opcion = document.createElement("option");
        opcion.value = item[campoValor];
        opcion.textContent = item[campoTexto];
        combo.appendChild(opcion);
    });
}

async function cargarServicios() {

    try {

        const respuesta = await fetch(`${API_URL}/ServiciosContratados`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

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

function botonAccion(color, icono, titulo, funcion, deshabilitado = false) {
    return `
        <button class="btn btn-${color} btn-sm d-flex align-items-center justify-content-center"
                title="${titulo}" aria-label="${titulo}"
                style="width: 46px; height: 32px;"
                ${deshabilitado ? "disabled" : ""}
                onclick="${funcion}">
            <i class="bi ${icono}"></i>
        </button>`;
}

function mostrarServicios(servicios) {

    tablaServicios.innerHTML = "";

    if (servicios.length === 0) {
        mostrarMensajeServicios("No se encontraron servicios");
        return;
    }

    servicios.forEach(servicio => {

        const fila = document.createElement("tr");
        const cancelado = estaCancelado(servicio);
        const cerrado = estaCerrado(servicio);
        const id = servicio.idServicio;
        const motivoCerrado = cancelado ? "El servicio está cancelado" : "El servicio está finalizado";

        fila.innerHTML = `
            <td>${id}</td>
            <td>${formatearFecha(servicio.fecha)}</td>
            <td>${servicio.cliente ?? "-"}</td>
            <td>${servicio.empleado ?? "-"}</td>
            <td>${servicio.sucursal ?? "-"}</td>
            <td>${servicio.tipoServicio ?? "-"}</td>
            <td>${servicio.descripcion ?? "-"}</td>
            <td>${etiquetaEstado(servicio.estado)}</td>
            <td class="text-end pe-0">
                <div class="d-inline-flex gap-2">
                    ${botonAccion("secondary", "bi-list-ul", "Ver detalles", `verDetallesServicio(${id})`)}
                    ${botonAccion("warning", "bi-arrow-repeat", cerrado ? motivoCerrado : "Cambiar estado", `abrirCambioEstado(${id})`, cerrado)}
                    ${botonAccion("primary", "bi-pencil-square", cerrado ? motivoCerrado : "Editar", `editarServicio(${id})`, cerrado)}
                    ${botonAccion("danger", "bi-trash3", cerrado ? motivoCerrado : "Dar de baja", `eliminarServicio(${id})`, cerrado)}
                </div>
            </td>
        `;

        if (cancelado) {
            fila.querySelectorAll("td").forEach(celda => celda.classList.add("text-danger"));
        }

        tablaServicios.appendChild(fila);
    });
}

function mostrarMensajeServicios(texto, clase = "") {
    tablaServicios.innerHTML = `
        <tr>
            <td colspan="9" class="text-center ${clase}">
                ${texto}
            </td>
        </tr>
    `;
}

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

function estaCancelado(servicio) {
    return normalizarServicio(servicio?.estado).includes("cancel");
}

function estaCerrado(servicio) {
    const e = normalizarServicio(servicio?.estado);
    return e.includes("cancel") || e.includes("finaliz");
}

const valorBusquedaServicio = document.getElementById("valorBusquedaServicio");
const fechasBusquedaServicio = document.getElementById("fechasBusquedaServicio");
const fechaDesdeServicio = document.getElementById("fechaDesdeServicio");
const fechaHastaServicio = document.getElementById("fechaHastaServicio");

async function buscarEnBackend(url) {

    try {
        const respuesta = await fetch(url, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!respuesta.ok) {
            throw new Error(await leerError(respuesta, "No se pudo hacer la búsqueda"));
        }

        const encontrados = await respuesta.json();
        mostrarServicios(Array.isArray(encontrados) ? encontrados : []);
    }
    catch (error) {
        console.error("Error en la búsqueda:", error);
        mostrarMensajeServicios(error.message, "text-danger");
    }
}

function filtrarEnFront(campo, texto) {
    mostrarServicios(listaServicios.filter(s => empiezaCon(s[campo], texto)));
}

document.getElementById("formBuscarServicio").addEventListener("submit", async event => {

    event.preventDefault();

    const tipo = tipoBusquedaServicio.value;
    const texto = txtBuscarServicio.value.trim();
    const soloNumeros = texto.replace(/\D/g, "");

    if (tipo === "clienteDni" && soloNumeros) {
        return buscarEnBackend(`${API_URL}/ServiciosContratados/ByDocCliente/${soloNumeros}`);
    }
    if (tipo === "empleadoDni" && soloNumeros) {
        return buscarEnBackend(`${API_URL}/ServiciosContratados/ByDocEmpleado/${soloNumeros}`);
    }
    if (tipo === "sucursal" && valorBusquedaServicio.value) {
        return buscarEnBackend(`${API_URL}/ServiciosContratados/BySucursal/${encodeURIComponent(valorBusquedaServicio.value)}`);
    }
    if (tipo === "estado" && valorBusquedaServicio.value) {
        return buscarEnBackend(`${API_URL}/ServiciosContratados/ByEstado/${encodeURIComponent(valorBusquedaServicio.value)}`);
    }
    if (tipo === "fecha" && fechaDesdeServicio.value) {
        const hasta = fechaHastaServicio.value ? `?fechaFin=${fechaHastaServicio.value}` : "";
        return buscarEnBackend(`${API_URL}/ServiciosContratados/ByFecha/${fechaDesdeServicio.value}${hasta}`);
    }

    const camposFront = { clienteNombre: "cliente", empleadoNombre: "empleado", tipo: "tipoServicio" };
    if (camposFront[tipo] && texto) {
        if (listaServicios.length === 0) await cargarServicios();
        return filtrarEnFront(camposFront[tipo], texto);
    }

    await cargarServicios();
});

function prepararCampoBusqueda() {

    const tipo = tipoBusquedaServicio.value;
    const usaLista = tipo === "sucursal" || tipo === "estado";
    const usaFechas = tipo === "fecha";

    txtBuscarServicio.classList.toggle("d-none", usaLista || usaFechas);
    valorBusquedaServicio.classList.toggle("d-none", !usaLista);
    fechasBusquedaServicio.classList.toggle("d-none", !usaFechas);
    fechasBusquedaServicio.classList.toggle("d-flex", usaFechas);

    const ayudas = {
        clienteDni: "Ingresá el DNI del cliente",
        clienteNombre: "Ingresá nombre o apellido del cliente",
        empleadoDni: "Ingresá el DNI del empleado",
        empleadoNombre: "Ingresá nombre o apellido del empleado",
        tipo: "Ingresá el tipo de servicio"
    };
    txtBuscarServicio.placeholder = ayudas[tipo] ?? "";
    txtBuscarServicio.value = "";

    if (tipo === "sucursal") {
        llenarCombo(valorBusquedaServicio, listaSucursales.map(s => ({ valor: s.sucursal })),
            "valor", "valor", "Elegí una sucursal");
    }
    if (tipo === "estado") {
        llenarCombo(valorBusquedaServicio,
            listaEstados.map(e => ({ valor: e.estado, texto: e.estado.charAt(0).toUpperCase() + e.estado.slice(1) })),
            "valor", "texto", "Elegí un estado");
    }

    if (!usaLista && !usaFechas) txtBuscarServicio.focus();
}

tipoBusquedaServicio.addEventListener("change", prepararCampoBusqueda);

const comboCliente = document.getElementById("clienteServicio");
const buscarCliente = document.getElementById("buscarClienteServicio");

function mostrarClientes(filtro = "", idSeleccionado = comboCliente.value) {

    const coinciden = listaClientes.filter(c =>
        filtro.trim() === "" || empiezaCon(c.cliente, filtro) || dniEmpiezaCon(c.documento, filtro));

    comboCliente.innerHTML = `<option value="">${
        coinciden.length === 0 ? "No hay clientes que coincidan" : "Seleccionar cliente"}</option>`;

    coinciden.forEach(c => {
        const opcion = document.createElement("option");
        opcion.value = c.idCliente;
        opcion.textContent = c.documento ? `${c.cliente} · DNI ${c.documento}` : c.cliente;
        comboCliente.appendChild(opcion);
    });

    if (coinciden.some(c => String(c.idCliente) === String(idSeleccionado))) {
        comboCliente.value = idSeleccionado;
    } else if (coinciden.length === 1) {
        comboCliente.value = coinciden[0].idCliente;
    }
}

buscarCliente.addEventListener("input", () => mostrarClientes(buscarCliente.value));

async function cargarCombosServicio() {

    async function cargarUno(nombre, url, guardar) {
        try {
            guardar(await traerLista(url));
        } catch (error) {
            console.error(`Error al cargar ${nombre}:`, error);
        }
    }

    await Promise.all([
        cargarUno("clientes", `${API_URL}/ServiciosContratados/Clientes`, lista => {
            listaClientes = lista;
            mostrarClientes();
        }),

        cargarUno("empleados", `${API_URL}/Empleados/GetAll`, empleados => {
            const activos = empleados
                .filter(e => e.activo)
                .map(e => ({ idEmpleado: e.idEmpleado, texto: `${e.nombre} ${e.apellido}` }));
            llenarCombo(document.getElementById("empleadoServicio"), activos, "idEmpleado", "texto", "Seleccionar empleado");
        }),

        cargarUno("sucursales", `${API_URL}/Empleados/By-Sucursal`, sucursales => {
            listaSucursales = sucursales;
            llenarCombo(document.getElementById("sucursalServicio"), sucursales, "idSucursal", "sucursal", "Seleccionar sucursal");
        }),

        cargarUno("tipos de servicio", `${API_URL}/ServiciosContratados/TiposServicio`, tipos =>
            llenarCombo(document.getElementById("tipoServicio"), tipos, "idTipoServicio", "descripcion", "Seleccionar tipo")),

        cargarUno("baterias", `${API_URL}/ServiciosContratados/Baterias`, lista => listaBaterias = lista),
        cargarUno("vehiculos", `${API_URL}/ServiciosContratados/Vehiculos`, lista => listaVehiculos = lista)
    ]);
}

const listaDetallesNuevo = document.getElementById("listaDetallesNuevo");

function actualizarTextoSinDetalles() {
    document.getElementById("sinDetallesNuevo").classList.toggle(
        "d-none", listaDetallesNuevo.children.length > 0);
}

function agregarFilaDetalle() {

    if (listaBaterias.length === 0 || listaVehiculos.length === 0) {
        mostrarAviso("error", "No se pueden agregar detalles",
            "No se pudieron cargar las baterías o los vehículos. ¿El backend está actualizado?");
        return;
    }

    const fila = document.createElement("div");
    fila.className = "row g-2 align-items-center mb-2 fila-detalle";
    fila.innerHTML = `
        <div class="col-md-6">
            <select class="form-select detalle-bateria" required></select>
        </div>
        <div class="col">
            <select class="form-select detalle-vehiculo" required></select>
        </div>
        <div class="col-auto">
            <button type="button" class="btn btn-outline-danger d-flex align-items-center justify-content-center"
                    title="Quitar detalle" aria-label="Quitar detalle" style="height: 38px; width: 46px;">
                <i class="bi bi-x-lg"></i>
            </button>
        </div>
    `;

    llenarCombo(fila.querySelector(".detalle-bateria"), listaBaterias, "idBateria", "descripcion", "Seleccionar batería");
    llenarCombo(fila.querySelector(".detalle-vehiculo"), listaVehiculos, "idVehiculo", "descripcion", "Seleccionar vehículo");

    fila.querySelector("button").addEventListener("click", () => {
        fila.remove();
        actualizarTextoSinDetalles();
    });

    listaDetallesNuevo.appendChild(fila);
    actualizarTextoSinDetalles();
}

document.getElementById("btnAgregarDetalle").addEventListener("click", agregarFilaDetalle);

function leerDetallesNuevo() {
    return [...listaDetallesNuevo.querySelectorAll(".fila-detalle")].map(fila => ({
        idBateria: Number(fila.querySelector(".detalle-bateria").value),
        idVehiculo: Number(fila.querySelector(".detalle-vehiculo").value)
    }));
}

function elegirPorTexto(combo, texto) {
    const buscado = normalizarServicio(texto);
    const opcion = [...combo.options].find(o => o.value !== "" && normalizarServicio(o.textContent) === buscado);
    combo.value = opcion ? opcion.value : "";
}

let idServicioEditando = null;

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

document.getElementById("modalNuevoServicio").addEventListener("show.bs.modal", event => {
    if (!event.relatedTarget) return;

    idServicioEditando = null;
    document.getElementById("formNuevoServicio").reset();
    document.getElementById("tituloNuevoServicio").textContent = "Nuevo servicio";
    document.getElementById("btnGuardarServicio").textContent = "Guardar servicio";

    buscarCliente.value = "";
    mostrarClientes("", "");

    document.getElementById("bloqueDetallesActuales").classList.add("d-none");
    document.getElementById("tituloDetallesNuevo").textContent = "Detalles (batería y vehículo)";
    listaDetallesNuevo.innerHTML = "";
    actualizarTextoSinDetalles();

    prepararFormularioServicio();
});

async function cargarDetallesActuales(id) {

    const lista = document.getElementById("listaDetallesActuales");
    lista.innerHTML = `<li class="list-group-item px-0 text-secondary">Cargando...</li>`;

    try {
        const respuesta = await fetch(`${API_URL}/ServiciosContratados/ByIdWithDetalles/${id}`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!respuesta.ok) {
            throw new Error(await leerError(respuesta, "No se pudieron cargar los detalles"));
        }

        let detalles = await respuesta.json();
        if (!Array.isArray(detalles)) detalles = [];

        lista.innerHTML = detalles.length === 0
            ? `<li class="list-group-item px-0 text-secondary">Este servicio todavía no tiene detalles.</li>`
            : detalles.map(det => `
                <li class="list-group-item px-0">
                    <i class="bi bi-battery-half text-secondary me-1"></i>
                    ${det.tipoBateria ?? "-"} ${det.voltajeBateria ?? ""}
                    <span class="text-secondary mx-1">·</span>
                    <i class="bi bi-car-front text-secondary me-1"></i>
                    ${det.descVehiculo ?? "-"}${det.tipoVehiculo ? ` (${det.tipoVehiculo})` : ""}
                </li>`).join("");
    }
    catch (error) {
        console.error("Error al cargar los detalles actuales:", error);
        lista.innerHTML = `<li class="list-group-item px-0 text-danger">No se pudieron cargar los detalles.</li>`;
    }
}

async function editarServicio(id) {

    try {
        const respuesta = await fetch(`${API_URL}/ServiciosContratados/${id}`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!respuesta.ok) {
            throw new Error(await leerError(respuesta, "No se pudo traer el servicio"));
        }

        const servicio = await respuesta.json();

        idServicioEditando = id;

        document.getElementById("tituloNuevoServicio").textContent = `Editar servicio N° ${id}`;
        document.getElementById("btnGuardarServicio").textContent = "Guardar cambios";

        listaDetallesNuevo.innerHTML = "";
        actualizarTextoSinDetalles();
        document.getElementById("tituloDetallesNuevo").textContent = "Agregar detalles nuevos";
        document.getElementById("bloqueDetallesActuales").classList.remove("d-none");
        cargarDetallesActuales(id);

        document.getElementById("fechaServicio").value = servicio.fecha ?? "";
        document.getElementById("descripcionServicio").value = servicio.descripcion ?? "";
        elegirPorTexto(document.getElementById("tipoServicio"), servicio.tipoServicio);
        elegirPorTexto(document.getElementById("empleadoServicio"), servicio.empleado);
        elegirPorTexto(document.getElementById("sucursalServicio"), servicio.sucursal);

        buscarCliente.value = "";
        const cliente = listaClientes.find(c =>
            normalizarServicio(c.cliente) === normalizarServicio(servicio.cliente));
        mostrarClientes("", cliente ? cliente.idCliente : "");

        bootstrap.Modal.getOrCreateInstance(document.getElementById("modalNuevoServicio")).show();
    }
    catch (error) {
        console.error("Error al abrir la edicion:", error);
        mostrarAviso("error", "No se pudo abrir el servicio", error.message);
    }
}

document.getElementById("formNuevoServicio").addEventListener("submit", async event => {

    event.preventDefault();

    const editando = idServicioEditando !== null;

    const servicio = {
        fechaServicio: document.getElementById("fechaServicio").value,
        idCliente: Number(comboCliente.value),
        idEmpleado: Number(document.getElementById("empleadoServicio").value),
        idSucursal: Number(document.getElementById("sucursalServicio").value),
        descripcion: document.getElementById("descripcionServicio").value.trim(),
        idTipoServicio: Number(document.getElementById("tipoServicio").value)
    };

    const detallesNuevos = leerDetallesNuevo();
    servicio.detalles = detallesNuevos;

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
            throw new Error(await leerError(respuesta, "No se pudo guardar el servicio"));
        }

        const datos = editando ? {} : await respuesta.json();

        const cantidadDetalles = detallesNuevos.length;
        const textoDetalles = cantidadDetalles > 0
            ? ` Se agregó ${cantidadDetalles} detalle${cantidadDetalles > 1 ? "s" : ""}.`
            : "";
        const tituloAviso = editando ? "Servicio modificado" : "Servicio registrado";
        const textoAviso = editando
            ? `Los cambios del servicio N° ${idServicioEditando} se guardaron correctamente.${textoDetalles}`
            : `El servicio se registró correctamente con el N° ${datos.idServicio}.${textoDetalles}`;

        idServicioEditando = null;

        document.getElementById("formNuevoServicio").reset();
        listaDetallesNuevo.innerHTML = "";
        bootstrap.Modal.getInstance(document.getElementById("modalNuevoServicio")).hide();

        txtBuscarServicio.value = "";
        await cargarServicios();

        mostrarAviso("ok", tituloAviso, textoAviso);
    }
    catch (error) {
        console.error("Error al guardar el servicio:", error);
        mostrarAviso("error", "No se pudo guardar el servicio", error.message);
    }
});

async function verDetallesServicio(id) {

    const servicio = listaServicios.find(s => s.idServicio === id);
    const tabla = document.getElementById("tablaDetallesServicio");

    document.getElementById("tituloDetallesServicio").textContent = `Detalles del servicio N° ${id}`;
    document.getElementById("subtituloDetallesServicio").textContent = servicio
        ? `${servicio.cliente ?? ""} · ${formatearFecha(servicio.fecha)}`
        : "";

    tabla.innerHTML = `<tr><td colspan="5" class="text-center text-secondary">Cargando...</td></tr>`;
    bootstrap.Modal.getOrCreateInstance(document.getElementById("modalDetallesServicio")).show();

    try {
        const respuesta = await fetch(`${API_URL}/ServiciosContratados/ByIdWithDetalles/${id}`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!respuesta.ok) {
            throw new Error(await leerError(respuesta, "No se pudieron cargar los detalles"));
        }

        let detalles = await respuesta.json();
        if (!Array.isArray(detalles)) detalles = [];

        if (detalles.length === 0) {
            tabla.innerHTML = `<tr><td colspan="5" class="text-center text-secondary">Este servicio no tiene detalles cargados.</td></tr>`;
            return;
        }

        tabla.innerHTML = detalles.map(d => `
            <tr>
                <td>${d.idDetalleServicio}</td>
                <td>${d.tipoBateria ?? "-"}</td>
                <td>${d.voltajeBateria ?? "-"}</td>
                <td>${d.descVehiculo ?? "-"}</td>
                <td>${d.tipoVehiculo ?? "-"}</td>
            </tr>`).join("");
    }
    catch (error) {
        console.error("Error al cargar los detalles:", error);
        tabla.innerHTML = `<tr><td colspan="5" class="text-center text-danger">${error.message}</td></tr>`;
    }
}

let idServicioEstado = null;

async function abrirCambioEstado(id) {

    const servicio = listaServicios.find(s => s.idServicio === id);

    if (estaCerrado(servicio)) {
        mostrarAviso("error", "No se puede cambiar el estado",
            "Un servicio cancelado o finalizado no puede volver a cambiar de estado.");
        return;
    }
    idServicioEstado = id;

    document.getElementById("datosEstadoServicio").textContent = servicio
        ? `N° ${id} · ${servicio.cliente ?? ""} · ${formatearFecha(servicio.fecha)}`
        : `N° ${id}`;

    const combo = document.getElementById("selectEstadoServicio");
    combo.innerHTML = "";
    listaEstados.forEach(e => {
        const opcion = document.createElement("option");
        opcion.value = e.idEstado;
        opcion.textContent = e.estado.charAt(0).toUpperCase() + e.estado.slice(1);
        combo.appendChild(opcion);
    });
    if (idEstadoDe(servicio)) combo.value = idEstadoDe(servicio);

    const boton = document.getElementById("btnGuardarEstado");
    boton.disabled = false;
    boton.innerHTML = "Guardar estado";

    bootstrap.Modal.getOrCreateInstance(document.getElementById("modalEstadoServicio")).show();
}

document.getElementById("btnGuardarEstado").addEventListener("click", async () => {

    const combo = document.getElementById("selectEstadoServicio");
    const nuevoEstado = Number(combo.value);
    const nombreEstado = combo.options[combo.selectedIndex]?.text ?? "";
    const boton = document.getElementById("btnGuardarEstado");
    const modal = bootstrap.Modal.getInstance(document.getElementById("modalEstadoServicio"));

    const servicio = listaServicios.find(s => s.idServicio === idServicioEstado);
    if (idEstadoDe(servicio) === nuevoEstado) {
        modal.hide();
        return;
    }

    boton.disabled = true;
    boton.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Guardando...';

    try {
        const respuesta = await fetch(`${API_URL}/ServiciosContratados/${idServicioEstado}/estado`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({ nuevoEstado })
        });

        if (!respuesta.ok) {
            throw new Error(await leerError(respuesta, "No se pudo cambiar el estado"));
        }

        modal.hide();
        await cargarServicios();
        mostrarAviso("ok", "Estado actualizado",
            `El servicio N° ${idServicioEstado} ahora está en estado "${nombreEstado.toLowerCase()}".`);
    }
    catch (error) {
        console.error("Error al cambiar el estado:", error);
        boton.disabled = false;
        boton.innerHTML = "Guardar estado";
        mostrarAviso("error", "No se pudo cambiar el estado", error.message);
    }
});

function idEstadoCancelado() {
    return listaEstados.find(e => e.estado === "cancelado").idEstado;
}

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

function eliminarServicio(id) {

    const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("modalBajaServicio"));

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
                body: JSON.stringify({ nuevoEstado: idEstadoCancelado() })
            });

            if (!respuesta.ok) {
                throw new Error(await leerError(respuesta, "Ocurrió un error inesperado."));
            }

            estadoVentanaBajaServicio("ok");
            await cargarServicios();
        }
        catch (error) {
            console.error("Error al dar de baja el servicio:", error);
            estadoVentanaBajaServicio("error", error.message);
        }
    };

    modal.show();
}

document.addEventListener("DOMContentLoaded", () => {
    cargarServicios();
    cargarCombosServicio();
});
