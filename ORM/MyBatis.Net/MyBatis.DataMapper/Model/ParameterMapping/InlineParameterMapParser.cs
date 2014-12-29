#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 408099 $
 * $Date: 2008-10-11 10:07:44 -0600 (Sat, 11 Oct 2008) $
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

#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.Exceptions;
using MyBatis.DataMapper.Model.Sql.Dynamic;
using MyBatis.DataMapper.Model.Statements;
using MyBatis.Common.Exceptions;
using MyBatis.Common.Utilities;

#endregion 

namespace MyBatis.DataMapper.Model.ParameterMapping
{
	/// <summary>
	/// Builds Paremeter property for Inline Parameter Map.
	/// </summary>
	public sealed class InlineParameterMapParser
	{
		private const string PARAMETER_TOKEN = "#";
		private const string PARAM_DELIM = ":";
        private const string MARK_TOKEN = "?";

        private const string NEW_BEGIN_TOKEN = "@{";
        private const string NEW_END_TOKEN = "}";

        /// <summary>
        /// Parse Inline ParameterMap
        /// 对statement insert update delete select 语句的参数进行分析
        /// </summary>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="sqlStatement">The SQL statement.</param>
        /// <returns>A new sql command text.</returns>
        public static SqlText ParseInlineParameterMap(DataExchangeFactory dataExchangeFactory, string statementId, IStatement statement, string sqlStatement)
		{
			string newSql = sqlStatement;//赋值得到一条SQL部分语句
            List<ParameterProperty> mappingList = new List<ParameterProperty>();
			Type parameterClassType = null;

			if (statement != null)
			{
				parameterClassType = statement.ParameterClass;//当前节点属性中的参数类 会不会有多个参数类的情况？？？
			}

            if (sqlStatement.Contains(NEW_BEGIN_TOKEN))//如果SQL语句包含"@{"
            {
                // V3 parameter syntax
                //@{propertyName,column=string,type=string,dbype=string,direction=[Input/Output/InputOutput],nullValue=string,handler=string}

                /*以@{开头的例子
                    <procedure id="InsertAccountViaSPWithDynamicParameter" parameterClass="Account" >
                        ps_InsertAccountWithDefault
                        @{Id,column=Account_ID}//每一个@对应一个ParameterProperty类
                        ,@{FirstName,column=Account_FirstName}
                        ,@{LastName,column=Account_LastName}
                         ,@{EmailAddress,column=Account_Email,nullValue=no_email@provided.com}
                         <isNotNull property="NullBannerOption">
                                ,@{NullBannerOption,column=Account_Banner_Option,dbType=Varchar,type=bool}
                        </isNotNull>
                        @{CartOption,column=Account_Cart_Option,handler=HundredsBool}
                   </procedure>
                           */
                if (newSql != null)
                {
                    string toAnalyse = newSql;
                    int start = toAnalyse.IndexOf(NEW_BEGIN_TOKEN);//@{的下标
                    int end = toAnalyse.IndexOf(NEW_END_TOKEN);//"}"的下标
                    StringBuilder newSqlBuffer = new StringBuilder();

                    while (start > -1 && end > start)
                    {
                        //将语句以 @{ ******  }拆分成3份字符串
                        string prepend = toAnalyse.Substring(0, start);//@{部分
                        string append = toAnalyse.Substring(end + NEW_END_TOKEN.Length);//}.....部分
                       
                        //EmailAddress,column=string,type=string,dbType=Varchar,nullValue=no_email@provided.com
                        string parameter = toAnalyse.Substring(start + NEW_BEGIN_TOKEN.Length, end - start - NEW_BEGIN_TOKEN.Length);//第一个中间内容部分

                        ParameterProperty mapping = NewParseMapping(parameter, parameterClassType, dataExchangeFactory, statementId);
                        mappingList.Add(mapping);//将解析后得到的ParameterProperty放入列表中
                        newSqlBuffer.Append(prepend);//将”@{"加入 或者是 "},@{"部分加入
                        newSqlBuffer.Append(MARK_TOKEN);//加入 "?"标志

                        //可能有多个部分 所以循环处理准备
                        toAnalyse = append;
                        start = toAnalyse.IndexOf(NEW_BEGIN_TOKEN);
                        end = toAnalyse.IndexOf(NEW_END_TOKEN);
                    }
                    /*while 循环完成后的形势是
                     *  ps_InsertAccountWithDefault
                     *  @{？}，@{？}的大概格式 保存到newSql中
                                   */
                    newSqlBuffer.Append(toAnalyse);
                    newSql = newSqlBuffer.ToString();
                }
            }
            else
            {
                #region old syntax
                /*
                   <insert id="InsertAccountViaInlineParameters"  parameterClass="Account" >
                       insert into Accounts
                      (Account_ID, Account_FirstName, Account_LastName, Account_Email)
                      values
                    (#Id#, #FirstName#, #LastName#, #EmailAddress:VarChar:no_email@provided.com#)
                   </insert>
                */
                //每# #之间就是一个ParameterProperty类
                StringTokenizer parser = new StringTokenizer(sqlStatement, PARAMETER_TOKEN, true);//"#"区分 true表示 如果有#则返回
			    StringBuilder newSqlBuffer = new StringBuilder();

			    string token = null;
			    string lastToken = null;

			    IEnumerator enumerator = parser.GetEnumerator();

			    while (enumerator.MoveNext())//获取当前符号到下一符号之间的字符串 
			    {
                    token = (string)enumerator.Current;//Current真正保存了获取当前符号到下一符号之间的字符串 或 #

				    if (PARAMETER_TOKEN.Equals(lastToken)) //如果上一次是#
				    {
                        // Double token ## = # 
                        //先处理#
					    if (PARAMETER_TOKEN.Equals(token)) //如果当前也是#
					    {
						    newSqlBuffer.Append(PARAMETER_TOKEN);
						    token = null;
					    }
                        else //如果当前不是# 也即是两个#之间的字符串
					    {
						    ParameterProperty mapping = null; 
                            //判断# #之间的内容是否含有“：”
						    if (token.IndexOf(PARAM_DELIM) > -1) //如果字符串中含有”:"符号 则进入该函数
						    {
                                mapping = OldParseMapping(token, parameterClassType, dataExchangeFactory);
						    } 
						    else //不含有"：",则再次默认","为分隔符
						    {
                                mapping = NewParseMapping(token, parameterClassType, dataExchangeFactory, statementId);
						    }															 

						    mappingList.Add(mapping);
                            newSqlBuffer.Append(MARK_TOKEN + " ");//添加"?"作为标志符号

						    enumerator.MoveNext();
						    token = (string)enumerator.Current;
						    if (!PARAMETER_TOKEN.Equals(token)) 
						    {
							    throw new DataMapperException("Unterminated inline parameter in mapped statement (" + statementId + ").");
						    }
						    token = null;
					    }
				    } 
				    else 
				    {
					    if (!PARAMETER_TOKEN.Equals(token)) 
					    {
						    newSqlBuffer.Append(token);
					    }
				    }

				    lastToken = token;
			    }
                /*处理后的sql语句格式应该为
                          insert into Accounts
                        (Account_ID, Account_FirstName, Account_LastName, Account_Email)
                        values
                       (?, ?, ?, ?)
                 *？对应的参数保存在 mappingList中  
                 *将这两部分 放到SqlText类中
                            */
                newSql = newSqlBuffer.ToString();
 
	            #endregion            
            }
            //将List内容转化为数组的形式
			ParameterProperty[] mappingArray =  mappingList.ToArray();                

            //将处理后的简化SQL语句 和 参数列表 保存到 SqlText中
			SqlText sqlText = new SqlText();
			sqlText.Text = newSql;
			sqlText.Parameters = mappingArray;

			return sqlText;
		}

