using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutoCadObjectEditor
{
    public static class DependencyObjectExtensions
    {
        public static string GetValidationErrors(this DependencyObject depObj)
        {
            var sb = new StringBuilder();

            foreach (DependencyObject element in FindVisualChildren(depObj))
            {
                if (Validation.GetHasError(element))
                {
                    sb.Append("Ошибка:\r\n");
                    foreach (ValidationError error in Validation.GetErrors(element))
                    {
                        sb.Append("  " + error.ErrorContent.ToString());
                        sb.Append("\r\n");
                    }
                }
            }

            return sb.ToString();
        }

        private static IEnumerable<DependencyObject> FindVisualChildren(DependencyObject depObj)
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null)
                    {
                        yield return child;
                    }

                    foreach (DependencyObject childOfChild in FindVisualChildren(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}