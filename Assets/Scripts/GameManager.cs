using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [HideInInspector]public static GameManager Instance;
    public Image Fader;
    private int numberoflevel = 2;
    int currentlevel;
    string scene_name;
    private int score_gold = 0;
    [SerializeField] private int _Live_count = 3;
    private static int _count_level = 3;
    [SerializeField] private float _Health;
    void Awake()
    {
        Refresh_player();
        currentlevel = 1;
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_Live_count == 0)
        {
            GameOver();
            _Live_count = _count_level;
        }
        LevelManager.Instance.Update_health(_Health);
    }
    public void Apllay_damage(float amount)
    {
        if (amount < _Health)
        {
            _Health -= amount;
        }
            else
            {
                LevelManager.Instance.Disable_one_image(_Live_count);
                _Live_count--;
            }
    }
    void Refresh_player()
    {
        _Health = 100f;
        _Live_count = 3;
    }
    public void Increse_health(float health)
    {
        if ((health + _Health) < 100)
            _Health += health;
        else
            _Health = 100;
    }
    public void Refresh_Health()
    {
        _Health = 100;
    }
   
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Level Reload or reset
    }
    public void collect_gold(int amount)
    {
        score_gold += amount;
        LevelManager.Instance.Update_gold(score_gold);
    }
    public void NextLevel()
    {
        scene_name = "level" + currentlevel.ToString();
        if (currentlevel < numberoflevel)
        {
            SceneManager.LoadScene(scene_name);   
        }
    }
    public void ReloadScene()
    {
        Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, 0); // incase loss at the time of fading the new scene
        StopCoroutine("FaderReload");
        StartCoroutine("FaderReload");
    }
    IEnumerator FaderReload()
    {
        yield return new WaitForSeconds(1);
        Fader.gameObject.SetActive(true);

        float startTime = Time.time;
        float Duration = 0.2f;
        while (Time.time < startTime + Duration)
        {
            float t = (Time.time - startTime) / Duration;
            Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        startTime = Time.time;
        while (Time.time < startTime + Duration)
        {
            float t = (Time.time - startTime) / Duration;
            Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }
        Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, 0);
    }
}
