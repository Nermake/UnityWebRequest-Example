using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MVP.Views
{
    public class DogBreedView : BaseView
    {
        public event UnityAction Clicked
        {
            add => _collectButton.onClick.AddListener(value);
            remove => _collectButton.onClick.RemoveListener(value);
        }
        
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _collectButton;

        public void SetNumber(string number) => _number.text = number;
        public void SetName(string dogName) => _name.text = dogName;
        
    }
}