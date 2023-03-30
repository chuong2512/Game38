using System;
using System.Collections;
using System.Collections.Generic;
using JumpFrog;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum State
{
    Choose,
    ChooseNext,
    Win,
}

public class GameManager : Singleton<GameManager>
{
    public GameObject winUI;

    public State currentState = State.Choose;

    public TesterTube currentTube = null;
    public TesterTube[] tubes;

    public void SetState(State state)
    {
        currentState = state;
    }

    public void Restart(bool up)
    {
        if (up)
        {
            DirGameDataManager.Ins.playerData.LevelUP();
        }

        SceneManager.LoadScene("Game");
    }

    void Start()
    {
        RandomColor();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider)
            {
                Debug.Log($"{hit.collider.transform.gameObject.name}");

                /////This is where I look for the gameObjects with the PlanetSelection components.
                TesterTube hitObject = hit.collider.transform.GetComponent<TesterTube>();

                if (hitObject != null)
                {
                    Debug.Log($"Index : {hitObject.gameObject.name}");

                    if (currentState == State.Choose)
                    {
                        currentTube = hitObject;
                        currentTube.Choose();

                        SetState(State.ChooseNext);
                        return;
                    }


                    if (currentState == State.ChooseNext)
                    {
                        currentTube.UnChoose();

                        MoveColor(hitObject);

                        if (CheckWin())
                        {
                            SetState(State.Win);
                            winUI.SetActive(true);
                        }
                        else
                        {
                            SetState(State.Choose);
                        }
                    }
                }
            }
        }
    }

    private bool CheckWin()
    {
        for (int i = 0; i < tubes.Length; i++)
        {
            if (!tubes[i].CheckTrueTube())
            {
                return false;
            }
        }

        return true;
    }

    public void MoveColor(TesterTube tube)
    {
        currentTube?.MoveToTube(tube);
    }

    public void RandomColor()
    {
        tubes[0].SetFullColor(Color.blue);
        tubes[1].SetFullColor(Color.cyan);
        tubes[2].SetFullColor(Color.red);
        tubes[3].SetFullColor(Color.green);
        tubes[4].SetFullColor(Color.yellow);
        tubes[5].SetEmpColor();
        tubes[6].SetEmpColor();
        tubes[7].SetEmpColor();

        for (int i = 0; i < 20000; i++)
        {
            var fromRan = Random.Range(0, 8);
            var toRan = Random.Range(0, 8);

            if (fromRan != toRan)
            {
                tubes[fromRan].MoveToTubeRan(tubes[toRan]);
            }
        }
    }

    public void Help()
    {
        if (DirGameDataManager.Ins.playerData.intHelp >= 1)
        {
            DirGameDataManager.Ins.playerData.SubHelp(1);
            for (int i = 0; i < 50; i++)
            {
                var fromRan = Random.Range(0, 8);
                var toRan = Random.Range(0, 8);

                if (fromRan != toRan)
                {
                    tubes[fromRan].MoveToTube(tubes[toRan]);
                }
            }
        }
    }
}