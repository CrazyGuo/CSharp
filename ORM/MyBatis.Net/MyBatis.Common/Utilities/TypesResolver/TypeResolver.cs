
#region Apache Notice
/*****************************************************************************
 * $Revision: 575944 $
 * $LastChangedDate: 2008-10-16 12:14:45 -0600 (Thu, 16 Oct 2008) $
 * $LastChangedBy: gbayon $
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

#region Remarks
// Inpspired from Spring.NET
#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MyBatis.Common.Contracts;
using MyBatis.Common.Contracts.Constraints;

#endregion

namespace MyBatis.Common.Utilities.TypesResolver
{
	/// <summary>
	/// Resolves a <see cref="System.Type"/> by name.
	/// </summary>
	/// <remarks>
	/// <p>
	/// The rationale behind the creation of this class is to centralise the
	/// resolution of type names to <see cref="System.Type"/> instances beyond that
	/// offered by the plain vanilla System.Type.GetType method call.
	/// </p>
	/// </remarks>
	/// <version>$Id: TypeResolver.cs 705306 2008-10-16 18:14:45Z gbayon $</version>
    public class TypeResolver : ITypeResolver
	{
        private const string NULLABLE_TYPE = "System.Nullable";

        #region ITypeResolver Members
        /// <summary>
        /// Resolves the supplied <paramref name="typeName"/> to a
        /// <see cref="System.Type"/> instance.
        /// </summary>
        /// <param name="typeName">
        /// The unresolved name of a <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        public virtual Type Resolve(string typeName)
        {
            Type type = ResolveGenericType(typeName.Replace(" ", string.Empty));//先进行一般的类型分析
            if (type == null)//一般类型分析失败，再进行第二次分析
            {
                type = ResolveType(typeName.Replace(" ", string.Empty));
            }
            return type;
        }
        #endregion

        /// <summary>
        /// Resolves the supplied generic <paramref name="typeName"/>,
        /// substituting recursively all its type parameters., 
        /// to a <see cref="System.Type"/> instance.
        /// </summary>
        /// <param name="typeName">
        /// The (possibly generic) name of a <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        private static Type ResolveGenericType(string typeName)
        {
            Contract.Require.That(typeName, Is.Not.Null & Is.Not.Empty).When("retrieving argument typeName for ResolveType method");

            if (typeName.StartsWith(NULLABLE_TYPE))//如果有NullAble类型开始
            {
                return null;
            }
            GenericArgumentsInfo genericInfo = new GenericArgumentsInfo(typeName);
            Type type = null;
            try
            {
                if (genericInfo.ContainsGenericArguments)
                {
                    type = TypeUtils.ResolveType(genericInfo.GenericTypeName);//此处可能会有递归的调用
                    if (!genericInfo.IsGenericDefinition)
                    {
                        string[] unresolvedGenericArgs = genericInfo.GetGenericArguments();
                        Type[] genericArgs = new Type[unresolvedGenericArgs.Length];
                        for (int i = 0; i < unresolvedGenericArgs.Length; i++)
                        {
                            genericArgs[i] = TypeUtils.ResolveType(unresolvedGenericArgs[i]);//对每一个参数进行类型判断分析
                        }
                        //替代由当前泛型类型定义的类型参数组成的类型数组的元素
                        type = type.MakeGenericType(genericArgs);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is TypeLoadException)
                {
                    throw;
                }
                throw BuildTypeLoadException(typeName, ex);
            }
            return type;
        }

        /// <summary>
        /// Resolves the supplied <paramref name="typeName"/> to a
        /// <see cref="System.Type"/>
        /// instance.
        /// </summary>
        /// <param name="typeName">
        /// The (possibly partially assembly qualified) name of a
        /// <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        public static Type ResolveType(string typeName)
        {
            Contract.Require.That(typeName, Is.Not.Null & Is.Not.Empty).When("retrieving argument typeName for ResolveType method");
            //程序集信息类
            TypeAssemblyInfo typeInfo = new TypeAssemblyInfo(typeName);
            Type type = null;
            try
            {
                type = (typeInfo.IsAssemblyQualified) ?
                     LoadTypeDirectlyFromAssembly(typeInfo) :
                     LoadTypeByIteratingOverAllLoadedAssemblies(typeInfo);
            }
            catch (Exception ex)
            {
                throw BuildTypeLoadException(typeName, ex);
            }

            Contract.Ensure.That<TypeLoadException>(type, Is.Not.Null).When("resolving type name: " + typeName+". Cause: A type alias for this type is not registered.");

            return type;
        }

        /// <summary>
        /// Uses <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/>
        /// to load an <see cref="System.Reflection.Assembly"/> and then the attendant
        /// <see cref="System.Type"/> referred to by the <paramref name="typeInfo"/>
        /// parameter.
        /// </summary>
        /// <remarks>
        /// <p>
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/> is
        /// deprecated in .NET 2.0, but is still used here (even when this class is
        /// compiled for .NET 2.0);
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/> will
        /// still resolve (non-.NET Framework) local assemblies when given only the
        /// display name of an assembly (the behaviour for .NET Framework assemblies
        /// and strongly named assemblies is documented in the docs for the
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/> method).
        /// </p>
        /// </remarks>
        /// <param name="typeInfo">
        /// The assembly and type to be loaded.
        /// </param>
        /// <returns>
        /// A <see cref="System.Type"/>, or <see lang="null"/>.
        /// </returns>
        /// <exception cref="System.Exception">
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/>
        /// </exception>
        private static Type LoadTypeDirectlyFromAssembly(TypeAssemblyInfo typeInfo)
        {
            Type type = null;
            // assembly qualified... load the assembly, then the Type
            Assembly assembly = Assembly.Load(typeInfo.AssemblyName);//通过给定程序集的长格式名称加载程序集

            if (assembly != null)
            {
                type = assembly.GetType(typeInfo.TypeName, true, true);
            }
            return type;
        }

        /// <summary>
        /// Check all assembly
        /// to load the attendant <see cref="System.Type"/> referred to by 
        /// the <paramref name="typeInfo"/> parameter.
        /// </summary>
        /// <param name="typeInfo">
        /// The type to be loaded.
        /// </param>
        /// <returns>
        /// A <see cref="System.Type"/>, or <see lang="null"/>.
        /// </returns>
        private static Type LoadTypeByIteratingOverAllLoadedAssemblies(TypeAssemblyInfo typeInfo)
        {
            Type type = null;
            //获取已加载到此应用程序域的执行上下文中的程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                //获取程序集实例中具有指定名称的 Type 对象，带有忽略大小写和在找不到该类型时引发异常的选项
                type = assemblies[i].GetType(typeInfo.TypeName, false, false);
                if (type != null)
                {
                    break;
                }
            }
            return type;
        }

        private static TypeLoadException BuildTypeLoadException(string typeName, Exception ex)
        {
            return new TypeLoadException("Could not load type from string value '" + typeName + "'.", ex);
        }

        #region Inner Class : GenericArgumentsInfo

        /// <summary>
        /// Holder for the generic arguments when using type parameters.
        /// </summary>
        /// <remarks>
        /// <p>
        /// Type parameters can be applied to classes, interfaces, 
        /// structures, methods, delegates, etc...
        /// </p>
        /// </remarks>
        internal class GenericArgumentsInfo
        {
            #region Constants

            /// <summary>
            /// The generic arguments prefix.
            /// </summary>
            public const string GENERIC_ARGUMENTS_PREFIX = "[[";

            /// <summary>
            /// The generic arguments suffix.
            /// </summary>
            public const string GENERIC_ARGUMENTS_SUFFIX = "]]";

            /// <summary>
            /// The character that separates a list of generic arguments.
            /// </summary>
            public const string GENERIC_ARGUMENTS_SEPARATOR = "],[";
            
            #endregion

            #region Fields
            /// <summary>
            /// 未分析类型的名称
            /// </summary>
            private string _unresolvedGenericTypeName = string.Empty;
            /// <summary>
            /// 未分析类型名称的参数列表
            /// </summary>
            private string[] _unresolvedGenericArguments = null;

            /// <summary>
            /// 使用正则表达式判断是否含有规定的标志
            /// </summary>
            private readonly static Regex generic = new Regex(@"`\d*\[\[", RegexOptions.Compiled); 

            #endregion

            #region Constructor (s) / Destructor

            /// <summary>
            /// Creates a new instance of the GenericArgumentsInfo class.
            /// </summary>
            /// <param name="value">
            /// The string value to parse looking for a generic definition
            /// and retrieving its generic arguments.默认调用对value字符串表示的类型进行分析
            /// </param>
            public GenericArgumentsInfo(string value)
            {
                ParseGenericArguments(value);
            }

            #endregion

            #region Properties

            /// <summary>
            /// The (unresolved) generic type name portion 
            /// of the original value when parsing a generic type.
            /// </summary>
            public string GenericTypeName
            {
                get { return _unresolvedGenericTypeName; }
            }


            /// <summary>
            /// Is the string value contains generic arguments ?
            /// </summary>
            /// <remarks>
            /// <p>
            /// A generic argument can be a type parameter or a type argument.
            /// </p>
            /// </remarks>
            public bool ContainsGenericArguments
            {
                get
                {
                    return (_unresolvedGenericArguments != null &&
                        _unresolvedGenericArguments.Length > 0);
                }
            }

            /// <summary>
            /// Is generic arguments only contains type parameters ?
            /// </summary>
            public bool IsGenericDefinition
            {
                get
                {
                    if (_unresolvedGenericArguments == null)
                        return false;

                    for (int i = 0; i < _unresolvedGenericArguments.Length; i++)
                    {
                        if (_unresolvedGenericArguments[i].Length > 0)
                            return false;
                    }
                    return true;
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns an array of unresolved generic arguments types.
            /// </summary>
            /// <remarks>
            /// <p>
            /// A empty string represents a type parameter that 
            /// did not have been substituted by a specific type.
            /// </p>
            /// </remarks>
            /// <returns>
            /// An array of strings that represents the unresolved generic 
            /// arguments types or an empty array if not generic.
            /// </returns>
            public string[] GetGenericArguments()
            {
                if (_unresolvedGenericArguments == null)
                {
                    return new string[] { };
                }

                return _unresolvedGenericArguments;
            }

            private void ParseGenericArguments(string originalString)
            {
                //指示 Regex 构造函数中指定的正则表达式在输入字符串中是否找到匹配项
                bool isMatch = generic.IsMatch(originalString); 

                if (!isMatch)
                {
                    _unresolvedGenericTypeName = originalString;//记录未分析成功的类型名称，并结束
                }
                else//继续分析参数类型
                {
                    //获取第一个“[[”的下标地址，也即
                    int argsStartIndex = originalString.IndexOf(GENERIC_ARGUMENTS_PREFIX);
                    //获取最后一个"]]"的下标地址
                    int argsEndIndex = originalString.LastIndexOf(GENERIC_ARGUMENTS_SUFFIX);
                    if (argsEndIndex != -1)
                    {
                        //获取“[["和”]]"的字符串并分析，也即其中的参数
                        SplitGenericArguments(originalString.Substring(
                            argsStartIndex + 1, argsEndIndex - argsStartIndex));
                        //从此实例中的指定位置开始删除指定数目的字符,也即字符串中去掉“[["和"]]"之间内容后剩下的字符串，等待分析，也即类型名称
                        _unresolvedGenericTypeName = originalString.Remove(argsStartIndex, argsEndIndex - argsStartIndex + 2);
                    }
                }
            }

            private void SplitGenericArguments(string originalArgs)
            {
                IList<string> arguments = new List<string>();
                //字符串中是否包含 "],["，也即判断是多个参数还是一个参数问题的分析
                if (originalArgs.Contains(GENERIC_ARGUMENTS_SEPARATOR))
                {
                    arguments = Parse(originalArgs);//多个参数分析
                }
                else
                {
                    //只包含一个参数情况的分析
                    string argument = originalArgs.Substring(1, originalArgs.Length - 2).Trim();
                    arguments.Add(argument);
                }
                _unresolvedGenericArguments = new string[arguments.Count];
                 arguments.CopyTo(_unresolvedGenericArguments, 0);//将参数保存到成员当中，等待分析
           }
            /// <summary>
            /// 对包含多个参数的字符串分析
            /// </summary>
            /// <param name="args"></param>
            /// <returns></returns>
            private static IList<string> Parse(string args)
            {
                StringBuilder argument = new StringBuilder();
                IList<string> arguments = new List<string>();

                TextReader input = new StringReader(args);
                int nbrOfRightDelimiter = 0;
                bool findRight = false;
                //采用栈的形似寻找在[]中的内容
                do
                {
                    char ch = (char)input.Read();
                    if (ch == '[')
                    {
                        nbrOfRightDelimiter++;
                        findRight = true;
                    }
                    else if (ch == ']')
                    {
                        nbrOfRightDelimiter--;
                    }
                    argument.Append(ch);
                    
                    //Find one argument
                    if (findRight && nbrOfRightDelimiter == 0)
                    {
                        string arg = argument.ToString();
                        arg = arg.Substring(1, arg.Length - 2);//获取在'[]'之中的字符串
                        arguments.Add(arg);
                        input.Read();
                        argument = new StringBuilder();
                    }
                }
                while (input.Peek() != -1);

                return arguments;
            }
            #endregion
        }

        #endregion


        #region Inner Class : TypeAssemblyInfo

        /// <summary>
        /// Holds data about a <see cref="System.Type"/> and it's
        /// attendant <see cref="System.Reflection.Assembly"/>.
        /// </summary>
        internal class TypeAssemblyInfo
        {
            #region Constants

            /// <summary>
            /// The string that separates a <see cref="System.Type"/> name
            /// from the name of it's attendant <see cref="System.Reflection.Assembly"/>
            /// in an assembly qualified type name.
            /// </summary>
            public const string TYPE_ASSEMBLY_SEPARATOR = ",";
            public const string NULLABLE_TYPE = "System.Nullable";
            public const string NULLABLE_TYPE_ASSEMBLY_SEPARATOR = "]],";
            #endregion

            #region Fields

            private string _unresolvedAssemblyName = string.Empty;
            private string _unresolvedTypeName = string.Empty;

            #endregion

            #region Constructor (s) / Destructor

            /// <summary>
            /// Creates a new instance of the TypeAssemblyInfo class.
            /// </summary>
            /// <param name="unresolvedTypeName">
            /// The unresolved name of a <see cref="System.Type"/>.调用默认程序分析处理unresolvedTypeName类型
            /// </param>
            public TypeAssemblyInfo(string unresolvedTypeName)
            {
                SplitTypeAndAssemblyNames(unresolvedTypeName);
            }

            #endregion

            #region Properties

            /// <summary>
            /// The (unresolved) type name portion of the original type name.
            /// </summary>
            public string TypeName
            {
                get { return _unresolvedTypeName; }
            }

            /// <summary>
            /// The (unresolved, possibly partial) name of the attandant assembly.
            /// </summary>
            public string AssemblyName
            {
                get { return _unresolvedAssemblyName; }
            }

            /// <summary>
            /// Is the type name being resolved assembly qualified?
            /// </summary>
            public bool IsAssemblyQualified
            {
                get { return HasText(AssemblyName); }
            }

            #endregion

            #region Methods

            private static bool HasText(string target)
            {
                if (target == null)
                {
                    return false;
                }
                return HasLength(target.Trim());
            }

            private static bool HasLength(string target)
            {
                return (!string.IsNullOrEmpty(target));
            }

            private void SplitTypeAndAssemblyNames(string originalTypeName)
            {
                //是否以"System.Nullable"开头
                if (originalTypeName.StartsWith(NULLABLE_TYPE))
                {
                    //字符串"]],"的位置 也即参数的结束标志位置
                    int typeAssemblyIndex = originalTypeName.IndexOf(NULLABLE_TYPE_ASSEMBLY_SEPARATOR);
                    if (typeAssemblyIndex < 0)//没有标志 则说明无参数
                    {
                        _unresolvedTypeName = originalTypeName;
                    }
                    else
                    {
                        //获取类型名类型
                        _unresolvedTypeName = originalTypeName.Substring(0, typeAssemblyIndex + 2).Trim();
                        //获取程序集的名称
                        _unresolvedAssemblyName = originalTypeName.Substring(typeAssemblyIndex + 3).Trim();
                    }
                }
                else
                {
                    int typeAssemblyIndex = originalTypeName.IndexOf(TYPE_ASSEMBLY_SEPARATOR);
                    if (typeAssemblyIndex < 0)
                    {
                        _unresolvedTypeName = originalTypeName;
                    }
                    else
                    {
                        _unresolvedTypeName = originalTypeName.Substring(0, typeAssemblyIndex).Trim();
                        _unresolvedAssemblyName = originalTypeName.Substring(typeAssemblyIndex + 1).Trim();
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}
