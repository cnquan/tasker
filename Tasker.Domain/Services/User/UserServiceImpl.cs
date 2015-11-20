using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;
using Tasker.DataObject;
using Tasker.ServiceContracts;
using Tasker.Domain.Repositories.UserRepository;

namespace Tasker.Domain.Services.User
{
    public class UserServiceImpl : ApplicationService, IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserServiceImpl(IRepositoryContext context,
            IUserRepository userRepository)
            : base(context)
        {
            this._UserRepository = userRepository;
        }

        public UserDTO GetUser(string userNo)
        {
            Model.User.User u = _UserRepository.GetUserByNo(userNo);
            //transfer to dto
            
            return null;
        }
    }
}
