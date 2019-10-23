using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;
    [SerializeField] InputController inputController;

    [SerializeField] GameObject mainCharacter;
    [SerializeField] GameObject startCollider;
    [SerializeField] Movement mov;
    [SerializeField] GameObject deadPanel;
    [SerializeField] GameObject pausePanel;
    public CanvasGroup overlayCanvas;
    public int coin;
    public Text coinText;

    [SerializeField] Camera2DFollow camFol;
    [SerializeField] CameraShake camShake;
    public Vector3 playerSpawnPoint;
    public Button attackButton;
    [SerializeField] private RectTransform attackCoolTimeBar;

    [Space]

    [Header("Stamina")]

    public int staminaCount;
    [SerializeField] private Transform staminaBarParent;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private List<GameObject> staminaBars = new List<GameObject>();
    private bool barActivated;

    [Space]

    [Header("Dead")]

    public bool dead;
    [SerializeField] GameObject shockWaveDeath;
    public GameObject deathParticle;

    [Space]

    [Header("Score")]

    public int score;
    public int scoreAdd;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;
    public float scoreAddDelay;
    public Text deadPanelScore;
    public Text pausePanelScore;

    void Awake ()
    {
        GameMaster.gameMaster = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        PlayerPrefs.SetInt("HighScore", 0);
        StartCoroutine(ScoreCoroutine());
    }

    public void StaminaActiveTrue(int c)
    {
        if (!barActivated)
        {
            barActivated = true;
            staminaCount = c;
            staminaBarParent.gameObject.SetActive(true);
            for (int i = 0; i < c; i++)
            {
                GameObject b = Instantiate(staminaBar, staminaBarParent);
                b.GetComponent<RectTransform>().localPosition = new Vector3(i * 1336 / (float)c - 668, 0, 0);
                b.GetComponent<RectTransform>().localScale = new Vector3(1 / (float)c, 1, 1);
                staminaBars.Add(b);
            }
        }
        else if (staminaCount == 0)
        {
            Dead();
        }
    }

    public void WallJump()
    {
        staminaBars[--staminaCount].SetActive(false);

    }

    public void StaminaActiveFalse ()
    {if (barActivated)
        {
            barActivated = false;
            StartCoroutine(StaminaActiveFalseCoroutine());
        }
    }

    IEnumerator StaminaActiveFalseCoroutine ()
    {
        for (int i = staminaCount; i < staminaBars.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            staminaBars[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < staminaBars.Count; i++)
        {
            Destroy(staminaBars[i]);
        }
        staminaBars.Clear();
        staminaCount = 0;
        staminaBarParent.gameObject.SetActive(false);
    }

    public IEnumerator AttackCoolTimeBarCoroutine(float coolTime)
    {
        attackCoolTimeBar.gameObject.SetActive(true);
        for (float t = 0; t < coolTime; t += Time.deltaTime)
        {
            attackCoolTimeBar.localScale = new Vector3(Mathf.Lerp(0, 1, t / coolTime), 1, 1);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        attackCoolTimeBar.gameObject.SetActive(false);
        mov.attackable = true;
    }

    IEnumerator ScoreCoroutine()
    {
        yield return new WaitForSeconds(1f);
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        while (!dead)
        {
            yield return new WaitForSeconds(scoreAddDelay);
            score += scoreAdd;
            scoreText.text = score.ToString();
            if (score > highScore)
            {
                highScoreText.text = score.ToString();
            }
        }
    }

    public void AddCoin(int c)
    {
        coin += c;
        coinText.text = coin.ToString();
    }

    public void Dead()
    {
        dead = true;
        AudioManager.PlaySound("death");
        camFol.enabled = false;
        Instantiate(shockWaveDeath, transform.position, Quaternion.Euler(0, 0, 0));
        camShake.Shake(100);
        GameObject _deathParticle = Instantiate(deathParticle, mov.transform.position, Quaternion.identity) as GameObject;
        Destroy(_deathParticle, 3f);
        Destroy(mov.gameObject);
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(2f);
        deadPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        deadPanelScore.text = score.ToString();
        overlayCanvas.alpha = 0.2f;
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
    }

    public void Pause()
    {
        inputController.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        overlayCanvas.alpha = 0.2f;
        pausePanelScore.text = score.ToString();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        inputController.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        overlayCanvas.alpha = 1.0f;
    }
    public void TokenResume()
    {
        dead = false;
        camFol.enabled = true;
        Instantiate(startCollider, playerSpawnPoint, Quaternion.Euler(0, 0, 0));
        GameObject mainChar = Instantiate(mainCharacter, playerSpawnPoint, Quaternion.Euler(0, 0, 0));
        mov = mainChar.GetComponent<Movement>();
        mov.camFol = camFol;
        mov.camShake = camShake;
        camFol.target = mainChar.transform;
        inputController.mov = mov;
        mov.inputController = inputController;
        attackButton.onClick = mov.attackFunction;
        StartCoroutine(ScoreCoroutine());
        scoreText.gameObject.SetActive(true);
        deadPanel.SetActive(false);
        Time.timeScale = 1;
        overlayCanvas.alpha = 1.0f;
    }

    public void ReTry()
    {
        Time.timeScale = 1;
        if (dead)
        {
            PlayerPrefs.SetInt("Coin", coin);
        }
        PlayerPrefs.SetInt("HighScore", highScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        if (dead)
        {
            PlayerPrefs.SetInt("Coin", coin);
        }
        PlayerPrefs.SetInt("HighScore", highScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
