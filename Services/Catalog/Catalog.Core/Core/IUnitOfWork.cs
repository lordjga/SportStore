namespace Catalog.Core.Core
{
    public interface IUnitOfWork: IDisposable
    {
        Task<bool> Commit();
    }
}
