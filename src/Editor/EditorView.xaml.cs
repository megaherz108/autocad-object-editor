using System.Windows;

namespace AutoCadObjectEditor.Editor
{
    public partial class EditorView : Window
    {
        public EditorView()
        {
            InitializeComponent();
        }

        // TODO: Создать команды во view model вместо обработчиков событий
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Выйти без сохранения изменений?", "Подтвердите выход", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
