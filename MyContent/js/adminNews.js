//修改状态
function ChangeStatus(id, status) {
    //alert(id + "++" + status);

    if (status == 12 && $("#txtHotTotal").val() > 3) {
        show("警告", "最多可以设置3个热门资讯，可以尝试把旧的热门资讯设为普通资讯!");
        return;
    }

    
    $.post("/AdminFun/ChangeNewsStatus", { "id": id, "status": status }, function (rst) {
        if (rst == "success") {
            show("系统提示", "修改成功！");
            setTimeout(function () {
                window.location.href = "/Admin/NewsRecord";
            }, 1000);
        } else if (rst == "error2") {
            show("警告", "请先登录!");
            setTimeout(function () {
                window.location.href = "/Admin/Login";
            }, 1000);
        } else if (rst == "error5") {
            show("警告", "最多可以设置3个热门资讯，可以尝试把旧的热门资讯设为普通资讯!");
        } else {
            show("警告", "系统出错，请重试！");
        }
    });
}


//设置热门信息
function SetHot(id, status) {
    //alert(id + "===" + status);
}

//编辑资讯
function EditNews(id) {
    //alert(123);
    //<textarea rows="30" cols="50" class="ckeditor" id="t1">Plase input...</textarea>
   
    //changeShow("数据正在加载中...", "<img style='margin-left:25%;' width='50%' src='/MyContent/images/loading.gif'>", 0);
    //发表时间： 2017/3/9 17:19:35来源：管理员
    $.post("/AdminFun/GetNewsInfo", { "id": id }, function (data) {
        var title = "资讯编辑";
        
        var content = "<fieldset style='border: 1px solid #99ccff; border-radius: 6px;'>                    <legend>请编辑内容</legend>                    <div class='edit-main'>                        <div class='edit-left'>                            <div class='input-group input-group-lg'>                                <span class='input-group-addon'>资讯&nbsp;标题&nbsp;</span>                                <input type='text' class='form-control ' placeholder='Title'                                        id='txtTitle' value='"+data["Title"]+"' title='"+data["Title"]+"'>                            </div>                            <br>                            <div class='input-group input-group-lg'>                                <span class='input-group-addon' >简单&nbsp;描述&nbsp;</span>                                <input type='text' class='form-control' placeholder='Description'                                        id='txtDescription' value='"+data["Description"]+"' title='"+data["Description"]+"' >                            </div>                            <br>                            <div class='input-group input-group-lg'>                                <span class='input-group-addon'>来源(作者)</span>                                <input type='text' class='form-control ' placeholder='From'                                        id='txtFrom' value='"+data["From"]+"' title='"+data["From"]+"'>                            </div>                            <br>                            <br>                            <div class='input-group input-group-lg'>                                                               <button title='点击选择图片上传，设置为封面图片' class='btn-lg btn-info' onclick='setImage()'>设置封面图片</button> <span class='glyphicon glyphicon-ok-sign ok'></span>                                                                <input type='file' alt='上传封面图片' class='' placeholder='From' id='uploadImage' onchange='ajaxFileupload()' name='uploadImage'  style='display:none;'>                                <input type='hidden' id='txtImage' value='"+data["ImageUrl"]+"' />                            </div>                            <br>                                                                            </div>                        <!--------------------【左右分界线】-------------------------->                        <div class='edit-right'>                            <div class='news_top'>                                <p style='font-size:25px;' class='top_title wrap-line' title='"+data["Title"]+"'><span class='glyphicon glyphicon-star hot-star'></span> "+data["Title"]+"</p>                                <p class='word1 top_date'>"+data["SubDate"]+"</p>                                                               <img src='"+data["ImageUrl"]+"' width='100%' class='top_image' />                                <p class='word1 wrap-line top_description' style='margin-top:4px;margin-bottom:4px;' title='"+data["Description"]+"'>"+data["Description"]+"</p>                                <br>                                <p>查看全文</p>                            </div>                        </div>                    </div>                    <br>                    <div>                        <textarea rows='30' cols='50' class='ckeditor' id='t1'>                            <span style='font-size: 30px'>"+data["Content"]+"</textarea>                    </div>                            </fieldset>";
        changeShow(title, content, 1);
        $("#showBtn2").html("<button type='button' class='btn btn-info' id='btnSave'>保存</button><button type='button' class='btn btn-warning' data-dismiss='modal' id='btnClose'>关闭</button>")
        CKEDITOR.replace('t1');
        if(data["ImageUrl"] != null && data["ImageUrl"] !="")
            $(".ok").fadeIn(2000);

        $("#txtTitle").keyup(titleKeyUp);
        $("#txtDescription").keyup(descKekup);
        $("#btnSave").click({"id":id},save);
    });  
}

function save(event) {
   
    
    //1、数据验证
    var id = event.data.id;
    var title = $("#txtTitle").val();
    var description = $("#txtDescription").val();
    var from = $("#txtFrom").val();
    var image = $("#txtImage").val();
    var content = CKEDITOR.instances.t1.getData();

    if (title == "" || description == "" || from == "" || image == "" || content == "") {
        show("警告", "请填写完整信息！");
        return;
    }

    $.post("/AdminFun/SaveNewsInfo", { "id":id,"title": title, "description": description, "from": from, "image": image, "content": content }, function (rst) {
        if (rst == "success") {
            $("#btnClose").click()
            show("成功", "提交成功！");
        } else if (rst == "error1") {
            document.location.href = "/Admin/Login";
        } else {
            show("警告", "提交失败，请重试！");
        }
    });
}


function LoadNesInfo() {
    
}