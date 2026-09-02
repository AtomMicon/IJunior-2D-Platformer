using Unity.VisualScripting;
using UnityEngine;

public class HealingFruit : MonoBehaviour
{
    [SerializeField] private int _healValue = 1;

    public int HealValue => _healValue;

}
