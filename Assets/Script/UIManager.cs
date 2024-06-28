using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject HomePanel, PlayPanle, GameOverPanel, GameWinPanel; 
    string HOMEPANEL = "HOMEPANEL";
    string PLAYPANEL = "PLAYPANEL";
    public string OVERPANEL = "OVERPANEL";
    public string WINPANEL = "WINPANEL";
    //string QUITPANEL = "QUITPANEL";
    public TextMeshProUGUI levelShowText;

    public bool IsMove=false;
    void Start()
    {
        instance = this;
        panelmanage(HOMEPANEL);
    }
    void Update()
    {
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    if (HomePanel.activeSelf)
        //    {
        //        panelmanage(QUITPANEL);
        //    }
        //}
    }
    public void panelmanage(string arg)
    {
        HomePanel.SetActive(false);
        PlayPanle.SetActive(false);
        GameOverPanel.SetActive(false);
        GameWinPanel.SetActive(false);
        //QuitPanel.SetActive(false);

        if (arg == HOMEPANEL)
        {
            HomePanel.SetActive(true);
        }
        else if (arg == PLAYPANEL)
        {
            if (!HomePanel.activeSelf)
            {
                
                PlayPanle.SetActive(true);
            }
        }

        else if (arg == OVERPANEL)
        {
            if (!HomePanel.activeSelf)
            {
               // LevelManager.instance.DestroyLevel();
                PlayPanle.SetActive(false);
                GameOverPanel.SetActive(true);
                GameOverPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.OutBounce)
                     .OnComplete(() =>
                     {

                     });
            }
        }
        else if (arg == WINPANEL)
        {
           // LevelManager.instance.DestroyLevel();
            GameWinPanel.SetActive(true);
            GameOverPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.OutBounce)
                     .OnComplete(() =>
                     {
                         
                     });

        }
        /*else if (arg == QUITPANEL)
        {
            QuitPanel.SetActive(true);
        }*/
    }
    public void StartGame()
    {
        //SoundManager.Instance.playButtonSound();
        panelmanage(PLAYPANEL);
        LevelManager.instance.CreateLevel();
        IsMove=true;
    }

    public void GameOverScreen()
    {
        panelmanage(OVERPANEL);
    }

    public void LevelClear()
    {
        panelmanage(WINPANEL);
       // LevelManager.instance.DestroyLevel();
    }

    public void Retry()
    {

        //SoundManager.Instance.playButtonSound();
        
        //LevelManager.instance.DestroyLevel();
        // LevelManager.instance.CreateLevel();
       // LevelScript.instance.OBSGenrete();
        LevelManager.instance.ResetLevel();
        panelmanage(PLAYPANEL);
    }

    public void HomeScreen()
    {
        //SoundManager.Instance.playButtonSound();
        panelmanage(HOMEPANEL);
       // LevelManager.instance.DestroyLevel();
    }

    public void ApplicationQuit()
    {
       // SoundManager.Instance.playButtonSound();
        Application.Quit();
    }

    public void NoButton()
    {
        //SoundManager.Instance.playButtonSound();
        //QuitPanel.SetActive(false);
        panelmanage(HOMEPANEL);
    }

    public void NextLevel()
    {
        
        panelmanage(PLAYPANEL);
        LevelManager.instance.NextLevel();
    }

    public void NewNextLevel()
    {
        //if (LevelManager.instance.LiveLevel)
        //{
        //    Destroy(LevelManager.instance.LiveLevel);
        //    LevelManager.instance.NextLevel();
        //}
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("info_text");
    }
}