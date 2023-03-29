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

    public TestTube currentTube = null;
    public TestTube[] tubes;

    public void SetState(State state)
    {
        currentState = state;
    }

    public void Restart(bool up)
    {
        if (up)
        {
            GameDataManager.Ins.playerData.UpLevel();
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
                TestTube hitObject = hit.collider.transform.GetComponent<TestTube>();

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

    public void MoveColor(TestTube tube)
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
        tubes[5].SetEmptyColor();
        tubes[6].SetEmptyColor();
        tubes[7].SetEmptyColor();

        for (int i = 0; i < 20000; i++)
        {
            var fromRan = Random.Range(0, 8);
            var toRan = Random.Range(0, 8);

            if (fromRan != toRan)
            {
                tubes[fromRan].MoveToTubeRandom(tubes[toRan]);
            }
        }
    }

    public void Help()
    {
        if (GameDataManager.Ins.playerData.intHelp >= 1)
        {
            GameDataManager.Ins.playerData.SubHelp(1);
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