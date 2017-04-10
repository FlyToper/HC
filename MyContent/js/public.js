/**
 * 共用js
 */



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
		if(rst == "success"){
			$("#msgShow").html("<p>退出成功，3秒后跳转登录页面！</p>");
			$(".btn-lg").click();
			
			setTimeout(function(){
				window.location.href="/Admin/Login";
			}, 3000);
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
}

function changeShow(title, content, isShow) {
    if (title == "警告") {
        $("#showContent").css("color", "#ff6a00");
    } else {
        $("#showContent").css("color", "black");
    }

    $("#showTitle").html(title);
    $("#showContent").html(content);

    if (isShow == 1) {
        $("#btnShow").click();
    }
}
