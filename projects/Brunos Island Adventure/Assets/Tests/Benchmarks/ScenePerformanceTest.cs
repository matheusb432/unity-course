using RPG.Util;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Benchmarks
{
    public class ScenePerformanceTest
    {
        // NOTE Benchmark the frame time and initial load time of each scene
        [UnityTest, Performance]
        public IEnumerator MainMenuScene_Load()
        {
            using (Measure.Scope("Setup.MainMenuScene"))
            {
                SceneManager.LoadScene(Consts.MAIN_MENU_SCENE_IDX);
            }
            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator MainMenuScene_Frames()
        {
            SceneManager.LoadScene(Consts.MAIN_MENU_SCENE_IDX);
            yield return null;

            yield return Measure.Frames().WarmupCount(10).MeasurementCount(100).Run();
        }

        [UnityTest, Performance]
        public IEnumerator IslandScene_Frames()
        {
            SceneManager.LoadScene(Consts.ISLAND_SCENE_IDX);
            yield return null;
            yield return Measure.Frames().WarmupCount(10).MeasurementCount(100).Run();
        }

        [UnityTest, Performance]
        public IEnumerator DungeonScene_Frames()
        {
            SceneManager.LoadScene(Consts.DUNGEON_SCENE_IDX);
            yield return null;

            yield return Measure.Frames().WarmupCount(10).MeasurementCount(100).Run();
        }
    }
}
