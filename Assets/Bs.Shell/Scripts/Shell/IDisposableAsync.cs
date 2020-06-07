using Nc.Shell.Async;

namespace Nc.Shell
{
    public interface IDisposableAsync
    {
        ManualYieldInstruction Dispose();
    }
}