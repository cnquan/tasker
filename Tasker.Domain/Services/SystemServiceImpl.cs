using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;

namespace Tasker.Domain.Services
{
    public class SystemServiceImpl : ApplicationService, ISystemService
    {
        private Repositories.ISystemRepository _SystemRepository;
        public SystemServiceImpl(Repositories.IRepositoryContext context,
            Repositories.ISystemRepository sys)
            : base(context)
        {
            _SystemRepository = sys;
        }

        public DateTime GetSysTime()
        {
            return _SystemRepository.GetSysTime();
        }
    }
}
