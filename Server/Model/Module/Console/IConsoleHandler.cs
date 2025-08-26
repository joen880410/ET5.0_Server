using System;
using System.Collections;
using System.Net;
using System.Threading;

namespace ETModel
{
    public class ConsoleResult
    {
        public int ErrorCode = ETModel.ErrorCode.ERR_Success;
        public string Message = "";
        public string info = "";

        public static ConsoleResult Ok(string msg = "ok", string info = "")
        {
            return new ConsoleResult
            {
                ErrorCode = ETModel.ErrorCode.ERR_Success,
                Message = msg,
                info = info,
            };
        }

        public static ConsoleResult Error(int errorCode = ETModel.ErrorCode.ERR_ConsoleError, string msg = "", string info = "")
        {
            return new ConsoleResult
            {
                ErrorCode = errorCode,
                Message = msg,
                info = info,
            };
        }

        public static ConsoleResult NotSupported(int errorCode = ETModel.ErrorCode.ERR_ConsoleNotSupported, string msg = "", string info = "")
        {
            return new ConsoleResult
            {
                ErrorCode = errorCode,
                Message = msg,
                info = info,
            };
        }
    }

    public interface IConsoleHandler
    {
        ETTask<ConsoleResult> Handle();
        ETTask<ConsoleResult> Handle(object a);
        ETTask<ConsoleResult> Handle(object a, object b);
        ETTask<ConsoleResult> Handle(object a, object b, object c);
        ETTask<ConsoleResult> Handle(object a, object b, object c, object d);
        Type[] parameterTypes { set; get; }
        ConsoleService ConsoleService { set; get; }
        AppType AppType { set; get; }
        string CommandPatternString { set; get; }
        CancellationTokenSource CancellationTokenSource { set; get; }
    }
    public interface IConsoleHandler<A> : IConsoleHandler
    {
        ETTask<ConsoleResult> Execute(A a);
    }

    public interface IConsoleHandler<A, B> : IConsoleHandler
    {
        ETTask<ConsoleResult> Execute(A a, B b);
    }

    public interface IConsoleHandler<A, B, C> : IConsoleHandler
    {
        ETTask<ConsoleResult> Execute(A a, B b, C c);
    }

    public interface IConsoleHandler<A, B, C, D> : IConsoleHandler
    {
        ETTask<ConsoleResult> Execute(A a, B b, C c, D d);
    }

    public abstract class AConsoleHandler : AEvent, IConsoleHandler
    {
        public sealed override void Run()
        {
            Execute();
        }

        public abstract ETTask<ConsoleResult> Execute();

        ETTask<ConsoleResult> IConsoleHandler.Handle()
        {
            return Execute();
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c, object d)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        public ConsoleService ConsoleService { get; set; }

        public CancellationTokenSource CancellationTokenSource { set; get; }
        public AppType AppType { get; set; }
        public string CommandPatternString { get; set; }

        public Type[] parameterTypes { set; get; }
    }

    public abstract class AConsoleHandler<A> : AEvent<A>, IConsoleHandler
    {
        public sealed override void Run(A a)
        {
            Execute(a);
        }

        public abstract ETTask<ConsoleResult> Execute(A a);

        ETTask<ConsoleResult> IConsoleHandler.Handle()
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));

        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a)
        {
            return Execute((A)a);
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c, object d)
        {
            throw new NotImplementedException();
        }

        public ConsoleService ConsoleService { get; set; }

        public CancellationTokenSource CancellationTokenSource { set; get; }
        public AppType AppType { set; get; }
        public string CommandPatternString { set; get; }

        public Type[] parameterTypes { set; get; }
    }

    public abstract class AConsoleHandler<A, B> : AEvent<A, B>, IConsoleHandler
    {
        public sealed override void Run(A a, B b)
        {
            Execute(a, b);
        }

        public abstract ETTask<ConsoleResult> Execute(A a, B b);

        ETTask<ConsoleResult> IConsoleHandler.Handle()
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b)
        {
            return Execute((A)a, (B)b);
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c, object d)
        {
            throw new NotImplementedException();
        }

        public ConsoleService ConsoleService { get; set; }

        public CancellationTokenSource CancellationTokenSource { set; get; }
        public AppType AppType { set; get; }
        public string CommandPatternString { set; get; }
        public Type[] parameterTypes { set; get; }
    }

    public abstract class AConsoleHandler<A, B, C> : AEvent<A, B, C>, IConsoleHandler
    {
        public sealed override void Run(A a, B b, C c)
        {
            Execute(a, b, c);
        }

        public abstract ETTask<ConsoleResult> Execute(A a, B b, C c);

        ETTask<ConsoleResult> IConsoleHandler.Handle()
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c)
        {
            return Execute((A)a, (B)b, (C)c);
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c, object d)
        {
            throw new NotImplementedException();
        }

        public ConsoleService ConsoleService { get; set; }

        public CancellationTokenSource CancellationTokenSource { set; get; }
        public AppType AppType { set; get; }
        public string CommandPatternString { set; get; }

        public Type[] parameterTypes { set; get; }
    }
    public abstract class AConsoleHandler<A, B, C, D> : AEvent<A, B, C, D>, IConsoleHandler
    {
        public sealed override void Run(A a, B b, C c, D d)
        {
            Execute(a, b, c, d);
        }

        public abstract ETTask<ConsoleResult> Execute(A a, B b, C c, D d);

        ETTask<ConsoleResult> IConsoleHandler.Handle()
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }

        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c)
        {
            return ETTask.FromResult(ConsoleResult.Error(errorCode: ErrorCode.ERR_ConsoleNotImplement));
        }
        ETTask<ConsoleResult> IConsoleHandler.Handle(object a, object b, object c, object d)
        {
            return Execute((A)a, (B)b, (C)c, (D)d);
        }
        public ConsoleService ConsoleService { get; set; }

        public CancellationTokenSource CancellationTokenSource { set; get; }
        public AppType AppType { set; get; }
        public string CommandPatternString { set; get; }

        public Type[] parameterTypes { set; get; }
    }
}