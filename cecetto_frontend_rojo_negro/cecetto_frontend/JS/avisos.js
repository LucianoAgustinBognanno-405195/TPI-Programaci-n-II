//NUEVOCAMIO: archivo nuevo. Ventana de aviso prolija (exito o error) para reemplazar los alert() del navegador.
// Tiene el mismo estilo que la ventana de baja. Se usa en empleados y en servicios:
//     mostrarAviso("ok", "Empleado modificado", "Los cambios se guardaron correctamente.");
//     mostrarAviso("error", "No se pudo guardar", error.message);

// Agregamos el HTML de la ventana a la pagina (asi no hay que copiarlo en cada .html)
document.body.insertAdjacentHTML("beforeend", `
<div class="modal fade" id="modalAviso" tabindex="-1" aria-labelledby="tituloAviso" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content border-0 shadow">
            <div class="modal-body text-center p-4">
                <div id="iconoAviso" class="d-inline-flex align-items-center justify-content-center rounded-circle mb-3"
                     style="width: 64px; height: 64px;"></div>
                <h5 class="fw-bold mb-2" id="tituloAviso"></h5>
                <p class="text-secondary small mb-0" id="textoAviso" style="white-space: pre-line;"></p>
            </div>
            <div class="modal-footer border-0 justify-content-center pb-4 pt-0">
                <button type="button" class="btn px-4" data-bs-dismiss="modal" id="btnAceptarAviso">Aceptar</button>
            </div>
        </div>
    </div>
</div>`);

// Muestra el aviso. Devuelve una promesa que se cumple cuando el usuario lo cierra.
function mostrarAviso(tipo, titulo, mensaje = "") {

    return new Promise(resolve => {

        const elemento = document.getElementById("modalAviso");
        const exito = tipo === "ok";
        const color = exito ? "success" : "danger";

        document.getElementById("iconoAviso").className =
            `d-inline-flex align-items-center justify-content-center rounded-circle bg-${color} bg-opacity-10 text-${color} mb-3`;
        document.getElementById("iconoAviso").innerHTML =
            `<i class="bi ${exito ? "bi-check-lg" : "bi-x-lg"} fs-3"></i>`;
        document.getElementById("tituloAviso").textContent = titulo;
        document.getElementById("textoAviso").textContent = mensaje;
        document.getElementById("btnAceptarAviso").className =
            `btn btn-${exito ? "success" : "secondary"} px-4`;

        const abrir = () => {
            // Si hay otra ventana abierta (por ejemplo un formulario con error), el aviso va encima
            const hayOtraAbierta = document.querySelector(".modal.show") !== null;
            elemento.style.zIndex = hayOtraAbierta ? 1065 : "";

            elemento.addEventListener("shown.bs.modal", () => {
                if (hayOtraAbierta) {
                    const fondos = document.querySelectorAll(".modal-backdrop");
                    fondos[fondos.length - 1].style.zIndex = 1060;
                }
            }, { once: true });

            elemento.addEventListener("hidden.bs.modal", () => {
                // Si quedo otra ventana abierta debajo, la dejamos funcionando bien
                if (document.querySelector(".modal.show")) {
                    document.body.classList.add("modal-open");
                }
                resolve();
            }, { once: true });

            bootstrap.Modal.getOrCreateInstance(elemento).show();
        };

        // Si otra ventana se esta cerrando en este momento, esperamos a que termine
        const otraCerrandose = document.body.classList.contains("modal-open")
            && document.querySelector(".modal.show") === null;

        if (otraCerrandose) {
            document.addEventListener("hidden.bs.modal", abrir, { once: true });
        } else {
            abrir();
        }
    });
}
