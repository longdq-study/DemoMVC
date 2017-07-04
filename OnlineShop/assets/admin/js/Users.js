

var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data("id");
            $.ajax({
                url: "/Admin/Users/ChangeStatus",
                data: { id: id },
                dataType: 'json',
                type: "POST",
                success: function (data) {
                    if (data.status) {
                        btn.text("Kích hoạt");
                        
                    } else {
                        btn.text("Khóa");
                       
                    }
                }

            });

        });
    }
}
user.init();