using UnityEngine;
using UnityEngine.UI;
public class portraitController : MonoBehaviour
{
    public Text text;
    public Sprite[] sprites;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(text.text)
        {
            case "Mobster 1":
            {
                image.sprite = sprites[1];
                break;
            }

            case "Phone":
            {
                image.sprite = sprites[2];
                break;
            }


            case "Maurice":
            {
                image.sprite = sprites[3];
                break;
            }


            case "Vick":
            {
                image.sprite = sprites[0];
                break;
            }

        }
    }
}
