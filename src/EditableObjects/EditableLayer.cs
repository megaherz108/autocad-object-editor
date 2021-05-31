﻿using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Windows;
using System.Collections.ObjectModel;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditableLayer : EditableObject
    {
        private const string DEFAULT_LAYER_NAME = "0";
        private const int MAX_NAME_LENGTH = 255;

        private string _name;
        private ColorItem _colorItem;
        private bool _isVisible;

        public EditableLayer(LayerTableRecord layer)
        {
            Id = layer.Id;
            Name = layer.Name;
            ColorItem = new ColorItem(layer.Color);
            IsVisible = !layer.IsOff;
            Objects = new ObservableCollection<EditableObject>();
            IsChanged = false;
        }

        // TODO: Сделать валидацию на основе ValidationRules, сделать SetProperty() вместо текущей установки
        public override string Name
        {
            get => _name;
            set
            {
                if (_name != value  && !string.IsNullOrEmpty(value))
                {
                    // TODO: Информировать пользователя при превышении макс. длины
                    _name = value.Length > MAX_NAME_LENGTH ? value.Substring(0, MAX_NAME_LENGTH) : value;
                    OnPropertyChanged("Name");
                    IsChanged = true;
                }
            }
        }

        public bool IsNameEditable => Name != DEFAULT_LAYER_NAME;

        public ColorItem ColorItem
        {
            get => _colorItem;
            set => SetProperty("ColorItem", ref _colorItem, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty("IsVisible", ref _isVisible, value);
        }

        public ObservableCollection<EditableObject> Objects { get; private set; }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var layerTableRecord = dbObject as LayerTableRecord;

            layerTableRecord.Name = Name;
            layerTableRecord.Color = ColorItem.AcadColor;
            layerTableRecord.IsOff = !IsVisible;
        }
    }
}
