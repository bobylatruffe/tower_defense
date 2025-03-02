using UnityEngine;

public class SimpleFlyingStrategy : MonoBehaviour, I_MoveStrategy
{
    private GameObject destination;

    private void Start()
    {
        transform.Translate(Vector3.up * Random.Range(5.0f, 12.0f));
    }

    public void move()
    {
        float speed = Random.Range(6f, 9f);
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

    }
}
