using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Storage
{
    public enum SortOrder
    {
        /// <summary>
        /// 默认
        /// </summary>
        Unspecified = -1,

        /// <summary>
        /// 正序
        /// </summary>
        Ascending = 0,

        /// <summary>
        /// 倒序
        /// </summary>
        Descending = 1
    }
}
