using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TextFilesManager_withCommandandMacro
{
    public class FileItem : INotifyPropertyChanged
    {
        private string _name;
        private DateTime _lastModified;
        private string _content;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                    LastModified = DateTime.Now;
                }
            }
        }

        public DateTime LastModified
        {
            get => _lastModified;
            set
            {
                if (_lastModified != value) 
                {
                    _lastModified = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                if (_content != value) 
                {
                    _content = value;
                    OnPropertyChanged();                    
                }
            }
        }

        public FileItem(string name)
        {
            Name = name;
            LastModified = DateTime.Now;
            Content = "";
        }

        // PropertyChanged /////////////////////////////////////////////////////////////////////////
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
