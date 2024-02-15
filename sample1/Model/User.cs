using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public enum AccountType
    {
        Admin,
        Simple
    }

    public class User
    {
        public string ID { get; }
        public string FirstName { get; set; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Password { get; }
        public int Reputation { get; set; }
        public AccountType AccountType { get; }
        public User(
            string id,
            string FirstName,
            string MiddleName,
            string LastName,
            string Username,
            string Password,
            int Reputation,
            AccountType AccountType
        ) {
            this.ID = id;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Username = Username;
            this.Password = Password;
            this.Reputation = Reputation;
            this.AccountType = AccountType;
        }

        public bool Match(string id, string username, string password)
        {
            if (this.ID == id &&
                this.Username == username &&
                this.Password == password) return true;
            return false;
        }

        public bool Match(string searchTest)
        {
            if (this.FirstName.ToLower().Contains(searchTest.ToLower()) ||
                this.LastName.ToLower().Contains(searchTest.ToLower()) ||
                this.MiddleName.ToLower().Contains(searchTest.ToLower())) return true;
            return false;
        }

        public string GetID()
        {
            return this.ID;
        }

        public string GetFullName()
        {
            return $"{FirstName} {MiddleName} {LastName}";
        }

        public void AddReputation(int reputation)
        {
            this.Reputation += reputation;
        }
        public void SetReputation(int reputation)
        {
            this.Reputation = reputation;
        }
    }
}
