﻿



function SupportWF(supprotInfo) {


   var html = "  <li class=\"time-label\">  "
       html+=     "    <span class=\"bg-green\" > "  +supprotInfo.CREATETIME  +" </span >"
       html +=  "     </li >"


    html += "   <li >"
    html += "     <i class=\"fa fa-user bg-aqua\"></i>"

    html += "      <div class=\"timeline-item\">"
    html += "          <span class=\"time\"><i class=\"fa fa-clock-o\"></i> "+ supprotInfo.CREATETIME +"</span>"

    html += "          <h3 class=\"timeline-header\">创建工单</h3>"

    html += "        <div class=\"timeline-body\">"

    html += "           <form class=\"form-horizontal\" style=\"margin-top: 10px\">"



    html += "               <div class=\"form-group\">"
    html += "                    <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">   创建者：</label>" 
    html += "                    <div class=\"col-sm-6\">" 
    html += "                          <input type=\"text\" class=\"form-control input-sm\" value=\""+supprotInfo.CREATORNAME+"\" readonly=\"readonly\">" 
    html += "                                </div>" 

    html += "                  </div>"


    html += "                 <div class=\"form-group\">" 
    html += "                     <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\"> 项目名称：</label>" 
    html += "                      <div class=\"col-sm-6\">" 
    html += "                         <input type=\"text\" class=\"form-control input-sm\" value=\""+supprotInfo.PROJECTNAME+"\" readonly=\"readonly\">" 
    html += "                                 </div>" 

    html += "                 </div>" 
    html += "                <div class=\"form-group\">" 
    html += "                 <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">问题类型：</label>" 
    html += "                 <div class=\"col-sm-6\">" 
    html += "                     <input type=\"text\" class=\"form-control input-sm\" value=\""+SetType(supprotInfo.TYPE)+"\" readonly=\"readonly\">" 
    html += "                             </div>" 

    html +=  "           </div>"
    html += "           <div class=\"form-group\">" 
    html += "                 <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">优先级：</label>" 
    html += "                   <div class=\"col-sm-6\">" 
    html += "                       <input type=\"text\" class=\"form-control input-sm\" value=\""+SetPriority(supprotInfo.PRIORITY)+"\" readonly=\"readonly\">" 
    html += "                             </div>" 

    html += "             </div>" 



    html += "         <div class=\"form-group\">" 
    html += "               <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\"> 严重程度：</label>" 
    html += "                 <div class=\"col-sm-6\">" 
    html += "               <input type=\"text\" class=\"form-control input-sm\" value=\""+SetSeverity(supprotInfo.SEVERITY)+"\" readonly=\"readonly\">" 
    html += "                      </div>" 

    html += "       </div>" 
    html += "       <div class=\"form-group\">" 
    html += "           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">发现日期：</label>" 
    html += "           <div class=\"col-sm-6\">" 
    html += "               <input type=\"text\" class=\"form-control input-sm\" id=\"txt_finddate\" value=\""+supprotInfo.FINDATE+"\" readonly=\"readonly\" />" 
    html += "                      </div>" 
    html += "       </div>" 
    html += "       <div class=\"form-group\">" 
    html += "           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">处理人：</label>" 
    html += "           <div class=\"col-sm-6\">" 
    html += "               <input type=\"text\" class=\"form-control input-sm\" value=\"" +supprotInfo.CONDUCTORNAME+"\" readonly=\"readonly\">" 
    html += "                      </div>" 

    html += "                   </div>" 

                            


    html += "             <div class=\"form-group\">" 
    html += "                <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">工单标题：</label>" 
    html += "                  <div class=\"col-sm-6\">" 
    html += "                      <input type=\"text\" class=\"form-control input-sm\" value=\""+supprotInfo.TITLE+"\" placeholder=\"请填写工单概要\" readonly=\"readonly\">" 
    html += "                     </div>" 

    html += "                   </div>" 


    html += "               <div class=\"form-group\">" 
    html += "                   <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">工单内容：</label>" 
    html += "                   <div class=\"col-sm-8\" style=\"text-align:left\">" 
    html += "                                 <textarea class=\"form-control\" style=\"margin-left:0px\" rows=\"3\" placeholder=\"请描述详细问题\" value=\"\" readonly=\"readonly\">" + supprotInfo.CONTENT +"</textarea>" 
    html += "                              </div>" 
    html += "                          </div>" 
    html += "                 </form>" 

    html += "                 </div>" 
    html += "                 <div class=\"timeline-footer\">" 

    html += "               </div>" 
    html += "            </div>" 
    html += "         </li>" 


   

    return html;
}

