using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameOver;

    public List<List<GameObject>> objList;

    public static Action onStartCreateFruit;

    public static Action<GameObject> onStartMovementFruit;

    private List<GameObject> notActiveFruits = new List<GameObject>();

    private void OnEnable()
    {
        ParticleManager.onStartMovement += StartCreateFruit;

        ParticleManager.onGetNotActiveFruit += SetFruitToList;

        StatisticsManager.onTaskCompleted += EventWinLvl;

        Timer.onGameOver += EventGameOver;
    }

    private void OnDisable()
    {
        ParticleManager.onStartMovement -= StartCreateFruit;

        ParticleManager.onGetNotActiveFruit -= SetFruitToList;

        StatisticsManager.onTaskCompleted -= EventWinLvl;

        Timer.onGameOver -= EventGameOver;
    }

    private void SetFruitToList(GameObject notActiveFruit)
    {
        notActiveFruits.Add(notActiveFruit);

        if (notActiveFruits.Count >30)
        {
            Destroy(notActiveFruits[0]);
            Destroy(notActiveFruits[1]);
            Destroy(notActiveFruits[2]);
            Destroy(notActiveFruits[3]);
            Destroy(notActiveFruits[4]);
            Destroy(notActiveFruits[5]);

            notActiveFruits.Remove(notActiveFruits[0]);
            notActiveFruits.Remove(notActiveFruits[1]);
            notActiveFruits.Remove(notActiveFruits[2]);
            notActiveFruits.Remove(notActiveFruits[3]);
            notActiveFruits.Remove(notActiveFruits[4]);
            notActiveFruits.Remove(notActiveFruits[5]);
        }
    }

    private void EventWinLvl()
    {
        winCanvas.gameObject.SetActive(true);

        gameCanvas.gameObject.SetActive(false);
    }

    private void EventGameOver()
    {
        gameOver.gameObject.SetActive(true);

        gameCanvas.gameObject.SetActive(false);
    }

    private void StartCreateFruit(GameObject obj)
    {
        onStartCreateFruit?.Invoke();
    }
}
