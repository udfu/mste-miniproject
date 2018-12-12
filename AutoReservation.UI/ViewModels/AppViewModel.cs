using System.ServiceModel;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.UI.ViewModels
{
    public static class AppViewModel
    {
        private static IAutoReservationService _target;
        public static IAutoReservationService Target
        {
            get
            {
                if (_target == null)
                {
                    ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    _target = channelFactory.CreateChannel();
                }
                return _target;
            }
        }
    }
}