using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<GameObject> Levels;

    public int current_level;
    public GameObject infotext;

    public CinemachineVirtualCamera cinemachineVirtual;
    public List<GameObject> BallsList;
    public GameObject LiveBall;
    
 
    private void Start()
    {
        instance = this;
      
        current_level = PlayerPrefs.GetInt("level");
       // CreateLevel();
    }
    public void CreateLevel()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(false);
        }
        Levels[PlayerPrefs.GetInt("level")].SetActive(true);
      
        Debug.Log("CreateLevel" + PlayerPrefs.GetInt("level"));
        int levelInt = PlayerPrefs.GetInt("level");
        ResetLevel();
       // UIManager.instance.levelShowText.text = (levelInt + 1).ToString();


    }
  

    public void NextLevel()
    {
        int levelInt = PlayerPrefs.GetInt("level") + 1;
        UIManager.instance.levelShowText.text = (levelInt + 1).ToString();
        PlayerPrefs.SetInt("level", levelInt);

        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(false);
        }
        Levels[PlayerPrefs.GetInt("level")].SetActive(true);
        UIManager.instance.GameWinPanel.SetActive(false);
    }

    public void ShowInfoText()
    {

        if (PlayerPrefs.GetInt("info_text") == 0)
        {

            infotext.SetActive(true);
        }
        else
        {

            infotext.SetActive(false);
        }

    }
    public void ResetLevel() {


        //Camera
         MyPostion camPos= cinemachineVirtual.gameObject.GetComponent<MyPostion>();

        cinemachineVirtual.Follow=null;
        cinemachineVirtual.LookAt=null;
        cinemachineVirtual.gameObject.transform.position= camPos.MyPos;
        cinemachineVirtual.gameObject.transform.localRotation= Quaternion.Euler(camPos.MyRot);

        //----------------------------------------------------------
        //Ball
        if (LiveBall) { 
            Destroy(LiveBall);
            LiveBall = Instantiate(BallsList[0]);
        }else
        {
            LiveBall = Instantiate(BallsList[0]);

        }
        LiveBall.transform.localScale=Vector3.zero;
        LiveBall.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.OutBounce);
        Invoke("setCamera",0.02f);
        //---------------------------------------------------------
        LevelScript levelScript = Levels[PlayerPrefs.GetInt("level")].GetComponent<LevelScript>();  
      
        for (int i = 0; i < levelScript.ResetOBSlist.Count; i++)
        {
            levelScript.ResetOBSlist[i].gameObject.transform.position = levelScript.ResetOBSlist[i].MyPos;
        }

    }
    //ShowTextStop()
    void setCamera()
    {

        cinemachineVirtual.Follow = LiveBall.transform.GetChild(0).transform;
        cinemachineVirtual.LookAt = LiveBall.transform.GetChild(0).transform;
    }
    public void ShowTextStop()
    {
        infotext.SetActive(false);
    }
}