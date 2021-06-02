using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace AutoCadObjectEditor.ValidationRules
{
    public class LayerNameValidationRule : ValidationRule
    {
        private const int MAX_LAYER_NAME_LENGTH = 255;
        private const string DEFAULT_LAYER_NAME = "0";

        private readonly char[] _invalidChars = { '<', '>', '/', '\\', '"', ';', ':', '?', '*', '|', ',', '=', '`' };

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            if (string.IsNullOrEmpty(str))
            {
                return new ValidationResult(false, "Введите название");
            }

            if (str.Length > MAX_LAYER_NAME_LENGTH)
            {
                return new ValidationResult(false, "Превышена максимальная длина (255 символов)");
            }

            if (str == DEFAULT_LAYER_NAME)
            {
                return new ValidationResult(false, "Название слоя является зарезервированным");
            }

            if (str.Any(c => _invalidChars.Contains(c)))
            {
                return new ValidationResult(false, "Следующие символы запрещены в названии слоя:\n" + new string(_invalidChars));
            }

            return ValidationResult.ValidResult;
        }
    }
}