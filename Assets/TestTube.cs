using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTube : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public ColorTube[] mau;

    public GameObject ChooseObj;

    public void Choose()
    {
        ChooseObj.SetActive(true);
    }

    public void UnChoose()
    {
        ChooseObj.SetActive(false);
    }

    public void SetEmptyColor()
    {
        for (int i = 0; i < mau.Length; i++)
        {
            mau[i].SetColor(Color.white);
        }

        colors = new List<Color>();
    }

    public void SetFullColor(Color color)
    {
        colors = new List<Color>();

        for (int i = 0; i < mau.Length; i++)
        {
            mau[i].SetColor(color);
            colors.Add(color);
        }
    }

    public void AddColor(Color color)
    {
        colors.Add(color);
        Paint();
    }

    public void RemoveColor()
    {
        colors.RemoveAt(colors.Count - 1);
        Paint();
    }

    public void Paint()
    {
        for (int i = 0; i < mau.Length; i++)
        {
            if (i < colors.Count)
            {
                mau[i].SetColor(colors[i]);
            }
            else
            {
                mau[i].SetColor(Color.white);
            }
        }
    }

    public void MoveToTube(TestTube moveTube, bool checkColor = false)
    {
        if (moveTube.colors.Count >= 4)
        {
            return;
        }

        if (colors != null && colors.Count > 0)
        {
            var last = colors[^1];

            if (moveTube.colors != null && moveTube.colors.Count > 0)
            {
                if (checkColor)
                {
                    if (last != moveTube.colors[^1])
                    {
                        return;
                    }
                }
            }

            RemoveColor();
            moveTube.AddColor(last);


            MoveToTube(moveTube, true);
        }
    }

    public void MoveToTubeRandom(TestTube moveTube)
    {
        if (moveTube.colors.Count >= 4)
        {
            return;
        }

        if (colors != null && colors.Count > 0)
        {
            var last = colors[^1];

            RemoveColor();
            moveTube.AddColor(last);
        }
    }

    public bool CheckTrueTube()
    {
        return colors.Count == 0
               || (colors.Count == 4 && colors[0] == colors[1] && colors[1] == colors[2] && colors[2] == colors[3]);
    }
}