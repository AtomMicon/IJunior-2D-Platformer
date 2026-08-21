using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public static ScoreUpdater Instance { get; private set; }

    [SerializeField] private TMP_Text _scoreText;

    private int _score;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = _score.ToString();
        }
    }
}