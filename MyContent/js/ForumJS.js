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
        s += "<div class='row MyForumDIV_Head'><div class='col-md-6'>标题</div> <div class='col-md-2'>作者</div> <div class='col-md-2'>回复</div><div class='col-md-2'>发布时间</div></div>";

        //遍历全部数据，处理展示
        for (var i = 0; i < data["Total"]; i++) {
            var k = parseInt(i);
            //alert(data["Total"]);

            //单双行切换样式
            if (k % 2 == 1) {
                s += "<div class='row MyForum_Tr1' ><div class='col-md-6'><a href='/Forum/ShowDetail?Id="+data["Row"][i].Id+"' target='_blank'> " + data["Row"][i].Title + "</a></div><div class='col-md-2'><a href='javascript:void(0); '>" + data["Row"][i].SubName + "</a></div><div class='col-md-1'>" + data["Row"][i].FloorCount + "</div><div class='col-md-3'>" + data["Row"][i].SubDate + "</div></div>";
            } else {
                s += "<div class='row MyForum_Tr2' ><div class='col-md-6'><a href='/Forum/ShowDetail?Id=" + data["Row"][i].Id + "' target='_blank'> " + data["Row"][i].Title + "</a></div><div class='col-md-2'><a href='javascript:void(0);'>" + data["Row"][i].SubName + "</a></div><div class='col-md-1'>" + data["Row"][i].FloorCount + "</div><div class='col-md-3'>" + data["Row"][i].SubDate + "</div></div>";
            }
        }

        //添加导航信息
        s += "</div>";
        s += "<div class='MyForum_PageNumber'>" + data["PageNumber"] + "</div>";
        $("#Forum_Main").html(s);
    });
}


//【展示相关主要内容的方法】
function changeMainContent(forumType) {
    if(forumType ==1){
        $("#CurrentLocation").html("营养美食");
        $("#Forum_Main").html("营养美食");
        $("#PostForum").html("<a href='/Forum/PostForum?forumType=1' target='_blank'>我要发帖</a>");//【我要发帖】
    } else if (forumType == 2) {
        $("#CurrentLocation").html("塑身美体");
        $("#Forum_Main").html("塑身美体");
        $("#PostForum").html("<a href='/Forum/PostForum?forumType=2' target='_blank'>我要发帖</a>");//【我要发帖】
    } else if (forumType == 3) {
        $("#CurrentLocation").html("健康宝典");
        $("#Forum_Main").html("健康宝典");
        $("#PostForum").html("<a href='/Forum/PostForum?forumType=3' target='_blank'>我要发帖</a>");//【我要发帖】
    } else if (forumType == 4) {
        $("#CurrentLocation").html("育儿宝典");
        $("#Forum_Main").html("育儿宝典");
        $("#PostForum").html("<a href='/Forum/PostForum?forumType=4' target='_blank'>我要发帖</a>");//【我要发帖】
    } else if (forumType == 5) {
        $("#CurrentLocation").html("娱乐杂评");
        $("#Forum_Main").html("娱乐杂评");
        $("#PostForum").html("<a href='/Forum/PostForum?forumType=5' target='_blank'>我要发帖</a>");//【我要发帖】
    } else if (forumType == 6) {
        $("#CurrentLocation").html("男人女人");
        $("#Forum_Main").html("男人女人");
        $("#PostForum").html("<a href='/Forum/PostForum?forumType=6' target='_blank'>我要发帖</a>");//【我要发帖】
    }
}

