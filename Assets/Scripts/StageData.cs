using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageData : MonoBehaviour {
    public int stageLevel = 1;
    public GameObject orcPrefab;  
    public GameObject skeletonPrefab;

    public Transform[] spawnPoints; // Assign these in the Unity Editor

    public List<MonsterSpawnInfo> stage1Monsters;
    public List<MonsterSpawnInfo> stage2Monsters;
    public List<MonsterSpawnInfo> stage3Monsters;

    void Awake() {     
         SetupStages();
    }

    private void SetupStages() {
        #region Stage 1
        int numberOfSpawnPoints = Random.Range(0, 3); 
        stage1Monsters.Add(new MonsterSpawnInfo(orcPrefab, 10, spawnPoints[numberOfSpawnPoints]));
        #endregion

        #region Stage 2
        numberOfSpawnPoints = Random.Range(0, 3);
        stage2Monsters.Add(new MonsterSpawnInfo(skeletonPrefab, 10, spawnPoints[numberOfSpawnPoints]));
        #endregion

        #region Stage 3
        numberOfSpawnPoints = Random.Range(0, 3);
        stage3Monsters.Add(new MonsterSpawnInfo(orcPrefab, 10, spawnPoints[numberOfSpawnPoints]));
        stage3Monsters.Add(new MonsterSpawnInfo(skeletonPrefab, 10, spawnPoints[numberOfSpawnPoints]));
        #endregion
    }

    public List<MonsterSpawnInfo> GetMonsterSpawnInfoForStage(int stageNumber) {
        switch (stageNumber) {
            case 1: return stage1Monsters;
            case 2: return stage2Monsters;
            case 3: return stage3Monsters;
            default: return new List<MonsterSpawnInfo>(); // Return empty list for invalid stage
        }
    }
}