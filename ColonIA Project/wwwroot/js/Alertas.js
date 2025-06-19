document.addEventListener('DOMContentLoaded', function () {
    const formularios = document.querySelectorAll('.form-eliminar-usuario');

    formularios.forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); 

            const usuarioNombre = form.querySelector('button').getAttribute('data-nombre') || 'este usuario';

            Toastify({
                text: `¿Seguro que desea eliminar a ${usuarioNombre}? Haz clic aquí para confirmar.`,
                duration: -1, 
                close: true,
                gravity: "top",
                position: "center",
                backgroundColor: "#dc3545", 
                stopOnFocus: true,
                onClick: function () {
                    form.submit(); 
                }
            }).showToast();
        });
    });
});


