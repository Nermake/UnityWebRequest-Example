using System.Collections.Generic;
using UnityEngine;

namespace Logic.Core
{
    public abstract class ListView<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T itemPrefab;
        [SerializeField] private Transform container;
        
        private readonly List<T> items = new();
        private readonly Queue<T> freeList = new();

        public T SpawnElement()
        {
            if (freeList.TryDequeue(out var item))
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Instantiate(itemPrefab, container);
            }
            
            items.Add(item);
            return item;
        }

        public void DespawnElement(T item)
        {
            if (item != null && items.Remove(item))
            {
                item.gameObject.SetActive(false);
                freeList.Enqueue(item);
            }
        }
        
        public void Clear()
        {
            foreach (var item in items)
            {
                item.gameObject.SetActive(false);
                freeList.Enqueue(item);
            }

            items.Clear();
        }
    }
}