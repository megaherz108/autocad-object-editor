using AutoCadObjectEditor.EditableObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoCadObjectEditor.Editor
{
    public class EditorViewModel : IChangeable
    {
        public EditorViewModel(List<EditableLayer> layers)
        {
            Layers = new ObservableCollection<EditableLayer>(layers);
        }

        public ObservableCollection<EditableLayer> Layers { get; set; }
        
        public bool HasChanges => Layers.Any(l => l.IsChanged || l.Objects.Any(obj => obj.IsChanged));
   }
}