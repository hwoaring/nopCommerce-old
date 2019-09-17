namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents a saler attribute formatter
    /// </summary>
    public partial interface ISalerAttributeFormatter
    {
        /// <summary>
        /// Format saler attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="separator">Separator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <returns>Formatted attributes</returns>
        string FormatAttributes(string attributesXml, string separator = "<br />", bool htmlEncode = true);
    }
}