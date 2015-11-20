using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Aggregate
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// GUID实体基类
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}
