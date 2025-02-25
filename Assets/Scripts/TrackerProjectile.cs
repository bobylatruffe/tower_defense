using UnityEngine;

public class TrackerProjectile : MonoBehaviour
{
    private A_Enemie target;
    private float speed;
    private float rotationSpeed = 90f;

    public void SetTarget(A_Enemie newTarget)
    {
        target = newTarget;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Start()
    {
        speed = GetComponent<Projectile>().projectileData.projectileSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = target.transform.position;
        targetPosition.y = transform.position.y - 0.25f;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
