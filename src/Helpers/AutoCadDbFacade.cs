using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoCadObjectEditor.Helpers
{
    public class AutoCadDbFacade : IDisposable
    {
        private Transaction _transaction;
        private Database _db;
        private readonly Type[] _supportedObjectTypes = { typeof(DBPoint), typeof(Line), typeof(Circle) };

        public AutoCadDbFacade()
        {
            _db = Application.DocumentManager.MdiActiveDocument.Database;
            _transaction = _db.TransactionManager.StartTransaction();
        }

        public List<LayerTableRecord> GetLayers()
        {
            var layers = new List<LayerTableRecord>();

            LayerTable layerTable = _transaction.GetObject(_db.LayerTableId, OpenMode.ForRead) as LayerTable;

            foreach (ObjectId objectId in layerTable)
            {
                LayerTableRecord layer = _transaction.GetObject(objectId, OpenMode.ForRead) as LayerTableRecord;
                layers.Add(layer);
            }

            return layers;
        }

        public List<DBObject> GetGraphicObjects()
        {
            List<DBObject> objects = new List<DBObject>();

            BlockTable blockTable = _transaction.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord blockTableRecord = _transaction.GetObject(blockTable[BlockTableRecord.ModelSpace],
                OpenMode.ForRead) as BlockTableRecord;

            foreach (ObjectId objectId in blockTableRecord)
            {
                DBObject dbObject = objectId.GetObject(OpenMode.ForRead);

                // Добавляем только элементы требуемых типов
                if (_supportedObjectTypes.Contains(dbObject.GetType()))
                {
                    objects.Add(dbObject);
                }
            }

            return objects;
        }

        public DBObject GetObjectForWrite(ObjectId id)
        {
            return _transaction.GetObject(id, OpenMode.ForWrite);
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void Commit()
        {
            _transaction.Commit();
        }
    }
}
