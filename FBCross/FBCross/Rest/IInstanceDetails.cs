using System;
using System.Threading.Tasks;
using FBCross.Rest.Dto;
using RestSharp;

namespace FBCross.Rest
{
    public interface IInstanceDetails
    {
        Task<IRestResponse<ClassInstanceDetail>> Get(Guid merchantGuid, string sessionToken, string id);
        Task<IRestResponse<ClassInstanceUpdateResponse>> Put(ClassInstanceUpdate update, string id, string sessionToken, Guid merchantGuid);
        Task<IRestResponse<ClassInstanceCancellationResponse>> Delete(string id, bool confirmCancel, bool refundCharges, string sessionToken, Guid merchantGuid);
    }
}