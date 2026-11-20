const formFiltro = document.getElementById("formFiltro");
const fechaDesde = document.getElementById("fechaDesde");
const fechaHasta = document.getElementById("fechaHasta");
const periodo = document.getElementById("periodo");
const sucursalFiltro = document.getElementById("sucursalFiltro");
const empleadoFiltro = document.getElementById("empleadoFiltro");
const fechasPersonalizadas = document.getElementById("fechasPersonalizadas");
const btnLimpiar = document.getElementById("btnLimpiar");
const textoPeriodo = document.getElementById("textoPeriodo");
const mensajeError = document.getElementById("mensajeError");

const ROJO = "#c62828";
const NEGRO = "#242424";
const GRIS = "#9e9e9e";

let graficoMeses = null;
let graficoServicios = null;
let graficoEmpleados = null;
let graficoVehiculos = null;

Chart.defaults.font.family = "'DM Sans', Arial, sans-serif";

function armarFiltro() {

    let filtro = "";

    function agregar(nombre, valor) {
        if (valor !== "") {
            filtro += (filtro === "" ? "?" : "&") + nombre + "=" + valor;
        }
    }

    agregar("desde", fechaDesde.value);
    agregar("hasta", fechaHasta.value);
    agregar("idSucursal", sucursalFiltro.value);
    agregar("idEmpleado", empleadoFiltro.value);

    return filtro;
}

