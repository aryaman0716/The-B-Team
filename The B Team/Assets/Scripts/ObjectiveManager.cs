using UnityEngine;
using System.Collections;
using TMPro;
public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public TextMeshProUGUI objectiveText;

    private string currentObjective;
    private bool canComplete = false;
    void Awake()
    {
        Instance = this;
    }
    public void SetObjective(string text)
    {
        currentObjective = text;
        objectiveText.text = text;
        objectiveText.alpha = 1f;
        canComplete = true;
    }
    public void CompleteObjective(string objectiveToCheck)
    {
        if (!canComplete || currentObjective != objectiveToCheck)
        {
            Debug.Log("Cannot complete objective: " + objectiveToCheck);
            return;
        }
        StartCoroutine(CompleteRoutine());
    }
    private IEnumerator CompleteRoutine()
    {
        canComplete = false;
        objectiveText.text = "<s>" + currentObjective + "</s>";  // strikethrough the text to indicate completion
        yield return new WaitForSeconds(2f);

        // Make the text smoothly slide down and fade out
        Vector3 startPos = objectiveText.rectTransform.anchoredPosition;
        Vector3 endPos = startPos + Vector3.down * 50f;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 2f;
            objectiveText.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            objectiveText.alpha = Mathf.Lerp(1f, 0f, t);  // Fade out over the same time
            yield return null;
        }
        objectiveText.text = "";
    }
}
