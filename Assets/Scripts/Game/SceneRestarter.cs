using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SceneRestarter : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;

    private void OnEnable()
    {
        if (_player != null)
        {
            _player.OnDied += RestartLevel;
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.OnDied -= RestartLevel;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
