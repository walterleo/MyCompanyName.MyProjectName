$(function () {
    var l = abp.localization.getResource("MyProjectName");
    
    // DELETING ITEMS /////////////////////////////////////////
    $('#TodoList').on('click', 'li i', function(){
        var $li = $(this).parent();
        var id = $li.attr('data-id');

        myCompanyName.myProjectName.services.todo.delete(id).then(function(){
            $li.remove();
            abp.notify.info(l('TodoDeleted'));
        });
    });

    // CREATING NEW ITEMS /////////////////////////////////////
    $('#NewItemForm').submit(function(e){
        e.preventDefault();

        var todoText = $('#NewItemText').val();
        myCompanyName.myProjectName.services.todo.create(todoText).then(function(result){
            $('<li data-id="' + result.id + '">')
                .html('<i class="fa fa-trash-o"></i> ' + result.text)
                .appendTo($('#TodoList'));
            $('#NewItemText').val('');
        });
    });
    ////////////////////Paul
    $('#NewItemForm1').submit(function (e) {
        e.preventDefault();

        var name = $('#NewItemName').val();
        var lname = $('#NewItemLastName').val();
        myCompanyName.myProjectName.services.persona.create(name,lname).then(function (result) {
            //$('<li data-id="' + result.id + '">')
              //  .html('<i class="fa fa-trash-o"></i><a>  Nombre: </a> ' + result.name +'<a> Apellido: </a>'+result.lastName)
            //  .appendTo($('#TodoList1'));
            $('<tr data-id="' + result.id + '">')
                .html('<td data-id="@todoItem.Id"><i  class="fa fa-trash-o icono-eliminar"></i></td><td data-id="@todoItem.Id"><i class="fa fa-pencil icono-editar"></td> <td>' + result.name + '</td> <td>' + result.lastName + '</td> </tr>')
                .appendTo($('#TodoList2'));
            $('#NewItemName').val('');
            $('#NewItemLastName').val('');
        });
    });
    //delete
    $('#TodoList1').on('click', 'li i', function () {
        var $li = $(this).parent();
        var id = $li.attr('data-id');

        myCompanyName.myProjectName.services.persona.delete(id).then(function () {
            $li.remove();
            abp.notify.info(l('Persona Deleted'));
        });
    });
    //para tabla
    $('#TodoList2').on('click', 'tr td i.icono-eliminar', function () {
        var $li = $(this).parent().parent();
        var id = $li.attr('data-id');

        abp.notify.info(l('Persona ' + id));
        if (confirm(l('¿Estás seguro de que deseas eliminar esta Persona: ?'))) {
            myCompanyName.myProjectName.services.persona.delete(id).then(function () {
                $li.remove();
                abp.notify.info(l('Persona Deleted'));
            });

        }


       
    });
    //update
    $('#TodoList2').on('click', 'tr td i.icono-editar', function () {
        var $li = $(this).parent().parent();
        var id = $li.attr('data-id');
        abp.notify.info(l('Persona ' + id));
        myCompanyName.myProjectName.services.persona.get(id).then(function (result) {
            $('#editar-persona-id').val(result.id);
            $('#editar-persona-nombre').val(result.name);
            $('#editar-persona-apellido').val(result.lastName);
            $('#miSegundoModalEditar').modal('show');
        });
        
    });

    $('#guardar-cambios-persona').click(function () {
        var id = $('#editar-persona-id').val();
        var name = $('#editar-persona-nombre').val();
        var lastName = $('#editar-persona-apellido').val();

        myCompanyName.myProjectName.services.persona.update(id, name,lastName).then(function () {
            $('#miSegundoModalEditar').modal('hide');
            abp.notify.info(l('Persona Updated'));
            window.location.reload();
        });
    });

    $('#cerrar-modal').click(function () {
        $('#miSegundoModalEditar').modal('hide');
    });
});
