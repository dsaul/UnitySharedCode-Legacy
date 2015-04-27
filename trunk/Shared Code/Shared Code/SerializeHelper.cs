using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace SharedCode
{
	public static class SerializeHelper
	{
		/// <summary>
		/// Serializes an object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializableObject"></param>
		/// <param name="fileName"></param>
		public static void SerializeObject<T>(T serializableObject, string fileName)
		{
			if (serializableObject == null) { return; }
			
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlDeclaration xmldecl = xmlDocument.CreateXmlDeclaration("1.0", null, null);
				XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
				using (MemoryStream stream = new MemoryStream())
				{
					serializer.Serialize(stream, serializableObject);
					stream.Position = 0;
					xmlDocument.Load(stream);
					//xmlDocument.Save(fileName);
					using (TextWriter sw = new StreamWriter(fileName, false, Encoding.UTF8)) //Set encoding
						xmlDocument.Save(sw);
					stream.Close();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				//Log exception here
			}
		}
		
		
		/// <summary>
		/// Deserializes an xml file into an object list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static T DeSerializeObject<T>(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) { return default(T); }
			
			T objectOut = default(T);
			
			try
			{
				//string attributeXml = string.Empty;
				
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(fileName);
				string xmlString = xmlDocument.OuterXml;
				
				using (StringReader read = new StringReader(xmlString))
				{
					Type outType = typeof(T);
					
					XmlSerializer serializer = new XmlSerializer(outType);
					using (XmlReader reader = new XmlTextReader(read))
					{
						objectOut = (T)serializer.Deserialize(reader);
						reader.Close();
					}
					
					read.Close();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				//Log exception here
			}
			
			return objectOut;
		}
	}
}
