using AutoCadObjectEditor.EditableObjects;
using Autodesk.AutoCAD.Windows;
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

        private void ButtonSelectColor_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new ColorDialog();

            if (colorDialog.ShowModal() ?? false)
            {
                var layer = DataContext as EditableLayer;
                if (layer != null)
                {
                    layer.ColorItem = new ColorItem(colorDialog.Color);
                }
            }
        }
    }
}