using System.ComponentModel;
using System.Windows;

namespace AutoCadObjectEditor.Editor
{
    public partial class EditorView : Window
    {
        private bool _forceClosing = false;

        public EditorView()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            IChangeable context = DataContext as IChangeable;

            if (context.HasChanges && !_forceClosing)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Выйти без сохранения изменений?", "Подтвердите выход", MessageBoxButton.YesNo);

                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            _forceClosing = true;
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}