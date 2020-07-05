using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace YH.ASM.Web.Attribute
{
    public class WebApiAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext Context)
        {

          

            base.OnActionExecuting(Context);

 
            var request = Context.HttpContext.Request;
            string timestamp = GetHeaderValue(request, "timestamp");//时间戳
            DateTime time = StampToDateTime(timestamp);

            if (time.AddSeconds(60) < DateTime.Now)
            {
                Context.Result = new JsonResult(new { Success = false, Code = 416, Message = "请求范围不符合要求！" });
                return;
            }

            string key =Entites.AppConfig.ApiKey;//密钥
            string body = GetBodyValueAsync(request);
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append($"path={request.Path}");
            sBuilder.Append($"&timestamp={timestamp}");
            sBuilder.Append($"&body={body}");
            sBuilder.Append($"&key={key}");

            string sign = Entites.Tool.MD5.Encrypt(sBuilder.ToString());
            string signature = GetHeaderValue(request, "SigningKey");//签名串 MD5(path={0}&timestamp={1}&token={2}&body={3}&key={4})

            if (sign != signature)
            {
                Context.Result = new JsonResult(new { Success = false, Code = 417, Message = "签名验证不通过！" });
                return;
            }


        }




        /// <summary>
        /// 获取指定key的Header值
        /// </summary>
        /// <param name="httpRequest">HttpRequest对象</param>
        /// <param name="key">要获取Headers的名称</param>
        /// <returns></returns>
        public  string GetHeaderValue( HttpRequest httpRequest, string key)
        {
            return httpRequest.Headers.ContainsKey(key) ? httpRequest.Headers[key].ToString() : string.Empty;
        }

        /// <summary>
        /// 获取Request的Body值
        /// <para>获取Body值后再重新设置Body</para>
        /// </summary>
        /// <param name="httpRequest">HttpRequest对象</param>
        /// <param name="encoding">默认编码为UTF8</param>
        /// <returns></returns>
        public string GetBodyValueAsync(HttpRequest httpRequest, Encoding encoding = null)
        {
            string result = string.Empty;
            try
            {

                using (var mStream = new MemoryStream())
                using (var reader = new StreamReader(mStream))
                {
                    httpRequest.Body.Seek(0, SeekOrigin.Begin);
                    httpRequest.Body.CopyTo(mStream);
                    mStream.Seek(0, SeekOrigin.Begin);
                    result = reader.ReadToEnd();
                    httpRequest.Body.Seek(0, SeekOrigin.Begin);
                }

            }
            catch (Exception e){
            
                
            
            }
            return result;
        }

        /// <summary>
        /// 时间戳转为时间
        /// </summary>
        /// <param name="timeStamp">时间戳字符串</param>
        /// <returns></returns>
        public  DateTime StampToDateTime( string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        /// <summary>
        /// 将时间转换为时间戳
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public  int DateTimeToStamp( DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }


    }
}
