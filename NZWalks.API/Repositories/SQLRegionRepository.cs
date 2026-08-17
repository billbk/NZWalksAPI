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

        public async Task<Region> CreateAsync(Region region)
        {
           await dbcontext.Regions.AddAsync(region);
            await dbcontext.SaveChangesAsync();
            return region;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbcontext.Regions.Remove(existingRegion);
            await dbcontext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbcontext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbcontext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
