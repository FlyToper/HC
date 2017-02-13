
//邮箱input的事件
function txtEmailOnblur() {
    if ($("#txtEmail").val() == "") {
        $("#msgEmail").text(" * 邮箱不能为空! *");

        $("#txtCode").css("display", "none");
        $("#divCode").addClass("tdCode");
        return -1;
    }
    else {
        if (checkEmail()) {
            //发送到ashs去发送邮件
            //$.post("~/ashx/ValidateEmail.ashx", $("#txtEmail").val(), function () { });
            //alert("邮箱格式正确！"); 
            $("#txtCode").css("display", "inline");
            $("#divCode").removeClass("tdCode");
            return 1;
        }
        else {
            $("#msgEmail").text(" * 邮箱格式不对！*");
            return -1;
        }
       
    }
   
}

//发送验证码按钮点击事件
function btnSendCodeClick() {
    if (txtEmailOnblur() == 1) { //邮箱地址正确可以发送验证码
        //alert("正确！");
        var data = $("#txtEmail").val();
        //$.get("../../Myashx/ValidateEmail.ashx", { "txtEmail": data }, function (result) {
        //    alert("验证码已经发送，请查看！" + result);
        //});

        $.post("../../Myashx/ValidateEmail.ashx", { "txtEmail":data,"CheckType":"ValidateFindEmail"}, function (result) {
            // alert("验证码已经发送，请查看！"+result);
            if (result == "error") {
                // $("#spSendCode").after("<span class='msg' id='sendCodeMsg'> * 验证码发送失败！*</span>");
                $("#msgSendCode").html("<span class='msg'> * 验证码发送失败！*</span>");
            }
            else if (result == "success") {
                //$("#spSendCode").remove("#sendCodeMsg");
                //提示用户发送成功的msg
                $("#msgSendCode").html("<span class='tip'> * 发送成功，注意查收！*</span>");
                
                //把发送按钮隐藏
                $("#btnSendCode").addClass("myhidedisplay");
                
                //添加一个虚拟背景
                $("#divSendCode").addClass("myhidebutton");
                

                i =30;
                function countTime() {
                    $("#spSendCode").html(i + " 秒后可以重新发送！");
                    i--;
                    if (i == 0) {
                        clearInterval(intervalId);
                        $("#spSendCode").html("");
                        $("#btnSendCode").removeClass("myhidedisplay");

                        $("#divSendCode").removeClass("myhidebutton");

                        $("#msgSendCode").html("");
                    }
                }

              intervalId =  setInterval(countTime, 1000);

            }
        });
    }
    else {
        //$("#msgSendCode").html("<span class='msg'> * 验证码发送失败！*</span>");
    }
}


//function btnOneNext() {
//    txtEmailOnblur();
//    //去服务端验证验证码，
//}


function txtEmailKeydown() {
    $(".msg").html("");
}

function btnOver(i) {
    if (i == 1) {
        $("#btnEmail").addClass("mybuttonhover");
    }
    else if (i == 2) {
        $("#btnSendCode").addClass("mybuttonhover");
    } else if (i == 3) {
        $("#btnUpdatePwd").addClass("mybuttonhover");
    }
}

function btnOut(j) {
    if (j == 1) {
        $("#btnEmail").removeClass("mybuttonhover");
    } else if (j == 2) {
        $("#btnSendCode").removeClass("mybuttonhover");
    } else if (j == 3) {
        $("#btnUpdatePwd").removeClass("mybuttonhover");
    }
}


