using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    [HideInInspector] public static LevelManager Instance;
    [Header("Image Of Heart")] public Image[] Heart;

    public TextMeshProUGUI gold;
    public Slider health_s;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance==null)
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Disable_one_image(int live_count)
    {
        if (Heart != null&&Heart.Length!=0)
            Heart[live_count - 1].enabled = false;
        GameManager.Instance.Refresh_Health();
    }
    void Enable_one_image(int count_live,int Level_count)
    {
        if (Heart != null)
            if (Heart.Length != count_live)
        {
                Heart[Level_count-1].enabled = true;
        }
    }
    public void Update_health(float health)
    {
        if (health_s != null)
              health_s.value = health/100;

    }
    public void Update_gold(int _gold)
    {
        if (gold != null)
        gold.text = " gold  " + _gold.ToString();
    }
}
