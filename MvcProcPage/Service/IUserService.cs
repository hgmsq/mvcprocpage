using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcProcPage.Models;
namespace MvcProcPage.Service
{
    interface IUserService
    {
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        List<UserInfo> GetAllList();
        /// <summary>
        /// 采用存储过程分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        UserPage GetPageByProcList(int page = 1, int pageSize = 10);
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetUserById(int id);
        /// <summary>
        /// 新增单个
        /// </summary>
        /// <param name="model"></param>
        void InsertSingle(UserInfo model);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        void InsertAll(List<UserInfo> list);
        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="model"></param>
        void DeleteSingle(UserInfo model);
        /// <summary>
        /// 删除所有
        /// </summary>
        void DeleteAll();
        /// <summary>
        /// 修改单个
        /// </summary>
        /// <param name="model"></param>
        void UpdateSingle(UserInfo model);
    }
}
