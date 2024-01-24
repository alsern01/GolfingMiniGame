using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IPText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        _text.SetText($"{_text.text} {NetworkManager.Instance.ipAddress}");
    }
}
