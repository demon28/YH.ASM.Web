
//获取url参数
function getQueryString(name) { 
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i"); 
        var r = window.location.search.substr(1).match(reg); 
        if (r != null) return unescape(r[2]); 
        return null; 
}  


//时间格式化
function diaplayTime(data) {

    var str = data;
    //将字符串转换成时间格式
    var timePublish = new Date(str);
    var timeNow = new Date();
    var minute = 1000 * 60;
    var hour = minute * 60;
    var day = hour * 24;
    var month = day * 30;
    var diffValue = timeNow - timePublish;
    var diffMonth = diffValue / month;
    var diffWeek = diffValue / (7 * day);
    var diffDay = diffValue / day;
    var diffHour = diffValue / hour;
    var diffMinute = diffValue / minute;

    if (diffValue < 0) {
        alert("错误时间");
    }
    else if (diffMonth > 3) {
        result = timePublish.getFullYear() + "-";
        result += timePublish.getMonth() + "-";
        result += timePublish.getDate();
        alert(result);
    }
    else if (diffMonth > 1) {
        result = parseInt(diffMonth) + " months ago";
    }
    else if (diffWeek > 1) {
        result = parseInt(diffWeek) + " weeks ago";
    }
    else if (diffDay > 1) {
        result = parseInt(diffDay) + " days ago";
    }
    else if (diffHour > 1) {
        result = parseInt(diffHour) + " hours ago";
    }
    else if (diffMinute > 1) {
        result = parseInt(diffMinute) + " mins ago";
    }
    else {
        result = "just now";
    }
    return result;
}

