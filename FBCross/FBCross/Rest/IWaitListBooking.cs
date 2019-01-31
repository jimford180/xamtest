using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IWaitListBooking
    {
        Task<IRestResponse<WaitListDeletionResponse>> Delete(int id, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<WaitListCreationResponse>> Post(WaitListRequest bookingRequest, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<WaitListUpdateResponse>> Put(int waitListId, WaitListRequest update, string sessionToken, Guid merchantGuid);
    }
}