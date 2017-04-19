function exit() {
    $.post("/Doc/Exit", {}, function (rst) {
        if (rst == "success") {
            show("系统提示", "退出成功，将在2秒后跳转登录页面！");
            setTimeout(function () {
                window.location.href = "/Doc/Login";

            }, 2000);
        } else {
            show("警告", "退出失败，请重试！");
        }
    });
}

function show(title, content) {
    if (title == "警告") {
        $("#showContent").css("color", "#ff6a00");
    } else {
        $("#showContent").css("color", "black");
    }

    $("#showTitle").html(title);
    $("#showContent").html(content);

    $("#btnShow").click();
    //$('#myModal').modal({ backdrop: true, keyboard: true });
}

function changeShow(title, content, isShow) {

    if (isShow == 1) {

        if (title == "警告") {
            $("#showContent2").css("color", "#ff6a00");
        } else {
            $("#showContent2").css("color", "black");
        }

        $("#showTitle2").html(title);
        $("#showContent2").html(content);
        $('#myModal2').modal({ backdrop: 'static', keyboard: false });
    } else {
        if (title == "警告") {
            $("#showContent").css("color", "#ff6a00");
        } else {
            $("#showContent").css("color", "black");
        }

        $("#showTitle").html(title);
        $("#showContent").html(content);
    }
}
