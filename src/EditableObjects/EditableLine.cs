using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditableLine : EditableObject
    {
        private const string LINE_NAME = "Отрезок";

        private double _startX;
        private double _startY;
        private double _startZ;
        private double _endX;
        private double _endY;
        private double _endZ;

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
            IsChanged = false;
        }

        public double StartX
        {
            get => _startX;
            set => SetProperty("StartX", ref _startX, value);
        }

        public double StartY
        {
            get => _startY;
            set => SetProperty("StartY", ref _startY, value);
        }

        public double StartZ
        {
            get => _startZ;
            set => SetProperty("StartZ", ref _startZ, value);
        }

        public double EndX
        {
            get => _endX;
            set => SetProperty("EndX", ref _endX, value);
        }

        public double EndY
        {
            get => _endY;
            set => SetProperty("EndY", ref _endY, value);
        }

        public double EndZ
        {
            get => _endZ;
            set => SetProperty("EndZ", ref _endZ, value);
        }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var line = dbObject as Line;

            line.StartPoint = new Point3d(StartX, StartY, StartZ);
            line.EndPoint = new Point3d(EndX, EndY, EndZ);
        }
    }
}
