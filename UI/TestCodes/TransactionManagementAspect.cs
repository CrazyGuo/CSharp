using System;
using System.Transactions;
using PostSharp.Aspects;

namespace TestCodes
{
    [Serializable]
    public class TransactionManagementAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            LoggingHelper.Writelog("Starting transaction");
            using (var scope = new TransactionScope())
            {
                var retries = 3;
                var succeeded = false;
                while (!succeeded)
                {
                    try
                    {
                        args.Proceed();//包含了被调用方法传递过来的参数了
                        scope.Complete();
                        succeeded = true;
                    }
                    catch
                    {
                        if (retries >= 0)
                            retries--;
                        else
                            throw;
                    }
                }
            }
            LoggingHelper.Writelog("Transaction complete");
        }
    }
}
