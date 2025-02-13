using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public float minX = -5f; // Limite gauche
    public float maxX = 5f;  // Limite droite
    private float targetX;   // Position cible aléatoire
    private float direction = 1f; // Direction initiale

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        // Déplacement vers la cible aléatoire
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), speed * Time.deltaTime);

        // Si l'objet atteint sa cible, choisir une nouvelle cible
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        targetX = Random.Range(minX, maxX); // Nouvelle position cible aléatoire
        speed = Random.Range(1f, 5f); // Nouvelle vitesse aléatoire
    }
}