using UnityEngine;

public class RotateTourelle : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public bool rotorOnZ = false;

    void Update()
    {
        if (rotorOnZ)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
