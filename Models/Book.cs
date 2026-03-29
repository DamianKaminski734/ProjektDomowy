using System.ComponentModel;

namespace ProjektDomowy.Models
{
    public class Book : INotifyPropertyChanged
    {
        private string title;
        private string author;
        private int year;
        private bool isRead;

        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Author
        {
            get => author;
            set { author = value; OnPropertyChanged(nameof(Author)); }
        }

        public int Year
        {
            get => year;
            set { year = value; OnPropertyChanged(nameof(Year)); }
        }

        public bool IsRead
        {
            get => isRead;
            set { isRead = value; OnPropertyChanged(nameof(IsRead)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}