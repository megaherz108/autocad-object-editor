using AutoCadObjectEditor.DB;
using AutoCadObjectEditor.Editor;
using AutoCadObjectEditor.EditableObjects;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System.Collections.Generic;
using System.Linq;

[assembly: CommandClass(typeof(AutoCadObjectEditor.Main))]
namespace AutoCadObjectEditor
{
    public class Main
    {
        [CommandMethod("AUTOCAD")]
        public void ShowEditor()
        {
            try
            {
                List<EditableLayer> layers = GetLayersWithObjects();

                var editorViewModel = new EditorViewModel(layers);
                var editorView = new EditorView { DataContext = editorViewModel };

                bool? result = editorView.ShowDialog();

                if (result ?? false)
                {
                    SaveChanges(editorViewModel.Layers);
                }
            }
            catch (System.Exception ex)
            {
                Application.ShowAlertDialog("Error!\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private List<EditableLayer> GetLayersWithObjects()
        {
            var layers = new List<EditableLayer>();

            using (var db = new AutoCadDbFacade())
            {
                // Получаем список слоев для редактирования
                List<LayerTableRecord> dbLayers = db.GetLayers();
                foreach(var dbLayer in dbLayers)
                {
                    layers.Add(new EditableLayer(dbLayer));
                }

                // Получаем графические объекты из базы и добавляем в коллекцию объектов родительского слоя
                List<DBObject> dbObjects = db.GetGraphicObjects();
                foreach(var dbObject in dbObjects)
                {
                    var layer = layers.First(l => l.Name == ((Entity)dbObject).Layer);
                    layer.Objects.Add(dbObject.ToEditableObject());
                }
            }

            return layers;
        }

        private void SaveChanges(IEnumerable<EditableObject> layers)
        {
            using (var db = new AutoCadDbFacade())
            {
                foreach (EditableLayer layer in layers)
                {
                    DBObject dbLayer = db.GetObjectForWrite(layer.Id);
                    layer.UpdateDbObject(dbLayer);

                    foreach (EditableObject graphicObject in layer.Objects)
                    {
                        DBObject dbObject = db.GetObjectForWrite(graphicObject.Id);
                        graphicObject.UpdateDbObject(dbObject);
                    }
                }

                db.Commit();
            }
        }
    }
}