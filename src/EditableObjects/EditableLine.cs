using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditableLine : EditableObject
    {
        private const string LINE_NAME = "Отрезок";

        public EditableLine(Line line)
        {
            Id = line.Id;
            Name = LINE_NAME;
            StartX = line.StartPoint.X;
            StartY = line.StartPoint.Y;
            StartZ = line.StartPoint.Z;
            EndX = line.EndPoint.X;
            EndY = line.EndPoint.Y;
            EndZ = line.EndPoint.Z;
        }

        public double StartX { get; set; }

        public double StartY { get; set; }

        public double StartZ { get; set; }

        public double EndX { get; set; }

        public double EndY { get; set; }

        public double EndZ { get; set; }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var line = dbObject as Line;

            line.StartPoint = new Point3d(StartX, StartY, StartZ);
            line.EndPoint = new Point3d(EndX, EndY, EndZ);
        }
    }
}
