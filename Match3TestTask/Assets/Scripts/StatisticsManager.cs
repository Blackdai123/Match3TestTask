using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    [SerializeField] LevelInfo lvlPref;

    [SerializeField] Slider slider;
    [SerializeField] GameObject bar;

    [SerializeField] GameObject redToken;
    [SerializeField] GameObject greenToken;
    [SerializeField] GameObject blueToken;
    [SerializeField] GameObject yellowToken;
    [SerializeField] GameObject pinkToken;

    public static Action onTaskCompleted;

    private bool redPresent = false;
    private bool greenPresent = false;
    private bool bluePresent = false;
    private bool yellowPresent = false;
    private bool pinkPresent = false;

    private int quantityRed;
    private int quantityGreen;
    private int quantityBlue;
    private int quantityYellow;
    private int quantityPink;

    private Text quantityTextYellow;
    private Text quantityTextPink;
    private Text quantityTextBlue;
    private Text quantityTextRed;
    private Text quantityTextGreen;

    private List<GameObject> posIndexator = new List<GameObject>();

    private List<GameObject> fruitsThatCame = new List<GameObject>();

    private List<GameObject> _obstaclesList = new List<GameObject>();

    private bool scoringMode = false;
    private bool obstacleDestructionMode = false;
    private bool colorSelectionMode = false;

    private int _score;

    private void Start()
    {
        if (lvlPref.goalCollectTheNumberOfTokens)
        {
            bar.SetActive(true);

            slider.gameObject.SetActive(true);

            slider.minValue = 0;

            slider.maxValue = lvlPref.requiredNumberOfPoints;

            scoringMode= true;
        }

        if (lvlPref.goalIsToDestroyAllObstacles)
        {
            obstacleDestructionMode= true;
        }

        if(lvlPref.goalIsToDestroyCertainTokens)
        {
            colorSelectionMode= true;

            if (lvlPref.quantityRedTokens!=0)
            {
                redToken.gameObject.SetActive(true);

                redPresent = true;

                posIndexator.Add(redToken);

                quantityTextRed = redToken.GetComponentInChildren<Text>();

                quantityRed = lvlPref.quantityRedTokens;

                quantityTextRed.text = quantityRed.ToString();
            }

            if (lvlPref.quantityBlueTokens!=0)
            {
                blueToken.gameObject.SetActive(true);

                bluePresent = true;

                posIndexator.Add(blueToken);

                quantityTextBlue = blueToken.GetComponentInChildren<Text>();

                quantityBlue = lvlPref.quantityBlueTokens;

                quantityTextBlue.text = quantityBlue.ToString();
            }

            if (lvlPref.quantityGreenTokens!=0)
            {
                greenToken.gameObject.SetActive(true);

                greenPresent = true;

                posIndexator.Add(greenToken);

                quantityTextGreen = greenToken.GetComponentInChildren<Text>();

                quantityGreen = lvlPref.quantityGreenTokens;

                quantityTextGreen.text = quantityGreen.ToString();
            }

            if (lvlPref.quantityYellowTokens!=0)
            {
                yellowToken.gameObject.SetActive(true);

                yellowPresent = true;

                posIndexator.Add(yellowToken);

                quantityTextYellow = yellowToken.GetComponentInChildren<Text>();

                quantityYellow = lvlPref.quantityYellowTokens;

                quantityTextYellow.text = quantityYellow.ToString();
            }

            if (lvlPref.quantityPinkTokens!=0)
            {
                pinkToken.gameObject.SetActive(true);

                pinkPresent = true;

                posIndexator.Add(pinkToken);

                quantityTextPink = pinkToken.GetComponentInChildren<Text>();

                quantityPink = lvlPref.quantityPinkTokens;

                quantityTextPink.text = quantityPink.ToString();
            }

            if (posIndexator.Count > 0)
            {
                if (posIndexator.Count >= 5) 
                {
                    posIndexator[0].gameObject.transform.localPosition = new Vector2(0f, 0f);

                    posIndexator[1].gameObject.transform.localPosition = new Vector2(-150f, 0f);

                    posIndexator[2].gameObject.transform.localPosition = new Vector2(150f, 0f);

                    posIndexator[3].gameObject.transform.localPosition = new Vector2(300f, 0f);

                    posIndexator[4].gameObject.transform.localPosition = new Vector2(-300f, 0f);
                }

                if (posIndexator.Count == 1)
                {
                    posIndexator[0].gameObject.transform.localPosition = new Vector2(0f, 0f);
                }

                if (posIndexator.Count == 2)
                {
                    posIndexator[0].gameObject.transform.localPosition = new Vector2(0f, 0f);

                    posIndexator[1].gameObject.transform.localPosition = new Vector2(-150f, 0f);
                }

                if (posIndexator.Count == 3)
                {
                    posIndexator[0].gameObject.transform.localPosition = new Vector2(0f, 0f);

                    posIndexator[1].gameObject.transform.localPosition = new Vector2(-150f, 0f);

                    posIndexator[2].gameObject.transform.localPosition = new Vector2(150f, 0f);
                }

                if (posIndexator.Count == 4)
                {
                    posIndexator[0].gameObject.transform.localPosition = new Vector2(0f, 0f);

                    posIndexator[1].gameObject.transform.localPosition = new Vector2(-150f, 0f);

                    posIndexator[2].gameObject.transform.localPosition = new Vector2(150f, 0f);

                    posIndexator[3].gameObject.transform.localPosition = new Vector2(300f, 0f);
                }
            }
        }
    }

    void Update()
    {
        if (scoringMode)
        {
            slider.value = _score;

            if (_score >= slider.maxValue)
            {
                onTaskCompleted?.Invoke();
            }
        }

        if (colorSelectionMode)
        {
            if (fruitsThatCame.Count > 0)
            {
                var verified = new List<GameObject> ();

                foreach(var fruit in fruitsThatCame)
                {
                    if (fruit.gameObject.CompareTag("FruitApple"))
                    {
                        if (redPresent)
                        {
                            quantityRed -= 1;

                            if (quantityRed <= 0)
                            {
                                redPresent = false;

                                quantityTextRed.text = "0";
                            }
                            else
                            {
                                quantityTextRed.text = quantityRed.ToString();
                            }
                        }
                    }

                    if (fruit.gameObject.CompareTag("FruitBanana"))
                    {
                        if (yellowPresent)
                        {
                            quantityYellow -= 1;

                            if (quantityYellow <= 0)
                            {
                                yellowPresent = false;

                                quantityTextYellow.text = "0";
                            }
                            else
                            {
                                quantityTextYellow.text = quantityYellow.ToString();
                            }
                        }
                    }

                    if (fruit.gameObject.CompareTag("FruitBlueberry"))
                    {
                        if (bluePresent)
                        {
                            quantityBlue -= 1;

                            if (quantityBlue <= 0)
                            {
                                bluePresent = false;
                                
                                quantityTextBlue.text = "0";
                            }
                            else
                            {
                                quantityTextBlue.text = quantityBlue.ToString();
                            }
                        }
                    }

                    if (fruit.gameObject.CompareTag("FruitGrapes"))
                    {
                        if(pinkPresent)
                        {
                            quantityPink -= 1;

                            if (quantityPink <= 0)
                            {
                                pinkPresent = false;

                                quantityTextPink.text = "0";
                            }
                            else
                            {
                                quantityTextPink.text = quantityPink.ToString();
                            }
                        }
                    }

                    if (fruit.gameObject.CompareTag("FruitPear"))
                    {
                        if (greenPresent)
                        {
                            quantityGreen -= 1;

                            if (quantityGreen <= 0)
                            {
                                greenPresent = false;

                                quantityTextGreen.text = "0";
                            }
                            else
                            {
                                quantityTextGreen.text = quantityGreen.ToString();
                            }
                        }
                    }

                    verified.Add(fruit);
                }

                foreach(var item in verified)
                {
                    fruitsThatCame.Remove(item);
                }
            }

            if (!greenPresent && !redPresent && !bluePresent && !pinkPresent && !yellowPresent)
            {
                onTaskCompleted?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        TouchManager.onStartScoring += PointsRecord;

        LvlCreater.onCreatingObstacle += SetObstaclesList;

        ObstaclesManager.onDisappearanceObstacle += AdjustmentObstacleList;

        ObstaclesManager.onCreatingObstacle += SetObstaclesList;

        TouchManager.onStartSetFruit += SetFruitsThatCameList;
    }

    private void OnDisable()
    {
        TouchManager.onStartScoring -= PointsRecord;

        LvlCreater.onCreatingObstacle -= SetObstaclesList;

        ObstaclesManager.onDisappearanceObstacle -= AdjustmentObstacleList;

        ObstaclesManager.onCreatingObstacle -= SetObstaclesList;
            
        TouchManager.onStartSetFruit -= SetFruitsThatCameList;
    }

    private void PointsRecord(int score)
    {
        _score += score;
    }

    private void SetObstaclesList(GameObject obstacle)
    {
        _obstaclesList.Add(obstacle);
    }

    private void AdjustmentObstacleList(GameObject obstacle)
    {
        _obstaclesList.Remove(obstacle);

        if (obstacleDestructionMode)
        {
            Debug.Log(_obstaclesList.Count);

            if (_obstaclesList.Count <= 0)
            {
                onTaskCompleted?.Invoke();
            }
        }
    }

    private void SetFruitsThatCameList(GameObject fruit)
    {
        fruitsThatCame.Add(fruit);
    }
}
