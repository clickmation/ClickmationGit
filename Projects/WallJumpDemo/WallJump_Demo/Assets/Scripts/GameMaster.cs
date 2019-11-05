using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;
    [SerializeField] InputController inputController;
    RandomMapGanerater rmg;
    public List<TriggerFunction> triggerFunctions = new List<TriggerFunction>();
    AudioManager am;

    [SerializeField] GameObject mainCharacter;
    [SerializeField] GameObject startCollider;
    [SerializeField] Movement mov;

    [SerializeField] Camera2DFollow camFol;
    [SerializeField] CameraShake camShake;
    public Vector3 playerSpawnPoint;
    public Button attackButton;

    public int deathCount;
    int jumpCount;
    int killCount;

    [Space]

    [Header("UI")]
    [SerializeField] GameObject deadPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject reTryCheckPanel;
    [SerializeField] GameObject mainmenuCheckPanel;
    [SerializeField] GameObject soundPanel;
    [SerializeField] GameObject adReviveButton;
    [SerializeField] GameObject tokenReviveButton;
    [SerializeField] List<GameObject> shockWaves = new List<GameObject>();
    public CanvasGroup overlayCanvas;
    public int coin;
    public Text coinText;
    public int adToken;
    [SerializeField] private int revivable;

    [Space]

    [Header("Stamina")]

    public int staminaCount;
    [SerializeField] private Transform staminaBarParent;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private List<GameObject> staminaBars = new List<GameObject>();
    private bool barActivated;

    [Space]

    [Header("Fever")]

    public float fever;
    private float oriFever;
    [SerializeField] int feverIndex;
    [SerializeField] private bool fevered;
    public GameObject feverEffect;
    [SerializeField] FeverStruct[] feverStructs;
    [System.Serializable]
    public struct FeverStruct
    {
        public bool started;
        public float fever;
        public float feverEater;
        public float feverStartPoint;
        public float feverStopPoint;
        public RectTransform feverImage;
    }

    [Space]

    [Header("Dead")]

    public bool dead;
    [SerializeField] GameObject shockWaveDeath;
    public GameObject deathParticle;

    [Space]

    [Header("Score")]

    public int score;
    public int realScore;
    public int scoreAdd;
    public int scoreMultiplier = 1;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;
    public float scoreAddDelay;
    public Text deadPanelScore;
    public Text pausePanelScore;
    public Transform inGameUI;
    public GameObject scorePlusText;

    [Space]

    [Header("Sound")]

    [SerializeField] bool sound;
    public float masterVolume;
    public float soundEffectVolume;
    public float bgmVolume;
    public Slider masterVolumeSlider;
    public Slider soundEffectVolumeSlider;
    public Slider bgmVolumeSlider;

    [Space]

    [Header("JumpAdd")]

    public AddJumpScore[] addJumpScores;
    [System.Serializable]
    public struct AddJumpScore
    {
        public int score;
        public int addJump;
    }

    void Awake ()
    {
        GameMaster.gameMaster = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.saveload.gm = this;
        if (AudioManager.audioManager != null)
        {
            am = AudioManager.audioManager;
            am.gm = this;
            am.SetAudioSources();
        }
        rmg = RandomMapGanerater.randomMapGanerater;
        adToken = PlayerPrefs.GetInt("AdToken");
        PlayerPrefs.SetInt("AdToken", 0);
        oriFever = fever;
        fever = 0;
        SaveLoad.saveload.SoundLoad();
        //SaveLoad.saveload.TokenLoad();
        //SaveLoad.saveload.TokenSave('+');
        masterVolumeSlider.onValueChanged = am.setMasterVolume;
        soundEffectVolumeSlider.onValueChanged = am.setSoundEffectVolume;
        bgmVolumeSlider.onValueChanged = am.setBGMVolume;
        //masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        //soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume");
        //bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        masterVolumeSlider.value = masterVolume;
        soundEffectVolumeSlider.value = soundEffectVolume;
        bgmVolumeSlider.value = bgmVolume;
        am.SetMasterVolume();
        am.SetSoundEffectVolume();
        am.SetBGMVolume();
        SaveLoad.saveload.GMLoad();
        StartCoroutine(ScoreCoroutine());
        revivable = 1;

        ADManager.adManager.RequestBanner();
    }

    public void Jumpcount (int c)
    {
        jumpCount += c;
    }

    public void KillCount (int c)
    {
        killCount += c;
    }

    public void SpawnShockWave (GameObject sw, float t)
    {
        GameObject shockWave = Instantiate(sw, mov.transform.position, Quaternion.Euler(0, 0, 0));
        shockWaves.Add(shockWave);
        StartCoroutine(ShockWavesCoroutine(shockWave, t));
    }
    IEnumerator ShockWavesCoroutine(GameObject _sw, float _t)
    {
        yield return new WaitForSeconds(_t);
        shockWaves.Remove(_sw);
    }

    bool timeScaleTriggered;
    public void TimeScaleFunction()
    {
        if (!timeScaleTriggered)
        {
            timeScaleTriggered = true;
            Time.timeScale = 0.2f;
        }
        else
        {
            timeScaleTriggered = false;
            Time.timeScale = 1f;
        }
    }

    IEnumerator safCoroutine;
    public void StaminaActiveTrue(int c)
    {
        if (!barActivated)
        {
            barActivated = true;
            if (safCoroutine != null) {
                StopCoroutine(safCoroutine);
                for (int i = 0; i < staminaBars.Count; i++)
                {
                    Destroy(staminaBars[i]);
                }
                staminaBars.Clear();
                staminaCount = 0;
            }
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
    }

    public void WallJump()
    {
        staminaBars[--staminaCount].GetComponent<Animator>().SetTrigger("Death");
    }

    public void StaminaActiveFalse ()
    {
        if (barActivated)
        {
            barActivated = false;
            safCoroutine = StaminaActiveFalseCoroutine();
            StartCoroutine(safCoroutine);
        }
    }

    IEnumerator StaminaActiveFalseCoroutine ()
    {
        for (int i = staminaCount; i < staminaBars.Count; i++)
        {
            yield return new WaitForSeconds(0.5f/ (float)staminaCount);
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
        safCoroutine = null;
    }

    public void FeverAdd (float f)
    {
        feverStructs[feverIndex].fever += f;
        //if (feverStructs[feverIndex].fever >= oriFever) feverStructs[feverIndex].fever = oriFever;
        feverStructs[feverIndex].feverImage.localScale = new Vector3(feverStructs[feverIndex].fever / oriFever, 1, 1);
        //if (!feverStructs[feverIndex].started && feverStructs[feverIndex].fever >= feverStructs[feverIndex].feverStartPoint) StartCoroutine(FeverCoroutine(feverIndex));
        while (feverIndex < feverStructs.Length)
        {
            if (!feverStructs[feverIndex].started && feverStructs[feverIndex].fever >= feverStructs[feverIndex].feverStartPoint) StartCoroutine(FeverCoroutine(feverIndex));
            if (feverStructs[feverIndex].fever > oriFever)
            {
                if (feverIndex < feverStructs.Length - 1) feverStructs[feverIndex + 1].fever += feverStructs[feverIndex].fever - oriFever;
                feverStructs[feverIndex].fever = oriFever;
                feverStructs[feverIndex].feverImage.localScale = new Vector3(feverStructs[feverIndex].fever / oriFever, 1, 1);
                feverIndex++;
            }
            else break;
        }
        if (feverIndex == feverStructs.Length - 1 && feverStructs[feverIndex].fever > oriFever) feverStructs[feverIndex].fever = oriFever;
        feverIndex = 0;
    }

    IEnumerator FeverCoroutine(int index)
    {
        feverStructs[index].started = true;
        scoreMultiplier++;
        if (scoreMultiplier == 2) am.FeverAudio(true);
        if (index == 0) feverEffect.SetActive(true);
        while (feverStructs[index].fever >= feverStructs[index].feverStopPoint)
        {
            feverStructs[index].fever -= feverStructs[index].feverEater;
            feverStructs[index].feverImage.localScale = new Vector3(feverStructs[index].fever  / oriFever, 1, 1);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        scoreMultiplier--;
        if (scoreMultiplier == 1) am.FeverAudio(false);
        feverStructs[index].started = false;
        if (index == 0)
        {
            feverEffect.SetActive(false);
            for (int i = 1; i < feverStructs.Length; i++)
            {
                feverStructs[i].fever = 0;
            }
        }
    }

    IEnumerator ScoreCoroutine()
    {
        yield return new WaitForSeconds(1f);
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        while (!dead)
        {
            yield return new WaitForSeconds(scoreAddDelay);
            score += scoreAdd * scoreMultiplier;
            realScore += scoreAdd;
            scoreText.text = score.ToString();
            if (score > highScore)
            {
                highScoreText.text = score.ToString();
            }
        }
    }

    public void AddScore (int s)
    {
        score += s;
        GameObject spt = Instantiate(scorePlusText, inGameUI);
        spt.GetComponent<Text>().text = "+" + s.ToString();
        scoreText.text = score.ToString();
        Destroy(spt, 1f);
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
        inputController.gameObject.SetActive(false);
        camFol.enabled = false;
        SpawnShockWave(shockWaveDeath, 2f);
        camShake.Shake(100);
        GameObject _deathParticle = Instantiate(deathParticle, mov.transform.position, Quaternion.identity) as GameObject;
        Destroy(_deathParticle, 3f);
        Destroy(mov.gameObject);
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (revivable == 1)
        {
            if (adToken == 0)
            {
                adReviveButton.SetActive(true);
                tokenReviveButton.SetActive(false);
            }
            else
            {
                adReviveButton.SetActive(false);
                tokenReviveButton.SetActive(true);
            }
        }
        deadPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        deadPanelScore.text = score.ToString();
        overlayCanvas.alpha = 0.2f;
        if (score > highScore) highScore = score;
        deathCount++;
    }

    public void Pause()
    {
        for (int i = 0; i < shockWaves.Count; i++) shockWaves[i].SetActive(false);
        inputController.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        overlayCanvas.alpha = 0.2f;
        pausePanelScore.text = score.ToString();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        for (int i = 0; i < shockWaves.Count; i++) shockWaves[i].SetActive(true);
        inputController.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        overlayCanvas.alpha = 1.0f;
    }

    public void TokenRevive ()
    {
        SaveLoad.saveload.TokenSave('-');
        SaveLoad.saveload.TokenLoad();
        Revive();
    }

    public void AdRevive ()
    {
        ADManager.adManager.ShowReviveRewardedAd();
    }

    public void Revive()
    {
        dead = false;
        revivable--;
        adReviveButton.SetActive(false);
        tokenReviveButton.SetActive(false);
        inputController.gameObject.SetActive(true);
        for (int i = 0; i < triggerFunctions.Count; i++) triggerFunctions[i].Trigger();
        rmg.StartSpawn();
        Instantiate(startCollider, playerSpawnPoint, Quaternion.Euler(0, 0, 0));
        GameObject mainChar = Instantiate(mainCharacter, playerSpawnPoint, Quaternion.Euler(0, 0, 0));
        mov = mainChar.GetComponent<Movement>();
        mov.dir = rmg.mapList[0].dir;
        camFol.enabled = true;
        camFol.SetTarget(mainChar.transform);
        inputController.mov = mov;
        inputController.col = mov.GetComponent<Collision>();
        mov.inputController = inputController;
        attackButton.onClick = mov.attackFunction;
        StartCoroutine(ScoreCoroutine());
        scoreText.gameObject.SetActive(true);
        deadPanel.SetActive(false);
        mov.camFol = camFol;
        mov.camShake = camShake;
        Time.timeScale = 1;
        overlayCanvas.alpha = 1.0f;
    }

    bool reTryCheckTriggered;
    public void ReTryCheck ()
    {
        if (!reTryCheckTriggered)
        {
            reTryCheckTriggered = true;
            reTryCheckPanel.SetActive(true);
        }
        else
        {
            reTryCheckTriggered = false;
            reTryCheckPanel.SetActive(false);
        }
    }

    public void ReTry()
    {
        if (dead) SaveLoad.saveload.GMSave();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool mainmenuCheckTriggered;

    public void MainMenuCheck ()
    {
        if (!mainmenuCheckTriggered)
        {
            mainmenuCheckTriggered = true;
            mainmenuCheckPanel.SetActive(true);
        }
        else
        {
            mainmenuCheckTriggered = false;
            mainmenuCheckPanel.SetActive(false);
        }
    }

    public void MainMenu()
    {
        if (dead) SaveLoad.saveload.GMSave();
        Time.timeScale = 1;
        SaveLoad.saveload.gm = null;
        ADManager.adManager.HideBanner();
        am.FeverAudioDefaultSet();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Sound()
    {
        if (!sound)
        {
            SaveLoad.saveload.SoundLoad();
            sound = true;
            soundPanel.SetActive(true);
        }
        else
        {
            sound = false;
            soundPanel.SetActive(false);
        }
    }
}
