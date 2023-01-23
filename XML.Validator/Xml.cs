using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Validator.Elements;

namespace XML.Validator
{
    public class Xml : IXml
    {
        
        public bool IsValid { get; private set; }
        private List<string> _elementList;
        private List<string> _elementNameList;
        private string _xml = string.Empty;

        public bool DetermineXml(string xml)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(xml, nameof(xml));

            _elementList = new List<string>();
            _elementNameList = new List<string>();
            _xml = xml;

			TryLoadXml();

            IsValid = IsXmlValid();
            return IsValid;
		}

        private void TryLoadXml()
        {
            int startIndex = 0;
            int lastIndex = _xml.Length -1;
            var elementsMap = new List<string>();

            while (startIndex < lastIndex)
            {
                int nextIndex = _xml.IndexOf(NodeTemplate.TokenEndOpenElement, startIndex) + 1;
                string elementString = _xml.Substring(startIndex, nextIndex - startIndex).ExtractElement();

                var elementName = elementString.ExtractElementName();
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

        private bool IsXmlValid()
        {
            var isValid = false;

            //It mast contain at least 2 elements
            isValid = _elementNameList.Count > 1;

            if (isValid)
                isValid = HasRootElement();

            if (isValid)
                isValid = PairsElementAreValids();

			return isValid;
        }

        private bool HasRootElement()
        {
            return  _elementNameList[0].Equals(_elementNameList[_elementNameList.Count - 1], StringComparison.Ordinal);
        }

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
