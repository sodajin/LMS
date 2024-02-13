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
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Password { get; }
        public AccountType AccountType { get; }
        public User(
            string id,
            string firstName,
            string middleName,
            string lastName,
            string username,
            string password,
            AccountType accountType
        ) {
            this.ID = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Username = username;
            this.Password = password;
            this.AccountType = accountType;
        }

        public bool Match(string id, string username, string password)
        {
            if (this.ID == id &&
                this.Username == username &&
                this.Password == password) return true;
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
    }
}
