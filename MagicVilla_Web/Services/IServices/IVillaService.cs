using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Services.IService
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateDTO villaCreate);
        Task<T> UpdateAsync<T>(VillaUpdateDTO villaUpdate);
        Task<T> DeleteAsync<T>(int id);

    }
}
