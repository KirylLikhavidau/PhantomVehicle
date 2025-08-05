using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private FinishZone _trigger;
    [SerializeField] private TextMeshProUGUI _raceNumber;
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _trigger.CarFinished += (obj) => 
        {
            if(Int32.TryParse(_raceNumber.text, out int result))
            {
                result++;
                _raceNumber.text = Convert.ToString(result); 
            }

            _startButton.gameObject.SetActive(true);
        };
    }

    private void OnDisable()
    {
        _trigger.CarFinished -= (obj) =>
        {
            if (Int32.TryParse(_raceNumber.text, out int result))
            {
                result++;
                _raceNumber.text = Convert.ToString(result);
            }

            _startButton.gameObject.SetActive(true);
        };
    }
}
