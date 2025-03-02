using UnityEngine;

public class OscillatingFlyingStrategy : MonoBehaviour, I_MoveStrategy
{
    private float oscillationFrequency = 2f;
    private float oscillationAmplitude = 5f;
    private float verticalOscillationFrequency = 1.5f;
    private float verticalOscillationAmplitude = 2.5f;
    private float speed;
    private float maxTiltAngle = 20f;
    private float maxPitchAngle = 10f;
    private float minAltitude = 7f;

    private float timeElapsed;
    private float oscillationPhase;
    private float verticalOscillationPhase;

    private void Start()
    {
        speed = Random.Range(6f, 9f);
        transform.Translate(Vector3.up * Random.Range(5.0f, 12.0f));
        oscillationPhase = Random.Range(0f, Mathf.PI * 2);
        verticalOscillationPhase = Random.Range(0f, Mathf.PI * 2);
    }

    public void move()
    {
        timeElapsed += Time.deltaTime;

        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;

        float oscillationOffset = Mathf.Sin(timeElapsed * oscillationFrequency + oscillationPhase) * oscillationAmplitude;
        Vector3 lateralMovement = transform.right * oscillationOffset * Time.deltaTime;

        float verticalOscillationOffset = Mathf.Sin(timeElapsed * verticalOscillationFrequency + verticalOscillationPhase) * verticalOscillationAmplitude;

        if (transform.position.y <= minAltitude && verticalOscillationOffset < 0)
        {
            verticalOscillationOffset = 0;
        }

        Vector3 verticalMovement = transform.up * verticalOscillationOffset * Time.deltaTime;

        transform.position += forwardMovement + lateralMovement + verticalMovement;

        float tiltAngle = -Mathf.Sin(timeElapsed * oscillationFrequency + oscillationPhase) * maxTiltAngle;
        float pitchAngle = -Mathf.Sin(timeElapsed * verticalOscillationFrequency + verticalOscillationPhase) * maxPitchAngle;

        if (transform.position.y <= minAltitude && pitchAngle > 0)
        {
            pitchAngle = 0;
        }

        transform.rotation = Quaternion.Euler(pitchAngle, transform.rotation.eulerAngles.y, tiltAngle);
    }

    public void setDestination(GameObject destination)
    {
    }

    public void initStrategy()
    {
        timeElapsed = 0f;
    }
}
