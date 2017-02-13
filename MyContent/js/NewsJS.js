/*
*新闻健康资讯JS
*
*/

$(function () {
    $('[data-toggle="tooltip"]').tooltip({ html: true });
    Load(1, 10);
    
});

/*
*加载分页
*
*@param pageIndex 页码
*@param pageSize 每页条数
*
*/
function Load(pageIndex, pageSize) {

    $.post("/News/LoadNewsInfo", { "pageIndex": pageIndex, "pageSize": pageSize }, function (rst) {
        var content = $("#news_list_table");//获取内容div
        var total = rst["Total"];
        if (total <= 0){
            content.html("<h2>暂无健康资讯信息！</h2>");
            return;
        }
        content.html("");
        for (var i = 0; i < rst["Total"]; i++) {
            content.append("<div class='news_list_item'><span class='news_list_title'><a target='_blank' href='/News/ShowDetail?Id="+rst["Row"][i].Id+"'>" + rst["Row"][i].Title + "</a></span><span class='news_list_foornumber'>#" + parseInt(i + 1) + "</span><br /><span class='news_list_description'>--------------------</span> <span class='news_list_description'>"+rst["Row"][i].Remark+"</span></div>");
        }

        $("#pageHtml").html(rst["PageHtml"]);


    });
}


