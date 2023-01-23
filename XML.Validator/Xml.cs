using XML.Validator.Elements;
using XML.Validator.Exceptions;

namespace XML.Validator
{
    public class Xml : IXml
    {       
        public bool IsValid { get; private set; }
        private List<string> _elementList;
        private List<string> _elementNameList;
        private string _xml = string.Empty;

        /// <summary>
        /// Determinates if the xml string is well-formed
        /// </summary>
        /// <param name="xml">The xml string</param>
        /// <returns>True if the xml string is valid</returns>
        public bool DetermineXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException(nameof(xml));

            _elementList = new List<string>();
            _elementNameList = new List<string>();
            _xml = xml;

			TryLoadXml();

            IsValid = IsXmlValid();
            return IsValid;
		}

        /// <summary>
        /// Tries to load the xml element for post processing
        /// </summary>
        private void TryLoadXml()
        {
            int startIndex = 0;
            int lastIndex = _xml.Length -1;

            //Loads only the valid elements with their corresponding open and close tokens '<', '</', '>' 
            //Otherwise the element is not valid and it will not be uploaded
            while (startIndex < lastIndex)
            {
                //Find the index by close token
                int nextIndex = _xml.IndexOf(NodeTemplate.TokenEndOpenElement, startIndex) + 1;
                string elementString = _xml.Substring(startIndex, nextIndex - startIndex).ExtractElement();
                string elementName = elementString.ExtractElementName();
                //If the variables are empty, mean that are not valid element
                if (!string.IsNullOrWhiteSpace(elementName))
                {
					_elementNameList.Add(elementName);
				}
                if (!string.IsNullOrWhiteSpace(elementString))
                {
                    _elementList.Add(elementString);
                }
                
                startIndex = nextIndex;
            }    
        }

        /// <summary>
        /// Runs the xml validations
        /// </summary>
        /// <returns>True if the xml string is valid</returns>
        private bool IsXmlValid()
        {
            var isValid = false;

            //It must contain at least 2 elements
            isValid = _elementNameList.Count > 1;

            if (isValid)
                isValid = HasRootElement();

            //Continue the validation if there are children nodes
            if (isValid && _elementNameList.Count > 2)
                isValid = PairsElementAreValids();

			return isValid;
        }

        /// <summary>
        /// Validates if the xml string contains their pairs root elements
        /// </summary>
        /// <returns>True if the string contains root elements</returns>
        private bool HasRootElement()
        {
            return  _elementNameList[0].Equals(_elementNameList[_elementNameList.Count - 1], StringComparison.Ordinal);
        }

        /// <summary>
        /// Validates if every element have their pairs, open and close "<element></element>"
        /// </summary>
        /// <returns>True if the elements have their pairs</returns>
        private bool PairsElementAreValids()
        {
            bool valid = false;
            for (int i = 1; i < _elementNameList.Count - 2; i++)
            {
                valid = _elementNameList[i].Equals(_elementNameList[i + 1], StringComparison.Ordinal);
                if (!valid)
                    break;
                i = i + 1;
            }

            return valid;
        }
       
    }
}
