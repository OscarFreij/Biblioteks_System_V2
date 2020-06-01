using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteks_System_V2
{
    public class Core
    {
        public class Library
        {
            public int Id { get; private set; }
            public int NextBookId { get; private set; }
            public string Name { get; private set; }
            public List<Book> BookList { get; private set; }
            public List<string> AvailibleGenreList { get; private set; }

            public Library(int Id, string Name)
            {
                this.Id = Id;
                this.NextBookId = 0;
                this.Name = Name;

                this.BookList = new List<Book>();
                this.AvailibleGenreList = new List<string>();
            }
             
            public bool AddBook(string Title, string Author)
            {
                try
                {
                    BookList.Add(new Book(this.NextBookId, Title, Author));
                    Console.WriteLine("SUCCESS: Added book to library: " + this.Name);
                    this.NextBookId++;
                    return true;
                }
                catch
                {
                    Console.WriteLine("ERROR: Faild to add book to library: " + this.Name);
                    return false;
                }
            }

        }

        public class Book
        {
            public int Id { get; private set; }
            public string Titel { get; private set; }
            public string Author { get; private set; }
            public bool Loaned { get; private set; }

            public List<string> GenreList { get; private set; }

            public Book(int Id, string Title, string Author)
            {
                this.Id = Id;
                this.Titel = Title;
                this.Author = Author;
                this.Loaned = false;
                this.GenreList = new List<string>();
            }

            public Book(int Id, string Title, string Author,  bool Loaned)
            {
                this.Id = Id;
                this.Titel = Title;
                this.Author = Author;
                this.Loaned = Loaned;
                this.GenreList = new List<string>();
            }

            public bool SetGenre(List<string> NewGenreList)
            {
                try
                {
                    this.GenreList = NewGenreList;
                    return true;
                }
                catch(Exception e)
                {
                    Console.WriteLine("ERROR: " + e);
                    return false;
                }
            }
        }
    }
}
