using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;
using Tasker.DataObject;
using AutoMapper;

namespace Tasker.Domain.Services
{
    /// <summary>
    /// 应用层服务类
    /// </summary>
    public abstract class ApplicationService : DisposableObject
    {
        /// <summary>
        /// 仓储上下文
        /// </summary>
        private readonly IRepositoryContext _Context;

        /// <summary>
        /// 对应用服务层进行初始化
        /// </summary>
        /// <remarks>
        /// 1. 进行 AutoMapper 注册
        /// </remarks>
        static ApplicationService()
        {
            Mapper.CreateMap<Model.User, UserDTO>()
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark));
        }

        /// <summary>
        /// 应用层服务初始化
        /// </summary>
        /// <param name="context"></param>
        public ApplicationService(IRepositoryContext context)
        {
            this._Context = context;
        }

        /// <summary>
        /// 仓储上下文
        /// </summary>
        protected IRepositoryContext Context { get { return _Context; } }

        /// <summary>
        /// 资源回收
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) { }
    }
}
