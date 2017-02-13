
//文件上传
function ajaxFileUpload() {

    //图片格式验证
    var x = document.getElementById("uploadImage");
    if (!x || !x.value) return;
    var patn = /\.jpg$|\.jpeg$|\.png$|\.gif$/i;
    if (!patn.test(x.value)) {
        alert("您选择的似乎不是图像文件。");
        x.value = "";
        return;
    }

    var elementIds = ["uploadImage"]; //flag为id、name属性名
    $.ajaxFileUpload({
        url: '/Forum/SaveImage',//上传的url，根据自己设置
        type: 'post',
        secureuri: false, //一般设置为false
        fileElementId: 'uploadImage', // 上传文件的id、name属性名
        dataType: 'text', //返回值类型，一般设置为json、application/json
        elementIds: elementIds, //传递参数到服务器
        success: function (data, status) {
            //alert(data);
            if (data == "Error1") {
                alert("文件太大，请上传不大于5M的文件！");
                return;
            } else if (data == "Error2") {
                alert("上传失败，请重试！");
                return;
            } else {
                //这里为上传并做一下请求显示处理，返回的data是对应上传的文件名
                $("#ReplyContent").append("<img width='300' height='300' src='" + "../UploadFile/ForumImages/" + data + "'/>");

            }
        },
        error: function (data, status, e) {
            alert(e);
        }
    });
    //return false;
}

//统计“对我有用”和“丢个板砖”的次数
/*
*
*@param type 信息类型
*
*@param conuntType 需要统计的类型
*
*@param id 信息的id
*/
function CountUsefulAndUseless(type, countType, id) {
    //alert(type + "___" + countType + "____" + id);
    var selector = "";
    if (type == 1) {
        selector = "";
    } else if (type == 2) {
        selector = "Answer_";
    }

    $.post("/Forum/Count", { "type": type, "countType": countType, "id": id }, function (rst) {
        if (rst == "success") {
                if (countType == 1) {
                    var currentSpan = $("#"+selector+"usefulCount_" + id);
                    var usefulCount = parseInt(currentSpan.html());
                    currentSpan.html(usefulCount + 1);

                    currentSpan.parent("a").removeAttr("onclick");
                    $("#" + selector + "uselessCount_" + id).parent("a").removeAttr("onclick");

                } else if (countType == 2) {
                    var currentSpan = $("#" + selector + "uselessCount_" + id);
                    var usefulCount = parseInt(currentSpan.html());
                    currentSpan.html(usefulCount + 1);

                    currentSpan.parent("a").removeAttr("onclick");
                    $("#" + selector + "usefulCount_" + id).parent("a").removeAttr("onclick");
                }
        }
    });
}


//回复验证码
$("#imgReplyCode").click(function () {
    s = Math.random();
    $(this).attr("src", "./CreateReplyCode?s=" + s);
});


$("#txtReplyCode").bind("blur", {}, function(){
    if ($(this).val() == "") {
        $("#msgReplyCode").show();
        $("#msgReplyCode").html("不能为空！");
    }
});


$("#txtReplyCode").bind("focus", {}, function () {
    $("#msgReplyCode").hide();
    $("#msgReplyCode").html("");
});

$("#t1").bind("focus", {}, function () {
    $("#msgReplyCode").hide();
    $("#msgReplyCode").html("");
});

//上传回复内容
function subReplyContent(id) {
    var code = $("#txtReplyCode").val();
    if (code != "") {
        var content = CKEDITOR.instances.t1.getData();
        if (content != "") {
            //执行上传
            content = CKEDITOR.instances.t1.getData();
            //alert(content);
            $.post("/Forum/SubReplContent", { "code": code, "replyContent": content, "id": id }, function (rst) {
                if (rst == "success") {
                    window.location.href = "/Forum/Showdetail?id=" + id;
                } else if (rst == "error1") {
                    $("#msgReplyCode").show();
                    $("#msgReplyCode").html("验证码不正确！");
                }
            });
        } else {
            $("#msgReplyCode").show();
            $("#msgReplyCode").html("回复内容不能为空！");
        }
        

    } else {
        $("#msgReplyCode").show();
        $("#msgReplyCode").html("不能为空！");
    }
}