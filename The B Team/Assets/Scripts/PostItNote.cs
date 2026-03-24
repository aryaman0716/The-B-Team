using UnityEngine;
using TMPro;

public class PostItNote : MonoBehaviour
{
    private TMP_Text text_Component;
    public string text;

    void Start()
    {
        text_Component = GetComponentInChildren<TMP_Text>();
        text_Component.text = text;
    }

}
