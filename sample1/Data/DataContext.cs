using System;
using System.Collections.Generic;
using LibraryManagementSystem.Model;
using MahApps.Metro.IconPacks;
using Npgsql;
using System.Diagnostics.Metrics;

namespace LibraryManagementSystem.Data
{
    public class DataContext
    {
        private string connectionString = "Host=localhost;Port=5432;Database=library_management_system;Username=postgres;Password=Qwerty!!;";

        public void SaveMember(User newUser)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO members (memberid, firstname, middlename, lastname, username, password, reputation, accounttype) VALUES (@memberid, @firstname, @middlename, @lastname, @username, @password, @reputation, @accounttype)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@memberid", newUser.ID);
                    command.Parameters.AddWithValue("@firstname", newUser.FirstName);
                    command.Parameters.AddWithValue("@middlename", newUser.MiddleName);
                    command.Parameters.AddWithValue("@lastname", newUser.LastName);
                    command.Parameters.AddWithValue("@username", newUser.Username);
                    command.Parameters.AddWithValue("@password", newUser.Password);
                    command.Parameters.AddWithValue("@reputation", 100);
                    command.Parameters.AddWithValue("@accounttype", newUser.AccountType.ToString());

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<User> LoadMembers()
        {
            List<User> members = new List<User>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM members"; 
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["accounttype"].ToString() == "Simple")
                            {
                                User member = new User(
                                reader["memberid"].ToString(),
                                reader["firstname"].ToString(),
                                reader["middlename"].ToString(),
                                reader["lastname"].ToString(),
                                reader["username"].ToString(),
                                reader["password"].ToString(),
                                Convert.ToInt32(reader["reputation"]),
                                AccountType.Simple
                                );

                                members.Add(member);
                            }
                            else if(reader["accounttype"].ToString() == "Admin")
                            {
                                User member = new User(
                                  reader["memberid"].ToString(),
                                  reader["firstname"].ToString(),
                                  reader["middlename"].ToString(),
                                  reader["lastname"].ToString(),
                                  reader["username"].ToString(),
                                  reader["password"].ToString(),
                                  Convert.ToInt32(reader["reputation"]),
                                  AccountType.Admin
                              );
                                members.Add(member);
                            } 
                        }
                    }
                }
                connection.Close();
            }

            return members;
        }
        public void PenalizeMember(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE members SET reputation = reputation - 20 WHERE memberid = @memberid";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@memberid", user.ID);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void ClearMember(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE members SET reputation = 100 WHERE memberid = @memberid";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@memberid", user.ID);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void SaveBook(Book newBook)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO books (bookid, isbn, title, author, publisher, publisheddate, genre, status) VALUES (@bookid, @isbn, @title, @author, @publisher, @publisheddate, @genre, @status)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@bookid", (long)newBook.ID); 
                    command.Parameters.AddWithValue("@isbn", (long)newBook.ISBN); 
                    command.Parameters.AddWithValue("@title", newBook.Title);
                    command.Parameters.AddWithValue("@author", newBook.Author);
                    command.Parameters.AddWithValue("@publisher", newBook.Publisher);
                    command.Parameters.AddWithValue("@publisheddate", newBook.PublishedDate);
                    command.Parameters.AddWithValue("@genre", newBook.Genre.ToString());
                    command.Parameters.AddWithValue("@status", newBook.Status.ToString());

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM books";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book(
                                Convert.ToUInt64(reader["bookid"]),
                                Convert.ToUInt64(reader["isbn"]),
                                reader["title"].ToString(),
                                reader["author"].ToString(),
                                reader["publisher"].ToString(),
                                Convert.ToDateTime(reader["publisheddate"]),
                                (Genre)Enum.Parse(typeof(Genre), reader["genre"].ToString()),
                                (BookStatus)Enum.Parse(typeof(BookStatus), reader["status"].ToString())
                            );
                            books.Add(book);
                        }
                    }
                }
                connection.Close();
            }

            return books;
        }

        public void EditBook(Book modifiedBook)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE books SET isbn = @isbn, title = @title, author = @author, publisher = @publisher, publisheddate = @publisheddate, genre = @genre, status = @status WHERE bookid = @bookid";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@bookid", (long)modifiedBook.ID);
                    command.Parameters.AddWithValue("@isbn", (long)modifiedBook.ISBN);
                    command.Parameters.AddWithValue("@title", modifiedBook.Title);
                    command.Parameters.AddWithValue("@author", modifiedBook.Author);
                    command.Parameters.AddWithValue("@publisher", modifiedBook.Publisher);
                    command.Parameters.AddWithValue("@publisheddate", modifiedBook.PublishedDate);
                    command.Parameters.AddWithValue("@genre", modifiedBook.Genre.ToString());
                    command.Parameters.AddWithValue("@status", modifiedBook.Status.ToString());

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

    }
}
