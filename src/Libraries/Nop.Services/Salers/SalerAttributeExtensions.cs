using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Salers;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents saler attribute extensions
    /// </summary>
    public static class SalerAttributeExtensions
    {
        /// <summary>
        /// A value indicating whether this saler attribute should have values
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        /// <returns>True if the attribute should have values; otherwise false</returns>
        public static bool ShouldHaveValues(this SalerAttribute salerAttribute)
        {
            if (salerAttribute == null)
                return false;

            if (salerAttribute.AttributeControlType == AttributeControlType.TextBox ||
                salerAttribute.AttributeControlType == AttributeControlType.MultilineTextbox ||
                salerAttribute.AttributeControlType == AttributeControlType.Datepicker ||
                salerAttribute.AttributeControlType == AttributeControlType.FileUpload)
                return false;

            //other attribute control types support values
            return true;
        }
    }
}