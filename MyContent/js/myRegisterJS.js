function refreshCodeImage() {
    s = Math.random();
    $("#imgCode").attr("src", "../../Myashx/SendValidateCode.ashx?s=" + s);
}


//封装头部错误信息提示   myRegisterHeaderErrorMsg
function showHeaderMsg(addclass, removeclass, msg) {
    $("#myRegisterHeaderErrorMsg").removeClass(removeclass);
    $("#mgRegisterHeaderErrorMsg").addClass(addclass);
    $("#myRegisterHeaderErrorMsg").text("* "+msg+" *");
}






//<-----------------------------------------txtUserNameOnblur--------------------------------------------->
//用户名文本框的 焦点离开事件
$("#txtUserName").bind("blur",txtUserNameOnblur);
function txtUserNameOnblur() {

    if ($("#txtUserName").val() != "") {
        $.post("../../Myashx/CheckRegister.ashx", { "txtUserName": $("#txtUserName").val(), "CheckType": "CheckUserName" }, function (result) {
            if (result == "NotExist") {
                $("#msgUserName").removeClass("errorMsg");

                $("#msgUserName").addClass("successMsg");
                $("#msgUserName").text(" √");

                return true;
            } else if (result == "Exist") {
                $("#msgUserName").removeClass("successMsg");

                $("#msgUserName").addClass("errorMsg");
                $("#msgUserName").text(" ×");

                showHeaderMsg("errorMsg", "successMsg", "用户名已经存在");

                return false;

            }
        });
    }
    else {//用户名为空！
        $("#msgUserName").text(" ×");
        $("#msgUserName").removeClass("successMsg");
        $("#msgUserName").addClass("errorMsg");

        return false;

    }
}
//<-------------------------------------------------------------------------------------------------------------->



//<-----------------------------------------txtEmailOnblur--------------------------------------------->
//邮箱的文本框的 焦点离开事件
$("#txtEmail").bind("blur", txtEmailOnblur);
function txtEmailOnblur() {
    if ($("#txtEmail").val() != "") {

        if (checkEmail($("#txtEmail").val())) {

            $.post("../../Myashx/CheckRegister.ashx", { "txtEmail": $("#txtEmail").val(), "CheckType": "CheckEmail" }, function (result) {
                if (result == "NotExist") {
                    $("#msgEmail").removeClass("errorMsg");
                    $("#msgEmail").addClass("successMsg");
                    $("#msgEmail").text(" √");

                    return true;
                } else if (result == "Exist") {
                    $("#msgEmail").removeClass("successMsg");
                    $("#msgEmail").addClass("errorMsg");
                    $("#msgEmail").text(" ×");

                    showHeaderMsg("errorMsg", "successMsg", "邮箱已经存在");

                    return false;
                }
            });
        }
        else {//邮箱格式不对
            $("#msgEmail").removeClass("successMsg");
            $("#msgEmail").addClass("errorMsg");
            $("#msgEmail").text(" ×");

            showHeaderMsg("errorMsg", "successMsg", "邮箱格式错误");

            return false;
        }
    } else {
        $("#msgEmail").removeClass("successMsg");
        $("#msgEmail").addClass("errorMsg");
        $("#msgEmail").text(" ×");

        return false;
    }
}
//<------------------------------------------------------------------------------------------------------>



