using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IHoliday
    {
        Task<IRestResponse<Rest.Dto.Holiday>> Get(int id, Guid merchantGuid, string sessionToken);
        Task<IRestResponse<CreateMerchantHolidayResponse>> Post(Rest.Dto.Holiday holiday, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<UpdateMerchantHolidayResponse>> Put(Rest.Dto.Holiday holiday, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<DeleteMerchantHolidayResponse>> Delete(int id, Guid merchantGuid, string sessionToken);
    }
}