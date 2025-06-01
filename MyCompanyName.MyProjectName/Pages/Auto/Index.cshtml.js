// Pages/Auto/Index.cshtml.js

$(function () {
    // 1) Cargar todos los autos
    function cargarAutos() {
        abp.ajax({
            url: '/api/app/auto',
            type: 'GET'
        }).done(function (data) {
            const cuerpo = $('#AutoTableBody');
            cuerpo.empty();

            data.forEach(function (auto) {
                const fila = `
                    <tr>
                        <td>${auto.marca}</td>
                        <td>${auto.modelo}</td>
                        <td>${auto.color}</td>
                        <td>${auto.año}</td>
                        <td>
                            <button class="btn btn-danger btn-sm"
                                    onclick="eliminarAuto('${auto.id}')">
                                Eliminar
                            </button>
                             <button class="btn btn-secondary btn-sm"
                                    onclick="mostrarModalEditar('${auto.id}','${auto.marca}','${auto.modelo}','${auto.color}','${auto.año}')">
                                Editar
                            </button>
                        </td>
                    </tr>
                `;
                cuerpo.append(fila);
            });
        }).fail(function (xhr) {
            console.error('Error al cargar autos:', xhr);
            abp.message.error('No se pudo cargar la lista de autos.');
        });
    }

    // Carga inicial
    cargarAutos();

    // 2) “Agregar Auto” con DTO
    $('#AddAutoBtn').click(function () {
        const marca = $('#NewAutoMarca').val().trim();
        const modelo = $('#NewAutoModelo').val().trim();
        const color = $('#NewAutoColor').val().trim();
        const año = parseInt($('#NewAutoAño').val());

        if (!marca || !modelo || !color || isNaN(año)) {
            abp.message.warn('Por favor, completa todos los campos correctamente.');
            return;
        }

        // Ahora enviamos un JSON que coincide con CreateUpdateAutoDto (sin parámetros separados)
        abp.ajax({
            url: '/api/app/auto',
            type: 'POST',
            data: JSON.stringify({
                marca: marca,     // input.Marca
                color: color,     // input.Color
                modelo: modelo,    // input.Modelo
                anio: año        // input.Anio  (sin tilde)
            })
        }).done(function () {
            // Limpiar formulario
            $('#NewAutoMarca').val('');
            $('#NewAutoModelo').val('');
            $('#NewAutoColor').val('');
            $('#NewAutoAño').val('');

            // Refrescar lista
            cargarAutos();
            abp.message.success('Auto agregado correctamente.');
        }).fail(function (xhr) {
            console.error('Error al crear auto:', xhr);
            abp.message.error('No se pudo crear el auto.');
        });
    });

    // 3) Eliminar Auto por ID
    window.eliminarAuto = function (id) {
        abp.ajax({
            url: `/api/app/auto/${id}`,
            type: 'DELETE'
        }).done(function () {
            cargarAutos();
            abp.message.success('Auto eliminado correctamente.');
        }).fail(function (xhr) {
            console.error('Error al eliminar auto:', xhr);
            abp.message.error('No se pudo eliminar el auto.');
        });
    };

    // ——————————————————————————————————————————————
    // 4) Función global para mostrar el modal de “Editar”
    //     Recibe los datos actuales del auto para precargarlos
    // ——————————————————————————————————————————————
    window.mostrarModalEditar = function (id, marca, modelo, color, año) {
        // Guarda el ID en el campo oculto
        $('#EditarAutoId').val(id);

        // Precarga los valores en los inputs del modal
        $('#EditarAutoMarca').val(marca);
        $('#EditarAutoModelo').val(modelo);
        $('#EditarAutoColor').val(color);
        $('#EditarAutoAño').val(año);

        // Muestra el modal
        $('#modalEditarAuto').modal('show');
    };

    // ——————————————————————————————————————————————
    // 5) Evento “click” en el botón “Guardar Cambios” del modal
    // ——————————————————————————————————————————————
    $('#GuardarCambiosAuto').click(function () {
        const id = $('#EditarAutoId').val();
        const marca = $('#EditarAutoMarca').val().trim();
        const modelo = $('#EditarAutoModelo').val().trim();
        const color = $('#EditarAutoColor').val().trim();
        const año = parseInt($('#EditarAutoAño').val());

        if (!marca || !modelo || !color || isNaN(año)) {
            abp.message.warn('Por favor, completa todos los campos.');
            return;
        }

        // Llamada PUT /api/app/auto/{id} con el DTO
        abp.ajax({
            url: `/api/app/auto/${id}`,  // UpdateAsync(id, CreateUpdateAutoDto)
            type: 'PUT',
            data: JSON.stringify({
                marca: marca,
                color: color,
                modelo: modelo,
                anio: año     // coincide con CreateUpdateAutoDto.Anio
            })
        }).done(function () {
            // Cierra el modal
            $('#modalEditarAuto').modal('hide');

            // Recarga la tabla
            cargarAutos();
            abp.message.success('Auto actualizado correctamente.');
        }).fail(function (xhr) {
            console.error('Error al actualizar auto:', xhr);
            abp.message.error('No se pudo actualizar el auto.');
        });
    });
});
