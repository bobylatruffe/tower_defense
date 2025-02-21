using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PossibleUpgrade", menuName = "Scriptable Objects/PossibleUpgrade")]
public class PossibleUpgrade : ScriptableObject
{
    public List<GameObject> towers;
}