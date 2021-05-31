using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoCadObjectEditor.EditableObjects
{
    public abstract class EditableObject : INotifyPropertyChanged
    {
        private bool _isChanged = false;

        public ObjectId Id { get; set; }

        public virtual string Name { get; set; }

        public bool IsChanged 
        {
            get => _isChanged;
            set
            {
                if (_isChanged != value)
                {
                    _isChanged = value;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        public abstract void UpdateDbObject(DBObject originalObject);

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (oldValue == null || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                OnPropertyChanged(propertyName);
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