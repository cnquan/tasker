using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Aggregate
{
    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {

    }

    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity
    {
 
    }
}
