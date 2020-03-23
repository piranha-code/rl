using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class EfSession : ISession
    {
        readonly NorthwindContext _dbContext;

        public EfSession(NorthwindContext dbContext)       
        {          
            _dbContext = dbContext;  
        }

        public void BeginTransaction()
        {            
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void EndTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}