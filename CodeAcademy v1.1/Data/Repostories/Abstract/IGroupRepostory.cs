using Core1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repostories.Abstract
{
    public interface IGroupRepostory
    {
        List<Group> GetAll();
        Group Get(int id);
        void Add(Group group);
        void Update(Group group);
        void Delete(Group group);
    }
}