        /// <summary>
        /// Parse inline parameter with syntax as
        /// #propertyName,type=string,dbype=Varchar,direction=Input,nullValue=N/A,handler=string#
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="parameterClassType">Type of the parameter class.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <param name="statementId">The statement id.</param>
        /// <returns></returns>
        /// <example>
        /// #propertyName,type=string,dbype=Varchar,direction=Input,nullValue=N/A,handler=string#
        /// </example>
        private static ParameterProperty NewParseMapping(string token, Type parameterClassType, DataExchangeFactory dataExchangeFactory, string statementId) 
		{
            string propertyName = string.Empty;
            string type = string.Empty;
            string dbType = string.Empty;
            string direction = string.Empty;
            string callBack = string.Empty;
            string nullValue = null;
            string columnName = string.Empty;

			StringTokenizer paramParser = new StringTokenizer(token, "=,", false);
            //以字符propertyName,type=string,dbype=Varchar,direction=Input,nullValue=N/A,handler=string为例
			IEnumerator enumeratorParam = paramParser.GetEnumerator();
            enumeratorParam.MoveNext();//此处获取 propertyName保存到内部的next中

            propertyName = ((string)enumeratorParam.Current).Trim();//实际上调用了next的内容

			while (enumeratorParam.MoveNext()) 
			{
                string field = ((string)enumeratorParam.Current).Trim().ToLower();
                //每MoveNext一次 获得一次数据到Current中

				if (enumeratorParam.MoveNext()) 
				{
                    string value = ((string)enumeratorParam.Current).Trim();
					if ("type".Equals(field)) 
					{
                        type = value;
					} 
					else if ("dbtype".Equals(field)) 
					{
                        dbType = value;
					} 
					else if ("direction".Equals(field)) 
					{
                        direction = value;
					}
                    else if ("nullvalue".Equals(field)) 
					{
                        if (value.StartsWith("\"") && value.EndsWith("\""))
                        {
                            nullValue = value.Substring(1, value.Length-2);
                        }
                        else
                        {
                            nullValue = value;
                        }
					} 
					else if ("handler".Equals(field)) 
					{
                        callBack = value;
					}
                    else if ("column".Equals(field))
                    {
                        columnName = value;
                    } 
					else 
					{
						throw new DataMapperException("When parsing inline parameter for statement '"+statementId+"', can't recognize parameter mapping field: '" + field + "' in " + token+", check your inline parameter syntax.");
					}
				} 
				else 
				{
					throw new DataMapperException("Incorrect inline parameter map format (missmatched name=value pairs): " + token);
				}
			}

            //将解析后的字符内容存入到ParameterProperty类中
            return new ParameterProperty(
                propertyName,
                columnName,
                callBack,
                type,
                dbType,
                direction,
                nullValue,
                0,
                0,
                -1,
                parameterClassType,
                dataExchangeFactory);
		}

