// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    $(".select2").select2({
        placeholder: "Select one",
        escapeMarkup: function (m) {
            return m;
        }
    });

    openFormModal = (url, title, callback) => {
        var placeholderElement = $('#modal-placeholder');
        $.get(url).done(function (data) {
            placeholderElement.find('.modal-body').html(data);
            placeholderElement.find('.modal-title').html(title);
            placeholderElement.find('.modal').modal('show');
            placeholderElement
                .off('click')
                .on('click', '[data-save="modal"]', function (event) {
                    var form = $('#modal-placeholder').find('form');
                    $.validator.unobtrusive.parse(form);
                    form.validate().form();
                    if (form.valid()) {
                        var actionUrl = form.prop('action');
                        var dataToSend = new FormData(form.get(0));
                        $.ajax({
                            url: actionUrl,
                            method: 'post',
                            data: dataToSend,
                            processData: false,
                            contentType: false
                        }).done(function (data) {
                            $('#modal-placeholder').find('.modal-body').html(data);
                            var shouldClose = $(data).find('[name="ShouldClose"]').val() == 'True';
                            if (shouldClose) {
                                $('#modal-placeholder').find('.modal').modal('hide');
                                callback();
                            }
                        });
                    }
                });
        });
    }
});
