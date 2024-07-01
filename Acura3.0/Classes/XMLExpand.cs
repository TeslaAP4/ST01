using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Acura3._0.Classes
{
    public class XMLExpand
    {
        /// <summary>
        /// 尋找XML的Elemnet
        /// </summary>
        /// <param name="Doc">需解析文件</param>
        /// <param name="NodeLocation">節點的位置,需以'/'區隔子節點</param>
        /// <returns>回傳Elemnet</returns>
        public static XmlElement GetElement(XmlDocument Doc, string NodeLocation)
        {
            XmlElement FatherElement = null;
            XmlElement ChildElement = null;
            string[] Nodes = NodeLocation.Split('/'); //切割Nodes
            for (int i = 0; i < Nodes.Length; i++)
            {
                if ((ChildElement = (XmlElement)Doc.SelectSingleNode(GetNodePath(Nodes, i))) == null)
                {
                    ChildElement = Doc.CreateElement(Nodes[i]);
                    if (FatherElement == null)
                        Doc.AppendChild(ChildElement);
                    else
                        FatherElement.AppendChild(ChildElement);
                }
                FatherElement = ChildElement;
            }
            return ChildElement;
        }

        /// <summary>
        /// 儲存編碼成unicode的XML
        /// </summary>
        /// <param name="WriteDoc">需儲存文件</param>
        /// <param name="WritePath">寫入路徑</param>
        public static void WriteUnicodeXML(XmlDocument WriteDoc, string WritePath)
        {
            // Create an XML declaration. 
            XmlDeclaration xmldecl;
            xmldecl = WriteDoc.CreateXmlDeclaration("1.0", null, null);
            xmldecl.Encoding = "unicode";
            xmldecl.Standalone = "yes";

            // Add the new node to the document.
            XmlElement root = WriteDoc.DocumentElement;
            WriteDoc.InsertBefore(xmldecl, root);
            WriteDoc.Save(WritePath);
        }

        /// <summary>
        /// 取得節點路徑
        /// </summary>
        /// <param name="Nodes">節點陣列</param>
        /// <param name="Index">節點的Index</param>
        /// <returns></returns>
        private static string GetNodePath(string[] Nodes, int Index)
        {
            string sNodePath = "";
            for (int i = 0; i <= Index; i++)
                if (i != Index)
                    sNodePath += Nodes[i] + "/";
                else
                    sNodePath += Nodes[i];
            return sNodePath;
        }
    }
}