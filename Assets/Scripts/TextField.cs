using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextField : MonoBehaviour
{
    TextMeshProUGUI m_Object;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        m_Object = GetComponent<TextMeshProUGUI>();
        this.text = "Unity";
    }

    // Update is called once per frame
    void Update()
    {
        m_Object.text = this.text;
    }

    public void SetText(string text)
    {
        Debug.Log("Setting text: " + text);
        this.text = text;
        Debug.Log("After setting text: " + text);
    }
}
