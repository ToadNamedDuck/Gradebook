using Gradebook.Models;

namespace Gradebook.Repositories
{
        public interface IUserRepository
        {
            //public BarrenUser GetById(int id);
            //public BarrenUser GetByEmail(string email);
            public User GetById(int id);
            public User GetByEmail(string email);
            public void Add(User user);
            //public void Update(BarrenUser user);
            public void Update(User user);
            public User GetByFirebaseId(string firebaseId);
            //public UserWithPosts GetByIdWithPosts(int id);
            //public List<BarrenUser> GetByDistrict(int districtId);
            public List<User> GetByDistrictId(int districtId);
        }
}
