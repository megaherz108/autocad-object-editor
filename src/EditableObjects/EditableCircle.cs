using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadObjectEditor.EditableObjects
{
    public class EditableCircle : EditableObject
    {
        private const string CIRCLE_NAME = "Окружность";

        public EditableCircle(Circle circle)
        {
            Id = circle.Id;
            Name = CIRCLE_NAME;
            CenterX = circle.Center.X;
            CenterY = circle.Center.Y;
            CenterZ = circle.Center.Z;
            Radius = circle.Radius;
        }

        public double CenterX { get; set; }

        public double CenterY { get; set; }

        public double CenterZ { get; set; }

        public double Radius { get; set; }

        public override void UpdateDbObject(DBObject dbObject)
        {
            var circle = dbObject as Circle;

            circle.Center = new Point3d(CenterX, CenterY, CenterZ);
            circle.Radius = Radius;
        }
    }
}