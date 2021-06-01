using AutoCadObjectEditor.DB;
using AutoCadObjectEditor.EditableObjects;
using AutoCadObjectEditor.Editor;
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
        [CommandMethod("ObjectEditor")]
        public void ShowEditor()
        {
            try
            {
                List<EditableLayer> layers = GetLayersWithObjects();

                var view = new EditorView();
                var viewModel = new EditorViewModel(layers);
                view.DataContext = viewModel;

                bool? result = view.ShowDialog();

                if (result ?? false)
                {
                    SaveChanges(viewModel.Layers);
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
                foreach (var dbLayer in dbLayers)
                {
                    layers.Add(new EditableLayer(dbLayer));
                }

                // Получаем графические объекты из базы и добавляем в коллекцию объектов родительского слоя
                List<DBObject> dbObjects = db.GetGraphicObjects();
                foreach (var dbObject in dbObjects)
                {
                    var layer = layers.First(l => l.Name == ((Entity)dbObject).Layer);
                    layer.Objects.Add(dbObject.ToEditableObject());
                }
            }

            return layers;
        }

        private void SaveChanges(IEnumerable<EditableLayer> layers)
        {
            using (var db = new AutoCadDbFacade())
            {
                bool isChanged = layers.Any(l => l.IsChanged || l.Objects.Any(obj => obj.IsChanged));

                if (isChanged)
                {
                    foreach (EditableLayer layer in layers)
                    {
                        // Сохраняем слои, в которых есть изменения
                        if (layer.IsChanged)
                        {
                            DBObject dbLayer = db.GetObjectForWrite(layer.Id);
                            layer.UpdateDbObject(dbLayer);
                        }

                        // Сохраняем объекты, в которых есть изменения
                        var changedObjects = layer.Objects.Where(obj => obj.IsChanged);
                        foreach (EditableObject graphicObject in changedObjects)
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
}