
$(function () { });

$(".mybutton").mouseover(function () {
    $(this).addClass("mybuttonhover");
});

$(".mybutton").mouseout(function () {
    $(this).removeClass("mybuttonhover");
});

$(".myEditBtn").mouseover(function () {
    $(this).addClass("myEditBtnhover");
});

$(".myEditBtn").mouseout(function () {
    $(this).removeClass("myEditBtnhover");
});


//邮箱修改按钮----点击事件
$("#EmailBtn").click(function () {
    alert("对不起，邮箱暂时不支持修改！");
    //if ($("#EmailTip").hasClass("myhidedisplay")) {

    //    $("#EmailTip").removeClass("myhidedisplay");
    //    $("#EmailHideText").addClass("myhidedisplay");
    //} else {
    //    $("#EmailTip").addClass("myhidedisplay");
    //    $("#EmailHideText").removeClass("myhidedisplay");
    //}
});


//密码修改按钮-----点击事件
$("#PwdBtn").click(function () {
    location.href = "/User/UpdatePwd";
});


//真实姓名修改按钮-----点击事件
$("#TrueNameBtn").click(TrueNameBtnClick);
function TrueNameBtnClick() {
    if ($("#TrueNameTip").hasClass("myhidedisplay") || !$("#TrueNameHideText").hasClass("myhidedisplay")) {

        $("#TrueNameTip").removeClass("myhidedisplay");
        $("#TrueNameHideText").addClass("myhidedisplay");
    } else {
        $("#TrueNameTip").addClass("myhidedisplay");
        $("#TrueNameHideText").removeClass("myhidedisplay");
    }
}

//设备Id修改按钮------点击事件
$("#DeviceIdBtn").click(DeviceIdBtnClick);
function DeviceIdBtnClick() {
    if ($("#DeviceIdTip").hasClass("myhidedisplay")) {

        $("#DeviceIdTip").removeClass("myhidedisplay");
        $("#DeviceIdHideText").addClass("myhidedisplay");
    } else {
        $("#DeviceIdTip").addClass("myhidedisplay");
        $("#DeviceIdHideText").removeClass("myhidedisplay");
    }
}

//手机号码修改按钮------点击事件
$("#PhoneBtn").click( PhoneBtnClick );
function   PhoneBtnClick() {
    if ($("#PhoneTip").hasClass("myhidedisplay")) {

        $("#PhoneTip").removeClass("myhidedisplay");
        $("#PhoneHideText").addClass("myhidedisplay");
    } else {
        $("#PhoneTip").addClass("myhidedisplay");
        $("#PhoneHideText").removeClass("myhidedisplay");
    }
}

//<--------------------------------------------保存按钮事件------------------------------------>

//邮箱保存
$("#EmailSaveBtn").click(function () {
    if ($("#EmailSaveText").val() != "") {
        //上传更新数据
        alert("123");
    } else {
        $("#EmailErrorMsg").removeClass("myhidedisplay");
        $("#EmailErrorMsg").text(" * 文本框不能为空 *");
        $("#EmailErrorMsg").addClass("errorMsg");
    }
});
//邮箱编辑文本框的 keydown 事件
$("#EmailSaveText").keydown(function () {

    $("#EmailErrorMsg").text("");
    $("#EmailErrorMsg").removeClass("errorMsg");
});


//<------------------------------------------------------------------------------------>


//真实姓名保存
$("#TrueNameSaveBtn").click(function () {
    if ($("#TrueNameSaveText").val() != "") {
        //上传数据
        $.post("/Home/UpdateUserInfo", { "item": "TrueName", "data": $("#TrueNameSaveText").val() }, function (result) {
            if (result == "success") {
                $("#TrueNameErrorMsg").removeClass("myhidedisplay");
                $("#TrueNameErrorMsg").text(" * 保存成功 *");
                $("#TrueNameErrorMsg").addClass("successMsg");
                $("#TrueNameErrorMsg").removeClass("errorMsg");

                setTimeout(TrueNameBtnClick, 2000);
            } else if (result == "error") {
                $("#TrueNameErrorMsg").removeClass("myhidedisplay");
                $("#TrueNameErrorMsg").text(" * 保存失败 *");
                $("#TrueNameErrorMsg").addClass("errorMsg");
                $("#TrueNameErrorMsg").removeClass("successMsg");
            }
        });
    } else {
        $("#TrueNameErrorMsg").removeClass("myhidedisplay");
        $("#TrueNameErrorMsg").text(" * 文本框不能为空 *");
        $("#TrueNameErrorMsg").addClass("errorMsg");
        $("#TrueNameErrorMsg").removeClass("successMsg");
    }
});
//真实姓名编辑文本框的 keydown 事件
$("#TrueNameSaveText").keydown(function () {

    $("#TrueNameErrorMsg").text("");
    $("#TrueNameErrorMsg").removeClass("errorMsg");
    $("#TrueNameErrorMsg").addClass("myhidedisplay");
});


