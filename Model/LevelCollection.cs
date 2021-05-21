using PseudoWolfenstein.Core;
using System;
using System.Collections.Generic;

namespace PseudoWolfenstein.Model
{
    public class LevelCollection
    {
        private readonly List<Scene> Levels = new();
        private int currentLevelIndex = 0;

        public event EventHandler OnCurrentLevelFinished;

        public LevelCollection()
        {
            LoadLevel(Scene.SceneBuilder.SingleBlockSceneStr, "SingleBlockScene");
            //LoadLevel(Scene.SceneBuilder.Level_1, "Level1");
            //LoadLevel(Scene.SceneBuilder.Level_2, "Level2");
            //LoadLevel(Scene.SceneBuilder.Level_4, "Level4");
        }

        public Scene GetNextLevel()
        {
            return Levels[currentLevelIndex++];
        }

        private void LoadLevel(string levelSource, string name)
        {
            var level = Scene.Builder.FromString(levelSource, name);
            level.Finished += OnLevelFinished;
            Levels.Add(level);
        }

        private void OnLevelFinished(object sender, GameEventArgs e)
        {
            OnCurrentLevelFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}