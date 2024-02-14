using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class UserList
    {
        public List<User> Users { get; }
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

        public List<User> GetSimpleUsers()
        {
            return Users.Where(u => u.AccountType == AccountType.Simple).ToList();
        }

        public User GetUserFromElement(int index)
        {
            return Users.ElementAt(index);
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
