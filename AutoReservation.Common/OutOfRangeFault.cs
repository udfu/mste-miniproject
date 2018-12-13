using System.Runtime.Serialization;

namespace AutoReservation.Common
{
  
    [DataContract]
    public class OutOfRangeFault
    {
        [DataMember]
        public string Operation { get; set; }
    }
}
