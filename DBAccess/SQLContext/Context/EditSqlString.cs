﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Data.SqlClient;
using DBAccess.Entity;
using DBAccess.ExpressionTree;

namespace DBAccess.SQLContext.Context
{
    /// <summary>
    /// 修改语句
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EditSqlString<T> : AbstractSqlContext<T> where T : BaseModel, new()
    {
        // UPDATE TAB SET  WHERE 1=1 
        List<SqlParameter> list_sqlpar;

        public EditSqlString()
        {
            list_sqlpar = new List<SqlParameter>();
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="mie"></param>
        /// <returns></returns>
        public override SQL_Container GetSqlString(T entity)
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL(entity);
        }

        public SQL_Container GetSqlString<M>(T entity, Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL(entity, " AND " + base.GetWhereString(where, ref list_sqlpar));
        }

        public SQL_Container GetSqlString<M>(T entity, M where) where M : BaseModel, new()
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL(entity, base.GetWhereString(where, ref list_sqlpar));
        }

        public SQL_Container GetSqlString(T entity, string where)
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL(entity, base.GetWhereString(where));
        }

        /// <summary>
        /// 获取 sql 默认主键为条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private SQL_Container GetSQL(T entity)
        {
            var TableName = entity.TableName;
            var list = entity.fileds.ToList();
            var KeyName = entity.EH.GetKeyName(entity);
            var set = new List<string>();
            var where = string.Empty;
            foreach (var item in list)
            {
                var value = item.Value;
                var key = item.Key;
                if (KeyName == key)
                    where += key + "=@" + key + "";
                else
                    set.Add(key + "=@" + key + "");
                list_sqlpar.Add(new SqlParameter() { ParameterName = key, Value = value == null ? DBNull.Value : value });
            }
            string sql = string.Format(" UPDATE {0} SET {1} WHERE 1=1 {2} ", TableName, string.Join(",", set), " AND " + where);
            return new SQL_Container(sql, list_sqlpar.ToArray());
        }

        private SQL_Container GetSQL(T entity, string where)
        {
            var TableName = entity.TableName;
            var list = entity.fileds.ToList();
            var set = new List<string>();
            foreach (var item in list)
            {
                var value = item.Value;
                var key = item.Key;
                set.Add(key + "=@" + key + "");
                list_sqlpar.Add(new SqlParameter() { ParameterName = key, Value = value == null ? DBNull.Value : value });
            }
            string sql = string.Format(" UPDATE {0} SET {1} WHERE 1=1 {2} ", TableName, string.Join(",", set), string.IsNullOrEmpty(where) ? where : " AND " + where);
            return new SQL_Container(sql, list_sqlpar.ToArray());
        }




    }
}
