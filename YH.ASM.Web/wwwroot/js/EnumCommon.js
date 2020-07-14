



//工单类型
function Power_Typelist() {

    var Typelist = [];

    Typelist.push({ value: 1, text: "页面访问" });
    Typelist.push({ value: 2, text: "功能操作" });


    return Typelist;

}



//工单类型
function Support_Typelist() {

    var Typelist = [];

    Typelist.push({ value: 0, text: "操作调试" });
    Typelist.push({ value: 1, text: "来料质量" });
    Typelist.push({ value: 2, text: "商务问题" });
    Typelist.push({ value: 3, text: "设计问题" });
    Typelist.push({ value: 4, text: "装配误差" });

    return Typelist;

}

//工单优先级
function Support_Prioritylist() {

    var Prioritylist = [];
    Prioritylist.push({ value: 0, text: "紧急" });
    Prioritylist.push({ value: 1, text: "高" });
    Prioritylist.push({ value: 2, text: "中" });
    Prioritylist.push({ value: 3, text: "低" });

    return Prioritylist;
}

//流程节点
function Support_Statuslist() {

    var Statuslist = [];

    Statuslist.push({ value: 0, text: "创建管理表-->责任人处理" });
    Statuslist.push({ value: 1, text: "分析完成-->售后内勤维护" });
    Statuslist.push({ value: 2, text: "分析完成-->现场整改" });
    Statuslist.push({ value: 3, text: "售后内勤维护完成-->现场整改" });
    Statuslist.push({ value: 4, text: "现场整改-->现场负责人审核" });
    Statuslist.push({ value: 5, text: "现场负责人审核-->驳回再整改" });
    Statuslist.push({ value: 6, text: "现在负责人审核-->已完成" });
    Statuslist.push({ value: 7, text: "已拒绝" });

    return Statuslist;
}

//工单严重度
function Support_Severitylist() {

    var Severitylist = [];



    Severitylist.push({ value: 0, text: "五级" });
    Severitylist.push({ value: 1, text: "四级" });
    Severitylist.push({ value: 2, text: "三级" });
    Severitylist.push({ value: 3, text: "二级" });
    Severitylist.push({ value: 4, text: "一级" });


    return Severitylist;
}

//工单状态
function Support_State() {
    var list = [];



    list.push({ value: 0, text: "待办" });
    list.push({ value: 1, text: "处理中" });
    list.push({ value: 2, text: "已完成" });



    return list;

}


//取值工单类型
function EnumGetSingle(value,Array) {

    var res = "";

    Array.forEach(function (element) {
        if (element.value == value) {
            res = element.text;
        }
    });
    return res;
}

