using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public TextMeshProUGUI objectiveText;

    private string currentObjective;
    private bool isAnimating = false;

    void Awake()
    {
        Instance = this;
    }

    public void SetObjectiveFromCheckpoint(int checkpointIndex)
    {
        switch (checkpointIndex)
        {
            case 0:
                SetObjective("Find a way to power on the ventilation system.");
                break;

            case 1:
                SetObjective("Find a way to unlock the shutter.");
                break;

            case 2:
                SetObjective("Find a way to turn off the security camera.");
                break;
        }
    }

    public void SetObjective(string text)
    {
        if (isAnimating)
        {
            StopAllCoroutines();
        }

        currentObjective = text;
        objectiveText.text = text;
        objectiveText.alpha = 1f;
        objectiveText.rectTransform.anchoredPosition = new Vector2(50, -270);
        isAnimating = false;
    }

    public void CompleteObjective(string objectiveToCheck, string nextObjective)
    {
        if (currentObjective != objectiveToCheck)
        {
            Debug.Log("Wrong objective attempted: " + objectiveToCheck);
            return;
        }
        StopAllCoroutines();
        StartCoroutine(CompleteRoutine(nextObjective));
    }

    private IEnumerator CompleteRoutine(string nextObjective)
    {
        isAnimating = true;
        objectiveText.text = "<s>" + currentObjective + "</s>";  // strikethrough to indicate completion

        yield return new WaitForSeconds(1f);

        // slide down and fade out
        Vector3 startPos = objectiveText.rectTransform.anchoredPosition;
        Vector3 endPos = startPos + Vector3.down * 50f;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2f;
            objectiveText.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            objectiveText.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
        objectiveText.text = "";
        objectiveText.alpha = 1f;
        objectiveText.rectTransform.anchoredPosition = new Vector2(50, -270);

        if (!string.IsNullOrEmpty(nextObjective))
        {
            currentObjective = nextObjective;
            objectiveText.text = nextObjective;
        }

        isAnimating = false;
    }
}