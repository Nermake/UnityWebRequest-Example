using System;
using Zenject;

namespace MVP.Models
{
    public interface IModel : IInitializable, IDisposable
    {
        event Action ModelChanged;
    }
}