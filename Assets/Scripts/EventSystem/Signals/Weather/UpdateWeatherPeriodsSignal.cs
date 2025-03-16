using Game.Type.JSON;

namespace EventSystem.Signals
{
    public class UpdateWeatherPeriodsSignal : ISignal
    {
        public readonly Period[] Periods;

        public UpdateWeatherPeriodsSignal(Period[] periods)
        {
            Periods = periods;
        }
    }
}