//<------------------------------------------------------------------------------------>


//设备Id保存
$("#DeviceIdSaveBtn").click(function () {
    if ($("#DeviceIdSaveText").val() != "") {
        $.post("/Home/UpdateUserInfo", { "item": "DeviceId", "data": $("#DeviceIdSaveText").val() }, function (result) {
            if (result == "success") {
                $("#DeviceIdErrorMsg").removeClass("myhidedisplay");
                $("#DeviceIdErrorMsg").text(" * 保存成功 *");
                $("#DeviceIdErrorMsg").addClass("successMsg");
                $("#DeviceIdErrorMsg").removeClass("errorMsg");

                setTimeout(DeviceIdBtnClick, 2000);
            } else if (result == "error") {
                $("#DeviceIdErrorMsg").removeClass("myhidedisplay");
                $("#DeviceIdErrorMsg").text(" * 保存失败 *");
                $("#DeviceIdErrorMsg").addClass("errorMsg");
                $("#DeviceIdErrorMsg").removeClass("successMsg");
            }
        });
    } else {
        $("#DeviceIdErrorMsg").removeClass("myhidedisplay");
        $("#DeviceIdErrorMsg").text(" * 文本框不能为空 *");
        $("#DeviceIdErrorMsg").addClass("errorMsg");
    }
});
//设备Id编辑文本框的 keydown 事件
$("#DeviceIdSaveText").keydown(function () {

    $("#DeviceIdErrorMsg").text("");
    $("#DeviceIdErrorMsg").removeClass("errorMsg");
});


//<------------------------------------------------------------------------------------>


//电话号码 保存
$("#PhoneSaveBtn").click(function () {
    if ($("#PhoneSaveText").val() != "") {
        //上传数据
        $.post("/Home/UpdateUserInfo", { "item": "Phone", "data": $("#PhoneSaveText").val() }, function (result) {
            if (result == "success") {
                $("#PhoneErrorMsg").removeClass("myhidedisplay");
                $("#PhoneErrorMsg").text(" * 保存成功 *");
                $("#PhoneErrorMsg").addClass("successMsg");
                $("#PhoneErrorMsg").removeClass("errorMsg");

                setTimeout(PhoneBtnClick, 2000);
            } else if (result == "error") {
                $("#PhoneErrorMsg").removeClass("myhidedisplay");
                $("#PhoneErrorMsg").text(" * 保存失败 *");
                $("#PhoneErrorMsg").addClass("errorMsg");
                $("#PhoneErrorMsg").removeClass("successMsg");
            }
        });
    } else {
        $("#PhoneErrorMsg").removeClass("myhidedisplay");
        $("#PhoneErrorMsg").text(" * 文本框不能为空 *");
        $("#PhoneErrorMsg").addClass("errorMsg");
    }
});
//电话号码编辑文本框的 keydown 事件
$("#PhoneSaveText").keydown(function () {

    $("#PhoneErrorMsg").text("");
    $("#PhoneErrorMsg").removeClass("errorMsg");
})

