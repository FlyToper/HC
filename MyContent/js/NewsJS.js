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
            //content.append("<hr><div class='news_list_item'><span class='news_list_title'><a target='_blank' href='/News/ShowDetail?Id=" + rst["Row"][i].Id + "'>" + rst["Row"][i].Title + "</a></span><span class='news_list_foornumber'>#" + parseInt(i + 1) + "</span><br /><span class='news_list_description'>--------------------</span> <span class='news_list_description'>" + rst["Row"][i].Description + "</span></div>");
            
            if (rst["Row"][i].Description == null) {
                rst["Row"][i].Description = "暂无简介";
            }
            if (i % 2 == 0) {
                content.append("<a href='/News/ShowDetail?id=" + rst["Row"][i].Id + "' target='_blank'><div class='news_list_item list1' style='overflow:auto'><div style='float:left;width:20%;margin-right:20px;'><img src='" + rst["Row"][i].ImgUrl + "' width='100%' /></div><div style='float:left;width:60%'><span class='news_list_title wrap-line'><span title='" + rst["Row"][i].Title + "'>" + rst["Row"][i].Title + "</span></span><br /><span class='news_list_description'></span><span class='news_list_description wrap-line' title='" + rst["Row"][i].Description + "'>-------------------- " + rst["Row"][i].Description + "</span></div></div></a>")
            } else {
                content.append("<a href='/News/ShowDetail?id=" + rst["Row"][i].Id + "'><div class='news_list_item list2' style='overflow:auto'><div style='float:left;width:20%;margin-right:20px;'><img src='" + rst["Row"][i].ImgUrl + "' width='100%' /></div><div style='float:left;width:60%'><span class='news_list_title wrap-line'><span title='" + rst["Row"][i].Title + "'>" + rst["Row"][i].Title + "</span></span><br /><span class='news_list_description'></span><span class='news_list_description wrap-line' title='" + rst["Row"][i].Description + "'>-------------------- " + rst["Row"][i].Description + "</span></div></div></a>")
            }

            if (i != rst["Total"] -1) {
                content.append("<hr>");
            }

           
        }

        $("#pageHtml").html(rst["PageHtml"]);


    });
}


