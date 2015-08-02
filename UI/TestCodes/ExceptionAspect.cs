using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCodes
{
    [OnMethodBoundaryAspectConfiguration(SerializerType = typeof(MsilAspectSerializer))]
    public class ExceptionAspect : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            if (Exceptions.Handle(args.Exception))
                args.FlowBehavior = FlowBehavior.Continue;
            args.FlowBehavior = FlowBehavior.ThrowException;
            LoggingHelper.Writelog("exception:"+args.Exception);
        }
    }
}
