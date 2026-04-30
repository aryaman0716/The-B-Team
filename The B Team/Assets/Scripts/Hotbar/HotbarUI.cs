using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
public class HotbarUI : MonoBehaviour
{
    public Sprite[] selectedTools;
    public Sprite[] notSelectedKeyboardUI;
    public Sprite[] selectedKeyboardUI;
    public EquipmentController equipment;
    public Image image;
    public Image[] numberKeys;
    public AudioSource audioSource;
    public AudioClip clip;


    void Start()
    {
        image.sprite = selectedTools[4];
        /*// Create 4 slots (2 on each panel)
        int slotCount = 4;
        slotBackgrounds = new Image[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            Transform parent = i < 2 ? leftPanel : rightPanel;
            GameObject slot = Instantiate(slotPrefab, parent);
            slotBackgrounds[i] = slot.GetComponent<Image>();
            Image icon = slot.transform.GetChild(0).GetComponent<Image>();
            icon.enabled = true;
            icon.sprite = equipment.GetToolIcon(i);
        
        }
        */
    }
    void Update()
    {
        int selected = equipment.GetCurrentIndex();
        if (selected == 4) selected = -1;
        selected += 1;
        if (image.sprite != selectedTools[selected])
        {
            image.sprite = selectedTools[selected];
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
            var i = 0;
            foreach(var item in numberKeys)
            {
                if (item.sprite == selectedTools[i]) { continue; }
                item.sprite = notSelectedKeyboardUI[i];
                numberKeys[i].transform.GetComponent<RectTransform>().localScale = new Vector3(0.52f, 0.5f, 0.5f);
                i++;
            }
            numberKeys[selected].sprite = selectedKeyboardUI[selected];
            numberKeys[selected].transform.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        
        
    }
}