//<-----------------------------------------txtCodeOnblur--------------------------------------------->
//验证码的文本框 焦点离开事件
$("#txtCode").bind("blur", txtCodeOnblur);
function txtCodeOnblur() {
    if ($("#txtCode").val() != "") {
        $.post("../../Myashx/ValidateCode.ashx", { "txtCode": $("#txtCode").val(), "CodeType": "Register" }, function (result) {
            if (result == "success") {

                $("#msgCode").removeClass("errorMsg")
                $("#msgCode").addClass("successMsg");
                $("#msgCode").text(" √");

                return true;

            } else if (result == "error") {
                $("#msgCode").removeClass("successMsg")
                $("#msgCode").addClass("errorMsg");
                $("#msgCode").text(" ×");

                $("#myRegisterHeaderErrorMsg").removeClass("successMsg")
                $("#myRegisterHeaderErrorMsg").addClass("errorMsg");
                $("#myRegisterHeaderErrorMsg").text("* 验证码错误 *");

                return false;
            }
        });
    
    } else {
        //showHeaderMsg("errorMsg", "successMsg", "验证码不能为空");
        $("#myRegisterHeaderErrorMsg").removeClass("successMsg")
        $("#myRegisterHeaderErrorMsg").addClass("errorMsg");
        $("#myRegisterHeaderErrorMsg").text("* 验证码不能为空 *");

        return false;
    }
}
//<------------------------------------------------------------------------------------------------------->



//<-----------------------------------------txtPwd1Onblur--------------------------------------------->
//密码1的 文本框的 焦点离开事件
$("#txtPwd1").bind("blur",txtPwd1Onblur );
function txtPwd1Onblur() {

    if ($("#txtPwd1").val() != "") {
        $("#msgPwd1").text(" √");
        $("#msgPwd1").removeClass("errorMsg");
        $("#msgPwd1").addClass("successMsg");

        return true;
    }
    else {//密码1为空！
        $("#msgPwd1").text(" ×");
        $("#msgPwd1").removeClass("successMsg");
        $("#msgPwd1").addClass("errorMsg");
        return false;
    }
}
//<------------------------------------------------------------------------------------------------------->


//<-----------------------------------------txtPwd2Onblur--------------------------------------------->
//密码2的 文本框 焦点离开事件
$("#txtPwd2").bind("blur", txtPwd2Onblur);
function txtPwd2Onblur() {
    if ($("#txtPwd2").val() != "") {
        if ($("#txtPwd1").val() != "") {
            if ($("#txtPwd1").val() == $("#txtPwd2").val()) {
                $("#msgPwd1").text(" √");
                $("#msgPwd1").removeClass("errorMsg");
                $("#msgPwd1").addClass("successMsg");

                $("#msgPwd2").text(" √");
                $("#msgPwd2").removeClass("errorMsg");
                $("#msgPwd2").addClass("successMsg");

                return true;
            } else {
                $("#msgPwd2").text(" ×");
                $("#msgPwd2").removeClass("successMsg");
                $("#msgPwd2").addClass("errorMsg");

                $("#myRegisterFooterErrorMsg").removeClass("successMsg")
                $("#myRegisterFooterErrorMsg").addClass("errorMsg");
                $("#myRegisterFooterErrorMsg").text("* 两次密码不一致 *");

                return false;
            }
        }
        else {
            $("#msgPwd1").text(" ×");
            $("#msgPwd1").removeClass("successMsg");
            $("#msgPwd1").addClass("errorMsg");

            return false;
        }
    } else {
        $("#msgPwd2").text(" ×");
        $("#msgPwd2").removeClass("successMsg");
        $("#msgPwd2").addClass("errorMsg");

        return false;
    }
}
//<-------------------------------------------------------------------------------------------------------->


//<----------------------------------------txtCode2Onblur---------------------------------------------------------------->
//注册表单提交的验证码 文本框 的 焦点离开事件
$("#txtCode2").bind("blur",txtCode2Onblur);
function txtCode2Onblur(){
    if ($("#txtCode2").val() != "") {
        $("#msgCode2").text(" √");
        $("#msgCode2").removeClass("errorMsg");
        $("#msgCode2").addClass("successMsg");
    } else {
        $("#msgCode2").text(" ×");
        $("#msgCode2").removeClass("successMsg");
        $("#msgCode2").addClass("errorMsg");
    }
}
//<---------------------------------------------------------------------------------------------------------------------------->


