using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository: IRegionRepository
    {
        private readonly NZWalksDbContext dbcontext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbcontext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbcontext.Regions.ToListAsync();
        }
    }
}
