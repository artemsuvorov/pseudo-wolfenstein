using PseudoWolfenstein.Core;
using System;
using System.Collections.Generic;

namespace PseudoWolfenstein.Model
{
    public class LevelCollection
    {
        private readonly List<Scene> Levels = new();
        private int currentLevelIndex = 0;

        public event EventHandler CurrentLevelFinished;
        public event EventHandler AllLevelsFinished;

        public LevelCollection()
        {
            //LoadLevel(Scene.SceneBuilder.Level_1, "Level1");
            LoadLevel(Scene.SceneBuilder.Level_2, "Level2");
            //LoadLevel(Scene.SceneBuilder.Level_4, "Level4");
        }

        public Scene GetNextLevel()
        {
            if (currentLevelIndex < Levels.Count)
                return Levels[currentLevelIndex++];

            return default;
        }

        private void LoadLevel(string levelSource, string name)
        {
            var level = Scene.Builder.FromString(levelSource, name);
            level.Finished += OnLevelFinished;
            Levels.Add(level);
        }

        private void OnLevelFinished(object sender, GameEventArgs e)
        {
            if (currentLevelIndex < Levels.Count)
                CurrentLevelFinished?.Invoke(this, EventArgs.Empty);
            else
                AllLevelsFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}