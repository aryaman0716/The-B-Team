using UnityEngine;
using UnityEngine.SceneManagement;

public class CashStack : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        StartEndingCutscene();
    }
    
    void StartEndingCutscene()
    {
        SceneManager.LoadScene("Cutscene End");
    }
}
