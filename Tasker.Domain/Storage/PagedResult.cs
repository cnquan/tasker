using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Storage
{
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T> : ICollection<T>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PagedResult()
        {
            this.Data = new List<T>();
        }

        /// <summary>
        /// 构造数据
        /// </summary>
        /// <param name="totalRecords">总条数</param>
        /// <param name="totalPages">总页数</param>
        /// <param name="pageSize">条数</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="data">数据</param>
        public PagedResult(int? totalRecords, int? totalPages, int? pageSize, int? pageNumber, IList<T> data)
        {
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = data;
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int? TotalRecords { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int? TotalPages { get; set; }

        /// <summary>
        /// 页条数
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Data { get; set; }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            this.Data.Add(obj);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="obj"></param>
        public bool Remove(T obj)
        {
           return this.Data.Remove(obj);
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            this.Data.Clear();
        }

        /// <summary>
        /// 包含数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Contains(T obj)
        {
            return this.Data.Contains(obj);
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="ary"></param>
        /// <param name="aryIndex"></param>
        public void CopyTo(T[] ary, int aryIndex)
        {
            this.Data.CopyTo(ary, aryIndex);
        }

        /// <summary>
        /// 数据条数
        /// </summary>
        public int Count
        {
            get { return Data.Count; }
        }


        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