//###############################【20160417 之后修改】###########################################
$(function(){
    $("#UpdateLink_Btn").click(function () {//【修改按钮的点击事件】
        $(this).hide();
        $("#SaveLink_Btn").show();
        $(this).parent().css("background-color", "transparent");

        $(".CanEditInput").removeAttr("readonly");
        $(".CanEditInput").addClass("CandEditInput_Back");
        $(".myCheckCodeDIV").show();
        $(".CanEditTrip").show();



        $("#TrueName_Input").focus();
        var TrueName = $("#TrueName_Input").val();
        //alert(TrueName);
        $("#TrueName_Input").val(TrueName);
    });

    $("#Cancel_Btn").click(function () {//【取消按钮的点击事件】
        $(this).parent().hide();
        $("#UpdateLink_Btn").show();
        $(this).parent().parent().css("background-color", "#CCFFCC");

        $(".CanEditInput").attr("readonly", "readonly");
        $(".CanEditInput").removeClass("CandEditInput_Back");
        $(".myCheckCodeDIV").hide();
        $(".CanEditTrip").hide();
    });


    $("#Save_Btn").click(function () {//【保存按钮保存事件】
        //alert("你点击保存按钮");
        var trueName = $("#TrueName_Input").val();
        var gender = $("input:radio[name='Gender']:checked").val();
        var deviceId = $("#DeviceId_Input").val();
        var phone = $("#Phone_Input").val();

        var code = $("#Code_Input").val();

        if (trueName != "") {
            if (gender != "") {
                if (deviceId != "") {
                    if (phone != "") {
                        if (code != "") {
                            //执行发送
                            $.post("../User/UpdateUserInfo", { "TrueName": trueName, "Gender": gender, "DeviceId": deviceId, "Phone": phone ,"Code":code}, function (result) {
                                if (result == "success") {
                                    $("#UpdateUserInfo_Result").text("更新成功！");
                                    $("#UpdateUserInfo_Btn").click();
                                    window.location.href = "/User/ShowMyInfo";
                                } else if (result == "error1") {
                                    $("#UpdateUserInfo_Result").text("更新失败，请重试！");
                                    $("#UpdateUserInfo_Btn").click();
                                } else if (result == "error2") {
                                    $("#UpdateUserInfo_Result").text("验证码输入错误，请重新输入！");
                                    $("#UpdateUserInfo_Btn").click();
                                } else if (result == "error3") {
                                    $("#UpdateUserInfo_Result").text("个人信息输入错误，请重新输入！");
                                    $("#UpdateUserInfo_Btn").click();
                                }
                            });
                        }else {
                            showEditError("Code");
                        }
                    } else {
                       showEditError("Phone");
                    }
                } else {
                    showEditError("DeviceId");
                }
            } else {
                showEditError("Gender");
            }
        } else {
            showEditError("TrueName");
        }
    });
    
    //编辑个人信息的错误提示
    function showEditError(typeId) {
        $("#"+typeId+"_Input_Msg").addClass("errorMsg");
        $("#"+typeId+"_Input_Msg").text(" * 不能为空 * ");
    }

    //文本框焦点离开事件
    function blur_Input(data) {
        var typeId = data.data.typeId;
        if ($("#" + typeId + "_Input").val() != "") {
            $("#" + typeId + "_Input_Msg").removeClass("errorMsg");
            $("#" + typeId + "_Input_Msg").text("");
        } else {
            showEditError(typeId);
        }
    }
    
    //文本框焦点获取事件
    function focus_Input(data) {
        var typeId = data.data.typeId;
        $("#"+typeId+"_Input_Msg").removeClass("errorMsg");
        $("#"+typeId+"_Input_Msg").text("");
    }

    //绑定文本框焦点获取事件
    $("#TrueName_Input").bind("focus", {"typeId": "TrueName" }, focus_Input);
    $("#DeviceId_Input").bind("focus", { "typeId": "DeviceId" }, focus_Input);
    $("#Phone_Input").bind("focus", { "typeId": "Phone" }, focus_Input);
    $("#Code_Input").bind("focus", { "typeId": "Code" }, focus_Input);

    //绑定文本框焦点离开事件
    $("#TrueName_Input").bind("blur", { "typeId": "TrueName" }, blur_Input);
    $("#DeviceId_Input").bind("blur", { "typeId": "DeviceId" }, blur_Input);
    $("#Phone_Input").bind("blur", { "typeId": "Phone" }, blur_Input);
    $("#Code_Input").bind("blur", { "typeId": "Code" }, blur_Input);


    //验证码图片点击更新
    $("#imgCode").click(function () {
        s = Math.random();
        $(this).attr("src", "./CreateCheckCode?s=" + s);
    });

});


//修改邮箱js
function btnOut(j) {
    if (j == 1) {
        $("#btnEmail").removeClass("mybuttonhover");
    } else if (j == 2) {
        $("#btnSendCode").removeClass("mybuttonhover");
    } else if (j == 3) {
        $("#btnUpdatePwd").removeClass("mybuttonhover");
    }
}


//邮箱input的事件
function txtEmailOnblur(typeNumber) {
    var typeId = "";
    if (typeNumber == 1) {
      
        typeId = "Email";
    } else if (typeNumber == 2) {
        typeId = "NewEmail";
    } else {
        typeId = "UpdatePwd";
    }

    if ($("#txt"+typeId).val() == "") {
        $("#msg"+typeId).text(" * 邮箱不能为空! *");

        $("#txt"+typeId+"Code").css("display", "none");
        $("#div"+typeId+"Code").addClass("tdCode");
        return -1;
    }
    else {
        $("#msg"+typeId).text("");
        if (checkEmail(typeNumber)) {
            $("#txt"+typeId+"Code").css("display", "inline");
            $("#div"+typeId+"Code").removeClass("tdCode");
            return 1;
        }
        else {
            $("#msg"+typeId).text(" * 邮箱格式不对！*");
            return -1;
        }

    }
}

