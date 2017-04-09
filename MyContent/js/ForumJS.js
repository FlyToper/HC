$(function () {
    $('[data-toggle="tooltip"]').tooltip({ html: true });

});


//【论坛类型的跳转方法】
function JumpByForumType( pageIndex, pageSize, forumType) {
    $("#Forum_Main").html("");
    $("#Forum_Refresh").css("display", "");
    $("#CurrentLocation").html("");//清空当前位置的信息

    //开始请求相关信息
    $.post("/Forum/Show", { "forumType": forumType, "pageIndex": pageIndex, "pageSize": pageSize }, function (data) {
        changeMainContent(forumType);//展示初始化信息

        //判断当前论坛类型是否有数据，如果数据总条数为0就不走下一步，直接return
        if (data["Total"] <= 0) {
            $("#Forum_Main").html("暂无数据");
            return;
        }
        //alert(data["Row"][0].SubName);
        //拼接首行
        var s = "<div class='MyForumDIV'> ";
        s += "<div class='row MyForumDIV_Head'><div class='col-md-6 col-xs-6'><span class='glyphicon glyphicon-subtitles'></span> 标题</div> <div class='col-md-2 col-xs-2'><span class='glyphicon glyphicon-user'></span> 作者</div> <div class='col-md-2 col-xs-2'><span class='glyphicon glyphicon-comment'></span> 回复</div><div class='col-md-2 col-xs-2'><span class='glyphicon glyphicon-time'></span> 发布时间</div></div><hr>";

        //遍历全部数据，处理展示
        for (var i = 0; i < data["Total"]; i++) {
            var k = parseInt(i);
            //alert(data["Total"]);

            //单双行切换样式
            if (k % 2 == 1) {
                s += "<div class='row MyForum_Tr1' ><div class='col-md-6 col-xs-6'><a href='/Forum/ShowDetail?Id=" + data["Row"][i].Id + "' target='_blank'> " + data["Row"][i].Title + "</a></div><div class='col-md-2 col-xs-2'><a href='javascript:void(0); '>" + data["Row"][i].SubName + "</a></div><div class='col-md-2 col-xs-2'>" + data["Row"][i].FloorCount + "</div><div class='col-md-2 col-xs-2'>" + data["Row"][i].SubDate + "</div></div><hr>";
            } else {
                s += "<div class='row MyForum_Tr2' ><div class='col-md-6 col-xs-6'><a href='/Forum/ShowDetail?Id=" + data["Row"][i].Id + "' target='_blank'> " + data["Row"][i].Title + "</a></div><div class='col-md-2 col-xs-2'><a href='javascript:void(0);'>" + data["Row"][i].SubName + "</a></div><div class='col-md-2 col-xs-2'>" + data["Row"][i].FloorCount + "</div><div class='col-md-2 col-xs-2'>" + data["Row"][i].SubDate + "</div></div><hr>";
            }
        }

        //添加导航信息
        s += "</div>";
        s += "<div class='MyForum_PageNumber'>" + data["PageNumber"] + "</div>";
        $("#Forum_Main").html(s);
    });
}


//根据类型标题进行跳转
function JumpByForumTitle(title) {
    if (title == "YYMS") {
        JumpByForumType(1, 20, 1);
    } else if (title == "SSMT") {
        JumpByForumType(1, 20, 2);
    } else if (title == "JKBD") {
        JumpByForumType(1, 20, 3);
    } else if (title == "YEBD") {
        JumpByForumType(1, 20, 4);
    } else if (title == "YLZP") {
        JumpByForumType(1, 20, 5);
    } else if (title == "NRNR") {
        JumpByForumType(1, 20, 6);
    } else {
        window.location.href = "/Forum/Index";
    }
}

function JumpByNumber(id) {
    JumpByForumType(1, 20, id);
}

//【展示相关主要内容的方法】
function changeMainContent(forumType) {
    if(forumType ==1){
        $("#CurrentLocation").html("营养美食");
        $("#Forum_Main").html("营养美食");
        $("#PostForum").html("<a class='btn btn-success' href='/Forum/PostForum?forumType=1' target='_blank'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>");//【我要发帖】
    } else if (forumType == 2) {
        $("#CurrentLocation").html("塑身美体");
        $("#Forum_Main").html("塑身美体");
        $("#PostForum").html("<a class='btn btn-success' href='/Forum/PostForum?forumType=2' target='_blank'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>");//【我要发帖】
    } else if (forumType == 3) {
        $("#CurrentLocation").html("健康宝典");
        $("#Forum_Main").html("健康宝典");
        $("#PostForum").html("<a class='btn btn-success' href='/Forum/PostForum?forumType=3' target='_blank'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>");//【我要发帖】
    } else if (forumType == 4) {
        $("#CurrentLocation").html("育儿宝典");
        $("#Forum_Main").html("育儿宝典");
        $("#PostForum").html("<a class='btn btn-success' href='/Forum/PostForum?forumType=4' target='_blank'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>");//【我要发帖】
    } else if (forumType == 5) {
        $("#CurrentLocation").html("娱乐杂评");
        $("#Forum_Main").html("娱乐杂评");
        $("#PostForum").html("<a class='btn btn-success' href='/Forum/PostForum?forumType=5' target='_blank'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>");//【我要发帖】
    } else if (forumType == 6) {
        $("#CurrentLocation").html("男人女人");
        $("#Forum_Main").html("男人女人");
        $("#PostForum").html("<a class='btn btn-success' href='/Forum/PostForum?forumType=6' target='_blank'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>");//【我要发帖】
    }
}


