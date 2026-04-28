using UnityEngine;
using UnityEngine.SceneManagement;

public class CashStack : MonoBehaviour
{
    public GameObject scenetransition;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartEndingCutscene();
        }
        
    }
    
    void StartEndingCutscene()
    {
        Instantiate(scenetransition).GetComponent<sceneTransition>().BeginTransition("Cutscene End");
    }
}
