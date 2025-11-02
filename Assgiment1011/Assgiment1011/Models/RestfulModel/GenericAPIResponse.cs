using System.Net;
using System.Runtime.Serialization;

namespace Assgiment1011.Models.RestfulModel
{
    [DataContract]
    public class GenericAPIResponse<T>
    {
        [DataMember]
        public HttpStatusCode StatusCode { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; } = true;

        [DataMember]
        public List<string> ErrorMessages { get; set; }

        [DataMember]
        public T Result { get; set; }
    }
}
