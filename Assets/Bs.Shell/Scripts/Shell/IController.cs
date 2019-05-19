namespace Bs.Shell
{
    public interface IController<TData>
    {
        void Bind(TData data);
    }
}