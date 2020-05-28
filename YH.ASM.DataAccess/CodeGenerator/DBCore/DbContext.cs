﻿
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
        public DbContext()
        {


            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = AppConfig.DevelopmentDatabase,
                DbType = DbType.Oracle,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据


        public SimpleClient<TASM_USER> TASM_USERDb { get { return new SimpleClient<TASM_USER>(Db); } }//用来处理TASM_USER表的常用操作
  
        public SimpleClient<TPMS_FUNCTION> TPMS_FUNCTIONDb { get { return new SimpleClient<TPMS_FUNCTION>(Db); } }//用来处理TPMS_FUNCTION表的常用操作
        public SimpleClient<TPMS_PAGE> TPMS_PAGEDb { get { return new SimpleClient<TPMS_PAGE>(Db); } }//用来处理TPMS_PAGE表的常用操作
        public SimpleClient<TPMS_FUNC_MEMBER> TPMS_FUNC_MEMBERDb { get { return new SimpleClient<TPMS_FUNC_MEMBER>(Db); } }//用来处理TPMS_FUNC_MEMBER表的常用操作
        public SimpleClient<TPMS_ROLE> TPMS_ROLEDb { get { return new SimpleClient<TPMS_ROLE>(Db); } }//用来处理TPMS_ROLE表的常用操作
        public SimpleClient<TPMS_ROLE_RIGHT> TPMS_ROLE_RIGHTDb { get { return new SimpleClient<TPMS_ROLE_RIGHT>(Db); } }//用来处理TPMS_ROLE_RIGHT表的常用操作
        public SimpleClient<TPMS_USER_RIGHT> TPMS_USER_RIGHTDb { get { return new SimpleClient<TPMS_USER_RIGHT>(Db); } }//用来处理TPMS_USER_RIGHT表的常用操作


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }


        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public virtual List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel, orderByExpression, orderByType);
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual T GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(T data)
        {
            return CurrentDb.Delete(data);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic[] ids)
        {
            return CurrentDb.AsDeleteable().In(ids).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<T> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(T obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<T> objs)
        {
            return CurrentDb.InsertRange(objs);
        }


        //自已扩展更多方法 
    }


}