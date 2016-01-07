$(document).ready(function () {
    $(function () {
        alert(1);
        $.ajaxSetup({ cache: false });
        $("a[data-modal]").on("click", function (e) {
            alert(2);
            $('#myModalContent').load(this.href, function () {
                alert(3);
                $('#myModal').modal({
                    keyboard: true
                }, 'show');
                bindForm(this);
            });
            return false;
        });
    });

    function bindForm(dialog) {
        alert(4);
        $('form', dialog).submit(function () {
            alert(5);
            $('#progress').show();
            $.ajax({
                async: false,
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.success) {
                        $('#myModal').modal('hide');
                        $('#progress').hide();
                        location.reload();
                    } else {
                        $('#progress').hide();
                        $('#myModalContent').html(result);
                        bindForm();
                    }
                }
            });
            return false;
        });
    }
});