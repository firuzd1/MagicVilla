using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public async Task<Villa> UpdateAsync(Villa entity, CancellationToken token)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync(token);
            return entity;
            
        }

    }
}
