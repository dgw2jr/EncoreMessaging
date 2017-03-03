namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}