        /// <summary>
        /// Parse inline parameter with syntax as
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="parameterClassType">Type of the parameter class.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        /// <example>
        /// #propertyName:dbType:nullValue#
        /// </example>
        /// <returns></returns>
        private static ParameterProperty OldParseMapping(string token, Type parameterClassType, DataExchangeFactory dataExchangeFactory) 
		{
            //目标就是解析出这3个参数
            string propertyName = string.Empty;
            string dbType = string.Empty;
            string nullValue = null;

			if (token.IndexOf(PARAM_DELIM) > -1) 
			{
				StringTokenizer paramParser = new StringTokenizer(token, PARAM_DELIM, true);
				IEnumerator enumeratorParam = paramParser.GetEnumerator();

				int n1 = paramParser.TokenNumber;
				if (n1 == 3) 
				{
					enumeratorParam.MoveNext();
					propertyName = ((string)enumeratorParam.Current).Trim();

					enumeratorParam.MoveNext();
					enumeratorParam.MoveNext(); //ignore ":"
                    dbType = ((string)enumeratorParam.Current).Trim();
				} 
				else if (n1 >= 5) 
				{
					enumeratorParam.MoveNext();
					propertyName = ((string)enumeratorParam.Current).Trim();

					enumeratorParam.MoveNext();
					enumeratorParam.MoveNext(); //ignore ":"
                    dbType = ((string)enumeratorParam.Current).Trim();

					enumeratorParam.MoveNext();
					enumeratorParam.MoveNext(); //ignore ":"
					nullValue = ((string)enumeratorParam.Current).Trim();

					while (enumeratorParam.MoveNext()) 
					{
						nullValue = nullValue + ((string)enumeratorParam.Current).Trim();
					}
				} 
				else 
				{
					throw new ConfigurationException("Incorrect inline parameter map format: " + token);
				}
			} 
			else 
			{
				propertyName = token;
			}

            return new ParameterProperty(
                propertyName,
                string.Empty,
                string.Empty,
                string.Empty,
                dbType,
                string.Empty,
                nullValue,
                0,
                0,
                -1,
                parameterClassType,
                dataExchangeFactory);
		}

	}
}
