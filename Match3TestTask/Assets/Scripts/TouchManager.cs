using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static Action onStartCheking;

    public static Action onSoundAdition;
    public static Action onSoundFade;

    public static Action<int> onStartScoring;
    public static Action<GameObject> onStartSetFruit;

    public static Action<GameObject> onStartPlayEffect;

    private List<GameObject> touchesGameObj = new List<GameObject>();

    private string tagName;

    private bool swither = false; //this variable shows whether we have selected the first fruit

    public void OnDrag(PointerEventData eventData)
    {
        foreach (var item in eventData.hovered)
        {
            if (item.tag!= "Untagged")
            {
                if (swither)
                {
                    if (item.tag == tagName)
                    {
                        ListEntry(item.GetInstanceID(), item);

                        item.transform.localScale = new Vector2(1.1f, 1.1f);
                    }
                    else
                    {
                        if (touchesGameObj.Count>=3)
                        {
                            if (touchesGameObj.Count >= 5 && touchesGameObj.Count <= 7)
                            {
                                var score = 0;

                                var indexator = 0;

                                var lastItemIndex = touchesGameObj.Count - 1;

                                var rayLeft = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, -transform.right);

                                var rayRight = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, transform.right);

                                foreach (var hitLeft in rayLeft)
                                {
                                    indexator = 0;

                                    foreach (var touchFruit in touchesGameObj)
                                    {
                                        if (hitLeft.rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                        {
                                            indexator++;
                                        }
                                    }

                                    if (indexator == touchesGameObj.Count)
                                    {
                                        if (hitLeft.rigidbody.gameObject.layer == 6)
                                        {
                                            hitLeft.rigidbody.gameObject.SetActive(false);

                                            onStartPlayEffect?.Invoke(hitLeft.rigidbody.gameObject);

                                            onStartSetFruit?.Invoke(hitLeft.rigidbody.gameObject);

                                            score++;
                                        }
                                    }
                                }

                                foreach (var hitRight in rayRight)
                                {
                                    indexator = 0;

                                    foreach (var touchFruit in touchesGameObj)
                                    {
                                        if (hitRight.rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                        {
                                            indexator++;
                                        }
                                    }

                                    if (indexator == touchesGameObj.Count)
                                    {
                                        if (hitRight.rigidbody.gameObject.layer == 6)
                                        {
                                            hitRight.rigidbody.gameObject.SetActive(false);

                                            onStartPlayEffect?.Invoke(hitRight.rigidbody.gameObject);

                                            onStartSetFruit?.Invoke(hitRight.rigidbody.gameObject);

                                            score++;
                                        }
                                    }
                                }

                                onStartScoring?.Invoke(score);
                            }

                            if (touchesGameObj.Count >= 8)
                            {
                                var score = 0;//the variable records the number of fruit scored (points)

                                var indexator = 0; //this variable is needed to record the number of discrepancies between the selected elements by the player and the elements affected by the bonus

                                var lastItemIndex = touchesGameObj.Count - 1;

                                var rayLeft = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, -transform.right);

                                var rayRight = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, transform.right);

                                var rayUp = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, transform.up);

                                var rayDown = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, -transform.up);

                                if (rayLeft.Length >= 2)
                                {
                                    RayUpCheker(rayLeft[1].rigidbody.gameObject);

                                    RayDownCheker(rayLeft[1].rigidbody.gameObject);

                                    indexator = 0;

                                    foreach(var touchFruit in touchesGameObj)
                                    {
                                        if (rayLeft[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                        {
                                            indexator++;
                                        }
                                    }

                                    if (indexator == touchesGameObj.Count)
                                    {
                                        rayLeft[1].rigidbody.gameObject.SetActive(false);

                                        onStartPlayEffect?.Invoke(rayLeft[1].rigidbody.gameObject);

                                        onStartSetFruit?.Invoke(rayLeft[1].rigidbody.gameObject);

                                        score++;
                                    }

                                    if (rayLeft.Length >= 3)
                                    {
                                        RayUpCheker(rayLeft[2].rigidbody.gameObject);

                                        RayDownCheker(rayLeft[2].rigidbody.gameObject);

                                        indexator = 0;

                                        foreach (var touchFruit in touchesGameObj)
                                        {
                                            if (rayLeft[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                            {
                                                indexator++;
                                            }
                                        }

                                        if (indexator == touchesGameObj.Count)
                                        {
                                            rayLeft[2].rigidbody.gameObject.SetActive(false);

                                            onStartPlayEffect?.Invoke(rayLeft[2].rigidbody.gameObject);

                                            onStartSetFruit?.Invoke(rayLeft[2].rigidbody.gameObject);

                                            score++;
                                        }
                                    }
                                }

                                if (rayRight.Length >= 2)
                                {
                                    RayUpCheker(rayRight[1].rigidbody.gameObject);

                                    RayDownCheker(rayRight[1].rigidbody.gameObject);

                                    indexator = 0;

                                    foreach (var touchFruit in touchesGameObj)
                                    {
                                        if (rayRight[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                        {
                                            indexator++;
                                        }
                                    }

                                    if (indexator == touchesGameObj.Count)
                                    {
                                        rayRight[1].rigidbody.gameObject.SetActive(false);

                                        onStartPlayEffect?.Invoke(rayRight[1].rigidbody.gameObject);

                                        onStartSetFruit?.Invoke(rayRight[1].rigidbody.gameObject);

                                        score++;
                                    }

                                    if (rayRight.Length >= 3)
                                    {
                                        RayUpCheker(rayRight[2].rigidbody.gameObject);

                                        RayDownCheker(rayRight[2].rigidbody.gameObject);

                                        indexator = 0;

                                        foreach (var touchFruit in touchesGameObj)
                                        {
                                            if (rayRight[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                            {
                                                indexator++;
                                            }
                                        }

                                        if (indexator == touchesGameObj.Count)
                                        {
                                            rayRight[2].rigidbody.gameObject.SetActive(false);

                                            onStartPlayEffect?.Invoke(rayRight[2].rigidbody.gameObject);

                                            onStartSetFruit?.Invoke(rayRight[2].rigidbody.gameObject);

                                            score++;
                                        }
                                    }
                                }

                                if (rayUp.Length >= 2)
                                {
                                    indexator = 0;

                                    foreach (var touchFruit in touchesGameObj)
                                    {
                                        if (rayUp[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                        {
                                            indexator++;
                                        }
                                    }

                                    if (indexator == touchesGameObj.Count)
                                    {
                                        rayUp[1].rigidbody.gameObject.SetActive(false);

                                        onStartPlayEffect?.Invoke(rayUp[1].rigidbody.gameObject);

                                        onStartSetFruit?.Invoke(rayUp[1].rigidbody.gameObject);

                                        score++;
                                    }

                                    if (rayUp.Length >= 3)
                                    {
                                        indexator = 0;

                                        foreach (var touchFruit in touchesGameObj)
                                        {
                                            if (rayUp[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                            {
                                                indexator++;
                                            }
                                        }

                                        if (indexator == touchesGameObj.Count)
                                        {
                                            rayUp[2].rigidbody.gameObject.SetActive(false);

                                            onStartPlayEffect?.Invoke(rayUp[2].rigidbody.gameObject);

                                            onStartSetFruit?.Invoke(rayUp[2].rigidbody.gameObject);

                                            score++;
                                        }
                                    }
                                }

                                if (rayDown.Length >= 2)
                                {
                                    indexator = 0;

                                    foreach (var touchFruit in touchesGameObj)
                                    {
                                        if (rayDown[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                        {
                                            indexator++;
                                        }
                                    }

                                    if (indexator == touchesGameObj.Count)
                                    {
                                        rayDown[1].rigidbody.gameObject.SetActive(false);

                                        onStartPlayEffect?.Invoke(rayDown[1].rigidbody.gameObject);

                                        onStartSetFruit?.Invoke(rayDown[1].rigidbody.gameObject);

                                        score++;
                                    }

                                    if (rayDown.Length >= 3)
                                    {
                                        indexator = 0;

                                        foreach (var touchFruit in touchesGameObj)
                                        {
                                            if (rayDown[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                                            {
                                                indexator++;
                                            }
                                        }

                                        if (indexator == touchesGameObj.Count)
                                        {
                                            rayDown[2].rigidbody.gameObject.SetActive(false);

                                            onStartPlayEffect?.Invoke(rayDown[2].rigidbody.gameObject);

                                            onStartSetFruit?.Invoke(rayDown[2].rigidbody.gameObject);

                                            score++;
                                        }
                                    }
                                }

                                onStartScoring?.Invoke(score);
                            }

                            foreach (var obj in touchesGameObj)
                            {
                                obj.SetActive(false);

                                onStartPlayEffect?.Invoke(obj);

                                onStartSetFruit?.Invoke(obj);
                            }

                            tagName = "";

                            swither = false;

                            onStartScoring?.Invoke(touchesGameObj.Count);

                            onSoundFade?.Invoke();

                            touchesGameObj.Clear();
                        }
                        else
                        {
                            foreach (var item1 in touchesGameObj)
                            {
                                item1.transform.localScale = new Vector2(1f, 1f);
                            }

                            touchesGameObj.Clear();

                            tagName = "";

                            swither = false;
                        }
                    }
                }
                else
                {
                    switch (item.tag)
                    {
                        case "FruitApple":

                            item.transform.localScale = new Vector2(1.1f, 1.1f);

                            ListEntry(item.GetInstanceID(), item);

                            tagName = item.tag;

                            swither = true;

                            break;

                        case "FruitBanana":

                            item.transform.localScale = new Vector2(1.1f, 1.1f);

                            ListEntry(item.GetInstanceID(), item);

                            tagName = item.tag;

                            swither = true;

                            break;

                        case "FruitBlueberry":

                            item.transform.localScale = new Vector2(1.1f, 1.1f);

                            ListEntry(item.GetInstanceID(), item);

                            tagName = item.tag;

                            swither = true;

                            break;

                        case "FruitGrapes":

                            item.transform.localScale = new Vector2(1.1f, 1.1f);

                            ListEntry(item.GetInstanceID(), item);

                            tagName = item.tag;

                            swither = true;

                            break;

                        case "FruitPear":

                            item.transform.localScale = new Vector2(1.1f, 1.1f);

                            ListEntry(item.GetInstanceID(), item);

                            tagName = item.tag;

                            swither = true;

                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (touchesGameObj.Count >= 3)
        {
            if (touchesGameObj.Count >= 5 && touchesGameObj.Count <= 7)
            {
                var score = 0;//explanations written above

                var indexator = 0;//explanations written above

                var lastItemIndex = touchesGameObj.Count - 1;

                var rayLeft = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, -transform.right);

                var rayRight = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, transform.right);

                foreach (var hitLeft in rayLeft)
                {
                    indexator = 0;

                    foreach (var touchFruit in touchesGameObj)
                    {
                        if (hitLeft.rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                        {
                            indexator++;
                        }
                    }

                    if (indexator == touchesGameObj.Count)
                    {
                        if (hitLeft.rigidbody.gameObject.layer == 6)
                        {
                            hitLeft.rigidbody.gameObject.SetActive(false);

                            onStartPlayEffect?.Invoke(hitLeft.rigidbody.gameObject);

                            onStartSetFruit?.Invoke(hitLeft.rigidbody.gameObject);

                            score++;
                        }
                    }
                }

                foreach (var hitRight in rayRight)
                {
                    indexator = 0;

                    foreach (var touchFruit in touchesGameObj)
                    {
                        if (hitRight.rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                        {
                            indexator++;
                        }
                    }

                    if (indexator == touchesGameObj.Count)
                    {
                        if (hitRight.rigidbody.gameObject.layer == 6)
                        {
                            hitRight.rigidbody.gameObject.SetActive(false);

                            onStartPlayEffect?.Invoke(hitRight.rigidbody.gameObject);

                            onStartSetFruit?.Invoke(hitRight.rigidbody.gameObject);

                            score++;
                        }
                    }
                }

                onStartScoring?.Invoke(score);
            }

            if (touchesGameObj.Count >= 8)
            {
                var score = 0;

                var indexator = 0;

                var lastItemIndex = touchesGameObj.Count - 1;

                var rayLeft = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, -transform.right);

                var rayRight = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, transform.right);

                var rayUp = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, transform.up);

                var rayDown = Physics2D.RaycastAll(touchesGameObj[lastItemIndex].transform.position, -transform.up);

                if (rayLeft.Length >= 2)
                {
                    RayUpCheker(rayLeft[1].rigidbody.gameObject);

                    RayDownCheker(rayLeft[1].rigidbody.gameObject);

                    indexator = 0;

                    foreach (var touchFruit in touchesGameObj)
                    {
                        if (rayLeft[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                        {
                            indexator++;
                        }
                    }

                    if (indexator == touchesGameObj.Count)
                    {
                        rayLeft[1].rigidbody.gameObject.SetActive(false);

                        onStartPlayEffect?.Invoke(rayLeft[1].rigidbody.gameObject);

                        onStartSetFruit?.Invoke(rayLeft[1].rigidbody.gameObject);

                        score++;
                    }

                    if (rayLeft.Length >= 3)
                    {
                        RayUpCheker(rayLeft[2].rigidbody.gameObject);

                        RayDownCheker(rayLeft[2].rigidbody.gameObject);

                        indexator = 0;

                        foreach (var touchFruit in touchesGameObj)
                        {
                            if (rayLeft[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                            {
                                indexator++;
                            }
                        }

                        if (indexator == touchesGameObj.Count)
                        {
                            rayLeft[2].rigidbody.gameObject.SetActive(false);

                            onStartPlayEffect?.Invoke(rayLeft[2].rigidbody.gameObject);

                            onStartSetFruit?.Invoke(rayLeft[2].rigidbody.gameObject);

                            score++;
                        }
                    }
                }

                if (rayRight.Length >= 2)
                {
                    RayUpCheker(rayRight[1].rigidbody.gameObject);

                    RayDownCheker(rayRight[1].rigidbody.gameObject);

                    indexator = 0;

                    foreach (var touchFruit in touchesGameObj)
                    {
                        if (rayRight[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                        {
                            indexator++;
                        }
                    }

                    if (indexator == touchesGameObj.Count)
                    {
                        rayRight[1].rigidbody.gameObject.SetActive(false);

                        onStartPlayEffect?.Invoke(rayRight[1].rigidbody.gameObject);

                        onStartSetFruit?.Invoke(rayRight[1].rigidbody.gameObject);

                        score++;
                    }

                    if (rayRight.Length >= 3)
                    {
                        RayUpCheker(rayRight[2].rigidbody.gameObject);

                        RayDownCheker(rayRight[2].rigidbody.gameObject);

                        indexator = 0;

                        foreach (var touchFruit in touchesGameObj)
                        {
                            if (rayRight[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                            {
                                indexator++;
                            }
                        }

                        if (indexator == touchesGameObj.Count)
                        {
                            rayRight[2].rigidbody.gameObject.SetActive(false);

                            onStartPlayEffect?.Invoke(rayRight[2].rigidbody.gameObject);

                            onStartSetFruit?.Invoke(rayRight[2].rigidbody.gameObject);

                            score++;
                        }
                    }
                }

                if (rayUp.Length >= 2)
                {
                    indexator = 0;

                    foreach (var touchFruit in touchesGameObj)
                    {
                        if (rayUp[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                        {
                            indexator++;
                        }
                    }

                    if (indexator == touchesGameObj.Count)
                    {
                        rayUp[1].rigidbody.gameObject.SetActive(false);

                        onStartPlayEffect?.Invoke(rayUp[1].rigidbody.gameObject);

                        onStartSetFruit?.Invoke(rayUp[1].rigidbody.gameObject);

                        score++;
                    }

                    if (rayUp.Length >= 3)
                    {
                        indexator = 0;

                        foreach (var touchFruit in touchesGameObj)
                        {
                            if (rayUp[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                            {
                                indexator++;
                            }
                        }

                        if (indexator == touchesGameObj.Count)
                        {
                            rayUp[2].rigidbody.gameObject.SetActive(false);

                            onStartPlayEffect?.Invoke(rayUp[2].rigidbody.gameObject);

                            onStartSetFruit?.Invoke(rayUp[2].rigidbody.gameObject);

                            score++;
                        }
                    }
                }

                if (rayDown.Length >= 2)
                {
                    indexator = 0;

                    foreach (var touchFruit in touchesGameObj)
                    {
                        if (rayDown[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                        {
                            indexator++;
                        }
                    }

                    if (indexator == touchesGameObj.Count)
                    {
                        rayDown[1].rigidbody.gameObject.SetActive(false);

                        onStartPlayEffect?.Invoke(rayDown[1].rigidbody.gameObject);

                        onStartSetFruit?.Invoke(rayDown[1].rigidbody.gameObject);

                        score++;
                    }

                    if (rayDown.Length >= 3)
                    {
                        indexator = 0;

                        foreach (var touchFruit in touchesGameObj)
                        {
                            if (rayDown[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                            {
                                indexator++;
                            }
                        }

                        if (indexator == touchesGameObj.Count)
                        {
                            rayDown[2].rigidbody.gameObject.SetActive(false);

                            onStartPlayEffect?.Invoke(rayDown[2].rigidbody.gameObject);

                            onStartSetFruit?.Invoke(rayDown[2].rigidbody.gameObject);

                            score++;
                        }
                    }
                }

                onStartScoring?.Invoke(score);
            }

            foreach (var obj in touchesGameObj)
            {
                obj.SetActive(false);

                onStartPlayEffect?.Invoke(obj);

                onStartSetFruit?.Invoke(obj);
            }

            tagName = "";

            swither = false;

            onStartScoring?.Invoke(touchesGameObj.Count);

            onSoundFade?.Invoke();

            touchesGameObj.Clear();
        }
        else
        {
            foreach (var item1 in touchesGameObj)
            {
                item1.transform.localScale = new Vector2(1f, 1f);
            }

            touchesGameObj.Clear();

            tagName = "";

            swither = false;
        }
    }

    private void ListEntry(int idNewGameObj, GameObject obj)
    {
        var inconsistencies = 0;

        foreach (var item in touchesGameObj)
        {
            var idElemForArr = item.GetInstanceID();
            
            if (idElemForArr != idNewGameObj )
            {
                inconsistencies++;
            }
            else
            {
                break;
            }
        }

        if (inconsistencies == touchesGameObj.Count)
        {
            touchesGameObj.Add(obj);

            onSoundAdition?.Invoke();
        }
    }

    private void RayUpCheker(GameObject gameObj)
    {
        var score = 0;

        var indexator = 0;
 
        var ray = Physics2D.RaycastAll(gameObj.transform.position, gameObj.transform.up);

        if (ray.Length >= 2)
        {
            foreach (var touchFruit in touchesGameObj)
            {
                if (ray[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                {
                    indexator++;
                }
            }

            if (indexator == touchesGameObj.Count)
            {
                ray[1].rigidbody.gameObject.SetActive(false);

                onStartPlayEffect?.Invoke(ray[1].rigidbody.gameObject);

                onStartSetFruit?.Invoke(ray[1].rigidbody.gameObject);

                score++;

                indexator = 0;
            }

            if (ray.Length>=3)
            {

                foreach (var touchFruit in touchesGameObj)
                {
                    if (ray[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                    {
                        indexator++;
                    }
                }

                if (indexator == touchesGameObj.Count)
                {
                    ray[2].rigidbody.gameObject.SetActive(false);

                    onStartPlayEffect?.Invoke(ray[2].rigidbody.gameObject);

                    onStartSetFruit?.Invoke(ray[2].rigidbody.gameObject);

                    score++;
                }
            }

            onStartScoring?.Invoke(score);
        }
    }

    private void RayDownCheker(GameObject gameObj)
    {
        var score = 0;

        var indexator = 0;

        var ray = Physics2D.RaycastAll(gameObj.transform.position, -gameObj.transform.up);

        if (ray.Length >= 2)
        {
            foreach (var touchFruit in touchesGameObj)
            {
                if (ray[1].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                {
                    indexator++;
                }
            }

            if (indexator == touchesGameObj.Count)
            {
                ray[1].rigidbody.gameObject.SetActive(false);

                onStartPlayEffect?.Invoke(ray[1].rigidbody.gameObject);

                onStartSetFruit?.Invoke(ray[1].rigidbody.gameObject);

                score++;

                indexator = 0;
            }

            if (ray.Length >= 3)
            {
                foreach (var touchFruit in touchesGameObj)
                {
                    if (ray[2].rigidbody.gameObject.GetInstanceID() != touchFruit.gameObject.GetInstanceID())
                    {
                        indexator++;
                    }
                }

                if (indexator == touchesGameObj.Count)
                {
                    ray[2].rigidbody.gameObject.SetActive(false);

                    onStartPlayEffect?.Invoke(ray[2].rigidbody.gameObject);

                    onStartSetFruit?.Invoke(ray[2].rigidbody.gameObject);

                    score++;
                }
            }

            onStartScoring?.Invoke(score);
        }
    }
}
