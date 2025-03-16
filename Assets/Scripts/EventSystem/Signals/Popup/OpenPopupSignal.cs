using MVP.Models;

namespace EventSystem.Signals
{
    public class OpenPopupSignal : ISignal
    {
        public readonly DogBreedModel Model;

        public OpenPopupSignal(DogBreedModel model)
        {
            Model = model;
        }
    }
}