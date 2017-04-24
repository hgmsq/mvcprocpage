using System.Collections.Generic;
using MvcProcPage.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using DapperExtensions;
namespace MvcProcPage.Service
{
    public class UserService:IUserService
    {
        public static string constr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        IDbConnection conn = new SqlConnection(constr);
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetAllList()
        {
            var list = new List<UserInfo>();
            //string sql = @"select Id,UserName,Nation,TrueName,Birthday,LocalAddressGender from UserInfo";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                //标准写法
                //list = conn.Query<UserInfo>(sql,commandType: CommandType.Text).AsList();
                //dapper扩展写法
                list = conn.GetList<UserInfo>().AsList();
                conn.Close();
            }
            return list;
        }


        /// <summary>
        /// 采用存储过程分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public UserPage GetPageByProcList(int page=1,int pageSize=10)
        {
            UserPage model = new UserPage();
            var list = new List<UserInfo>();
            //string sql = @"select Id,UserName,Nation,TrueName,Birthday,LocalAddressGender from UserInfo";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                DynamicParameters parm = new DynamicParameters();
                parm.Add("viewName", "UserInfo");
                parm.Add("fieldName", "*");
                parm.Add("keyName", "Id");
                parm.Add("pageSize", pageSize);
                parm.Add("pageNo", page);
                parm.Add("orderString", "Id");
                parm.Add("recordTotal", 0, DbType.Int32, ParameterDirection.Output);
                //参数名得和存储过程的变量名相同（参数可以跳跃传，键值对方式即可）
                //强类型
                //list = conn.Query<UserInfo>("P_GridViewPager", new { viewName = "Edu_StudentSelectedCourse", fieldName = "*", keyName = "Id", pageSize = 20, pageNo = 1, orderString = "id" }, commandType: CommandType.StoredProcedure).ToList();
                //标准写法
                //list = conn.Query<UserInfo>(sql,commandType: CommandType.Text).AsList();
                //dapper扩展写法
                //list = conn.GetList<UserInfo>().AsList();
                list = conn.Query<UserInfo>("ProcViewPager", parm, commandType: CommandType.StoredProcedure).AsList();
                int totalCount = parm.Get<int>("@recordTotal");//返回总页数
                model.user = list;
                model.TotalCount = totalCount;
                conn.Close();
            }
            return model;
        }
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetUserById(int id)
        {
            UserInfo model = new UserInfo();
            string sql = @"select Id,UserName,Nation,TrueName,Birthday,LocalAddressGender from UserInfo where Id=@id";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                //参数名得和存储过程的变量名相同（参数可以跳跃传，键值对方式即可）
                //动态类型
                //var list = conn.Query("usp_test", new { aId = 11 }, commandType: CommandType.StoredProcedure);
                //强类型
                //list = conn.Query<UserInfo>("P_GridViewPager", new { viewName = "Edu_StudentSelectedCourse", fieldName = "*", keyName = "Id", pageSize = 20, pageNo = 1, orderString = "id" }, commandType: CommandType.StoredProcedure).ToList();
              
                //model = conn.QueryFirst<UserInfo>(sql, commandType: CommandType.Text);
                model = conn.Get<UserInfo>(id);
                conn.Close();
            }
            return model;
        }
        /// <summary>
        /// 新增单个
        /// </summary>
        /// <param name="model"></param>
        public void InsertSingle(UserInfo model)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                conn.Insert<UserInfo>(model);
            }
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        public void InsertAll(List<UserInfo> list)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                conn.Insert<UserInfo>(list);           
            }
        }
        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="model"></param>
        public void DeleteSingle(UserInfo model)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                conn.Delete<UserInfo>(model);
            }
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        public void DeleteAll()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                conn.Delete<UserInfo>(conn.GetList<UserInfo>());
            }
        }
        /// <summary>
        /// 修改单个
        /// </summary>
        /// <param name="model"></param>
        public void UpdateSingle(UserInfo model)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                conn.Update<UserInfo>(model);
            }
        }

    }
}