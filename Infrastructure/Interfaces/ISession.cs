namespace Domain.Interfaces
{
    public interface ISession
    {
        void BeginTransaction();
        void EndTransaction();
        void Commit();
        void Rollback();
    }
}