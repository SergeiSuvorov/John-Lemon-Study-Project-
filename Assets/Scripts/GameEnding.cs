using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] Text _enemyCountText;
    private int _enemyCount;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    private void Start()
    {
        {
            if (_enemyContainer != null)
                CheckEnemyCount();
        }
    }
    private void CheckEnemyCount()
    {
        int enemyHealthCount = 0;
        if (_enemyContainer != null && _enemyCount != _enemyContainer.transform.childCount)
        {
            for (int i = 0; i < _enemyContainer.transform.childCount; i++)
                if (_enemyContainer.transform.GetChild(i).GetComponentInChildren<Health>() != null)
                {
                    enemyHealthCount++;
                }
            //enemyHealthCount = GetComponentInChildren<Health>();
            _enemyCountText.text = "Enemy: " + enemyHealthCount;
        }
            
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    void Update ()
    {
        if (m_IsPlayerAtExit || _enemyContainer.transform.childCount==0)
        {
            
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }

        else if (m_IsPlayerCaught)
        {
           
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

        CheckEnemyCount();
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}
