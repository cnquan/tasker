using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;
using Tasker.DataObject;
using Tasker.ServiceContracts;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Services
{
    public class UserServiceImpl : ApplicationService, IUserService
    {
        private IUserRepository _UserRepository;
        public UserServiceImpl(IRepositoryContext context,
            IUserRepository userRepository)
            : base(context)
        {
            this._UserRepository = userRepository;
        }

        public UserDTO GetUser(string userNo)
        {
            Model.User u = _UserRepository.GetUserByNo(userNo);
            return AutoMapper.Mapper.Map<Model.User, UserDTO>(u);
        }
    }
}
