using Game.Type.JSON;

namespace EventSystem.Signals
{
    public class UpdateDogBreedDataSignal : ISignal
    {
        public readonly BreedData[] BreedDats;

        public UpdateDogBreedDataSignal(BreedData[] breedDats)
        {
            BreedDats = breedDats;
        }
    }
}