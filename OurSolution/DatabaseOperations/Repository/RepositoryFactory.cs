using Models;

namespace Repository
{
    public class RepositoryFactory
    {
        public Repository<Instance> repoInstans = new Repository<Instance>();
        public Repository<User> repoUser = new Repository<User>();
        public Repository<InstanceWithUserInfo> repoInstanceWithUserInfo  = new Repository<InstanceWithUserInfo>();
        public Repository<Options> repoOption  = new Repository<Options>();

        private static RepositoryFactory instance;

        private RepositoryFactory()
        {
            
        }

        public static RepositoryFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new RepositoryFactory();
                }
                return instance;
            }
        }

    }
}
