using System;
using System.Collections.Generic;
using MVP.Models;

namespace Logic.Core
{
    public abstract class ModelService<TModel> where TModel : IModel
    {
        public event Action<TModel> ModelAdded;
        public event Action<TModel> ModelRemoved;
        
        public IReadOnlyList<TModel> Models => _weatherModels;

        private readonly List<TModel> _weatherModels = new();

        public void AddModel(TModel model)
        {
            if (!_weatherModels.Contains(model))
            {
                _weatherModels.Add(model);
                ModelAdded?.Invoke(model);
            }
        }

        public void RemoveModelAt(int index)
        {
            if (index < 0 || index >= _weatherModels.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            
            RemoveModel(_weatherModels[index]);
        }

        public void RemoveModel(TModel model)
        {
            if (_weatherModels.Remove(model))
            {
                ModelRemoved?.Invoke(model);
            }
        }
    }
}