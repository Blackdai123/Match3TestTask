using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] GameObject obstacle1Lvl;

    public static Action<GameObject> onCreatingObstacle;

    public static Action<GameObject> onDisappearanceObstacle;

    private GameObject firstFruitUp;
    private GameObject firstFruitDown;
    private GameObject firstFruitLeft;
    private GameObject firstFruitRight;

    void Start()
    {
        if (gameObject.CompareTag("StoneLvl2") || gameObject.CompareTag("StoneLvl1"))
        {
            CheckBackgraund();
        }

        SetFirstFruit();
    }

    private void OnEnable()
    {
        ParticleManager.onStartChekingObstacle += StartСomparison;

        ParticleManager.onStartResetObstacle += ResetAllFirstFruit;
    }

    private void OnDisable()
    {
        ParticleManager.onStartChekingObstacle -= StartСomparison;

        ParticleManager.onStartResetObstacle -= ResetAllFirstFruit;
    }

    private void ResetAllFirstFruit(int IdObstacle)
    {
        if (gameObject.GetInstanceID() == IdObstacle)
        {
            SetFirstFruit();
        }
    }

    private void StartСomparison(GameObject touchFruit)
    {
        if (gameObject.CompareTag("IseLvl2") || gameObject.CompareTag("IseLvl1"))
        {
            if (touchFruit == firstFruitUp || touchFruit == firstFruitDown || touchFruit == firstFruitLeft || touchFruit == firstFruitRight)
            {
                if (gameObject.CompareTag("IseLvl2"))
                {
                    gameObject.SetActive(false);

                    var ise = Instantiate(obstacle1Lvl, transform.position, Quaternion.identity);

                    onCreatingObstacle?.Invoke(ise);

                    onDisappearanceObstacle?.Invoke(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);

                    onDisappearanceObstacle?.Invoke(gameObject);
                }
            }
        }
        else
        {
            if (touchFruit == firstFruitUp || touchFruit == firstFruitDown || touchFruit == firstFruitLeft || touchFruit == firstFruitRight)
            {
                if (gameObject.CompareTag("StoneLvl2") || gameObject.CompareTag("IseLvl2"))
                {
                    gameObject.SetActive(false);

                    var stone = Instantiate(obstacle1Lvl, transform.position, Quaternion.identity);

                    onCreatingObstacle?.Invoke(stone);

                    onDisappearanceObstacle?.Invoke(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);

                    RayCheking(gameObject);

                    onDisappearanceObstacle?.Invoke(gameObject);
                }
            }
        }
    }

    private void SetFirstFruit()
    {
        var rayUp = Physics2D.RaycastAll(transform.position, transform.up);

        var rayDown = Physics2D.RaycastAll(transform.position, -transform.up);

        var rayLeft = Physics2D.RaycastAll(transform.position, -transform.right);

        var rayRight = Physics2D.RaycastAll(transform.position, transform.right);

        var hitList = new List<RaycastHit2D>();

        firstFruitUp = AssignmentOfTheFirstElement(hitList, firstFruitUp, rayUp);

        firstFruitDown = AssignmentOfTheFirstElement(hitList, firstFruitDown, rayDown);

        firstFruitLeft = AssignmentOfTheFirstElement(hitList, firstFruitLeft, rayLeft);

        firstFruitRight = AssignmentOfTheFirstElement(hitList, firstFruitRight, rayRight);
    }

    private GameObject AssignmentOfTheFirstElement(List<RaycastHit2D> hitList, GameObject firstFruit, RaycastHit2D[] ray)
    {
        foreach (var hit in ray)
        {
            if (hit.rigidbody.gameObject.layer == 6)
            {
                hitList.Add(hit);
            }
        }

        if (gameObject.CompareTag("IseLvl2") || gameObject.CompareTag("IseLvl1"))
        {
            if (hitList.Count > 1)
            {
                firstFruit = hitList[1].rigidbody.gameObject;
            }
        }
        else
        {
            if (hitList.Count > 0)
            {
                firstFruit = hitList[0].rigidbody.gameObject;
            }
        }

        hitList.Clear();

        return firstFruit;
    }

    private void CheckBackgraund()
    {
        var ray = Physics2D.RaycastAll(transform.position, transform.forward);

        foreach (var hit in ray)
        {
            if (hit.collider != null)
            {
                if (hit.rigidbody.gameObject.layer == 6)
                {
                    hit.rigidbody.gameObject.SetActive(false);
                }
            }
        }
    }

    private void RayCheking(GameObject delObj)
    {
        var rayUp = Physics2D.RaycastAll(delObj.transform.position, transform.up);

        foreach (var item in rayUp)
        {
            var endPos = new Vector3(item.transform.position.x, item.transform.position.y - 1, item.transform.position.z);

            item.transform.position = Vector3.Lerp(item.transform.position, endPos, 1);
        }
    }
}
