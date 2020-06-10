using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace YH.ASM.Entites.Tool
{

    //获取用GET，新增用POST，修改用PUT，删除用DELETE


    /// <summary>
    /// 验证API客户端请求的 签名
    /// </summary>
    public static class VerifyApiMd5 
    {


        public static bool Check(string encryptString,params string[] array) {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                builder.Append(array[i].ToLower());
            }
            
            builder.Append(Entites.AppConfig.ApiKey);

            return (encryptString.ToLower().Equals(MD5.Encrypt(builder.ToString()).ToLower()));
           
        
        }


      
    }
}