//20170402 下拉框的事件
function CateSelect(type) {
    var catename = "";
    if (type == 1) {
        catename = "营养美食";
    } else if (type == 2) {
        catename = "塑身美体";
    } else if (type == 3) {
        catename = "健康宝典";
    } else if (type == 4) {
        catename = "育儿宝典";
    } else if (type == 5) {
        catename = "娱乐杂评";
    } else if (type == 6) {
        catename = "男人女人";
    } else {
        $("#txtCName").val("0");
        $("#selectedCate").html("").hide();
        return;
    }

    $("#txtCName").val(type);//设置类型id
    $("#selectedCate").html('<span class="glyphicon glyphicon-ok"></span> <span class="catename">' + catename + '</span>');
    $("#selectedCate").show();
}

function TimeSelect(type) {
    var time = "";
    if (type == 1) {
        time = "最近一天";
    } else if (type == 2) {
        time = "最近一周";
    } else if (type == 3) {
        time = "最近一个月";
    } else if (type == 4) {
        time = "最近一年";
    } else if (type == 5) {
        time = "更久";
    } else {
        $("#selectedTime").html("").hide();
        $("#txtSTime").val("0");
        return;
    }

    $("#txtSTime").val(type);//设置时间ID

    $("#selectedTime").html('<span class="glyphicon glyphicon-ok"></span> <span class="time">' + time + '</span>');
    $("#selectedTime").show();
}

//查找所有资讯
$("#btnSearchAll").click(function () {
    var $btn = $(this).button('loading');

    setTimeout(function () {
        Search(1, 1, 5, 0, 0, 0);
       $btn.button('reset');
    }, 1000)
    
});

//精确查找
$("#btnSearchFocus").click(function () {
    var $btn = $(this).button('loading');

    var cname = $("#txtCName").val();//分类名称
    var time = $("#txtSTime").val();//时间限制
    var name = $("#txtUName").val();//发表人名称

    if (name == undefined || name == "")
        name = 0;

    //alert(cname + "#" + time + "#" + name);

    //$btn.button('reset');
    //return;
    setTimeout(function () {
        Search(2, 1, 5, cname, time, name);
        $btn.button('reset');
    }, 1000)
});



//返回论坛首页
function Reback() {
    window.location.href = "/Forum/Index";
}


/**
 * 快速搜索
 * 20170402
 *
 * @param type 查询类型：全部查找、精确查找
 * @param index 页码:1
 * @param size  每页条数:10
 * @param cname 分类名字 :YYMS
 * @param ltime 时间:最近一周
 * @param name  发表人 :小王
 * 
 */
function Search(type, index, size, cname, ltime, name) {
    $.post("/Forum/Search", { "type": type, "pageIndex": index, "pageSize": size, "cname": cname, "ltime": ltime, "name": name }, function (data) {

        $(".main_content").html("");
        if (data["Total"] <= 0) {
            $(".main_content").html("<div class='list_content'><a class='btn btn-info' href='/Forum/Index'><span class='glyphicon glyphicon-refresh'></span>返回论坛首页</a> 暂无数据 </div>");
            return;
        }
        var str = "<div class='list_content'><div>&nbsp;&nbsp;<button onclick='Reback()' class='btn btn-info'><span class='glyphicon glyphicon-refresh'></span> 返回论坛首页</button>&nbsp;&nbsp;<a class='btn btn-success' target='_blank' href='/Forum/PostForum'><span class='glyphicon glyphicon-comment'></span> 我要发帖</a>&nbsp;&nbsp;共找到<span style='color:red'>" + data["AllTotal"] + "</span>条数据 </div> <hr style='border:1px solid gray;'>";

        //表格标题行
        str += "<div class='row'><div  class='col-md-1 no-wrap-line' title='@forumInfo.Title'><span class='glyphicon glyphicon-sort-by-order'></span> 序号</div><div  class='col-md-3 no-wrap-line' title='@forumInfo.Title'><span class='glyphicon glyphicon-subtitles'></span> 标题</div><div class='col-md-3'><span class='glyphicon glyphicon-user'></span> 发表人</div><div class='col-md-3'><span class='glyphicon glyphicon-time'></span> 发表时间</div> <div class='col-md-2 no-wrap-line' ><span class='glyphicon glyphicon-sort-by-attributes'></span> 分类</div></div><hr style='border:1px solid gray;'>";
        for (var i = 0; i < data["Total"]; i++) {
            str += "<div class='row'> <div  class='col-md-1 no-wrap-line' title='@forumInfo.Title'> " + data["Row"][i].SortNum + "</div><div  class='col-md-3 no-wrap-line' title='" + data["Row"][i].Title + "'><a href='/Forum/ShowDetail?Id=" + data["Row"][i].Id + "' target='_blank'>" + data["Row"][i].Title + "</a></div><div class='col-md-3'>" + data["Row"][i].SubName + "</div><div class='col-md-3'>" + data["Row"][i].SubDate + "</div> <div class='col-md-2 no-wrap-line' >" + data["Row"][i].Type + "</div></div>";

            if (i != data["Total"] - 1) {
                str += "<hr>";
            } else {
                str += "<hr style='border:1px solid gray;'>";
            }
        }
        str += "<div class='row dPageLink'>"+data["PageNumber"]+"</div>";
        str += "</div>";
       
        $(".main_content").html(str);
       

       
    });
    
}