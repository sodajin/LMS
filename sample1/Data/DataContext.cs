using System;
using System.Collections.Generic;
using LibraryManagementSystem.Model;
using MahApps.Metro.IconPacks;
using Npgsql;
using System.Diagnostics.Metrics;
using System.Net;
using NpgsqlTypes;
using static System.Data.Entity.Infrastructure.Design.Executor;

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

        public bool CheckExistingMember(string ID)
        {
            bool memberExists = false;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT memberid FROM members WHERE memberid = @memberid;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@memberid", ID);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            memberExists = true;
                        }
                    }
                }

                connection.Close();
            }

            return memberExists;
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

        public void RequestBook(Book book, User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO requestbook (bookid, memberid) VALUES (@bookid, @memberid)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@bookid", (long)book.ID);
                    command.Parameters.AddWithValue("@memberid", user.ID);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<RequestedBook> LoadRequests(Library library, UserList users)
        {
            List<RequestedBook> requests = new List<RequestedBook>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM requestbook;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UInt64 bookID = Convert.ToUInt64(reader["bookid"]);
                            String memberID = reader["memberid"].ToString();

                            User member = users.SearchUserByMemberID(memberID);
                            Book book = library.GetBookByID(bookID);

                            RequestedBook requestedBook = new RequestedBook(member, book);
                            requests.Add(requestedBook);
                        }
                    }
                }
                connection.Close();
            }
            return requests;
        }

        public void RemoveRequest(RequestedBook request)
        {
            Book book= request.GetBook();
            User user = request.GetUser();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM requestbook WHERE bookid = @bookid AND memberid = @memberid;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.Add("@bookid", NpgsqlDbType.Bigint).Value = (long)book.ID;
                    command.Parameters.Add("@memberid", NpgsqlDbType.Varchar).Value = user.ID;

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

        }

        public void SaveBorrowedBook(BorrowedBook book)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                
                string sql = "INSERT INTO borrowedbooks (borrowedbookid, memberid, bookid, dateborrowed, duedate, datereturned, status) VALUES (@borrowedbookid, @memberid, @bookid, @dateborrowed, @duedate, @datereturned, @status)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@borrowedbookid", (long)book.ID);
                    command.Parameters.AddWithValue("@memberid", book.User.ID);
                    command.Parameters.AddWithValue("@bookid", (long)book.Book.ID); 
                    command.Parameters.AddWithValue("@dateborrowed", book.DateBorrowed); 
                    command.Parameters.AddWithValue("@duedate", book.DateBorrowed.AddDays(3));
                    command.Parameters.AddWithValue("@datereturned", book.DateReturned);
                    command.Parameters.AddWithValue("@status", book.Status.ToString());

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public List<BorrowedBook> LoadBorrowedBooks(Library library, UserList users)
        {
            List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM borrowedbooks;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UInt64 bookID = Convert.ToUInt64(reader["bookid"]);
                            String memberID = reader["memberid"].ToString();

                            User member = users.SearchUserByMemberID(memberID);
                            Book book = library.GetBookByID(bookID);

                            BorrowedBook borrowedBook = new BorrowedBook(
                                Convert.ToUInt64(reader["borrowedbookid"]),
                                member,
                                book,
                                (Status)Enum.Parse(typeof(Status), reader["status"].ToString()),
                                Convert.ToDateTime(reader["dateborrowed"])
                            );

                            borrowedBook.SetReturnDate(Convert.ToDateTime(reader["datereturned"]));

                            borrowedBooks.Add(borrowedBook);
                        }

                    }
                }
                connection.Close();
            }
            return borrowedBooks;
        }

        public void SetBookReturned(BorrowedBook returnedBook, DateTime dateReturned)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE borrowedbooks SET status = 'Returned', datereturned = @datereturned WHERE borrowedbookid = @borrowedbookid";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@borrowedbookid", (long)returnedBook.ID);
                    command.Parameters.AddWithValue("@datereturned", dateReturned);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void SetBookUnavailable(Book book)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE books SET status = 'Unavailable' WHERE bookid = @bookid";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@bookid", (long)book.ID);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void SetBookAvailable(Book book)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE books SET status = 'Available' WHERE bookid = @bookid";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@bookid", (long)book.ID);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
