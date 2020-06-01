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
    public partial class BookForm : Form
    {
        public MainForm mainForm { get; private set; }
        public Core.Book Book { get; private set; }
        public Core.Library OldLibrary { get; private set; }
        public BookForm(MainForm mainForm, Core.Book Book)
        {

            this.mainForm = mainForm;
            this.Book = Book;
            this.Name = ($"{Book.Id}: {Book.Titel}");

            foreach (Core.Library library in this.mainForm.LibraryList)
            {
                if (mainForm.LibraryDropdown.SelectedItem.ToString() == library.Name)
                {
                    this.OldLibrary = library;
                }
            }



            InitializeComponent();

            this.TitleInput.Text = Book.Titel;
            this.AuthorInput.Text = Book.Author;

            foreach (var item in this.mainForm.LibraryDropdown.Items)
            {
                this.LibrarySelectionDropdown.Items.Add(item);
            }

            this.LibrarySelectionDropdown.SelectedItem = mainForm.LibraryDropdown.SelectedItem;


            try
            {
                foreach (string Genere in this.OldLibrary.AvailibleGenreList)
                {
                    if (this.Book.GenreList.Contains(Genere))
                    {
                        this.GenreSelectionBox.Items.Add(Genere, true);
                    }
                    else
                    {
                        this.GenreSelectionBox.Items.Add(Genere, false);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:" + e);
            }

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
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
                            
                            MessageBox.Show("Book Uppdated!");

                            var row = new string[] { this.Book.Id.ToString(), this.TitleInput.Text, this.AuthorInput.Text, this.Book.Loaned.ToString() };

                            var lvi = new ListViewItem(row);

                            lvi.Tag = "Book";

                            lvi.ToolTipText = "Doubble click to open or edit book!";

                            ListViewItem Rlvi = new ListViewItem();

                            foreach (ListViewItem item in mainForm.BookListView.Items)
                            {
                                Console.WriteLine(item.SubItems[0].Text.ToString());
                                if (item.SubItems[0].Text.ToString() == this.Book.Id.ToString())
                                {
                                    Rlvi = item;
                                }
                            }

                            this.mainForm.BookListView.Items.Remove(Rlvi);


                            this.mainForm.BookListView.Items.Insert(this.Book.Id, lvi);
                            

                            activeLibrary.BookList.Insert(this.Book.Id,new Core.Book(this.Book.Id, this.TitleInput.Text, this.AuthorInput.Text, this.Book.Loaned));
                            activeLibrary.BookList.Remove(this.Book);

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
