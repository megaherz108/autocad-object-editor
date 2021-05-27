using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditablePoint : EditableObject
    {
        private const string POINT_NAME = "Точка";

        public EditablePoint(DBPoint point)
        {
            Id = point.Id;
            Name = POINT_NAME;
            X = point.Position.X;
            Y = point.Position.Y;
            Z = point.Position.Z;
        }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var point = dbObject as DBPoint;

            point.Position = new Point3d(X, Y, Z);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }
    }
}
