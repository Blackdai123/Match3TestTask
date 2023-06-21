using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="LevelInfo", menuName ="Assets/LvlInfo")]
public class LevelInfo : ScriptableObject
{
    public int width;

    public int height;

    public GameObject obstacleLvl1First;

    public GameObject obstacleLvl2First;

    public GameObject obstacleLvl1Second;

    public GameObject obstacleLvl2Second;

    public GameObject backgraundTile;

    public Vector2[] posFirsObstacleLvl1;

    public Vector2[] posFirsObstacleLvl2;

    public Vector2[] posSecondObstacleLvl1;

    public Vector2[] posSecondObstacleLvl2;

    public bool goalCollectTheNumberOfTokens;

    public float timeToScore;

    public int requiredNumberOfPoints;

    public bool goalIsToDestroyAllObstacles;

    public bool goalIsToDestroyCertainTokens;

    public int quantityRedTokens;

    public int quantityGreenTokens;

    public int quantityBlueTokens;

    public int quantityYellowTokens;

    public int quantityPinkTokens;
}
