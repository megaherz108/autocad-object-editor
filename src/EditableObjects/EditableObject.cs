using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoCadObjectEditor.EditableObjects
{
    public abstract class EditableObject : INotifyPropertyChanged
    {
        private string _name;

        public ObjectId Id { get; set; }

        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public abstract void UpdateDbObject(DBObject originalObject);
    }
}
