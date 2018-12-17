using System.Runtime.Serialization;

namespace AutoReservation.Common
{
  
    [DataContract]
    public class OutOfRangeFault
    {
        [DataMember]
        public string Operation { get; set; }
    }

    [DataContract]
    public class AutoUnavailableFault
    {
        [DataMember]
        public string Operation { get; set; }
    }

    [DataContract]
    public class InvalidDateRangeFault
    {
        [DataMember]
        public string Operation { get; set; }
    }
}
