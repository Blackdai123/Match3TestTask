using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffectApple;

    [SerializeField] ParticleSystem particleEffectBanana;

    [SerializeField] ParticleSystem particleEffectPear;

    [SerializeField] ParticleSystem particleEffectGrapes;

    [SerializeField] ParticleSystem particleEffectBlueberry;

    public static Action<GameObject> onStartMovement;

    public static Action onStartCheking;

    public static Action<GameObject> onStartChekingObstacle;

    public static Action<int> onStartResetObstacle;

    public static Action<GameObject> onGetNotActiveFruit;

    void Start()
    {
        particleEffectApple.GetComponent<ParticleSystem>();

        particleEffectBanana.GetComponent<ParticleSystem>();

        particleEffectPear.GetComponent<ParticleSystem>();

        particleEffectGrapes.GetComponent<ParticleSystem>();

        particleEffectBlueberry.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        TouchManager.onStartPlayEffect += StartPlayEffect;
    }

    private void OnDisable()
    {
        TouchManager.onStartPlayEffect -= StartPlayEffect;
    }

    private IEnumerator StartMovementFruit(GameObject obj, ParticleSystem particle)
    {
        onStartChekingObstacle?.Invoke(obj);

        yield return new WaitForSeconds(1.5f);

        Destroy(particle);

        RayCheking(obj);

        onGetNotActiveFruit?.Invoke(obj);

        onStartCheking?.Invoke();
    }

    private void StartPlayEffect(GameObject gameObject)
    {
        if (gameObject.CompareTag("FruitApple"))
        {
            var position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, -5);

            var effect = Instantiate(particleEffectApple, position, Quaternion.identity);

            effect.Play();

            StartCoroutine(StartMovementFruit(gameObject, effect));
        }

        if (gameObject.CompareTag("FruitBanana"))
        {
            var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5);

            var effect = Instantiate(particleEffectBanana, position, Quaternion.identity);

            effect.Play();

            StartCoroutine(StartMovementFruit(gameObject, effect));
        }

        if (gameObject.CompareTag("FruitPear"))
        {
            var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5);

            var effect = Instantiate(particleEffectPear, position, Quaternion.identity);

            effect.Play();

            StartCoroutine(StartMovementFruit(gameObject, effect));
        }

        if (gameObject.CompareTag("FruitBlueberry"))
        {
            var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5);

            var effect = Instantiate(particleEffectBlueberry, position, Quaternion.identity);

            effect.Play();

            StartCoroutine(StartMovementFruit(gameObject, effect));
        }

        if (gameObject.CompareTag("FruitGrapes"))
        {
            var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5);

            var effect = Instantiate(particleEffectGrapes, position, Quaternion.identity);

            effect.Play();

            StartCoroutine(StartMovementFruit(gameObject, effect));
        }
    }

    private void RayCheking(GameObject delObj)
    {
        var rayUp = Physics2D.RaycastAll(delObj.transform.position, transform.up);

        foreach (var item in rayUp)
        {
            var endPos = new Vector3(item.transform.position.x, item.transform.position.y - 1, item.transform.position.z);

            item.transform.position = Vector3.Lerp(item.transform.position, endPos, 1);

            if (item.rigidbody.gameObject.CompareTag("StoneLvl1") || gameObject.CompareTag("StoneLvl2"))
            {
                onStartResetObstacle?.Invoke(item.rigidbody.gameObject.GetInstanceID());
            }
        }
    }
}
