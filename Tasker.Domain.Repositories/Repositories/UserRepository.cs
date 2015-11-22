using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Model;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.Repositories
{
    public class UserRepository : EntityFramework.EntityFrameworkRepository<Model.User>,
                                  IUserRepository
    {
        public UserRepository(IRepositoryContext context) : base(context) { }

        public Model.User GetUserByNo(string userNo)
        {
            return base.Find(x => x.UserNo == userNo);
        }
    }
}
