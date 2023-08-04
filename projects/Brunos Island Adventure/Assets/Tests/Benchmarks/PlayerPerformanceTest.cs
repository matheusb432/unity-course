using RPG.Quests;
using RPG.Util;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Benchmarks
{
    public sealed class PlayerPerformanceTest
    {
        [UnityTest, Performance]
        public IEnumerator PlayerFind1k()
        {
            SceneManager.LoadScene(Consts.ISLAND_SCENE_IDX);
            yield return null;

            Measure
                .Method(() => GameObject.FindWithTag(Consts.PLAYER_TAG))
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1_000)
                .Run();
            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator PlayerFindInventory1k()
        {
            SceneManager.LoadScene(Consts.ISLAND_SCENE_IDX);
            yield return null;

            // ? Finding the player done 1000 times takes around 0.1ms, while getting the player and then the inventory takes around 0.35ms
            Measure
                .Method(() => GameObject.FindWithTag(Consts.PLAYER_TAG).GetComponent<Inventory>())
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1_000)
                .Run();
            yield return null;
        }
    }
}
