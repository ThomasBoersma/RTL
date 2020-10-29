namespace Domain.Repositories
{
    public abstract class Repository
    {
        protected readonly SQLServerDBContext context;

        public Repository(SQLServerDBContext context)
        {
            this.context = context;
        }
    }
}
