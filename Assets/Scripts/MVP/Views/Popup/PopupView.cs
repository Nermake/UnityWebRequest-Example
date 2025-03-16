using EventSystem;
using EventSystem.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace MVP.Views
{
    [RequireComponent(typeof(Animator))]
    public class PopupView : BaseView
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Animator _animator;

        [Inject] private EventBus _eventBus;
        
        private readonly string _isActive = "IsActive";

        public void SetTitle(string title) => _title.text = title;
        public void SetDescription(string description) => _description.text = description;

        public override void Show()
        {
            base.Show();
            
            _animator.SetBool(_isActive, true);
        }

        public override void Hide() => _animator.SetBool(_isActive, false);
        public void OnHide() => gameObject.SetActive(false);

        public void OnClick()
        {
            _eventBus.Invoke(new ClosePopupSignal());
            _eventBus.Invoke(new UnlockRaycastSignal());
        }
    }
}