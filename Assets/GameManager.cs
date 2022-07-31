using System.Collections;
using BoomGames.Template;
using Dreamteck.Splines;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public enum States
{
    TapToStart,
    Playing,
    Fail,
    Win
}
public class GameManager : Singleton<GameManager>
{
    public States currentState;
    public SplineFollower spF;
    public GameObject startPanel, failPanel, winPanel;
    public CinemachineSwitcher cs;
    public CinemachineVirtualCamera failCam, startCam, winCam;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject child;
    
    private void OnEnable()
    {
        spF.onEndReached += WinFunc;
    }

    private void OnDisable()
    {
        spF.onEndReached -= WinFunc;
    }

    void Start()
    {
        currentState = States.TapToStart;
        spF.followSpeed = 0;
        startPanel.SetActive(true);
        failPanel.SetActive(false);
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Playing:
                startPanel.SetActive(false);
                failPanel.SetActive(false);
                break;
            case States.Fail:
                Fail();
                break;
        }
    }
    public IEnumerator Dash()
     {
         
         
         while (Vector3.Distance(player.transform.position , target.transform.position) > 0.01f)
         {
              var newPos = new Vector3(0, child.transform.localPosition.y, 0);
              child.transform.localPosition = Vector3.MoveTowards(child.transform.localPosition, newPos, 8 * Time.deltaTime);
              player.transform.position = Vector3.MoveTowards(player.transform.position, target.transform.position, 8 * Time.deltaTime);
              yield return null;
         }
         currentState = States.Win;
     }
    void Fail()
    {
        cs.SwitchPri(failCam, startCam);
        failPanel.SetActive(true);
    }
    public void TapToStart()
    {
        startPanel.SetActive(false);
        currentState = States.Playing;
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    public void WinFunc(double _)
    {
        spF.follow = false;
        spF.enabled = false;
        StartCoroutine(Dash());
        winPanel.SetActive(true);
        cs.SwitchPri(winCam, startCam);
    }
}
/*using System.Collections;
using System.Collections.Generic;
using BoomGames.Template;
using Dreamteck.Splines;
using UnityEngine;

using Cinemachine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum States
{
    TapToStart,
    Playing,
    Fail,
    Win
}
public class GameManager : Singleton<GameManager>
{
    public States currentState;
    public SplineFollower spF;
    public GameObject startPanel, failPanel, winPanel;
    public CinemachineSwitcher cs;
    public CinemachineVirtualCamera failCam, startCam, winCam;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject child;
    
    private void OnEnable()
    {
        spF.onEndReached += WinFunc;
    }

    private void OnDisable()
    {
        spF.onEndReached -= WinFunc;
    }

    void Start()
    {
        currentState = States.TapToStart;
        spF.followSpeed = 0;
        startPanel.SetActive(true);
        failPanel.SetActive(false);
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Playing:
                startPanel.SetActive(false);
                failPanel.SetActive(false);
                break;
            case States.Fail:
                Fail();
                break;
        }
    }

    public void MoveDash()
    {
        player.transform.DOMove(target.transform.position, 2f).OnComplete(()=>
        {
            currentState = States.Win;
        });
    }
    // public IEnumerator Dash()
    // {
    //     player.transform.DOMove(target.transform.position, 2f).OnComplete(()=> currentState = States.Win);
    //     // var targetpos = target.transform.position;;
    //     // while (Vector3.Distance(player.transform.position , targetpos) > 0.001f)
    //     // {
    //     //     player.transform.position = Vector3.MoveTowards(player.transform.position, targetpos, 80 * Time.deltaTime);
    //     //     yield return null;
    //     // }
    //     // currentState = States.Win;
    // }
    void Fail()
    {
        cs.SwitchPri(failCam, startCam);
        failPanel.SetActive(true);
    }
    public void TapToStart()
    {
        startPanel.SetActive(false);
        currentState = States.Playing;
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    public void WinFunc(double _)
    {
        spF.follow = false;
        spF.enabled = false;
        // StartCoroutine(Dash());
        MoveDash();
        child.transform.localPosition = new Vector3(0, child.transform.localPosition.y, 0);
        winPanel.SetActive(true);
        cs.SwitchPri(winCam, startCam);
    }
}*/

