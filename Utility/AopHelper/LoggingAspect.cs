using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using System;

namespace AopHelper
{
    [OnMethodBoundaryAspectConfiguration(SerializerType = typeof(MsilAspectSerializer))]
    public class LoggingAspect : OnMethodBoundaryAspect
    {

        public string BusinessName { get; set; }

        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            string defaultLog = string.Format("{0} : Entry at {1}", eventArgs.Method.Name, DateTime.Now);
            LoggingHelper.Writelog(defaultLog);
            foreach (var argument in eventArgs.Arguments)
            {
                if (argument.GetType() == typeof(int))
                {

                }
            }
        }

        public override void OnExit(MethodExecutionArgs eventArgs)
        {
            string defaultLog = string.Format("{0} : Exit at {1}", eventArgs.Method.Name, DateTime.Now);
            LoggingHelper.Writelog(defaultLog);
        }

        public override void OnSuccess(MethodExecutionArgs eventArgs)
        {
            string defaultLog = string.Format("{0} : Success at {1}", eventArgs.Method.Name, DateTime.Now);
            LoggingHelper.Writelog(defaultLog);
        }
    }
}
