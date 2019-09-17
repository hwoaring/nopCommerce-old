using System.Net;
using System.Text;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Html;
using Nop.Services.Localization;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents a saler attribute formatter implementation
    /// </summary>
    public partial class SalerAttributeFormatter : ISalerAttributeFormatter
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISalerAttributeParser _salerAttributeParser;
        private readonly ISalerAttributeService _salerAttributeService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public SalerAttributeFormatter(ILocalizationService localizationService,
            ISalerAttributeParser salerAttributeParser,
            ISalerAttributeService salerAttributeService,
            IWorkContext workContext)
        {
            _localizationService = localizationService;
            _salerAttributeParser = salerAttributeParser;
            _salerAttributeService = salerAttributeService;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format saler attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="separator">Separator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <returns>Formatted attributes</returns>
        public virtual string FormatAttributes(string attributesXml, string separator = "<br />", bool htmlEncode = true)
        {
            var result = new StringBuilder();

            var attributes = _salerAttributeParser.ParseSalerAttributes(attributesXml);
            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var valuesStr = _salerAttributeParser.ParseValues(attributesXml, attribute.Id);
                for (var j = 0; j < valuesStr.Count; j++)
                {
                    var valueStr = valuesStr[j];
                    var formattedAttribute = string.Empty;
                    if (!attribute.ShouldHaveValues())
                    {
                        //no values
                        if (attribute.AttributeControlType == AttributeControlType.MultilineTextbox)
                        {
                            //multiline textbox
                            var attributeName = _localizationService.GetLocalized(attribute, a => a.Name, _workContext.WorkingLanguage.Id);
                            //encode (if required)
                            if (htmlEncode)
                                attributeName = WebUtility.HtmlEncode(attributeName);
                            formattedAttribute = $"{attributeName}: {HtmlHelper.FormatText(valueStr, false, true, false, false, false, false)}";
                            //we never encode multiline textbox input
                        }
                        else if (attribute.AttributeControlType == AttributeControlType.FileUpload)
                        {
                            //file upload
                            //not supported for saler attributes
                        }
                        else
                        {
                            //other attributes (textbox, datepicker)
                            formattedAttribute = $"{_localizationService.GetLocalized(attribute, a => a.Name, _workContext.WorkingLanguage.Id)}: {valueStr}";
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = WebUtility.HtmlEncode(formattedAttribute);
                        }
                    }
                    else
                    {
                        if (int.TryParse(valueStr, out var attributeValueId))
                        {
                            var attributeValue = _salerAttributeService.GetSalerAttributeValueById(attributeValueId);
                            if (attributeValue != null)
                            {
                                formattedAttribute = $"{_localizationService.GetLocalized(attribute, a => a.Name, _workContext.WorkingLanguage.Id)}: {_localizationService.GetLocalized(attributeValue, a => a.Name, _workContext.WorkingLanguage.Id)}";
                            }
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = WebUtility.HtmlEncode(formattedAttribute);
                        }
                    }

                    if (string.IsNullOrEmpty(formattedAttribute)) 
                        continue;

                    if (i != 0 || j != 0)
                        result.Append(separator);
                    result.Append(formattedAttribute);
                }
            }

            return result.ToString();
        }

        #endregion
    }
}