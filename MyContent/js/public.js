/**
 * 共用js
 */
$(function () {
    setInterval(function () {
        $("#spanShowTime").html(showtime());
    }, 1000);
});
function showtime() {
    var mydate = new Date();
    var str = "";
    str += mydate.getHours() + ":";
    str += mydate.getMinutes() + ":";
    str += mydate.getSeconds() + "<br>";
    str += "" + mydate.getFullYear() + "年";
    str += (mydate.getMonth() + 1) + "月";
    str += mydate.getDate() + "日";
    

    return str;
}

function hasDo(id){
			$.post("/Admin/Score/dealWith", { "id":id}, function(rst){
				if(rst == "success"){
					$("#msgShow").html("<p>处理完成！</p>");
					$(".btn-lg").click();
					setTimeout(function(){
						window.location.href="/Admin";
					}, 3000);
					
				}else{
					$("#msgShow").html("<p>处理失败，请重试！</p>");
					$(".btn-lg").click();
					
				}
			});
		}

//退出
function exit(){
	$.post('/Admin/Exit',{}, function(rst){
	    if (rst == "success") {
	        show("系统系统", "退出成功，2秒后跳转登录页面！")
			
			
			setTimeout(function(){
				window.location.href="/Admin/Login";
			}, 2000);
		}else{
			
		}
	});
}

function myHelpLink() {
    $("#btnHelp").click();
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

function changeShow(title, content, isShow) {
    
    if (isShow == 1) {

        if (title == "警告") {
            $("#showContent2").css("color", "#ff6a00");
        } else {
            $("#showContent2").css("color", "black");
        }

        $("#showTitle2").html(title);
        $("#showContent2").html(content);
        $('#myModal2').modal({ backdrop: 'static', keyboard: false });
    } else {
        if (title == "警告") {
            $("#showContent").css("color", "#ff6a00");
        } else {
            $("#showContent").css("color", "black");
        }

        $("#showTitle").html(title);
        $("#showContent").html(content);
    }
}
