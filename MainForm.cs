using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteks_System_V2
{
    public partial class MainForm : Form
    {
        public List<Core.Library> LibraryList { get; private set; }
        public Core.Library activeLibrary { get; private set; }
        public MainForm()
        {
            InitializeComponent();

            BookListView.MouseDoubleClick += new MouseEventHandler(BookListView_MouseDoubleClick);
            BookListView.View = View.Details;
            this.LibraryList = new List<Core.Library>();

            LibraryList.Add(new Core.Library(0, "Oscars Bibliotek"));

            foreach (var library in LibraryList)
            {
                LibraryDropdown.Items.Add($"{library.Id} : {library.Name}");
            }

            //var row = new string[] { "0", "Hello", "World!", "Yes" };

            this.activeLibrary = LibraryList[0];

            if (LibraryDropdown.SelectedItem != null)
            {
                foreach (var library in LibraryList)
                {
                    if (LibraryDropdown.SelectedItem.ToString() == library.Name)
                    {
                        this.activeLibrary = library;
                        break;
                    }
                }
            }
            else
            {
                LibraryDropdown.SelectedItem = LibraryDropdown.Items[0];
            }


            if (this.activeLibrary == null)
            {
                this.activeLibrary = LibraryList[0];
            }

            for (int i = 0; i < 2; i++)
            {
                this.activeLibrary.AddBook("Hello World Volume." + i, "God");
            }
            
            
            foreach (var Book in this.activeLibrary.BookList)
            {
                var row = new string[] { Book.Id.ToString(), Book.Titel, Book.Author, Book.Loaned.ToString() };

                var lvi = new ListViewItem(row);

                lvi.Tag = "Book";

                lvi.ToolTipText = "Doubble click to open or edit book!";

                BookListView.Items.Add(lvi);
            }

        }

        private void AddBookBtn_Click(object sender, EventArgs e)
        {
            CreateBookForm form = new CreateBookForm(this);
            form.Show();

            
        }

        private void BookListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void BookListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                foreach (var book in this.activeLibrary.BookList)
                {
                    if (BookListView.SelectedItems[0].SubItems[0].Text == book.Id.ToString())
                    {
                        BookForm form = new BookForm(this, book);
                        form.Show();
                    }
                }

                //MessageBox.Show("ID: "+BookListView.SelectedItems[0].SubItems[0].Text);
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
