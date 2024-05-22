

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AStar.Utils.DesignPattern.Singleton;
using UnityEngine.SceneManagement;
using System.Transactions;

public class LevelManager : SingletonMonoBase<LevelManager>
{
    public void LoadScene(string InSceneName)
    {
        SceneManager.LoadSceneAsync(InSceneName);

        //if (InSceneName == "Card3d")
        //{
        //    GameManager.Instance.isInCombatScene = true;
        //}
    }
}