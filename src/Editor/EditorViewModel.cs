using AutoCadObjectEditor.EditableObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoCadObjectEditor.Editor
{
    public class EditorViewModel
    {
        public ObservableCollection<EditableLayer> Layers { get; set; }

        public EditorViewModel(List<EditableLayer> layers)
        {
            Layers = new ObservableCollection<EditableLayer>(layers);
        }
    }
}
