using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProcPage.Models
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Nation { get; set; }
        public string TrueName { get; set; }
        public DateTime Birthday { get; set; }
        public string LocalAddress { get; set; }
        public int Gender { get; set; }
    }
    /// <summary>
    /// 分页对象
    /// </summary>
    public class UserPage
    {
        public List<UserInfo> user { get; set; }
        public int TotalCount{get;set;}
    }
}