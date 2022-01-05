using Rest1.Interfaces;
using Rest1.Models;

namespace Rest1.Respositories
{
    public class UserRepository : IUserRepository
    {

        public static List<User> UserDataList;
        public UserRepository()
        {
            UserDataList = new List<User> {
            new User {
                Id=Guid.NewGuid(),
                Name="Ralf",
                LastName="Jones",
                Email ="ralfjones@email.com",
                OtherData="NaNaNaNaNaNana"
            },
            new User {
                 Id=Guid.NewGuid(),
                Name="Clark",
                LastName="Still",
                Email ="clarkstill@email.com",
                OtherData="Hey"
            },
            new User {
                 Id=Guid.NewGuid(),
                Name="Heidern",
                LastName="",
                Email ="heidern@email.com",
                OtherData="Atention!"
            }
        };
        }

        public async Task<User> CreateUser(User User, CancellationToken cancellationToken)
        {
            await Task.Run(() => UserDataList.Add(User), cancellationToken);
            return User;
        }

        public async Task<bool> DeleteUser(Guid Id, CancellationToken cancellationToken)
        {
            User user = default;
            await Task.Run(() =>
            {
                user = UserDataList.FirstOrDefault(x => x.Id == Id);
            }, cancellationToken);
            return UserDataList.Remove(user);
        }

        public async Task<User> GetUserById(Guid Id, CancellationToken cancellationToken)
        {
            User user = default;
            await Task.Run(() =>
            {
                user = UserDataList.FirstOrDefault(x => x.Id == Id);
            }, cancellationToken);
            return user as User;
        }

        public async Task<IReadOnlyList<User>> ListUsers(CancellationToken cancellationToken)
        {
            List<User> users = default;
            await Task.Run(() =>
            {
                users = UserDataList;
            }, cancellationToken);
            return users;
        }

        public async Task<User> UdateUser(Guid Id, User User, CancellationToken cancellationToken)
        {
            User user = default;
            await Task.Run(() =>
            {
                user = UserDataList.FirstOrDefault(x => x.Id == Id);
                user.Name = User.Name;
                user.LastName = User.LastName;
                user.Email = User.Email;
                user.OtherData = User.OtherData;
            }, cancellationToken);

            return user;
        }
    }
}