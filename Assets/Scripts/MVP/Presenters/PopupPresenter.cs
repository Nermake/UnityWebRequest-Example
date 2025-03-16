using System;
using EventSystem;
using EventSystem.Signals;
using MVP.Views;
using Zenject;

namespace MVP.Presenters
{
    public class PopupPresenter : IInitializable, IDisposable
    {
        private readonly PopupView _view;
        private readonly EventBus _eventBus;

        public PopupPresenter(EventBus eventBus, PopupView view)
        {
            _eventBus = eventBus;
            _view = view;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<OpenPopupSignal>(OnOpenPopupSignal);
            _eventBus.Subscribe<ClosePopupSignal>(OnClosePopupSignal);
        }

        private void OnClosePopupSignal(ClosePopupSignal obj)
        {
            _view.Hide();
        }

        private void OnOpenPopupSignal(OpenPopupSignal signal)
        {
            _view.SetTitle(signal.Model.Name);
            _view.SetDescription(signal.Model.Description);
            
            _view.Show();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OpenPopupSignal>(OnOpenPopupSignal);
            _eventBus.Unsubscribe<ClosePopupSignal>(OnClosePopupSignal);
        }
    }
}