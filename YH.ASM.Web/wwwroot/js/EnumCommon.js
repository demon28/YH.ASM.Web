


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

//工单状态
function Support_Statuslist() {

    var Statuslist = [];

    Statuslist.push({ value: 0, text: "已提交" });
    Statuslist.push({ value: 1, text: "处理中(PMC跟进)" });
    Statuslist.push({ value: 2, text: "已处理(无需PMC)" });
    Statuslist.push({ value: 3, text: "已处理(PMC完成)" });
    Statuslist.push({ value: 4, text: "待审核" });
    Statuslist.push({ value: 5, text: "未完成" });
    Statuslist.push({ value: 6, text: "已完成" });
    Statuslist.push({ value: 7, text: "已拒绝" });


    return Statuslist;
}

//工单严重度
function Support_Severitylist() {

    var Severitylist = [];

    Severitylist.push({ value: 0, text: "致命" });
    Severitylist.push({ value: 1, text: "严重" });
    Severitylist.push({ value: 2, text: "一般" });
    Severitylist.push({ value: 3, text: "提示" });
    Severitylist.push({ value: 4, text: "建议" });

    return Severitylist;
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

