using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    
    private int _score = 0;

    private void Start()
    {
        UpdateUI(_score);
    }

    private void OnEnable()
    {
        UpdateUI(_score);
    }

    private void OnDisable()
    {
        UpdateUI(_score);
    }

    public void UpdateUI(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = score.ToString();
        }
    }
}
