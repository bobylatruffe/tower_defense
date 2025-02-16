using UnityEngine;

public class SimpleFlyingStrategy : MonoBehaviour, I_MoveStrategy
{
    private GameObject destination;
    private bool strategyInited = true;

    private void Start()
    {
        transform.Translate(Vector3.up * Random.Range(3.0f, 10.0f));
    }

    public void move()
    {
        float speed = Random.Range(1f, 1.5f);
        // transform.position =
        //     Vector3.MoveTowards(transform.position, destination.transform.position + Vector3.up, speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void setDestination(GameObject destination)
    {
        this.destination = destination;
    }

    public void initStrategy()
    {
        strategyInited = true;
    }
}