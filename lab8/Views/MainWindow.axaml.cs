using Avalonia.Controls;
using lab8.ViewModels;
using lab8.Models;

namespace lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("New").Click += delegate
            {
                var context = this.DataContext as lab8.ViewModels.MainWindowViewModel;
                context.Clear();
            };

            this.FindControl<MenuItem>("Save").Click += async delegate
            {
                var window = new SaveFileDialog()
                {
                    Title = "Save File"
                };
                string? path = await window.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path == null) context.SaveFile("");
                else context.SaveFile(path);
            };

            this.FindControl<MenuItem>("Load").Click += async delegate
            {
                var window = new OpenFileDialog()
                {
                    Title = "Open File"
                };
                string[]? path = await window.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path != null) context.LoadFile(path[0]);
            };  

            this.FindControl<MenuItem>("About").Click += delegate
            {
                var window = new AboutView();
                window.ShowDialog((Window)this.VisualRoot);
            };

            this.FindControl<MenuItem>("Exit").Click += delegate
            {
                this.Close();
            };
        }

        public async void AddImage(Note note)
        {
            var filter = new FileDialogFilter()
            {
                Name = "Open Image",
                Extensions = { "png", "bmp", "jpg" }
            };
            var window = new OpenFileDialog()
            {
                Title = "Open Image",
                Filters = { filter }
            };
            string[]? path = await window.ShowAsync((Window)this.VisualRoot);
            if (path != null) note.setImage(path[0]);
        }
    }
}