function checkEmail() {
    str = $("#txtEmail").val();
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

$("#txtCode").bind("keydown", function () {
    $("#msgCheckCode").text("");
});


//安全设置 手机号码下一步	
$(".sjyz-one-next").click(function () {

    var code = $("#txtCode").val();
    //点击下一步验证验证码是否正确，正确就下一步，不正确就return

    if ($("#txtEmail").val() != "") {
        if (code != "") {
            $.post("../../Myashx/ValidateCode.ashx", { "txtCode": code,"CodeType":"Find" }, function (result) {
                if (result == "success") {
                    $(".sjyz-one-next").parents("form").next(".sjyz-two").show();
                    $(".sjyz-one-next").parents(".sjyz-one").hide();
                    $(".sjyz-one-next").parents("dd").find(".Step li:eq(1)").addClass("stepCur");
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


$("#btnUpdatePwd").click(function () {
    var pwd1 = $("#txtPwd1").val();
    var pwd2 = $("#txtPwd2").val();

    if (pwd1 == "") {
        $("#msgPwd1").text(" * 密码不能为空！ *");
        return;
    }
    if (pwd2 == "") {
        $("#msgPwd2").text(" * 重新输入密码不能为空！*");
        return;
    }

    if (pwd1 == pwd2) {
        //发送修改密码
        $.post("../../Myashx/UpdatePwd.ashx", { "txtPwd1": pwd1, "txtPwd2": pwd2,"txtEmail":$("#txtEmail").val() }, function (result) {
            if (result == "success") {
                $("#btnUpdatePwd").parents(".sjyz-two").hide();
                $("#btnUpdatePwd").parents("form").prev(".sjyz-one").hide();
                $("#btnUpdatePwd").parents("dd").find(".sjyz-ok").show();
                $("#btnUpdatePwd").parents("dd").find(".Step li:eq(2)").addClass("stepCur");
            } else if (result == "error") {
                $("#msgUpdateError").text(" * 修改密码失败,请重新修改！* ");
                $("#msgPwd1").text("");
                $("#msgPwd2").text("");
                $("#txtPwd1").val("");
                $("#txtPwd2").val("");
            }
        });
    } else {
        $("#msgPwd2").text(" * 两次密码不一致！ *");
    }

    
});

////////////////////////////////  $function
$(function () {
   
    

   // checkEmail("contact@cnblogs.com");//调用


    function myfunction() {
        $(this).find("b").toggle();
        $(this).find("i").toggle();
        $(this).parents("dt").next("dd").toggle().siblings("dd").show();
    }
    //document.onload = myfunction;


   // setTimeout('myfunction()', 1000);
    //$(this).parents(".sjyz-one").show();
    //$(this).parents("dt").next("dd").toggle().siblings("dd").show();
    //最后一个没有边框
    $("nav dl:last,.shouyi dl:last").css("border", "0");
    //.bankList li 银行选择	
    $(".bankList li").click(function () {
        $(this).addClass("banSty").siblings("li").removeClass("banSty");
    });
    //添加银行卡	
    $(".tianjiayinhang").click(function () {
        $(".addYinhang").fadeIn();
    });
    $(".glyphicon-remove").click(function () {
        $(".addYinhang").fadeOut();
    });

    //站内信息 点击展开收起效果	
    $(".znxx dt em").click( myfunction() 
        //$(this).find("b").toggle();
        //$(this).find("i").toggle();
        //$(this).parents("dt").next("dd").toggle().siblings("dd").show();
    );


    ////安全设置 手机号码下一步	
    //$(".sjyz-one-next").click(function () {
    //    //txtEmailOnblur();
    //    //alert("asdasd");
    //    var code = $("#txtCode").val();
    //    //点击下一步验证验证码是否正确，正确就下一步，不正确就return
    //    $.post("../../Myashx/ValidateCode.ashx", { txtCode: code }, function (result) {
    //        if (result == "success") {
    //            alert("验证码正确！");
    //        } else {
    //            alert("验证码错误！");
    //        }
    //    });

    //    //$(this).parents("form").next(".sjyz-two").show();
    //    //$(this).parents(".sjyz-one").hide();
    //    //$(this).parents("dd").find(".Step li:eq(1)").addClass("stepCur");
    //});


    //$(".sjyz-two-next").click(function () {
    //    $(this).parents(".sjyz-two").hide();
    //    $(this).parents("form").prev(".sjyz-one").hide();
    //    $(this).parents("dd").find(".sjyz-ok").show();
    //    $(this).parents("dd").find(".Step li:eq(2)").addClass("stepCur");
    //});

    function myfun2() {
        $(this).parents("dt").next("dd").toggle().siblings("dd").hide();
    }

    // 安全设置 点击展开收起效果	
    $(".Safety dt em").click(myfun2
        //$(this).parents("dt").next("dd").toggle().siblings("dd").hide();
    );
});
	
	
function next()
{
document.write("<a href='javascript:history.go(-1)'>后退</a>   &nbsp;&nbsp;<a href='javascript:history.go(0)'>刷新</a>&nbsp;&nbsp;<a href='javascript:history.go(1)'>前进</a>   <form>   <input name='ht' type='button' onclick='javascript:history.go(-1)' value='后退' />   <input name='sx' type='button' onclick='javascript:history.go(0)' value='刷新' />   <input name='qj' type='button' onclick='javascript:history.go(1)' value='前进' />   </form> ");
}	