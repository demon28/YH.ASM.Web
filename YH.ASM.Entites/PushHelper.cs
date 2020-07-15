using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace YH.ASM.Entites
{
   public class PushHelper
    {
            #region Class

            /// <summary>
            /// 账户类型
            /// </summary>
            public enum AccountType
            {
                /// <summary>
                /// 未知
                /// </summary>
                [Description("未知")]
                UnKnown = -1,
                /// <summary>
                /// 管理员
                /// </summary>
                [Description("管理员")]
                Admin,
                /// <summary>
                /// 人员
                /// </summary>
                [Description("个人")]
                User,
                /// <summary>
                /// 企业
                /// </summary>
                [Description("企业")]
                Enterprise
            }

            /// <summary>
            /// 文本类型
            /// </summary>
            public enum ContentType
            {
                [Description("文本类型")]
                Text = 0,
                [Description("其他")]
                Other = 1

            }

            /// <summary>
            /// 平台类型
            /// </summary>
            public enum Platform
            {
                /// <summary>
                /// 未知
                /// </summary>
                [Description("未知")]
                UnKnown = -1,
                /// <summary>
                /// Web
                /// </summary>
                [Description("Web")]
                Web,
                /// <summary>
                /// Android
                /// </summary>
                [Description("安卓")]
                Android,
                /// <summary>
                /// IOS
                /// </summary>
                [Description("IOS")]
                IOS,
                /// <summary>
                /// PC
                /// </summary>
                [Description("PC")]
                PC,
                /// <summary>
                /// Mobile
                /// </summary>
                [Description("Mobile")]
                Mobile,
                /// <summary>
                /// WeChat
                /// </summary>
                [Description("WeChat")]
                WeChat,
                /// <summary>
                /// 小程序
                /// </summary>
                [Description("MiniProgram")]
                MiniProgram
            }

            /// <summary>
            /// 登录模型
            /// </summary>
            public class LoginModel
            {
                /// <summary>
                /// 用户名
                /// </summary>
                [Required(ErrorMessage = "请输入用户名")]
                public string UserName { get; set; }

                /// <summary>
                /// 密码
                /// </summary>
                [Required(ErrorMessage = "请输入密码")]
                public string Password { get; set; }

                /// <summary>
                /// 账户类型
                /// </summary>
                [Required(ErrorMessage = "请选择账户类型")]
                public AccountType AccountType { get; set; } = AccountType.Admin;

                /// <summary>
                /// 平台
                /// </summary>
                public Platform Platform { get; set; }

                /// <summary>
                /// 验证码
                /// </summary>
                public VerifyCodeModel VerifyCode { get; set; }
            }

            /// <summary>
            /// 验证码模型
            /// </summary>
            public class VerifyCodeModel
            {
                /// <summary>
                /// ID
                /// </summary>
                public string Id { get; set; }

                /// <summary>
                /// 验证码
                /// </summary>
                public string Code { get; set; }
            }

            /// <summary>
            /// 推送消息模型
            /// </summary>
            public class WeChatSenContent
            {
                /// <summary>
                /// 工号列表
                /// </summary>
                public IEnumerable<string> empnos { get; set; }
                /// <summary>
                /// 标题
                /// </summary>
                public string title { get; set; }
                /// <summary>
                /// 消息内容
                /// </summary>
                public string content { get; set; }
                /// <summary>
                /// 消息类型 0文本，1其他待扩充
                /// </summary>
                public ContentType ContentType { get; set; } = 0;
            }

            /// <summary>
            /// JWT令牌
            /// </summary>
            [Serializable]
            public class JwtTokenModel
            {
                /// <summary>
                /// jwt令牌
                /// </summary>
                public string AccessToken { get; set; }

                /// <summary>
                /// 刷新令牌
                /// </summary>
                public string RefreshToken { get; set; }

                /// <summary>
                /// 有效期(秒)
                /// </summary>
                public int ExpiresIn { get; set; }
            }

            #endregion

            #region HttpPostEntity

            /// <summary>
            /// HttpPost方式，通过HttpContent传入对象【包括复合对象】TParam作为WebAPI接口参数
            /// </summary>
            /// <typeparam name="TResult"></typeparam>
            /// <typeparam name="TParam"></typeparam>
            /// <param name="apiName"></param>
            /// <param name="functionName"></param>
            /// <param name="param"></param>s
            /// <returns></returns>
            public static TResult HttpPostEntity<TResult, TParam>(string apiName, string functionName, TParam param, string Token)
            {
                //apiUrl
                string apiUrl = @"http://106.52.172.159:4436/api/" + apiName + "/";

                HttpClientHandler handler = new HttpClientHandler() { UseCookies = false };
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    //添加api参数
                    string apiFunction = apiUrl + functionName;
                    //序列化参数对象
                    string jsonParam = JsonConvert.SerializeObject(param);
                    if (Token != string.Empty)//Token为空时，不携带Token访问
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    HttpContent httpContent = new StringContent(jsonParam, Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = httpClient.PostAsync(apiFunction, httpContent).Result;
                    string sRet = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<TResult>(sRet);
                }
            }

            #endregion

            #region Method

            /// <summary>
            /// 获取Token
            /// </summary>
            /// <param name="loginModel"></param>
            /// <param name="Token"></param>
            /// <returns></returns>
            public static JwtTokenModel GetAccessToken(LoginModel loginModel, string Token)
            {
                //接口名称
                string apiName = "admin/auth";
                //方法名称
                string functionName = "login";

                var loginresult = HttpPostEntity<Dictionary<string, object>, LoginModel>(apiName, functionName, loginModel, Token);

                if (Convert.ToInt32(loginresult["code"]) == 1)
                {
                    return JsonConvert.DeserializeObject<JwtTokenModel>(loginresult["data"].ToString());
                }
                else
                {
                    return new JwtTokenModel();
                }
            }

            /// <summary>
            /// 微信推送消息
            /// </summary>
            /// <param name="empno"></param>
            /// <param name="content"></param>
            public static void PushWeChat(string empno, string title, string content)
            {
                LoginModel loginfo = new LoginModel
                {
                    AccountType = 0,
                    Platform = 0,
                    UserName = "admin",
                    Password = "admin"
                };

                //获取Token
                var Identjwt = GetAccessToken(loginfo, string.Empty);
                if (!Identjwt.Equals(new JwtTokenModel()))
                {
                    #region 模拟远端调用WeChat推送

                    var wechat = new WeChatSenContent
                    {
                        title = title,
                        empnos = new List<string>() { empno },
                        content = content,
                        ContentType = ContentType.Text
                    };


                    //接口名称
                    string apiName = "np_manager/wechat";
                    //方法名称
                    string functionName = "wechatpush";

                    var pushresult = HttpPostEntity<Dictionary<string, object>, WeChatSenContent>(apiName, functionName, wechat, Identjwt.AccessToken);

                    #endregion
                }
            }

            #endregion
        }
    }
