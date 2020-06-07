namespace Nc.Shell
{
    public interface IController<TModel>
    {
        TModel model { get; set; }
    }
}