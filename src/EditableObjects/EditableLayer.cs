using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditableLayer : EditableObject
    {
        private const string DEFAULT_LAYER_NAME = "0";
        private const int MAX_NAME_LENGTH = 255;

        private string _name;

        public EditableLayer(LayerTableRecord layer)
        {
            Id = layer.Id;
            Name = layer.Name;
            Color = Color.FromRgb(layer.Color.ColorValue.R, layer.Color.ColorValue.G, layer.Color.ColorValue.B);
            IsVisible = !layer.IsOff;
            Objects = new ObservableCollection<EditableObject>();
        }

        public override string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // TODO: Информировать пользователя при превышении макс. длины
                    _name = value.Length > MAX_NAME_LENGTH ? value.Substring(0, MAX_NAME_LENGTH) : value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public bool IsNameEditable => Name != DEFAULT_LAYER_NAME;

        public Color Color { get; set; }

        public bool IsVisible { get; set; }

        public ObservableCollection<EditableObject> Objects { get; set; }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var layerTableRecord = dbObject as LayerTableRecord;

            layerTableRecord.Name = Name;
            layerTableRecord.Color = Autodesk.AutoCAD.Colors.Color.FromColor(Color);
            layerTableRecord.IsOff = !IsVisible;
        }
    }
}
