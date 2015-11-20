using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Model;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.Repositories.UserRepository
{
    public class UserRepository : EntityFramework.EntityFrameworkRepository<Model.User.User>,
                                  Tasker.Domain.Repositories.UserRepository.IUserRepository
    {

        public UserRepository()
        {
 
        }

        public Model.User.User GetUserByNo(string userNo)
        {
            return base.Find(x => x.UserNo == userNo);
        }
    }
}