//发送验证码按钮点击事件
function btnSendCodeClick() {

    if ($("#txtEmail").val() != "") {

        if (checkEmail($("#txtEmail").val())) { //邮箱地址正确可以发送验证码


            //检查邮箱是否存在
            $.post("../../Myashx/CheckRegister.ashx", { "txtEmail": $("#txtEmail").val(), "CheckType": "CheckEmail" }, function (result) {
                if (result == "NotExist") {
                    $("#msgEmail").removeClass("errorMsg");

                    $("#msgEmail").addClass("successMsg");
                    $("#msgEmail").text(" √");

                    //发送验证码
                    $.post("../../Myashx/ValidateEmail.ashx", { "txtEmail": $("#txtEmail").val() ,"CheckType":"ValidateRegisterEmail"}, function (result) {
                        if (result == "error") {

                            showHeaderMsg("errorMsg", "successMsg", "验证码发送失败");
                        }
                        else if (result == "success") {
                            //$("#spSendCode").remove("#sendCodeMsg");
                            //提示用户发送成功的msg

                            //showHeaderMsg("successMsg", "errorMsg", "发送成功，注意查收");
                            $("#myRegisterHeaderErrorMsg").removeClass("errorMsg")
                            $("#myRegisterHeaderErrorMsg").addClass("successMsg");
                            $("#myRegisterHeaderErrorMsg").text("发送成功，注意查收");

                            //把发送按钮隐藏
                            $("#btnSendCode").addClass("myhidedisplay");

                            //添加一个虚拟背景
                            //$("#divSendCode").addClass("myhidebutton");
                            $("#spSendCode").addClass("myhidebutton");


                            i = 30;
                            function countTime() {
                                $("#spSendCode").html("<span class='myRegisternumber'>"+i +"</span>"+ " 秒后可以重新发送！");
                                i--;
                                if (i == 0) {
                                    clearInterval(intervalId);
                                    $("#spSendCode").html("");
                                    $("#btnSendCode").removeClass("myhidedisplay");

                                    //$("#divSendCode").removeClass("myhidebutton");
                                    $("#spSendCode").removeClass("myhidebutton");

                                }
                            }

                            intervalId = setInterval(countTime, 1000);

                        }
                    });


                } else if (result == "Exist") {
                    $("#msgEmail").addClass("errorMsg");
                    $("#msgEmail").text(" ×");

                    showHeaderMsg("errorMsg", "successMsg", "邮箱已经存在");
                }
            });


            
        }
        else {
            showHeaderMsg("errorMsg", "successMsg", "邮箱地址格式不正确");
        }
    }
    else {//邮箱txt为空！
        showHeaderMsg("errorMsg", "successMsg", "邮箱地址不能为空");
    }
}


//注册按钮点击事件
$("#btnRegister").click(function () {
    
    if ($("#msgUserName").text() == " √") {
        if ($("#msgEmail").text() == " √") {
            if ($("#msgCode").text() == " √") {
                if ($("#msgDeviceId").text() == " √") {
                    if ($("#msgPwd1").text() == " √") {
                        if ($("#msgPwd2").text() == " √") {
                            if ($("#msgCode2").text() == " √") {
                                //全部信息完善
                                $("#myRegisterFooterErrorMsg").removeClass("errorMsg")
                                $("#myRegisterFooterErrorMsg").addClass("successMsg");
                                $("#myRegisterFooterErrorMsg").text("注册信息完善，正在注册中。。。");


                                $.post("/User/FinishRegister", { "txtUserName": $("#txtUserName").val(), "txtEmail": $("#txtEmail").val(), "txtCode": $("#txtCode").val(), "txtDeviceId": $("#txtDeviceId").val(), "txtPwd1": $("#txtPwd1").val(), "txtPwd2": $("#txtPwd2").val(), "txtCode2": $("#txtCode2").val() }, function (result) {
                                    if (result == "success") {

                                        $("#divLg").text("Hello," + $("#txtUserName").val());
                                        $(".btn-lg").click();
                                        $("#divLg").text("Hello," + $("#txtUserName").val());
                                    }
                                    else if(result == "error") {
                                        alert("注册失败！");
                                    }
                                });
                            } else {
                                owFooterErrorMsg("#msgCode2");
                            }
                        } else {
                            showFooterErrorMsg("#msgPwd2");
                        }
                    }
                    else {
                        showFooterErrorMsg("#msgPwd1");
                    }
                } else {
                    showFooterErrorMsg("#msgDeviceId");
                }
            } else {
                showFooterErrorMsg("#msgCode");
            }
        }
        else {
            showFooterErrorMsg("#msgEmail");

        }
    } else {
        showFooterErrorMsg("#msgUserName");
    }
});



