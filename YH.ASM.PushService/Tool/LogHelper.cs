using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;



[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace YH.ASM.PushService.Tool
{
    /// <summary>
    /// 系统日志对象，引用于log4net日志
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// 内部引入的是Log4Net组件，要确保能记录日志则必须保证Log4Net.dll的引用，和Log4Net.config文件在根目录
    /// ]]>
    /// </remarks>
    public static class LogHelper
    {
        private static ILog _log;
        static LogHelper()
        {


        }
        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public static void Debug(object message)
        {
            System.Diagnostics.Debug.WriteLine("Debug Log:" + message);
            log.Debug(message);
        }

        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象</param>
        public static void Debug(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Debug Log:" + message);
            System.Diagnostics.Debug.WriteLine("Exception:" + exception.Message);
            System.Diagnostics.Debug.WriteLine("StackTrace:" + exception.StackTrace);
            log.Debug(message, exception);
        }
        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        public static void Debug(object message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Debug Log:" + string.Format(message.ToString(), args));
            log.Debug(string.Format(message.ToString(), args));
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public static void Error(object message)
        {
            System.Diagnostics.Debug.WriteLine("Error Log:" + message);
            log.Error(message);
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象</param>
        public static void Error(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Error Log:" + message);
            System.Diagnostics.Debug.WriteLine("Exception:" + exception.Message);
            System.Diagnostics.Debug.WriteLine("StackTrace:" + exception.StackTrace);
            log.Error(message, exception);
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        public static void Error(object message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Error Log:" + string.Format(message.ToString(), args));
            log.Error(string.Format(message.ToString(), args));
        }

        /// <summary>
        /// 记录 系统致命 信息
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(object message)
        {
            System.Diagnostics.Debug.WriteLine("Fatal Log:" + message);
            log.Fatal(message);
        }

        /// <summary>
        /// 记录 系统致命 级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象</param>
        public static void Fatal(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Fatal Log:" + message);
            System.Diagnostics.Debug.WriteLine("Exception:" + exception.Message);
            System.Diagnostics.Debug.WriteLine("StackTrace:" + exception.StackTrace);
            log.Fatal(message, exception);
        }

        /// <summary>
        /// 记录 系统致命 级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        public static void Fatal(object message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Fatal Log:" + string.Format(message.ToString(), args));
            log.Fatal(string.Format(message.ToString(), args));
        }

        /// <summary>
        /// 记录 Info 信息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            System.Diagnostics.Debug.WriteLine("Info Log:" + message);
            log.Info(message);
        }

        /// <summary>
        /// 记录 Info 级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象</param>
        public static void Info(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Info Log:" + message);
            System.Diagnostics.Debug.WriteLine("Exception:" + exception.Message);
            System.Diagnostics.Debug.WriteLine("StackTrace:" + exception.StackTrace);
            log.Info(message, exception);
        }

        /// <summary>
        /// 记录 Info 级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        public static void Info(object message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Info Log:" + string.Format(message.ToString(), args));
            log.Info(string.Format(message.ToString(), args));
        }

        /// <summary>
        /// 记录 警告 信息
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            System.Diagnostics.Debug.WriteLine("Warn Log:" + message);
            log.Warn(message);
        }

        /// <summary>
        /// 记录 警告 级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象</param>
        public static void Warn(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Warn Log:" + message);
            System.Diagnostics.Debug.WriteLine("Exception:" + exception.Message);
            System.Diagnostics.Debug.WriteLine("StackTrace:" + exception.StackTrace);
            log.Warn(message, exception);
        }

        /// <summary>
        /// 记录 警告 级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        public static void Warn(object message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Warn Log:" + string.Format(message.ToString(), args));
            log.Warn(string.Format(message.ToString(), args));
        }

        private static ILog log
        {
            get
            {
                if (_log == null)
                {
                    _log = LogManager.GetLogger(typeof(LogHelper));
                }
                return _log;
            }
        }
    }
}
