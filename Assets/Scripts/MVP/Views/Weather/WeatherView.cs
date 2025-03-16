using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
    public class WeatherView : BaseView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _temperature;

        public void SetSprite(Sprite sprite) => _image.sprite = sprite;
        public void SetWeekday(string weekday) => _name.text = weekday;
        public void SetTemperature(string temperature) => _temperature.text = temperature;
    }
}