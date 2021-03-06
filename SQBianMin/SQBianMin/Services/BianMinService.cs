﻿
/////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////数据操作类//////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////

//---------------------------------------------------------------------------------
// <copyright company="XiaoQingWa" file="BianMinModelRespository.cs" >
//      Author: Peng.Zhu
//      Create Time: 2018/3/5 14:50:45
//      Description: 
// </copyright>
//---------------------------------------------------------------------------------

using Dapper;
using SQBianMin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace SQBianMin.Services
{
    public class BianMinService
    {
        public static string GetConnstr
        {
            get { return WebConfigurationManager.AppSettings["ConnectionString"].ToString(); }

        }

        /// <summary>
        /// 新增实体
        /// </summary>
        public bool AddBianMinModel(BianMinModel model)
        {
            using (IDbConnection conn = new SqlConnection(GetConnstr))
            {
                var result = conn.Insert<int>(model);
                if (result > 0)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 新增实体--事物
        /// </summary>
        public bool AddBianMinModel(BianMinModel model, IDbConnection conn, IDbTransaction trans)
        {
            var result = conn.Insert<int>(model, trans);
            if (result > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public bool DelBianMinModel(int id)
        {
            if (id > 0)
            {
                using (IDbConnection conn = new SqlConnection(GetConnstr))
                {
                    string strSql = "delete from BianMinModel where Id=@Id";
                    var param = new { Id = id };
                    var result = conn.Execute(strSql, param);
                    if (result > 0)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelBianMinModelBatch(int[] ids)
        {
            if (ids.Length > 0)
            {
                using (IDbConnection conn = new SqlConnection(GetConnstr))
                {
                    string strSql = "delete from  BianMinModel where Id in @Id ";

                    DynamicParameters param = new DynamicParameters();
                    param.Add("Id", ids);
                    var result = conn.Execute(strSql, param);
                    if (result > 0)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取类别实体根据ID
        /// </summary>
        /// <returns></returns>
        public BianMinModel GetBianMinModel(int id)
        {
            var mResult = new BianMinModel();
            using (IDbConnection conn = new SqlConnection(GetConnstr))
            {
                mResult = conn.Get<BianMinModel>(id);
            }
            return mResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BianMinModel> GetBianMinModelList()
        {
            var mResult = new List<BianMinModel>();
            using (IDbConnection conn = new SqlConnection(GetConnstr))
            {
                mResult = conn.GetList<BianMinModel>().ToList();
            }
            return mResult;
        }
        public List<BianMinModel> GetBianMinModelListByPage(PageModel page)
        {
            var mResult = new List<BianMinModel>();
            using (IDbConnection conn = new SqlConnection(GetConnstr))
            {
                mResult = conn.GetListPaged<BianMinModel>(page.PageNumber, page.PageSize, "", "Id desc").ToList();
            }
            return mResult;
        }
        public int GetRecordCount()
        {
            using (IDbConnection conn = new SqlConnection(GetConnstr))
            {
               return conn.RecordCount<BianMinModel>();
            }
        }
        /// <summary>
        /// 更新实体列表
        /// </summary>
        /// <returns></returns>
        public bool UpdateBianMinModel(BianMinModel entity)
        {
            int row;
            using (IDbConnection conn = new SqlConnection(GetConnstr))
            {
                row = conn.Update(entity);
            }
            return row > 0;
        }
        /// <summary>
        /// 更新实体列表--事物
        /// </summary>
        /// <returns></returns>
        public bool UpdateBianMinModel(BianMinModel entity, IDbConnection conn, IDbTransaction trans)
        {
            int row;
            row = conn.Update(entity, trans);
            return row > 0;
        }
    }
}