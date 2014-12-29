#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 591621 $
 * $Date: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
 * 
 * iBATIS.NET Data Mapper
 * Copyright (C) 2008/2005 - The Apache Software Foundation
 *  
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 ********************************************************************************/
#endregion

using System.Xml;
using System.Collections.Generic;
using MyBatis.Common.Configuration;

namespace MyBatis.DataMapper.Configuration.Interpreters.Config.Xml.Processor
{
    /// <summary>
    /// Represents an Xml node
    /// 一个xml节点信息汇总类
    /// </summary>
    public class Tag 
    {
        /// <summary>
        /// 节点的名字
        /// </summary>
        private readonly string name = string.Empty;
        /// <summary>
        /// 节点的值
        /// </summary>
        private readonly string value = string.Empty;
        /// <summary>
        /// 节点的属性集合
        /// </summary>
        private readonly IDictionary<string, string> attributes = new Dictionary<string, string>();
        /// <summary>
        /// 节点的属性名称集合
        /// </summary>
        private readonly List<string> attributesName = new List<string>();
        /// <summary>
        /// 节点的属性值集合
        /// </summary>
        private readonly List<string> attributesValue = new List<string>();
        /// <summary>
        /// 节点的父节点
        /// </summary>
        private readonly Tag parent = null;
        private IConfiguration configuration = null;

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public Tag Parent
        {
            get { return parent; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public IDictionary<string, string> Attributes
        {
            get { return attributes; }
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetAttributeName(int index)
        {
            return attributesName[index];
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetAttributeValue(int index)
        {
            return attributesValue[index];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="tagParent">The tag parent. I</param>
        /// <param name="configurationStore">The configuration store.</param>
        public Tag(XmlTextReader reader, Tag tagParent, IConfigurationStore configurationStore)
		{
            if (reader.Name.Length == 0)
            {
                // Case of txt node
                name = reader.NodeType.ToString();//当前节点的类型
                /*
                       * None:     None This is returned by the XmlReader if a Read method has not been called.  
                       * Element :   An element (for example, <item> ). 
                       * Attribute  :   An attribute (for example, id='123' ). 
                       * Text  :    The text content of a node.
                       * CDATA  :      A CDATA section (for example, <![CDATA[my escaped text]]> ). 
                       * EntityReference  :        A reference to an entity (for example, &num; ). 
                       * Entity         :       An entity declaration (for example, <!ENTITY...> ). 
                       * ProcessingInstruction :A processing instruction (for example, <?pi test?> ). 
                       * Comment :A comment (for example, <!-- my comment --> ). 
                      * Document:A document object that, as the root of the document tree, provides access to the entire XML document.
                      * DocumentType :The document type declaration, indicated by the following tag (for example, <!DOCTYPE...> ). 
                      *DocumentFragment  ： A document fragment.
                     * Notation ： A notation in the document type declaration (for example, <!NOTATION...> ). 
                     * Whitespace ：  White space between markup.
                     * SignificantWhitespace ： White space between markup in a mixed content model or white space within the xml:space="preserve" scope. 
                     * EndElement ： An end element tag (for example, </item> ). 
                     * EndEntity ： Returned when XmlReader gets to the end of the entity replacement as a result of a call to ResolveEntity. 
                 * XmlDeclaration ： The XML declaration (for example, <?xml version='1.0'?> ). 
                    */
            }
            else
            {
                name = reader.Name;//当前节点的名字
            }
            parent = tagParent;//当前节点的父节点
            value = Tag.ParsePropertyTokens(reader.Value, configurationStore);//处理${}之间的属性值
            LoadAttributes(reader, configurationStore);
		}

        /// <summary>
        /// Loads the attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="configurationStore">The configuration store.</param>
        private void LoadAttributes(XmlTextReader reader, IConfigurationStore configurationStore)
        {
            reader.MoveToFirstAttribute();//指向第一个属性

            //遍历所有的当前节点的属性
            for (int index = 0; index < reader.AttributeCount; index++)
            {
                attributesName.Add(reader.Name);//当前节点的当前属性名称

                string value = Tag.ParsePropertyTokens(reader.Value, configurationStore);
                attributesValue.Add(value);//当前节点的当前属性名称对应的值

                attributes[reader.Name] = value;//当前节点的当前属性名称与值
                reader.MoveToNextAttribute();//指向下一个属性
            }

            reader.MoveToElement();
        }


        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
		public override string ToString()
		{
				return Name;
		}

        /// <summary>
        /// Replace properties by their values in the given string
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="configStore">The config store.</param>
        /// <returns></returns>
        public static string ParsePropertyTokens(string str, IConfigurationStore configStore)
        {
            string OPEN = "${";
            string CLOSE = "}";

            string newString = str;
            if (newString != null && configStore != null)
            {
                int start = newString.IndexOf(OPEN);
                int end = newString.IndexOf(CLOSE);

                while (start > -1 && end > start)
                {
                    string prepend = newString.Substring(0, start);
                    string append = newString.Substring(end + CLOSE.Length);

                    int index = start + OPEN.Length;
                    string propName = newString.Substring(index, end - index);
                    IConfiguration config = configStore.GetPropertyConfiguration(propName);
                    if (config == null)
                    {
                        newString = prepend + propName + append;
                    }
                    else
                    {
                        newString = prepend + config.Value + append;
                    }
                    start = newString.IndexOf(OPEN);
                    end = newString.IndexOf(CLOSE);
                }
            }
            return newString;
        }
    }
}
