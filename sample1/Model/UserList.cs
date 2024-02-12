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
    }
}
