using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgraundTile : MonoBehaviour
{
    [SerializeField] GameObject[] fruits;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var fruitToUse = Random.Range(0, fruits.Length);

        var fruitPosition = new Vector3(transform.position.x, transform.position.y, -1);

        var fruit = Instantiate(fruits[fruitToUse], fruitPosition, Quaternion.identity);
    }

    private void OnEnable()
    {
        ParticleManager.onStartCheking += DelayedMethodCall;
    }

    private void OnDisable()
    {
        ParticleManager.onStartCheking += DelayedMethodCall;
    }

    private void StartCheking()
    {
        var ray = Physics2D.Raycast(transform.position, -transform.forward);

        if (ray.rigidbody != null)
        {
            if (ray.rigidbody.gameObject.layer != 6 && ray.rigidbody.gameObject.layer != 7)
            {
                var fruitToUse = Random.Range(0, fruits.Length);

                var fruitPosition = new Vector3(transform.position.x, transform.position.y, -1);

                var fruit = Instantiate(fruits[fruitToUse], fruitPosition, Quaternion.identity);
            }
        }
        else
        {
            var fruitToUse = Random.Range(0, fruits.Length);

            var fruitPosition = new Vector3(transform.position.x, transform.position.y, -1);

            var fruit = Instantiate(fruits[fruitToUse], fruitPosition, Quaternion.identity);
        }
    }

    private void DelayedMethodCall()
    {
        Invoke("StartCheking", 1.2f);
    }
}
