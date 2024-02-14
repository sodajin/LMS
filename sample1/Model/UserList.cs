using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class UserList
    {
        public List<User> Users { get; set; }
        public UserList() 
        {
            Users = new List<User>();
        }

        public void SignUp(User user)
        {
            Users.Add(user);
        }

        public User SignIn(string id, string username, string password)
        {
            IEnumerator<User> usersEnumerate = Users.GetEnumerator();

            while (usersEnumerate.MoveNext()) 
            {
                if (usersEnumerate.Current.Match(id, username, password) ) 
                {
                    return usersEnumerate.Current;
                }
            }

            return null;
        }
        public List<User> SearchUserByString(string searchText)
        {
            return Users.Where(u => u.Match(searchText)).ToList();
        }
        public List<User> GetSimpleUsers()
        {
            return Users.Where(u => u.AccountType == AccountType.Simple).ToList();
        }
        public List<User> GetUsers()
        {
            return Users;
        }
        public User GetUserFromElement(int index)
        {
            return Users.ElementAt(index);
        }
        public User GetUserFromID(string id)
        {
            return Users.Where(u => u.ID == id).FirstOrDefault();
        }
        public void ReplaceUser(User user, int index)
        {
            Users[index] = user;
        }
        public int GetReputation(int index)
        {
            return Users.ElementAt(index).Reputation;
        }
        public void AddReputation(int index, int toAdd)
        {
            Users.ElementAt(index).AddReputation(toAdd);
        }
    }
}
