using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TextFilesManager_withCommandandMacro
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FileItem> Files { get; set; } = new ObservableCollection<FileItem>();

        private Dictionary<ICommand, object> Commands;

        private FileItem _selectedFile;
        public FileItem SelectedFile
        {
            get => _selectedFile;
            set
            {
                if (value != _selectedFile) 
                {
                    _selectedFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newFileName;
        public string NewFileName
        {
            get => _newFileName;
            set
            {
                if (value != _newFileName) 
                {
                    _newFileName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isNameBoxVisible;
        public bool IsNameBoxVisible
        {
            get => _isNameBoxVisible;
            set
            {
                if (value != _isNameBoxVisible)
                {
                    _isNameBoxVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isContentVisible;
        public bool IsContentVisible
        {
            get => _isContentVisible;
            set
            {
                if (value != _isContentVisible)
                {
                    _isContentVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEditingAvailable;
        public bool IsEditingAvailable
        {
            get => _isEditingAvailable;
            set
            {
                if (value != _isEditingAvailable)
                {
                    _isEditingAvailable = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRecording;
        public bool IsRecording
        {
            get => _isRecording;
            set
            {
                if (value != _isRecording)
                {
                    _isRecording = value;
                    OnPropertyChanged();
                    if(IsRecording)
                    {
                        Commands.Clear();
                    }
                }
            }
        }

        public MainViewModel()
        {
            IsNameBoxVisible = true;
            IsContentVisible = false;
            IsEditingAvailable = false;
            IsRecording = false;

            Commands = new Dictionary<ICommand, object>()
            {
                { CreateFileCommand, null },
                { EditFileCommand, null },
                { WriteTextCommand, "This text from macros"},
                { SaveFileCommand, null },
                { CloseFileCommand, null }
            };
        }

        public ICommand ActivateMacroCommand => new RelayCommand(parameter => ActivateMacro(parameter));

        private async Task ActivateMacro(object parameter)
        {
            foreach (var command in Commands)
            {
                command.Key.Execute(command.Value);
                await Task.Delay(1000);
            }
        }

        public ICommand WriteTextCommand => new RelayCommand(parameter =>
        {
            if (SelectedFile != null && parameter is string newText)
            {
                SelectedFile.Content = newText;
                OnPropertyChanged(nameof(SelectedFile));
            }
        });

        public ICommand RecordCommands => new RelayCommand(parameter =>
        {

        });

        //
        public ICommand CreateFileCommand => new RelayCommand(parameter =>
        {                        
            RecordCommand(CreateFileCommand, null);            

            if (!string.IsNullOrWhiteSpace(NewFileName))
            {
                CreateFile(NewFileName);
            }
            else
            {
                CreateFile("New File");
            }
        });

        public ICommand DeleteFileCommand => new RelayCommand(parameter =>
        {
            if (SelectedFile != null)
            {                                
                RecordCommand(DeleteFileCommand, null);                

                Files.Remove(SelectedFile);
                SelectedFile = Files.FirstOrDefault();
            }
        });

        public ICommand OpenFileCommand => new RelayCommand(parameter => 
        {
            if (SelectedFile != null) 
            {
                RecordCommand(OpenFileCommand, null);

                IsNameBoxVisible = false;
                IsContentVisible = true;
            }
        });

        public ICommand CloseFileCommand => new RelayCommand(parameter =>
        {
            RecordCommand(CloseFileCommand, null);

            IsContentVisible = false;
            IsEditingAvailable = false;
            IsNameBoxVisible = true;
        });

        public ICommand EditFileCommand => new RelayCommand(parameter =>
        {
            if (SelectedFile != null)
            {                                              
                RecordCommand(EditFileCommand, null);                

                IsContentVisible = false;
                IsNameBoxVisible = false;
                IsEditingAvailable = true;                
            }
        });

        public ICommand SaveFileCommand => new RelayCommand(parameter =>
        {
            if (SelectedFile != null)
            {
                RecordCommand(WriteTextCommand, SelectedFile.Content);
                RecordCommand(SaveFileCommand, null);

                SelectedFile.LastModified = DateTime.Now;
            }
        });

        public void RecordCommand(ICommand command, object parameter)
        {
            if (IsRecording) Commands.Add(command, parameter);
        }

        public void CreateFile(string fileName)
        {
            var newFile = new FileItem(fileName);
            Files.Add(newFile);
            SelectedFile = newFile;
            fileName = "";
        }        

        // PropertyChanged /////////////////////////////////////////////////////////////////////////
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
