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
    public partial class CreateBookForm : Form
    {
        public MainForm mainForm { get; private set; }
        public CreateBookForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            foreach (var item in this.mainForm.LibraryDropdown.Items)
            {
                this.LibrarySelectionDropdown.Items.Add(item);
            }

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            mainForm.BookListView.Update();
            this.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (this.TitleInput.Text != "" && this.AuthorInput.Text != "" && this.LibrarySelectionDropdown.SelectedItem != null)
            {
                Core.Library activeLibrary;

                string SelectedName = this.LibrarySelectionDropdown.SelectedItem.ToString().Substring(this.LibrarySelectionDropdown.SelectedItem.ToString().IndexOf(':') + 2);

                foreach (Core.Library library in this.mainForm.LibraryList)
                {
                    Console.WriteLine(library.Name);
                    if (SelectedName == library.Name)
                    {
                        try
                        {
                            activeLibrary = library;
                            activeLibrary.AddBook(this.TitleInput.Text.ToString(), this.AuthorInput.Text.ToString());
                            MessageBox.Show("Book added!");
                            Core.Book Book;
                            
                            if (activeLibrary.BookList.Count != 0)
                            {
                                Book = activeLibrary.BookList[activeLibrary.BookList.Count - 1];
                            }
                            else
                            {
                                Book = activeLibrary.BookList[0];
                            }
                            
                            var row = new string[] { Book.Id.ToString(), Book.Titel, Book.Author, Book.Loaned.ToString() };

                            var lvi = new ListViewItem(row);

                            lvi.Tag = "Book";

                            lvi.ToolTipText = "Doubble click to open or edit book!";

                            this.mainForm.BookListView.Items.Add(lvi);

                            mainForm.BookListView.Update();
                            this.Close();
                            return;
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Library not found!");
                    }
                }
            }
            else
            {
                if (this.TitleInput.Text == "")
                {
                    MessageBox.Show("Title can not be empty!");
                }
                else if (this.AuthorInput.Text == "")
                {
                    MessageBox.Show("Author can not be empty!");
                }
                else if (this.LibrarySelectionDropdown.SelectedItem == null)
                {
                    MessageBox.Show("Please select a library!");
                }
                else
                {
                    MessageBox.Show("Unknown error!");
                }
            }
        }
    }
}