function DisposerWF(disposerInfo) {

    var html = "";


html+="                                                                                                                                                                                                                                              "
html+="    <li class=\"time-label\" v-if=\"disposerShow\">                                                                                                                                                                                           "
html+="        <span class=\"bg-blue\">                                                                                                                                                                                                              "
html+="            "+disposerInfo.CREATETIME +"                                                                                                                                                                                                     "
html+="        </span>                                                                                                                                                                                                                               "
html+="    </li>                                                                                                                                                                                                                                     "
html+="                                                                                                                                                                                                                                              "
html+="        <li v-if=\"disposerShow\">                                                                                                                                                                                                            "
html+="            <i class=\"fa fa-user bg-aqua\"></i>                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="            <div class=\"timeline-item\">                                                                                                                                                                                                     "
    html += "                <span class=\"time\"><i class=\"fa fa-clock-o\"></i>  " + disposerInfo.CREATETIME +"</span>                                                                                                                                     "
html+="                                                                                                                                                                                                                                              "
html+="                <h3 class=\"timeline-header\">技术人员处理</h3>                                                                                                                                                                               "
html+="                                                                                                                                                                                                                                              "
html+="                <div class=\"timeline-body\">                                                                                                                                                                                                 "
html+="                                                                                                                                                                                                                                              "
html+="                    <form class=\"form-horizontal\" style=\"margin-top:10px\">                                                                                                                                                                "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                        <div class=\"form-group\">                                                                                                                                                                                            "
html+="                            <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">分析人员：</label>                                                                                                        "
html+="                            <div class=\"col-sm-6\">                                                                                                                                                                                          "
html+="                                <input type=\"text\" class=\"form-control input-sm\" value=\""+disposerInfo.ANALYZEUSER+"\" placeholder=\"请选择分析人员\" readonly=\"readonly\">                                                               "
html+="                                    </div>                                                                                                                                                                                                    "
html+="                                                                                                                                                                                                                                              "
html+="                            </div>                                                                                                                                                                                                            "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                            <div class=\"form-group\">                                                                                                                                                                                        "
html+="                                <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">责任方：</label>                                                                                                      "
html+="                                <div class=\"col-sm-6\">                                                                                                                                                                                      "
html+="                                    <input type=\"text\" class=\"form-control input-sm\" value=\""+disposerInfo.RESPONSIBLE+"\" placeholder=\"请选择分析人员\" readonly=\"readonly\">                                                           "
html+="                                    </div>                                                                                                                                                                                                    "
html+="                                                                                                                                                                                                                                              "
html+="                                </div>                                                                                                                                                                                                        "
html+="                                                                                                                                                                                                                                              "
html+="                                <div class=\"form-group\">                                                                                                                                                                                    "
html+="                                    <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">责任人：</label>                                                                                                  "
html+="                                    <div class=\"col-sm-6\">                                                                                                                                                                                  "
html+="                                        <input type=\"text\" class=\"form-control input-sm\" value=\""+disposerInfo.DUTY+"\" placeholder=\"请选择分析人员\" readonly=\"readonly\">                                                              "
html+="                                    </div>                                                                                                                                                                                                    "
html+="                                                                                                                                                                                                                                              "
html+="                                    </div>                                                                                                                                                                                                    "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                    <div class=\"form-group\">                                                                                                                                                                                "
html+="                                        <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">处理人：</label>                                                                                              "
html+="                                        <div class=\"col-sm-6\">                                                                                                                                                                              "
html+="                                            <input type=\"text\" class=\"form-control input-sm\" value=\""+disposerInfo.NEXT_USER+"\" placeholder=\"请选择下一处理人员\" readonly=\"readonly\">                                                 "
html+="                                    </div>                                                                                                                                                                                                    "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                        </div>                                                                                                                                                                                                "
html+="                                                                                                                                                                                                                                              "
html+="                                        <div class=\"form-group\">                                                                                                                                                                            "
html+="                                            <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">工单状态：</label>                                                                                        "
html+="                                            <div class=\"col-sm-6\">                                                                                                                                                                          "
html+="                                                <input type=\"text\" class=\"form-control input-sm\" value=\""+SetStatus(disposerInfo.NEXT_STATUS)+"\" readonly=\"readonly\">                                                                    "
html+="                                                                                                                                                                                                                                              "
html+="                                    </div>                                                                                                                                                                                                    "
html+="                                        </div>                                                                                                                                                                                                "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                <div class=\"form-group\">                                                                                                                                                                                    "
html+="                                            <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">初步原因：</label>                                                                                        "
html+="                                            <div class=\"col-sm-8\">                                                                                                                                                                          "
html+="                                                                                                                                                                                                                                              "
    html += "                                                <textarea class=\"form-control\" rows=\"3\" placeholder=\"请填写初步原因分析\" value=\"\" readonly=\"readonly\">" + disposerInfo.ANALYZE +"</textarea>                                             "
html+="                                            </div>                                                                                                                                                                                            "
html+="                                                                                                                                                                                                                                              "
html+="                                        </div>                                                                                                                                                                                                "
html+="                                        <div class=\"form-group\">                                                                                                                                                                            "
html+="                                            <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">解决方案：</label>                                                                                        "
html+="                                            <div class=\"col-sm-8\" style=\"text-align:left\">                                                                                                                                                "
html+="                                                                                                                                                                                                                                              "
    html += "                                                <textarea class=\"form-control\" style=\"margin-left:0px\" rows=\"3\" placeholder=\"请填写建议解决方案\" value=\"\" readonly=\"readonly\">" + disposerInfo.SOLUTION +"</textarea>                  "
html+="                                            </div>                                                                                                                                                                                            "
html+="                                                                                                                                                                                                                                              "
html+="                                        </div>                                                                                                                                                                                                "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                            </form>                                                                                                                                                                                                           "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                                                                                                                                                                                                                              "
html+="                                </div>                                                                                                                                                                                                        "
html+="                                <div class=\"timeline-footer\">                                                                                                                                                                               "
html+="                                                                                                                                                                                                                                              "
html+="                                </div>                                                                                                                                                                                                        "
html+="                            </div>                                                                                                                                                                                                            "
html+="                </li>                                                                                                                                                                                                                         "
                                                                                                                                                                                                                                                  
                                                                                                                                                                                                                                                   
                                                                                                                                                                                                                                                   
                                                                                                                                                                                                                                                   
    return html;                                                                                                                                                                                                                                   
                                                                                                                                                                                                                                                   
                                                                                                                                                                                                                                                   
                                                                                                                                                                                                                                                   
}     

