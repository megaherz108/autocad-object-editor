using AutoCadObjectEditor.EditableObjects;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace AutoCadObjectEditor.Helpers
{
    public static class DBObjectExtensions
    {
        /// <summary>
        /// Создает объект для работы в редакторе из объекта БД
        /// </summary>
        /// <param name="dbObject">Объект БД</param>
        /// <returns>Объект редактора</returns>
        public static EditableObject ToEditableObject(this DBObject dbObject)
        {
            switch (dbObject)
            {
                case DBPoint point:
                    return new EditablePoint(point);

                case Line line:
                    return new EditableLine(line);

                case Circle circle:
                    return new EditableCircle(circle);

                default:
                    throw new NotImplementedException($"Тип объекта {dbObject.GetType()} не поддерживается");
            }
        }
    }
}
