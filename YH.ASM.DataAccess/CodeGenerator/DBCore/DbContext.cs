
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
namespace YH.ASM.DataAccess.CodeGenerator.DBCore
{
    public class DbContext<T> where T : class, new()
    {


        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据


        public DbContext()
        {


            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = AppConfig.DB,
                DbType = DbType.Oracle,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {

                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
     

    
    }


}