//发送验证码按钮点击事件
function btnSendCodeClick(typeNumber) {
    
    var typeId = "";
    var checkType = "";
    if (typeNumber == 1) {//第一次邮箱验证
        typeId = "Email";
        checkType = "UpdateEmail";
    }
    else if (typeNumber == 2) {//新邮箱验证
        typeId = "NewEmail";
        checkType = "Register";
        //alert(typeId);
    } else {
        typeId = "UpdatePwd";
        checkType = "UpdatePwd";
    }

    if (txtEmailOnblur(typeNumber) == 1) { //邮箱地址正确可以发送验证码
        //alert("正确！");
        var data = $("#txt"+typeId).val();
        //$.get("../../Myashx/ValidateEmail.ashx", { "txtEmail": data }, function (result) {
        //    alert("验证码已经发送，请查看！" + result);
        //});
        //alert(data);
        //把发送按钮隐藏
        $("#btnSend" + typeId + "Code").addClass("myhidedisplay");
        $.post("../User/SendEmail", { "txtEmail": data, "CheckType": "Validate"+checkType }, function (result) {
            // alert("验证码已经发送，请查看！"+result);
            if (result == "error") {
                // $("#spSendCode").after("<span class='msg' id='sendCodeMsg'> * 验证码发送失败！*</span>");
                $("#msgSend"+typeId+"Code").html("<span class='msg'> * 验证码发送失败！*</span>");
            }
            else if (result == "success") {
                //$("#spSendCode").remove("#sendCodeMsg");
                //提示用户发送成功的msg
                $("#msgSend"+typeId+"Code").html("<span class='tip'> * 发送成功，注意查收！*</span>");

                ////把发送按钮隐藏
                //$("#btnSend"+typeId+"Code").addClass("myhidedisplay");

                //添加一个虚拟背景
                $("#divSend"+typeId+"Code").addClass("myhidebutton");


                var i = 30;
                function countTime() {
                    $("#spSend"+typeId+"Code").html(i + " 秒后可以重新发送！");
                    i--;
                    if (i == 0 || i < 0) {
                        clearInterval(intervalId);
                        $("#spSend"+typeId+"Code").html("");
                        $("#btnSend"+typeId+"Code").removeClass("myhidedisplay");

                        $("#divSend"+typeId+"Code").removeClass("myhidebutton");

                        $("#msgSend"+typeId+"Code").html("");
                    }
                }

                intervalId = setInterval(countTime, 1000);

            }
        });
    }
    else {
        //$("#msgSendCode").html("<span class='msg'> * 验证码发送失败！*</span>");
    }
}