function PmcWF(pmcInfo) {

    var html = "";


html+="   <li class=\"time-label\" v-if=\"pmcShow\">                                                                                                                                                                           ";
html+="       <span class=\"bg-blue\">                                                                                                                                                                                         ";
html+="           "+pmcInfo.CREATETIME +"                                                                                                                                                                                   ";
html+="       </span>                                                                                                                                                                                                          ";
html+="   </li>                                                                                                                                                                                                                ";
html+="                                                                                                                                                                                                                        ";
html+="       <li v-if=\"pmcShow\">                                                                                                                                                                                            ";
html+="           <i class=\"fa fa-user bg-aqua\"></i>                                                                                                                                                                         ";
html+="                                                                                                                                                                                                                        ";
html+="           <div class=\"timeline-item\">                                                                                                                                                                                ";
    html += "               <span class=\"time\"><i class=\"fa fa-clock-o\"></i> " + pmcInfo.CREATETIME +"</span>                                                                                                                     ";
html+="                                                                                                                                                                                                                        ";
html+="               <h3 class=\"timeline-header\">PMC人员处理</h3>                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="               <div class=\"timeline-body\">                                                                                                                                                                            ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                   <form class=\"form-horizontal\" style=\"margin-top:10px\">                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                       <div class=\"form-group\">                                                                                                                                                                       ";
html+="                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">BOM图纸：</label>                                                                                    ";
html+="                           <div class=\"col-sm-6\">                                                                                                                                                                     ";
html+="                               <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.BOM+"\" placeholder=\"请输入BOM图纸\" readonly=\"readonly\">                                                          ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                                                                                                                                                                                                        ";
html+="                           </div>                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                           <div class=\"form-group\">                                                                                                                                                                   ";
html+="                               <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">是否下单：</label>                                                                               ";
html+="                               <div class=\"col-sm-6\">                                                                                                                                                                 ";
html+="                                   <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.ISBOOK+"\" placeholder=\"请输入BOM图纸\" readonly=\"readonly\">                                                   ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                               </div>                                                                                                                                                                                   ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                               <div class=\"form-group\">                                                                                                                                                               ";
html+="                                   <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">下单日期：</label>                                                                           ";
html+="                                   <div class=\"col-sm-6\">                                                                                                                                                             ";
html+="                                       <input type=\"text\" class=\"form-control input-sm\" id=\"txt_bookdate\" value=\""+pmcInfo.BOOKDATE+"\" placeholder=\"请选择下单日期\" readonly=\"readonly\" />                      ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                                                                                                                                                                                                        ";
html+="                               </div>                                                                                                                                                                                   ";
html+="                               <div class=\"form-group\">                                                                                                                                                               ";
html+="                                   <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">下单人：</label>                                                                             ";
html+="                                   <div class=\"col-sm-6\">                                                                                                                                                             ";
html+="                                       <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.BOOKUSER+"\" placeholder=\"请输入下单人姓名\" readonly=\"readonly\">                                          ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                                                                                                                                                                                                        ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                   <div class=\"form-group\">                                                                                                                                                           ";
html+="                                       <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">物料单号：</label>                                                                       ";
html+="                                       <div class=\"col-sm-6\">                                                                                                                                                         ";
html+="                                           <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.BOOKNO+"\" placeholder=\"请输入物料单号\" readonly=\"readonly\">                                          ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                                                                                                                                                                                                        ";
html+="                                       </div>                                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                       <div class=\"form-group\">                                                                                                                                                       ";
html+="                                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">交付日期：</label>                                                                   ";
html+="                                           <div class=\"col-sm-6\">                                                                                                                                                     ";
html+="                                               <input type=\"text\" class=\"form-control input-sm\" id=\"txt_delivery\" value=\""+pmcInfo.DELIVERY+"\" placeholder=\"请选择交付日期\" readonly=\"readonly\" />              ";
html+="                                           </div>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                       </div>                                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                       <div class=\"form-group\">                                                                                                                                                       ";
html+="                                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">发货日期：</label>                                                                   ";
html+="                                           <div class=\"col-sm-6\">                                                                                                                                                     ";
html+="                                               <input type=\"text\" class=\"form-control input-sm\" id=\"txt_senddate\" value=\""+pmcInfo.SENDDATE+"\" placeholder=\"请选择发货日期\" readonly=\"readonly\" />              ";
html+="                                           </div>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                       </div>                                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                       <div class=\"form-group\">                                                                                                                                                       ";
html+="                                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">发货单号：</label>                                                                   ";
html+="                                           <div class=\"col-sm-6\">                                                                                                                                                     ";
html+="                                               <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.SENDNO+"\" placeholder=\"请填写发货单号\" readonly=\"readonly\" />                                    ";
html+="                                           </div>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                       </div>                                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                       <div class=\"form-group\">                                                                                                                                                       ";
html+="                                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">收货人：</label>                                                                     ";
html+="                                           <div class=\"col-sm-6\">                                                                                                                                                     ";
html+="                                               <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.CONSIGNEE+"\" placeholder=\"请填写收货人\" readonly=\"readonly\" />                                   ";
html+="                                           </div>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                       </div>                                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                       <div class=\"form-group\">                                                                                                                                                       ";
html+="                                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">工单状态：</label>                                                                   ";
html+="                                           <div class=\"col-sm-6\">                                                                                                                                                     ";
html+="                                               <input type=\"text\" class=\"form-control input-sm\" value=\""+SetStatus(pmcInfo.NEXT_STATUS)+"\" readonly=\"readonly\">                                                     ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                       </div>                                                                                                                                                                           ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                       <div class=\"form-group\">                                                                                                                                                       ";
html+="                                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">处理人：</label>                                                                     ";
html+="                                           <div class=\"col-sm-6\">                                                                                                                                                     ";
html+="                                               <input type=\"text\" class=\"form-control input-sm\" value=\""+pmcInfo.NEXT_USER+"\" placeholder=\"请选择下一处理人员\" readonly=\"readonly\">                               ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                           </div>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                           <div class=\"form-group\">                                                                                                                                                   ";
html+="                                               <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">结果描述：</label>                                                               ";
html+="                                               <div class=\"col-sm-8\" style=\"text-align:left\">                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
    html += "                                                   <textarea class=\"form-control\" style=\"margin-left:0px\" rows=\"3\" placeholder=\"\" value=\"\" readonly=\"readonly\">" + pmcInfo.REMARKS +"</textarea>                   ";
html+="                                               </div>                                                                                                                                                                   ";
html+="                                                                                                                                                                                                                        ";
html+="                                           </div>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                        ";
html+="                                           <!-- /.box-body -->                                                                                                                                                          ";
html+="                               <!-- /.box-footer -->                                                                                                                                                                    ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                           </form>                                                                                                                                                                                      ";
html+="                                                                                                                                                                                                                        ";
html+="                                                                                                                                                                                                                        ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                                   <div class=\"timeline-footer\">                                                                                                                                                      ";
html+="                                                                                                                                                                                                                        ";
html+="                                   </div>                                                                                                                                                                               ";
html+="                               </div>                                                                                                                                                                                   ";
html+="               </li>                                                                                                                                                                                                    ";
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            
    return html;                                                                                                                                                                                                            
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            
}    


