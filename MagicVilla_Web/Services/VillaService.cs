using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IService;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        string villaUrl;
        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _clientFactory = httpClient;
            villaUrl = configuration.GetValue<string>("SeviceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO villaCreate)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = villaCreate,
                Url = villaUrl+ "/api/VillaAPI/"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            { 
                ApiType= SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/"
            });

        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO villaUpdate)
        {
            return SendAsync <T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = villaUpdate,
                Url = villaUrl + "/api/VillaAPI/" + villaUpdate.Id
            });
        }
    }
}
