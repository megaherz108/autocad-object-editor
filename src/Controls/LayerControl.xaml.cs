using System.Windows;
using System.Windows.Controls;

namespace AutoCadObjectEditor.Controls
{
    public partial class LayerControl : UserControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }

        private void TextBoxName_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }
    }
}