async function cargarCombos() {

    try {

        const respuesta = await fetch(`${API_URL}/Empleados/By-Sucursal`);
        const sucursales = await respuesta.json();

        sucursales.forEach(sucursal => {
            const opcion = document.createElement("option");
            opcion.value = sucursal.idSucursal;
            opcion.textContent = sucursal.sucursal;
            sucursalFiltro.appendChild(opcion);
        });

    }
    catch (error) {
        console.error("Error sucursales:", error);
    }

    try {

        const respuesta = await fetch(`${API_URL}/Empleados/GetAll`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (!respuesta.ok) {
            empleadoFiltro.disabled = true;
            return;
        }

        const empleados = await respuesta.json();

        empleados.forEach(empleado => {
            const opcion = document.createElement("option");
            opcion.value = empleado.idEmpleado;
            opcion.textContent = `${empleado.nombre} ${empleado.apellido}`;
            empleadoFiltro.appendChild(opcion);
        });

    }
    catch (error) {
        console.error("Error empleados:", error);
    }
}

async function traerDatos(endpoint) {

    const respuesta = await fetch(`${API_URL}/Dashboard/${endpoint}${armarFiltro()}`, {
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    if (!respuesta.ok) {
        throw new Error("No se pudieron cargar los datos del dashboard");
    }

    return await respuesta.json();
}

function nombres(lista) {
    return lista.map(item => item.nombre);
}

function cantidades(lista) {
    return lista.map(item => item.cantidad);
}

function opcionesBarras(horizontal) {

    return {
        responsive: true,
        maintainAspectRatio: false,
        indexAxis: horizontal ? "y" : "x",
        plugins: {
            legend: { display: false }
        },
        scales: {
            x: { beginAtZero: true, ticks: { precision: 0 }, grid: { display: horizontal } },
            y: { beginAtZero: true, ticks: { precision: 0 }, grid: { display: !horizontal } }
        }
    };
}

function mostrarTarjetas(servicios, empleados, meses) {

    let total = 0;

    for (const servicio of servicios) {
        total += servicio.cantidad;
    }

    let mesMayor = null;

    for (const mes of meses) {
        if (mesMayor === null || mes.cantidad > mesMayor.cantidad) {
            mesMayor = mes;
        }
    }

    document.getElementById("kpiTotal").textContent = total;
    document.getElementById("kpiServicio").textContent = servicios.length > 0 ? servicios[0].nombre : "Sin datos";
    document.getElementById("kpiEmpleado").textContent = empleados.length > 0 ? empleados[0].nombre : "Sin datos";
    document.getElementById("kpiMes").textContent = mesMayor !== null ? mesMayor.nombre : "Sin datos";

    return mesMayor;
}

function dibujarMeses(meses, mesMayor) {

    if (graficoMeses !== null) {
        graficoMeses.destroy();
    }

    const colores = meses.map(mes => mesMayor !== null && mes.nombre === mesMayor.nombre ? ROJO : NEGRO);

    graficoMeses = new Chart(document.getElementById("graficoMeses"), {
        type: "bar",
        data: {
            labels: nombres(meses),
            datasets: [{
                label: "Servicios",
                data: cantidades(meses),
                backgroundColor: colores,
                borderRadius: 6
            }]
        },
        options: opcionesBarras(false)
    });
}

function dibujarServicios(servicios) {

    if (graficoServicios !== null) {
        graficoServicios.destroy();
    }

    graficoServicios = new Chart(document.getElementById("graficoServicios"), {
        type: "bar",
        data: {
            labels: nombres(servicios),
            datasets: [{
                label: "Servicios",
                data: cantidades(servicios),
                backgroundColor: NEGRO,
                borderRadius: 6
            }]
        },
        options: opcionesBarras(false)
    });
}

function dibujarEmpleados(empleados) {

    if (graficoEmpleados !== null) {
        graficoEmpleados.destroy();
    }

    graficoEmpleados = new Chart(document.getElementById("graficoEmpleados"), {
        type: "bar",
        data: {
            labels: nombres(empleados),
            datasets: [{
                label: "Servicios",
                data: cantidades(empleados),
                backgroundColor: ROJO,
                borderRadius: 6
            }]
        },
        options: opcionesBarras(true)
    });
}

function dibujarVehiculos(vehiculos) {

    if (graficoVehiculos !== null) {
        graficoVehiculos.destroy();
    }

    const colores = [ROJO, NEGRO, GRIS, "#e57373", "#616161"];

    let total = 0;

    for (const vehiculo of vehiculos) {
        total += vehiculo.cantidad;
    }

    document.getElementById("totalVehiculos").textContent = total;

    graficoVehiculos = new Chart(document.getElementById("graficoVehiculos"), {
        type: "doughnut",
        data: {
            labels: nombres(vehiculos),
            datasets: [{
                data: cantidades(vehiculos),
                backgroundColor: colores,
                borderWidth: 2,
                hoverOffset: 12
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            cutout: "65%",
            layout: { padding: 12 },
            plugins: {
                legend: { display: false },
                tooltip: {
                    callbacks: {
                        label: contexto => {
                            const porcentaje = Math.round(contexto.raw * 100 / total);
                            return ` ${contexto.raw} servicios · ${porcentaje}%`;
                        }
                    }
                }
            },
            onClick: (evento, elementos) => {
                if (elementos.length > 0) {
                    seleccionarVehiculo(elementos[0].index);
                }
            }
        }
    });

    const lista = document.getElementById("listaVehiculos");
    lista.innerHTML = "";

    if (vehiculos.length === 0) {
        lista.innerHTML = '<li class="texto-suave">Sin datos</li>';
        return;
    }

    vehiculos.forEach((vehiculo, indice) => {

        const porcentaje = Math.round(vehiculo.cantidad * 100 / total);

        const item = document.createElement("li");
        item.classList.add("item-dona");

        item.innerHTML = `
            <span class="punto-color" style="background:${colores[indice]}"></span>
            <span class="nombre-item">${vehiculo.nombre}</span>
            <span class="cantidad-item">${vehiculo.cantidad}</span>
            <span class="porcentaje-item">${porcentaje}%</span>
        `;

        item.addEventListener("click", () => seleccionarVehiculo(indice));

        lista.appendChild(item);
    });
}

function seleccionarVehiculo(indice) {

    const items = document.querySelectorAll("#listaVehiculos .item-dona");

    items.forEach(item => item.classList.remove("seleccionado"));
    items[indice].classList.add("seleccionado");

    const elemento = [{ datasetIndex: 0, index: indice }];

    graficoVehiculos.setActiveElements(elemento);
    graficoVehiculos.tooltip.setActiveElements(elemento, {
        x: graficoVehiculos.width / 2,
        y: graficoVehiculos.height / 2
    });
    graficoVehiculos.update();
}

function mostrarPeriodo() {

    let texto = "";

    if (fechaDesde.value === "" && fechaHasta.value === "") {
        texto = "Todos los servicios";
    }
    else {
        const desde = fechaDesde.value !== "" ? fechaDesde.value.split("-").reverse().join("/") : "el inicio";
        const hasta = fechaHasta.value !== "" ? fechaHasta.value.split("-").reverse().join("/") : "hoy";
        texto = `Servicios desde ${desde} hasta ${hasta}`;
    }

    if (sucursalFiltro.value !== "") {
        texto += " · " + sucursalFiltro.options[sucursalFiltro.selectedIndex].text;
    }

    if (empleadoFiltro.value !== "") {
        texto += " · " + empleadoFiltro.options[empleadoFiltro.selectedIndex].text;
    }

    textoPeriodo.textContent = texto;
}

async function cargarDashboard() {

    try {

        mensajeError.textContent = "";

        const servicios = await traerDatos("ServiciosMasPedidos");
        const empleados = await traerDatos("EmpleadosConMasServicios");
        const vehiculos = await traerDatos("VehiculosMasUsados");
        const meses = await traerDatos("ServiciosPorMes");

        const mesMayor = mostrarTarjetas(servicios, empleados, meses);

        dibujarMeses(meses, mesMayor);
        dibujarServicios(servicios);
        dibujarEmpleados(empleados.slice(0, 5));
        dibujarVehiculos(vehiculos.slice(0, 5));

        mostrarPeriodo();

    }
    catch (error) {

        mensajeError.textContent = error.message;
        console.error(error);

    }
}

function formatoFecha(fecha) {

    const anio = fecha.getFullYear();
    const mes = String(fecha.getMonth() + 1).padStart(2, "0");
    const dia = String(fecha.getDate()).padStart(2, "0");

    return `${anio}-${mes}-${dia}`;
}

function aplicarPeriodo() {

    const hoy = new Date();
    let desde = null;
    let hasta = null;

    if (periodo.value === "esteMes") {
        desde = new Date(hoy.getFullYear(), hoy.getMonth(), 1);
        hasta = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 0);
    }
    else if (periodo.value === "mesAnterior") {
        desde = new Date(hoy.getFullYear(), hoy.getMonth() - 1, 1);
        hasta = new Date(hoy.getFullYear(), hoy.getMonth(), 0);
    }
    else if (periodo.value === "tresMeses") {
        desde = new Date(hoy.getFullYear(), hoy.getMonth() - 2, 1);
        hasta = hoy;
    }
    else if (periodo.value === "esteAnio") {
        desde = new Date(hoy.getFullYear(), 0, 1);
        hasta = new Date(hoy.getFullYear(), 11, 31);
    }

    if (periodo.value === "personalizado") {
        fechasPersonalizadas.classList.remove("d-none");
        fechaDesde.focus();
        return;
    }

    fechasPersonalizadas.classList.add("d-none");

    fechaDesde.value = desde !== null ? formatoFecha(desde) : "";
    fechaHasta.value = hasta !== null ? formatoFecha(hasta) : "";

    cargarDashboard();
}

periodo.addEventListener("change", aplicarPeriodo);
sucursalFiltro.addEventListener("change", cargarDashboard);
empleadoFiltro.addEventListener("change", cargarDashboard);

formFiltro.addEventListener("submit", async event => {

    event.preventDefault();

    if (fechaDesde.value !== "" && fechaHasta.value !== "" && fechaDesde.value > fechaHasta.value) {
        mensajeError.textContent = "La fecha desde no puede ser mayor que la fecha hasta";
        return;
    }

    if (fechaDesde.value === "" && fechaHasta.value === "") {
        mensajeError.textContent = "Elegí un período o cargá las fechas desde y hasta";
        return;
    }

    await cargarDashboard();
});

btnLimpiar.addEventListener("click", async () => {

    fechaDesde.value = "";
    fechaHasta.value = "";
    periodo.value = "todo";
    sucursalFiltro.value = "";
    empleadoFiltro.value = "";
    fechasPersonalizadas.classList.add("d-none");

    await cargarDashboard();
});

document.addEventListener("DOMContentLoaded", () => {
    cargarCombos();
    cargarDashboard();
});
