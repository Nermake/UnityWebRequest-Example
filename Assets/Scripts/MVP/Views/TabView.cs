using UnityEngine;

namespace MVP.Views
{
    public abstract class TabView : BaseView
    {
        [SerializeField] private GameObject _lockRaycast;
        [SerializeField] private GameObject _loadingWidget;

        public void LockRaycast() => _lockRaycast.SetActive(true);
        public void UnlockRaycast() => _lockRaycast.SetActive(false);
        
        public void StartLoading() => _loadingWidget.SetActive(true);
        public void FinishLoading() => _loadingWidget.SetActive(false);
    }
}