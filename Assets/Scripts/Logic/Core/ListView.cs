using System.Collections.Generic;
using UnityEngine;

namespace Logic.Core
{
    public abstract class ListView<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _itemPrefab;
        [SerializeField] private Transform _container;
        
        private readonly List<T> _items = new();
        private readonly Queue<T> _freeList = new();

        public T SpawnElement()
        {
            if (_freeList.TryDequeue(out var item))
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Instantiate(_itemPrefab, _container);
            }
            
            _items.Add(item);
            return item;
        }

        public void DespawnElement(T item)
        {
            if (item != null && _items.Remove(item))
            {
                item.gameObject.SetActive(false);
                _freeList.Enqueue(item);
            }
        }
        
        public void Clear()
        {
            foreach (var item in _items)
            {
                item.gameObject.SetActive(false);
                _freeList.Enqueue(item);
            }

            _items.Clear();
        }
    }
}