//【邮箱格式验证】
function checkEmail(typeNumber) {
    var str = "";
    if (typeNumber == 1) {

        str = $("#txtEmail").val();
       
    } else if (typeNumber == 2) {
        str = $("#txtNewEmail").val();
    } else {
        str = $("#txtUpdatePwd").val();
    }

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

function myInputFocus(typeId) {
    $("#msg" + typeId).text("");
}

function myInputBlur(typeId) {
    if ($("#txt" + typeId).val() == "") {
        $("#msg" + typeId).text(" * 不能为空！");
    }
}

//【邮箱修改的下一步操作】
$("#btnUpdateEmail-One-Next").click(function () {

    var code = $("#txtEmailCode").val();
    //点击下一步验证验证码是否正确，正确就下一步，不正确就return

    if ($("#txtEmail").val() != "") {
        if (code != "") {
            $.post("../User/CheckCode", { "txtCode": code, "CodeType": "UpdateEmail" }, function (result) {
                if (result == "success") {
                    $("#btnUpdateEmail-One-Next").parents("form").next("#updateemail-two").show();
                    $("#btnUpdateEmail-One-Next").parents("#updateemail-one").hide();
                    $("#btnUpdateEmail-One-Next").parents("dd").find(".Step li:eq(1)").addClass("stepCur");
                } else if (result == "error") {
                    //alert("验证码错误！");
                    $("#msgCheckCode").text(" * 验证码错误，请重新输入！ *");
                    $("#txtCode").val("");
                }
            });
        }
        else {
            $("#msgCheckCode").text(" * 验证码不能为空！ *");
        }
    } else {
        $("#msgEmail").text(" * 邮箱不能为空! *");
    }


});
$("#btnUpdateEmail-Two-Next").click(function () {

    var code = $("#txtNewEmailCode").val();
    //点击下一步验证验证码是否正确，正确就下一步，不正确就return

    if ($("#txtNewEmail").val() != "") {
        if (code != "") {
            $.post("../User/CheckCode", { "txtCode": code, "CodeType": "Register" }, function (result) {
                if (result == "success") {
                    $("#btnUpdateEmail-Two-Next").parents("form").next("#updateemail-ok").show();
                    $("#btnUpdateEmail-Two-Next").parents("#updateemail-two").hide();
                    $("#btnUpdateEmail-Two-Next").parents("dd").find(".Step li:eq(2)").addClass("stepCur");
                } else if (result == "error") {
                    //alert("验证码错误！");
                    $("#msgNewEmailCode").text(" * 验证码错误，请重新输入！ *");
                    $("#txtNewEmailCode").val("");
                }
            });
        }
        else {
            $("#msgNewEmailCode").text(" * 验证码不能为空！ *");
        }
    } else {
        $("#msgNewEmail").text(" * 邮箱不能为空! *");
    }


});


//【修改密码的下一步操作】
$(".updatepwd-one-next").click(function () {

    var code = $("#txtUpdatePwdCode").val();
    //点击下一步验证验证码是否正确，正确就下一步，不正确就return

    if ($("#txtUpdatePwd").val() != "") {
        if (code != "") {
            $.post("../User/CheckCode", { "txtCode": code, "CodeType": "UpdatePwd" }, function (result) {
                if (result == "success") {
                    $(".updatepwd-one-next").parents("form").next("#updatepwd-two").show();
                    $(".updatepwd-one-next").parents("#updatepwd-one").hide();
                    $(".updatepwd-one-next").parents("dd").find(".Step li:eq(1)").addClass("stepCur");
                } else if (result == "error") {
                    //alert("验证码错误！");
                    $("#msgUpdatePwdCheckCode").text(" * 验证码错误，请重新输入！ *");
                    $("#txtUpdatePwdCode").val("");
                }
            });
        }
        else {
            $("#msgUpdatePwdCheckCode").text(" * 验证码不能为空！ *");
        }
    } else {
        $("#msgUpdatePwd").text(" * 邮箱不能为空! *");
    }


});
$(".updatepwd-two-next").click(function () {

    
    //点击下一步验证验证码是否正确，正确就下一步，不正确就return

    var pwd1 = $("#txtNewPwd1").val();
    var pwd2 = $("#txtNewPwd2").val();

    if (pwd1 != "" && pwd2 != "") {
        if (pwd1 == pwd2) {
            $.post("../User/CheckUpdatePwd", { "UpdatePwd1": pwd1, "UpdatePwd2":pwd2 }, function (result) {
                if (result == "success") {
                    $(".updatepwd-two-next").parents("form").next("#updatepwd-ok").show();
                    $(".updatepwd-two-next").parents("#updatepwd-two").hide();
                    $(".updatepwd-two-next").parents("dd").find(".Step li:eq(2)").addClass("stepCur");
                } else if (result == "error") {
                    //alert("验证码错误！");
                    $("#msgUpdatePwdError").text(" * 验证码错误，请重新输入！ *");
                    $("#txtNewPwd2").val("");
                }
            });
                 
        } else {
            $("#msgUpdatePwdError").text(" * 两次密码不一致！");
        }
    } else {
        if (pwd1 == "") {
            $("#msgNewPwd1").text(" * 请输入密码");
        } else {
            $("#msgNewPwd2").text(" * 请重新输入密码");
        }
    }


});



$("#aUpdateEmail").click(function () {
    if($("#isShow_UpdateEmail").val() == 0){
        $("#isShow_UpdateEmail").val(1);
        if ($("#isShow_UpdatePwd").val() == 1) {
            $("#aUpdatePwd").click();
            $("#isShow_UpdatePwd").val(0);
        }
    }else{
        $("#isShow_UpdateEmail").val(0);
    }
});

$("#aUpdatePwd").click(function () {
    if ($("#isShow_UpdatePwd").val() == 0) {
        $("#isShow_UpdatePwd").val(1);
        if ($("#isShow_UpdateEmail").val() == 1) {
            $("#aUpdateEmail").click();
            $("#isShow_UpdateEmai;").val(0);
        }
    } else {
        $("#isShow_UpdatePwd").val(0);
    }
});

 //###############################【20160417 之后修改】###########################################