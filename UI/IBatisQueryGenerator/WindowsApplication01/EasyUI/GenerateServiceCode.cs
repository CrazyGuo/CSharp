using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBatisQueryGenerator.EasyUI
{
    public class GenerateServiceCode
    {
        public string Name;
        public string prefix = "    ";

        public string IServiceCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using Study.ApplicationServices;");
            builder.Append("\r\n");
            builder.Append("using Study.Entity;");
            builder.Append("\r\n");
            builder.Append("\r\n");
            builder.Append("namespace Study.BusinessService");
            builder.Append("\r\n");
            builder.Append("{");
            builder.Append("\r\n");
            string className = "public interface " + "I" + Name + "Service :" + "IServiceStudyBase <" + Name + "Dto" + ", " + Name + "Query>";
            builder.Append("    "+className);
            builder.Append("\r\n");
            builder.Append(prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + " //Here add your service code");
            builder.Append("\r\n");
            builder.Append(prefix  + "}");
            builder.Append("\r\n");
            builder.Append("}");
            return builder.ToString();
        }

        public string ServiceCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;");
            builder.Append("\r\n");
            builder.Append("using Study.Entity;");
            builder.Append("\r\n");
            builder.Append("using Study.ApplicationServices;");
            builder.Append("\r\n");
            builder.Append("\r\n");
            builder.Append("namespace Study.BusinessService");
            builder.Append("\r\n");
            builder.Append("{");
            builder.Append("\r\n");
            string className = "public class " + Name + "Service :" + "ServiceStudyBase <" + Name + ", "+Name+"Dto"+ "," + Name + "Query>, "+ "I"+Name+"Service";
            builder.Append("    " + className);
            builder.Append("\r\n");
            builder.Append(prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix +"public "+Name+"Service()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "protected override " + Name + "Dto" + " ToDto(" + Name + " entity)");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return null;");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "protected override " + Name + " ToEntity(" + Name + "Dto" + " dto)");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return null;");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "public override " + Name + "Dto" + " Create()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + prefix + Name + "Dto" + " dto = new " + Name + "Dto();");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return dto;");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "public override string " + " GetFetchQueryId()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return "+"\"q"+Name+"\";");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "public override string " + " GetFetchId()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return "+"\"q"+Name+"Id\";");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "public override string " + " GetDeleteId()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return " +"\"d"+Name+"Id\";");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "public override string " + " GetAddId()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return " + "\"i" + Name + "Id\";");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + prefix + "public override string " + " GetUpdateId()");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "    return " + "\"u" + Name + "Id\";");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");

            builder.Append(prefix + "}");
            builder.Append("\r\n");
            builder.Append("}");
            return builder.ToString();
        }

        public string ControllerCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("using Study.Webs.EayUI.Base;");
            builder.Append("\r\n");
            builder.Append("using Study.Entity;");
            builder.Append("\r\n");
            builder.Append("using Study.BusinessService;");
            builder.Append("\r\n");            
            builder.Append("\r\n");
            builder.Append("namespace Study.Webs.EayUI.Areas..Controllers");
            builder.Append("\r\n");
            builder.Append("{");
            builder.Append("\r\n");
            string className = "public class " + Name + "Controller :" + "CrudControllerBase <" + Name +"Dto"+ ", " + Name + "Query>";
            builder.Append("    " + className);
            builder.Append("\r\n");
            builder.Append(prefix + "{");
            builder.Append("\r\n");
            builder.Append("    "+"I"+Name+"Service "+Name+"Service { get; set;}");
            builder.Append("\r\n");
            builder.Append("    public " + Name + "Controller(" + "I" + Name + "Service service)");
            builder.Append("\r\n");
            builder.Append("        :base(service)");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "{");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "        this." + Name + "Service = service;");
            builder.Append("\r\n");
            builder.Append(prefix + prefix + "}");
            builder.Append("\r\n");
            builder.Append(prefix + "}");
            builder.Append("\r\n");
            builder.Append("}");
            return builder.ToString();
        }

        public string QueryCode()
        {
            return string.Empty;
        }

        public string DtoCode()
        {
            return string.Empty;
        }
    }
}
