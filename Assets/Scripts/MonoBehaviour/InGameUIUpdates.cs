using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIUpdates : MonoBehaviour
{
    public List<GameObject> PlayerLives;
    public Text Score;

    public void SetScore(int value)
    {
        Score.text = value.ToString();
    }

    public void RemoveLife()
    {
        PlayerLives.First(x => x.activeSelf).SetActive(false);
    }

}
