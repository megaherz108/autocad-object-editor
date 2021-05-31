using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoCadObjectEditor.EditableObjects
{
    public abstract class EditableObject : INotifyPropertyChanged
    {
        public ObjectId Id { get; set; }

        public virtual string Name { get; set; }

        public bool IsChanged { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public abstract void UpdateDbObject(DBObject originalObject);

        protected bool SetProperty<T>(string name, ref T oldValue, T newValue)
        {
            if (oldValue == null || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
                IsChanged = true;

                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}