using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using NotificationMicroservice.DataAccess.NotificationMicroserviceDbContext;

namespace NotificationMicroservice.DataAccess.Implementation
{
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        private AppDbContext dbContext;

        public ContentRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}