//最后注册按钮的错误提示信息
function showFooterErrorMsg(id) {
    $(id).removeClass("successMsg")
    $(id).addClass("errorMsg");
    $(id).text(" ×");

    $("#myRegisterFooterErrorMsg").removeClass("successMsg")
    $("#myRegisterFooterErrorMsg").addClass("errorMsg");
    $("#myRegisterFooterErrorMsg").text("* 以上信息有误，请完善注册信息 *");
}


//用户名文本框 KeyDown 事件
$("#txtUserName").bind("keydown", function () {
    $("#msgUserName").text("");
    $("#myRegisterHeaderErrorMsg").html("&nbsp;");
});

//邮箱文本框 KeyDown 事件
$("#txtEmail").bind("keydown", function () {
    $("#msgEmail").text("");
    $("#myRegisterHeaderErrorMsg").html("&nbsp;");
});

//验证码文本框的 KeyDown 事件
$("#txtCode").bind("keydown", function () {
    $("#msgCode").text("");
    $("#myRegisterHeaderErrorMsg").html("&nbsp;")
});

//密码1 文本框的 KeyDown事件
$("#txtPwd1").bind("keydown", function () {
    $("#msgPwd1").text("");
    $("#myRegisterFooterErrorMsg").html("&nbsp;");
});

//密码2 文本框的 KeyDown事件
$("#txtPwd2").bind("keydown", function () {
    $("#msgPwd2").text("");
    $("#myRegisterFooterErrorMsg").html("&nbsp;");
});

// 注册表单的验证码2 文本框 的 KeyDown事件
$("#txtCode2").bind("keydown", function () {
    $("#txtCode2").text("");
    $("#myRegisterFooterErrorMsg").html("&nbsp;");
});

//邮箱的焦点点中事件--》验证用户名
$("#txtEmail").bind("focus", function () {
    if ($("#txtUserName").val() == "") {
        $("#msgUserName").removeClass("successMsg");
        $("#msgUserName").addClass("errorMsg");
        $("#msgUserName").text(" ×");
    }
});


//密码框的焦点事件
$("#txtPwd1").bind("focus", function () {
    if ($("#txtDeviceId").val() == "") {
        $("#msgDeviceId").removeClass("successMsg");
        $("#msgDeviceId").addClass("errorMsg");
        $("#msgDeviceId").text(" ×");
    }
    else {
        $("#msgDeviceId").removeClass("errorMsg");
        $("#msgDeviceId").addClass("successMsg");
        $("#msgDeviceId").text(" √");
    }
});

function btnOver(i) {
    if (i == 1) {
        $("#btnRegister").addClass("mybuttonhover");
    }
    else if (i == 2) {
        $("#btnSendCode").addClass("mybuttonhover");
    } 
}

function btnOut(j) {
    if (j == 1) {
        $("#btnRegister").removeClass("mybuttonhover");
    } else if (j == 2) {
        $("#btnSendCode").removeClass("mybuttonhover");
    } 
}



function checkEmail(str) {
    //str = $("#txtEmail").val();
    var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
    //var re = ([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+
    if (re.test(str)) {
        //alert("正确");
        return true;
    } else {
        //alert("错误");
        return false;
    }
}

$(function () {



});