using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SceneRestarter : MonoBehaviour
{
    [SerializeField] private Health _player;

    private void OnEnable()
    {
        if (_player != null)
        {
            _player.Died += RestartLevel;
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.Died -= RestartLevel;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