function SiteWF(siteInfo) {

    var html = "";




html+="   <li class=\"time-label\" v-if=\"siteShow\">                                                                                                                                                    ";
html+="       <span class=\"bg-blue\">                                                                                                                                                                   ";
html+="           "+siteInfo.CREATETIME +"                                                                                                                                                            ";
html+="       </span>                                                                                                                                                                                    ";
html+="   </li>                                                                                                                                                                                          ";
html+="                                                                                                                                                                                                  ";
html+="       <li v-if=\"siteShow\">                                                                                                                                                                     ";
html+="           <i class=\"fa fa-user bg-aqua\"></i>                                                                                                                                                   ";
html+="                                                                                                                                                                                                  ";
html+="           <div class=\"timeline-item\">                                                                                                                                                          ";
    html += "               <span class=\"time\"><i class=\"fa fa-clock-o\"></i> " + siteInfo.CREATETIME +"</span>                                                                                              ";
html+="                                                                                                                                                                                                  ";
html+="               <h3 class=\"timeline-header\">现场人员处理</h3>                                                                                                                                    ";
html+="                                                                                                                                                                                                  ";
html+="               <div class=\"timeline-body\">                                                                                                                                                      ";
html+="                                                                                                                                                                                                  ";
html+="                   <form class=\"form-horizontal\" style=\"margin-top:10px\">                                                                                                                     ";
html+="                                                                                                                                                                                                  ";
html+="                       <div class=\"form-group\">                                                                                                                                                 ";
html+="                           <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">完成时间：</label>                                                             ";
html+="                           <div class=\"col-sm-6\">                                                                                                                                               ";
    html += "                               <input type=\"text\" class=\"form-control input-sm\" value=\"" + siteInfo.ENDDATE +"\" id=\"txt_enddate\" readonly=\"readonly\">                                          ";
html+="                                   </div>                                                                                                                                                         ";
html+="                                                                                                                                                                                                  ";
html+="                           </div>                                                                                                                                                                 ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                           <div class=\"form-group\">                                                                                                                                             ";
html+="                               <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">工单状态：</label>                                                         ";
html+="                               <div class=\"col-sm-6\">                                                                                                                                           ";
    html += "                                   <input type=\"text\" class=\"form-control input-sm\" value=\"" + SetStatus(siteInfo.NEXT_STATUS) +"\" readonly=\"readonly\">                                          ";
html+="                                   </div>                                                                                                                                                         ";
html+="                           </div>                                                                                                                                                                 ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                           <div class=\"form-group\">                                                                                                                                             ";
html+="                               <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">处理人：</label>                                                           ";
html+="                               <div class=\"col-sm-6\">                                                                                                                                           ";
    html += "                                   <input type=\"text\" class=\"form-control input-sm\" value=\"" + siteInfo.NEXT_USER +"\" placeholder=\"请选择下一处理人员\" readonly=\"readonly\">                    ";
html+="                                   </div>                                                                                                                                                         ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                               </div>                                                                                                                                                             ";
html+="                                                                                                                                                                                                  ";
html+="                               <div class=\"form-group\">                                                                                                                                         ";
html+="                                   <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">结果描述：</label>                                                     ";
html+="                                   <div class=\"col-sm-8\" style=\"text-align:left\">                                                                                                             ";
html+="                                                                                                                                                                                                  ";
    html += "                                       <textarea class=\"form-control\" style=\"margin-left:0px\" rows=\"3\" placeholder=\"\" value=\"\" readonly=\"readonly\" >" + siteInfo.DESCRIPTION +"</textarea>   ";
html+="                                   </div>                                                                                                                                                         ";
html+="                                                                                                                                                                                                  ";
html+="                               </div>                                                                                                                                                             ";
html+="                                                                                                                                                                                                  ";
html+="                           </form>                                                                                                                                                                ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                  ";
html+="                       </div>                                                                                                                                                                     ";
html+="                       <div class=\"timeline-footer\">                                                                                                                                            ";
html+="                                                                                                                                                                                                  ";
html+="                       </div>                                                                                                                                                                     ";
html+="                   </div>                                                                                                                                                                         ";
html+="               </li>                                                                                                                                                                              ";
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
    return html;                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
}



