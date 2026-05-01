using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class timer : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
      text.text = Time.time.ToString();
    }
}
