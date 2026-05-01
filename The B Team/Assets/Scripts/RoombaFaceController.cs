using UnityEngine;

public class RoombaFaceController : MonoBehaviour
{
    public MeshRenderer roombaMeshRenderer;
    public Material[] roombaMaterials;
    public Light screenLight;
    public AudioClip alarmedSound;
    public bool soundPlayed;

    private RoombaBehaviour roombaBehaviour;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roombaBehaviour = GetComponent<RoombaBehaviour>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (roombaBehaviour.GetCurrentState())
        {
            case 0:
                SetMaterial(0);
                if (soundPlayed) { soundPlayed = false; }
                screenLight.color = Color.green;
                break;
            case 1:
                SetMaterial(1);
                if (!soundPlayed) { audioSource.PlayOneShot(alarmedSound, GlobalSettings.SFXVolume * 0.5f); soundPlayed = true; }
                screenLight.color = Color.orange;
                break;
            case 2:
                SetMaterial(2);
                screenLight.color = Color.red;
                break;
        }
    }

    void SetMaterial(int val)
    {
        var materials = roombaMeshRenderer.materials;

        materials[1] = roombaMaterials[val];

        roombaMeshRenderer.materials = materials;
    }
}
