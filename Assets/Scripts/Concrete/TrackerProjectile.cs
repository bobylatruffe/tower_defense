using UnityEngine;

public class TrackerProjectile : MonoBehaviour
{
    private A_Enemy target;
    private float speed;
    private float rotationSpeed = 90f;

    public void SetTarget(A_Enemy newTarget)
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
        // targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
