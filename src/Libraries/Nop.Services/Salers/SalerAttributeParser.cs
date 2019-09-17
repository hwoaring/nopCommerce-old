using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Nop.Core.Domain.Salers;
using Nop.Services.Localization;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents a saler attribute parser implementation
    /// </summary>
    public partial class SalerAttributeParser : ISalerAttributeParser
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISalerAttributeService _salerAttributeService;

        #endregion

        #region Ctor

        public SalerAttributeParser(ILocalizationService localizationService,
            ISalerAttributeService salerAttributeService)
        {
            _localizationService = localizationService;
            _salerAttributeService = salerAttributeService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets saler attribute identifiers
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>List of saler attribute identifiers</returns>
        protected virtual IList<int> ParseSalerAttributeIds(string attributesXml)
        {
            var ids = new List<int>();
            if (string.IsNullOrEmpty(attributesXml))
                return ids;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//Attributes/SalerAttribute"))
                {
                    if (node.Attributes?["ID"] == null) 
                        continue;

                    var str1 = node.Attributes["ID"].InnerText.Trim();
                    if (int.TryParse(str1, out var id))
                    {
                        ids.Add(id);
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }

            return ids;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets saler attributes from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>List of saler attributes</returns>
        public virtual IList<SalerAttribute> ParseSalerAttributes(string attributesXml)
        {
            var result = new List<SalerAttribute>();
            if (string.IsNullOrEmpty(attributesXml))
                return result;

            var ids = ParseSalerAttributeIds(attributesXml);
            foreach (var id in ids)
            {
                var attribute = _salerAttributeService.GetSalerAttributeById(id);
                if (attribute != null)
                {
                    result.Add(attribute);
                }
            }

            return result;
        }

        /// <summary>
        /// Get saler attribute values from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>List of saler attribute values</returns>
        public virtual IList<SalerAttributeValue> ParseSalerAttributeValues(string attributesXml)
        {
            var values = new List<SalerAttributeValue>();
            if (string.IsNullOrEmpty(attributesXml))
                return values;

            var attributes = ParseSalerAttributes(attributesXml);
            foreach (var attribute in attributes)
            {
                if (!attribute.ShouldHaveValues())
                    continue;

                var valuesStr = ParseValues(attributesXml, attribute.Id);
                foreach (var valueStr in valuesStr)
                {
                    if (string.IsNullOrEmpty(valueStr)) 
                        continue;

                    if (!int.TryParse(valueStr, out var id)) 
                        continue;

                    var value = _salerAttributeService.GetSalerAttributeValueById(id);
                    if (value != null)
                        values.Add(value);
                }
            }

            return values;
        }

        /// <summary>
        /// Gets values of the selected saler attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="salerAttributeId">Saler attribute identifier</param>
        /// <returns>Values of the saler attribute</returns>
        public virtual IList<string> ParseValues(string attributesXml, int salerAttributeId)
        {
            var selectedSalerAttributeValues = new List<string>();
            if (string.IsNullOrEmpty(attributesXml))
                return selectedSalerAttributeValues;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/SalerAttribute");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["ID"] == null) 
                        continue;

                    var str1 = node1.Attributes["ID"].InnerText.Trim();
                    if (!int.TryParse(str1, out var id)) 
                        continue;

                    if (id != salerAttributeId) 
                        continue;

                    var nodeList2 = node1.SelectNodes(@"SalerAttributeValue/Value");
                    foreach (XmlNode node2 in nodeList2)
                    {
                        var value = node2.InnerText.Trim();
                        selectedSalerAttributeValues.Add(value);
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }

            return selectedSalerAttributeValues;
        }

        /// <summary>
        /// Adds a saler attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="salerAttribute">Saler attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes in XML format</returns>
        public virtual string AddSalerAttribute(string attributesXml, SalerAttribute salerAttribute, string value)
        {
            var result = string.Empty;
            try
            {
                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(attributesXml))
                {
                    var element1 = xmlDoc.CreateElement("Attributes");
                    xmlDoc.AppendChild(element1);
                }
                else
                {
                    xmlDoc.LoadXml(attributesXml);
                }

                var rootElement = (XmlElement)xmlDoc.SelectSingleNode(@"//Attributes");

                XmlElement attributeElement = null;
                //find existing
                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/SalerAttribute");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["ID"] == null) 
                        continue;

                    var str1 = node1.Attributes["ID"].InnerText.Trim();
                    if (!int.TryParse(str1, out var id)) 
                        continue;

                    if (id != salerAttribute.Id) 
                        continue;

                    attributeElement = (XmlElement)node1;
                    break;
                }

                //create new one if not found
                if (attributeElement == null)
                {
                    attributeElement = xmlDoc.CreateElement("SalerAttribute");
                    attributeElement.SetAttribute("ID", salerAttribute.Id.ToString());
                    rootElement.AppendChild(attributeElement);
                }

                var attributeValueElement = xmlDoc.CreateElement("SalerAttributeValue");
                attributeElement.AppendChild(attributeValueElement);

                var attributeValueValueElement = xmlDoc.CreateElement("Value");
                attributeValueValueElement.InnerText = value;
                attributeValueElement.AppendChild(attributeValueValueElement);

                result = xmlDoc.OuterXml;
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }

            return result;
        }

        /// <summary>
        /// Validates saler attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Warnings</returns>
        public virtual IList<string> GetAttributeWarnings(string attributesXml)
        {
            var warnings = new List<string>();

            //ensure it's our attributes
            var attributes1 = ParseSalerAttributes(attributesXml);

            //validate required saler attributes (whether they're chosen/selected/entered)
            var attributes2 = _salerAttributeService.GetAllSalerAttributes();
            foreach (var a2 in attributes2)
            {
                if (!a2.IsRequired) 
                    continue;

                var found = false;
                //selected saler attributes
                foreach (var a1 in attributes1)
                {
                    if (a1.Id != a2.Id) 
                        continue;

                    var valuesStr = ParseValues(attributesXml, a1.Id);
                    foreach (var str1 in valuesStr)
                    {
                        if (string.IsNullOrEmpty(str1.Trim())) 
                            continue;

                        found = true;
                        break;
                    }
                }
                
                if (found) 
                    continue;

                //if not found
                var notFoundWarning = string.Format(_localizationService.GetResource("ShoppingCart.SelectAttribute"), _localizationService.GetLocalized(a2, a => a.Name));

                warnings.Add(notFoundWarning);
            }

            return warnings;
        }

        #endregion
    }
}