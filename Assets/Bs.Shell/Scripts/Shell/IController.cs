namespace Bs.Shell
{
    public interface IController<TModel>
    {
        TModel model { get; set; }
    }
}