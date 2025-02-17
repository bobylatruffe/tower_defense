using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   public Transform target;
   private NavMeshAgent agent;

   private void Start()
   {
      agent = GetComponent<NavMeshAgent>();

      StartCoroutine(FollowEnemy());
   }

   private IEnumerator FollowEnemy()
   {
      while (enabled)
      {
         agent.SetDestination(target.position);

         yield return new WaitForSeconds(1f);
      }
   }
}
