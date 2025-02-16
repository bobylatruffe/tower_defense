using System;
using UnityEngine;

public abstract class A_Enemie : MonoBehaviour
{
    protected int CurrentHealth { get; set; }
    protected int Speed { get; set; }

    public bool IsMoving { get; set; }
    public I_MoveStrategy MoveStrategy { get; set; }
}