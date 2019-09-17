using System.Collections.Generic;
using Nop.Core.Domain.Salers;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents a saler attribute parser
    /// </summary>
    public partial interface ISalerAttributeParser
    {
        /// <summary>
        /// Gets saler attributes from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>List of saler attributes</returns>
        IList<SalerAttribute> ParseSalerAttributes(string attributesXml);

        /// <summary>
        /// Get saler attribute values from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>List of saler attribute values</returns>
        IList<SalerAttributeValue> ParseSalerAttributeValues(string attributesXml);

        /// <summary>
        /// Gets values of the selected saler attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="salerAttributeId">Saler attribute identifier</param>
        /// <returns>Values of the saler attribute</returns>
        IList<string> ParseValues(string attributesXml, int salerAttributeId);

        /// <summary>
        /// Adds a saler attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="salerAttribute">Saler attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes in XML format</returns>
        string AddSalerAttribute(string attributesXml, SalerAttribute salerAttribute, string value);

        /// <summary>
        /// Validates saler attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Warnings</returns>
        IList<string> GetAttributeWarnings(string attributesXml);
    }
}