using UnityEngine;
public class OliveOil : MonoBehaviour
{
    public float useDistance = 3f;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UseOil();
        }
    }
    void UseOil()
    {
        Camera cam = Camera.main;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, useDistance))
        {
            Room1ExitDoor door = hit.collider.GetComponent<Room1ExitDoor>();
            if (door != null)
            {
                door.ApplyOil();
                Destroy(gameObject);
            }
        }
    }
}
