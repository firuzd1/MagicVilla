using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
       // Task<List<VillaNumberDTO>> GetAllVillaNumbersAsync(CancellationToken token, Expression<Func<VillaNumber, bool>> filter = null);
       // Task<VillaNumberDTO> GetVillaNumberAsync(CancellationToken token, Expression<Func<VillaNumber, bool>> filter = null, bool tracked = true);
       // Task DeleteVillaNumberAsync(VillaNumber entity, CancellationToken token);
        Task<VillaNumber> UpdateVillaNumberAsync(VillaNumber entity, DateTime CreatedDate, CancellationToken token);
        //Task SaveAsync(CancellationToken token);
    }
}
