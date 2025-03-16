using EventSystem;
using EventSystem.Signals;
using Game.Const;
using Game.Type.JSON;
using Logic.Core;
using UnityEngine;

namespace Logic.DogBreeds
{
    public class DogBreedUpdater : RequestUpdater<UpdateDogBreedSignal>
    {
        public DogBreedUpdater(RequestManager requestManager, EventBus eventBus) : base(requestManager, eventBus) { }

        public override void Initialize()
        {
            base.Initialize();

            _url = StringConst.DOG_BREED_URL;
        }

        protected override void Callback(string json)
        {
            var breed = JsonUtility.FromJson<BreedJSON>(json);
            
            _eventBus.Invoke(new UpdateDogBreedDataSignal(breed.data.ToArray()));
            _eventBus.Invoke(new FinishDataLoadingSignal());
        }
    }
}