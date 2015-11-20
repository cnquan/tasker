using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.DataObject
{
    public class UserDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户电话
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 用户邮件
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 用户创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
