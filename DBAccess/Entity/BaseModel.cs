﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess.Reflection;
using DBAccess.HelperClass;

namespace DBAccess.Entity
{
    public class BaseModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName = string.Empty;

        /// <summary>
        /// 不验证的字段容器
        /// </summary>
        public readonly List<string> NotChecks = new List<string>();

        /// <summary>
        /// 属性set临时容器 【用来存储要数据操作的属性】
        /// </summary>
        public readonly Dictionary<string, object> fileds = new Dictionary<string, object>();

        /// <summary>
        /// 实体操作Helper
        /// </summary>
        public readonly EntityHelper<BaseModel> EH = new EntityHelper<BaseModel>();

        /// <summary>
        /// 放置不操作数据的字段容器 [用来操作 添加 修改 的字段]
        /// </summary>
        public List<string> NotFiled = new List<string>();

        public BaseModel()
        {
            NotChecks = new List<string>();
            fileds = new Dictionary<string, object>();
            EH = new EntityHelper<BaseModel>();
            NotFiled = new List<string>();
        }

        /// <summary>
        /// 此函数用在属性set时  如下用法:
        ///  public string cMenu_Name
        /// {
        ///     set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        ///     get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        /// }
        /// </summary>
        /// <param name="FiledName"></param>
        /// <param name="Value"></param>
        public void SetValue(string FiledName, object Value)
        {
            if (FiledName.Contains("set_")) FiledName = FiledName.Replace("set_", "");
            if (Value is string && Value != null && Value.ToString() == "null")
            {
                Value = null;
            }
            if (fileds.ContainsKey(FiledName))
                fileds[FiledName] = Value;
            else
                fileds.Add(FiledName, Value);
        }

        /// <summary>
        /// 此函数用在属性get时  如下用法:
        /// public string cMenu_Name
        /// {
        ///     set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        ///     get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        /// }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FiledName"></param>
        /// <returns></returns>
        public T GetValue<T>(string FiledName)
        {
            try
            {
                if (FiledName.Contains("get_")) FiledName = FiledName.Replace("get_", "");
                if (fileds.ContainsKey(FiledName))
                {
                    object Value = fileds[FiledName];
                    if (Value is string && Value != null && Value.ToString() == "null")
                    {
                        Value = null;
                    }
                    return (T)Value;
                }
                else
                    return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

    }
}
