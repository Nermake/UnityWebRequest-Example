using System;

namespace MVP.Models
{
    public interface IModel
    {
        event Action ModelChanged;
    }
}