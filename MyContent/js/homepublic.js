$(function () {
    //show("123", "456");
    //<div class='row row-body'><div class='col-md-2 col-sm-12' style='color:#ff6a00'>暂无数据</div></div> 
    //NotifyList();
});

function NotifyList() {
    
    $.post("/Notify/List", {}, function (data) {

        if (data["Total"] <= 0) {
            content = " <div class='row row-body'><div class='col-md-2 col-sm-12' style='color:#ff6a00'>暂无数据</div></div> ";
            show("我的通知", content);
            return;
        }
        var title = "我的通知";
        var content = "<div style='color:glay' class='myRowTable'><div class='row row-head'><div class='col-md-3 col-sm-12'><span class='glyphicon glyphicon-sort-by-order'></span> 序号</div><div class='col-md-3 col-sm-12'><span class='glyphicon glyphicon-subtitles'></span> 标题</div><div class='col-md-3 col-sm-12'><span class='glyphicon glyphicon-comment'></span> 通知内容</div> <div class='col-md-3 col-sm-12 row-center'><span class='glyphicon glyphicon-time'></span> 发送时间</div></div><hr>";
        for (var i = 0; i < data["Total"]; i++) {
            content += " <div class='row row-body' style='color:glay'><div class='col-md-3 col-sm-12'>" + (i + 1) + "</div><div class='col-md-3 col-sm-12'><span class='wrap-line' title='" + data["Row"][i].Title + "'>" + data["Row"][i].Title + "</span> </div><div class='col-md-3 col-sm-12'><span class='wrap-line' title='" + data["Row"][i].Content + "'>" + data["Row"][i].Content + "</span> </div><div class='col-md-3 col-sm-12 row-center'><span class='wrap-line' title='" + getLocalTime(data["Row"][i].SubDate) + "'>" + getLocalTime(data["Row"][i].SubDate) + "</span> </div></div> <div class='row-foot'></div> </div>";
        }
        $("#showBtn").html("<button type='button' class='btn btn-info' onclick='Runread()' >全部标记为已读</button> <button type='button' class='btn btn-default' data-dismiss='modal'>关闭</button>");
        show(title, content);
    });
}

function Runread() {
    //alert(12322);
    $.post("/Notify/ReadAll", {}, function (rst) {
        if (rst == "success") {
            //alert("suucess");
            location.reload();
        } else {
            alert("error");
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

function getLocalTime(nS) {
    var ts = nS.replace("/Date(", "").replace(")/", "");
    return new Date(parseInt(ts)).toLocaleString().replace(/年|月/g, "/").replace(/日/g, " ").replace(/上午|下午/g, "");
}