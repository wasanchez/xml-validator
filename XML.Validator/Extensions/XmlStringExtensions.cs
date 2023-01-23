using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using XML.Validator.Elements;

namespace XML.Validator
{
	public static class XmlStringExtensions
	{
		/// <summary>
		/// Extrats only the element name
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>The element name</returns>
		public static string ExtractElementName(this string xmlFragment)
		{
			string elementName = string.Empty;
			int startTokenIndex = 0;
			int endTokenIndex = 0;
			if (string.IsNullOrWhiteSpace(xmlFragment) || !xmlFragment.HasTokens())
				return elementName;

			//Finds the token index
			startTokenIndex = xmlFragment.HasTokenStartCloseElement() ? xmlFragment.GetTokenStartIndex() + 1 : xmlFragment.GetTokenStartIndex();
			endTokenIndex = xmlFragment.GetTokenEndIndex();
			//Avoid an ArgumentOutOfRangeException
			if (startTokenIndex < 0 || endTokenIndex < 0)
				return elementName;

			//Extract only the element name
			elementName = xmlFragment.Substring(startTokenIndex + 1, endTokenIndex - startTokenIndex - 1);
			
			return elementName;
		}

		/// <summary>
		/// Returns the element from the xml string fragment
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>The string element</returns>
		public static string ExtractElement(this string xmlFragment)
		{
			string element = string.Empty;
			int startTokenIndex = 0;
			int endTokenIndex = 0;
			if (string.IsNullOrWhiteSpace(xmlFragment) || !xmlFragment.HasTokens())
				return element;

			//Finds the token index
			startTokenIndex = xmlFragment.GetTokenStartIndex();
			endTokenIndex = xmlFragment.GetTokenEndIndex();
			//Avoid an ArgumentOutOfRangeException
			if (startTokenIndex < 0 || endTokenIndex < 0)
				return element;

			//Extract only the element name
			element = xmlFragment.Substring(startTokenIndex, endTokenIndex - startTokenIndex + 1);

			return element;
		}

		/// <summary>
		/// Returns true if the string contains the open and close element tokens <b> '<' </' '>' </b>
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>True if has the tokens element</returns>
		public static bool HasTokens(this string xmlFragment)
		{
			return xmlFragment.Contains(NodeTemplate.TokenStartOpenElement) ||
					xmlFragment.Contains(NodeTemplate.TokenStartCloseElement) &&
					xmlFragment.Contains(NodeTemplate.TokenEndOpenElement);
		}

		/// <summary>
		/// Returns the start token index <b>'<' or '</'</b>
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>The index</returns>
		public static int GetTokenStartIndex(this string xmlFragment)
		{
			int index = -1;

			if (xmlFragment.HasTokenStartOpenElement())
				index = xmlFragment.IndexOf(NodeTemplate.TokenStartOpenElement);
			else if (xmlFragment.HasTokenStartCloseElement())
				index = xmlFragment.IndexOf(NodeTemplate.TokenStartCloseElement);

			return index;
		}

		/// <summary>
		/// Returns true if the string contains the token open element <b>'<'</b>
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>True if contains the token</returns>
		public static bool HasTokenStartOpenElement(this string xmlFragment)
		{
			return xmlFragment.Contains(NodeTemplate.TokenStartOpenElement);
		}

		/// <summary>
		/// Returns true if the string contains the token close element <b>'</'</b>
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>True if contains the token</returns>
		public static bool HasTokenStartCloseElement(this string xmlFragment)
		{
			return xmlFragment.Contains(NodeTemplate.TokenStartCloseElement);
		}

		/// <summary>
		/// Returns the end token index <b>'>'</b>
		/// </summary>
		/// <param name="xmlFragment">A xml string fragment</param>
		/// <returns>The index</returns>
		public static int GetTokenEndIndex(this string xmlFragment)
		{
			int index = -1;

			if (xmlFragment.Contains(NodeTemplate.TokenEndOpenElement))
				index = xmlFragment.IndexOf(NodeTemplate.TokenEndOpenElement);			 

			return index;
		}
	}
}

