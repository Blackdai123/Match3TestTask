using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LvlCreater : MonoBehaviour
{
    [SerializeField] LevelInfo lvlPref;

    public static Action<GameObject> onCreatingObstacle;

    void Start()
    {
        BackgroundInitialization();
        
        if (lvlPref.posFirsObstacleLvl1 != null)
        {
            SettingObstacles(lvlPref.posFirsObstacleLvl1, lvlPref.obstacleLvl1First);
        }

        if (lvlPref.posFirsObstacleLvl2 != null)
        {
            SettingObstacles(lvlPref.posFirsObstacleLvl2, lvlPref.obstacleLvl2First);
        }

        if (lvlPref.posSecondObstacleLvl1 != null)
        {
            SettingObstacles(lvlPref.posSecondObstacleLvl1, lvlPref.obstacleLvl1Second);
        }

        if (lvlPref.posSecondObstacleLvl2 != null)
        {
            SettingObstacles(lvlPref.posSecondObstacleLvl2, lvlPref.obstacleLvl2Second);
        }
    }

    public void BackgroundInitialization()
    {
        for (int i = 0; i < lvlPref.width; i++)
        {
            for (int j = 0; j < lvlPref.height; j++)
            {
                var tempPosition = new Vector2(i, j);

                var backgroundTile = Instantiate(lvlPref.backgraundTile, tempPosition, Quaternion.identity);
            }
        }
    }

    private void SettingObstacles(Vector2[] posObstacles, GameObject obstacle)
    {
        foreach (var pos in posObstacles)
        {
            var positiontToV3 = new Vector3(pos.x, pos.y, -1);

            obstacle = Instantiate(obstacle, positiontToV3, Quaternion.identity);

            onCreatingObstacle?.Invoke(obstacle);
        }
    }
}
