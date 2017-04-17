//修改用户状态
function ChangeStatus(status, id) {
    //show("123", status+"=="+id);
    if (status == 11 || status == 12) {
        if (!confirm("你确定要执行本操作？执行之后用户将无法登录。"))
            return;
    }

    $.post("/Admin/ChangeUserStatus", { "id": id, "status": status }, function (rst) {
        if (rst == "success") {
            show("系统提示", "操作成功！");
            setTimeout(function () {
                window.location.href = "/Admin/User";
            }, 1000);
        } else {
            show("警告", "操作失败！");
        }
    });
}

function GetMore(id) {
    //<div class='row'>  <div class='col-md-6'>用户名</div><div class='col-md-6'>123</div> </div>
    show("数据正在加载中...", "<img style='margin-left:25%;' width='50%' src='/MyContent/images/loading.gif'>");
    //return;
    setTimeout(function () {
        $.post("/AdminFun/GetUserInfo", { "id": id }, function (data) {
            var title = data["NickName"]+"("+ data["Email"]+")";
            var content = "<div class='row'>  <div class='col-md-6'>用户名</div><div class='col-md-6'>" + data["NickName"] + "</div> </div> <div class='row'>  <div class='col-md-6'>真实姓名</div><div class='col-md-6'>" + data["TrueName"] + "</div> </div> <div class='row'>  <div class='col-md-6'>联系电话</div><div class='col-md-6'>" + data["Phone"] + "</div> </div> <div class='row'>  <div class='col-md-6'>性别</div><div class='col-md-6'>" + data["Gender"] + "</div> </div> <div class='row'>  <div class='col-md-6'>设备ID</div><div class='col-md-6'>" + data["DeviceId"] + "</div> </div> <div class='row'>  <div class='col-md-6'>最近登录时间</div><div class='col-md-6'>" + data["LastLoginTime"] + "</div> </div> <div class='row'>  <div class='col-md-6'>最近登录IP</div><div class='col-md-6'>" + data["LastLoginIP"] + "</div> </div> <div class='row'>  <div class='col-md-6'>注册时间</div><div class='col-md-6'>" + data["SubDate"] + "</div> </div> <div class='row'>  <div class='col-md-6'>状态</div><div class='col-md-6'>" + data["Status"] + "</div> </div><hr>";
            $("#showBtn").html("<button type='button' class='btn btn-warning' data-dismiss='modal'>关闭</button>");
            changeShow(title, content, 0);
        });

    }, 0);

}

function Notify(id, email, name) {

    //$('#myModal').modal({ backdrop: 'static', keyboard: false });
    //return;
    var title = "<span class='glyphicon glyphicon-envelope' style='color:green'></span> 给用户发送通知："+name+"("+email+")";
    var content = "<form><div class='form-group'><label for='recipient-name' class='control-label'>主题:</label><input type='text' class='form-control' id='txtTitle'></div><div class='form-group'><label for='message-text' class='control-label'>内容:</label><textarea id='txtContent' class='form-control' id='message-text' rows='5'></textarea></div> </form>";
    $("#showBtn2").html("通知方式：  <input id='toEmail' type='radio' name='notype' value='1'> <label for='toEmail'>邮件方式</label>  <input id='toNote' type='radio' name='notype' checked='checked' value='2'><label for='toNote'>站内消息</label>   <input id='toAll' type='radio' name='notype' value='3'><label for='toAll'>邮件&站内</label> <button type='button' class='btn btn-info' id='btnSend'>发送</button> <button type='button' class='btn btn-warning' data-dismiss='modal' id='btnClose'>关闭</button><div id='divMsg' style='color:red'></div>");
    changeShow(title, content, 1);
    
    $("#btnSend").click({ "id": id, "email": email }, btnSend);
}

function btnSend(event) {
    var title = $("#txtTitle").val();
    var content = $("#txtContent").val();
    var type = $("input:radio[name='notype']:checked").val();

    //alert(title + "===" + content+"----"+type);

    //alert(event.data.id + "+++" + event.data.email);
    if (title == "" || content == "") {
        $("#divMsg").html("请填写完整的信息");
        return;
    }
    $("#divMsg").html("");
    $.post("/AdminFun/SendNote", { "title": title, "content": content, "type": type, "id": event.data.id, "email": event.data.email, "utype": "1" }, function (rst) {
        btnClose();
        if (rst == "success") {
            show("系统提示", "发送成功！");
        }
        else {
            show("警告", "发送失败！");
        }
    });

}

function btnClose() {
    $("#btnClose").click()
}