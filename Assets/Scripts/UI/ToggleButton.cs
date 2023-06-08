using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject on;
    [SerializeField] private GameObject off;
    private Toggle _toggle;
    void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate
        {
            OnValueChange(_toggle.isOn);
        });
    }

    void OnValueChange(bool isOn)
    {
        on.SetActive(isOn);
        off.SetActive(!isOn);
    }
}