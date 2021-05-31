using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditablePoint : EditableObject
    {
        private const string POINT_NAME = "Точка";

        private double _x;
        private double _y;
        private double _z;

        public EditablePoint(DBPoint point)
        {
            Id = point.Id;
            Name = POINT_NAME;
            X = point.Position.X;
            Y = point.Position.Y;
            Z = point.Position.Z;
            IsChanged = false;
        }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var point = dbObject as DBPoint;

            point.Position = new Point3d(X, Y, Z);
        }

        public double X
        {
            get => _x;
            set => SetProperty("X", ref _x, value);
        }

        public double Y
        {
            get => _y;
            set => SetProperty("Y", ref _y, value);
        }

        public double Z
        {
            get => _z;
            set => SetProperty("Z", ref _z, value);
        }
    }
}
