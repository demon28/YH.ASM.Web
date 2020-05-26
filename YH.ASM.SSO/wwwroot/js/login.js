
$(document).ready(function () {

    $("#btn_login").click(function () {

        var username = $("#txt_username").val();
        var userpwd = $("#txt_pwd").val();
        var returnUrl = getQueryString("returnUrl");

        $.ajax({
            url: "/Account/LoginByWorkid",
            type: "POST",
            data: {
                Username: username,
                Password: userpwd,
                ReturnUrl: returnUrl
            },
            success: function (result) {

                if (!result.success) {
                    
                    alert_info(result.message);

                } else {
                    window.location.href = result.message;
                }
            }
        });

    });

});