function PrincipalWF(principalInfo) {

    var html = "";


html+="    <li class=\"time-label\" v-if=\"principalShow\">                                                                                                                                                                   ";
html+="        <span class=\"bg-blue\">                                                                                                                                                                                       ";
html+="            "+principalInfo.CREATETIME+"                                                                                                                                                                             ";
html+="        </span>                                                                                                                                                                                                        ";
html+="    </li>                                                                                                                                                                                                              ";
html+="                                                                                                                                                                                                                       ";
html+="        <li v-if=\"principalShow\">                                                                                                                                                                                    ";
html+="            <i class=\"fa fa-user bg-aqua\"></i>                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="            <div class=\"timeline-item\">                                                                                                                                                                              ";
    html += "                <span class=\"time\"><i class=\"fa fa-clock-o\"></i> " + principalInfo.CREATETIME +" </span>                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
html+="                <h3 class=\"timeline-header\">负责人审核</h3>                                                                                                                                                          ";
html+="                                                                                                                                                                                                                       ";
html+="                <div class=\"timeline-body\">                                                                                                                                                                          ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                    <form class=\"form-horizontal\" style=\"margin-top:10px\">                                                                                                                                         ";
html+="                                                                                                                                                                                                                       ";
html+="                        <div class=\"form-group\">                                                                                                                                                                     ";
html+="                            <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">完成时间：</label>                                                                                 ";
html+="                            <div class=\"col-sm-6\">                                                                                                                                                                   ";
    html += "                                <input type=\"text\" class=\"form-control input-sm\" value=\"" + principalInfo.ENDDATE +" \" id=\"txt_enddate\" readonly=\"readonly\">                                                         ";
html+="                                    </div>                                                                                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
html+="                            </div>                                                                                                                                                                                     ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                            <div class=\"form-group\">                                                                                                                                                                 ";
html+="                                <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">审核人员：</label>                                                                             ";
html+="                                <div class=\"col-sm-6\">                                                                                                                                                               ";
    html += "                                    <input type=\"text\" class=\"form-control input-sm\" value=\"" + principalInfo.CHECKUSER +" \" placeholder=\"请输入审核人员姓名\" readonly=\"readonly\">                                   ";
html+="                                    </div>                                                                                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
html+="                                </div>                                                                                                                                                                                 ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                <div class=\"form-group\">                                                                                                                                                             ";
html+="                                    <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">工单状态：</label>                                                                         ";
html+="                                    <div class=\"col-sm-6\">                                                                                                                                                           ";
    html += "                                        <input type=\"text\" class=\"form-control input-sm\" value=\"" + SetStatus(principalInfo.NEXT_STATUS) +" \" readonly=\"readonly\">                                                     ";
html+="                                    </div>                                                                                                                                                                             ";
html+="                                </div>                                                                                                                                                                                 ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                <div class=\"form-group\">                                                                                                                                                             ";
html+="                                    <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">处理人：</label>                                                                           ";
html+="                                    <div class=\"col-sm-6\">                                                                                                                                                           ";
    html += "                                        <input type=\"text\" class=\"form-control input-sm\" value=\"" + principalInfo.NEXT_USER +" \" placeholder=\"请选择下一处理人员\" readonly=\"readonly\">                               ";
html+="                                    </div>                                                                                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                    </div>                                                                                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                    <div class=\"form-group\">                                                                                                                                                         ";
html+="                                        <label for=\"inputEmail3\" class=\"col-sm-1 control-label\" style=\"padding-right:0px\">结果描述：</label>                                                                     ";
html+="                                        <div class=\"col-sm-8\" style=\"text-align:left\">                                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
    html += "                                            <textarea class=\"form-control\" style=\"margin-left:0px\" rows=\"3\" placeholder=\"\" value=\"\" readonly=\"readonly\">" + principalInfo.RESULT +" </textarea>                    ";
html+="                                        </div>                                                                                                                                                                         ";
html+="                                                                                                                                                                                                                       ";
html+="                                    </div>                                                                                                                                                                             ";
html+="                                                                                                                                                                                                                       ";
html+="                                    <!-- /.box-body -->                                                                                                                                                                ";
html+="                                <!-- /.box-footer -->                                                                                                                                                                  ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                                                                                                                                                                                                                       ";
html+="                            </form>                                                                                                                                                                                    ";
html+="                                                                                                                                                                                                                       ";
html+="                            </div>                                                                                                                                                                                     ";
html+="                            <div class=\"timeline-footer\">                                                                                                                                                            ";
html+="                                                                                                                                                                                                                       ";
html+="                            </div>                                                                                                                                                                                     ";
html+="                        </div>                                                                                                                                                                                         ";
html+="                </li>                                                                                                                                                                                                  ";
html+="                                                                                                                                                                                                                       ";
                                                                                                                                                                                                                           
                                                                                                                                                                                                                           
    return html;                                                                                                                                                                                                           
}