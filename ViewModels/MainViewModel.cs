using ProjektDomowy.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace ProjektDomowy.ViewModels
{
    public class MainViewModel
    {
        private const string FilePath = "books.xml";

        public ObservableCollection<Book> Books { get; set; }

        public string NewTitle { get; set; }
        public string NewAuthor { get; set; }
        public string NewYear { get; set; }

        public Book SelectedBook { get; set; }

        public MainViewModel()
        {
            Books = new ObservableCollection<Book>();
            LoadBooks();
        }

        private void SubscribeToBookChanges(Book book)
        {
            book.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsRead")
                {
                    SaveBooks(); 
                }
            };
        }

        public void AddBook()
        {
            if (string.IsNullOrWhiteSpace(NewTitle) ||
                string.IsNullOrWhiteSpace(NewAuthor) ||
                !int.TryParse(NewYear, out int year))
            {
                MessageBox.Show("Wprowadź poprawne dane książki.");
                return;
            }

            var book = new Book
            {
                Title = NewTitle,
                Author = NewAuthor,
                Year = year,
                IsRead = false
            };

            SubscribeToBookChanges(book);
            Books.Add(book);
            SaveBooks();

            NewTitle = NewAuthor = NewYear = string.Empty;
        }

        public void DeleteBook()
        {
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook);
                SaveBooks();
            }
        }

        public void MarkAsRead()
        {
            if (SelectedBook != null)
            {
                SelectedBook.IsRead = true; 
                SaveBooks();               
            }
        }

        private void SaveBooks()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Book>));
            using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            {
                serializer.Serialize(fs, Books);
            }
        }

        private void LoadBooks()
        {
            if (File.Exists(FilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Book>));
                using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                {
                    Books = (ObservableCollection<Book>)serializer.Deserialize(fs);
                }

                foreach (var book in Books)
                {
                    SubscribeToBookChanges(book);
                }
            }
        }
    }
}