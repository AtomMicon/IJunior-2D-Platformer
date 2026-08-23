using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private UI _ui;

    private int _score;

    public void AddScore(int amount)
    {
        _score += amount;
        _ui.UpdateUI(_score);
    }
}