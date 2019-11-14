using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMapGenerater : MonoBehaviour
{
    [SerializeField] GameObject[] maps;
    [SerializeField] Vector3 mapSpawnPoint;
    int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("TutorialMapIndex");
        Instantiate(maps[index], mapSpawnPoint, Quaternion.Euler(0, 0, 0));
    }

    public void Reload()
    {
        index++;
        PlayerPrefs.SetInt("TutorialMapIndex", index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
