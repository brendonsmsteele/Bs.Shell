namespace Bs.Shell
{
    public interface IUI<TData>
    {
        void Bind(TData data);
    }
}