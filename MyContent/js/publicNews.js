$(function () {
    
});

//标题
$("#txtTitle").keyup(function () {
    if ($(this).val() != "") {
        $(".top_title").html("<span class='glyphicon glyphicon-star hot-star'></span>" +$(this).val());
    } else {
        $(".top_title").html("<span class='glyphicon glyphicon-star hot-star'></span>" + "标题");
    }
});


//描述
$("#txtDescription").keyup(function () {
    if ($(this).val() != "") {
        $(".top_description").html($(this).val());
    } else {
        $(".top_description").html( "描述");
    }
});

//上传图片
function ajaxFileupload() {
    //图片验证
    var x = document.getElementById("uploadImage");
    if (!x || !x.value)
        return;

    var path = /\.jpg$|\.jpeg$|\.png$|\.gif$/i;
    if (!path.test(x.value)) {
        show("警告", "您选择的似乎不是图像文件！");
        x.value = "";
        return;
    }

    var elementIds = ["uploadImage"];

    $.ajaxFileUpload({
        url: '/Public/SaveImage',
        type: 'post',
        secureuri: false,
        dataType: 'text',
        fileElementId:'uploadImage',
        elementIds: elementIds,

        success: function (data, status) {
            if (data == "error1") {
                show("警告","文件太大，请上传小于5M图片!");
                return;
            } else if (data == "error2") {
                show("警告","上传失败，请重试！");
                return;

            } else {
                //上传成功
                $(".top_image").attr("src", data);
                $("#txtImage").val(data);
                $(".ok").fadeIn(2000);
            }
        },

        error: function (data, status) {
            //alert(data);
            show("警告", data);
        }
    });
}



//设置封面图片按钮
function setImage() {
    $("#uploadImage").click();
}


//执行上传操作
function submit() {
    //1、数据验证
    var title = $("#txtTitle").val();
    var description = $("#txtDescription").val();
    var from = $("#txtFrom").val();
    var image = $("#txtImage").val();
    var content = CKEDITOR.instances.t1.getData();

    if (title == "" || description == "" || from == "" || image == "" || content == "") {
        show("警告", "请填写完整信息！");
        return;
    }

    $.post("/Admin/SaveNews", { "title": title, "description": description, "from": from, "image": image, "content": content }, function (rst) {
        if (rst == "success") {
            show("成功", "提交成功！");
        } else if (rst == "error1") {
            document.location.href = "/Admin/Login";
        } else {
            show("警告", "提交失败，请重试！");
        }
    });

   //show("提示",title+"#"+description+"#"+from+"#"+image+"#"+content);
}