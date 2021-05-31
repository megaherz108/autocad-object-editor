using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditableCircle : EditableObject
    {
        private const string CIRCLE_NAME = "Окружность";

        private double _centerX;
        private double _centerY;
        private double _centerZ;
        private double _radius;

        public EditableCircle(Circle circle)
        {
            Id = circle.Id;
            Name = CIRCLE_NAME;
            CenterX = circle.Center.X;
            CenterY = circle.Center.Y;
            CenterZ = circle.Center.Z;
            Radius = circle.Radius;
            IsChanged = false;
        }

        public double CenterX
        {
            get => _centerX;
            set => SetProperty("CenterX", ref _centerX, value);
        }

        public double CenterY
        {
            get => _centerY;
            set => SetProperty("CenterY", ref _centerY, value);
        }

        public double CenterZ
        {
            get => _centerZ;
            set => SetProperty("CenterZ", ref _centerZ, value);
        }

        public double Radius
        {
            get => _radius;
            set => SetProperty("Radius", ref _radius, value);
        }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var circle = dbObject as Circle;

            circle.Center = new Point3d(CenterX, CenterY, CenterZ);
            circle.Radius = Radius;
        }
    }
}