using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    internal class EnemyAi
    {
        public bool HasPath => currentPath is object && currentPath.Count > 0;
        public Vector2 PathEnd => currentPath[^1];

        private readonly Enemy enemy;

        private bool isTargetReachable = false;
        private List<Vector2> currentPath;
        private int currentStep = 0;

        public EnemyAi(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public bool TryGetNextStep(Scene scene, Vector2 target, out Vector2 nextPosition)
        {
            if (!HasPath)
                BuildPath(scene, target);

            if (!isTargetReachable)
            {
                nextPosition = default;
                currentStep = 0;
                return false;
            }

            if (currentStep < currentPath.Count)
            {
                nextPosition = currentPath[currentStep++];
                return true;
            }
            else
            {
                BuildPath(scene, target);
                if (!isTargetReachable)
                {
                    nextPosition = default;
                    return false;
                }
                nextPosition = currentPath[currentStep++];
                return true;
            }
        }

        public void Reset(Scene scene, Vector2 target)
        {
            BuildPath(scene, target);
        }

        private void BuildPath(Scene scene, Vector2 target)
        {
            currentPath = GetPathToTarget(scene, target);
            isTargetReachable = currentPath is object && currentPath.Count > 0;
            currentStep = 0;
        }

        private List<Vector2> GetPathToTarget(Scene scene, Vector2 target)
        {
            var queue = new Queue<Vector2>();
            var visited = new HashSet<Vector2>();
            var paths = new Dictionary<Vector2, List<Vector2>>();

            var startLocation = Scene.ToIndexCoords(enemy.Center);
            visited.Add(startLocation);
            queue.Enqueue(startLocation);
            paths.Add(startLocation, new List<Vector2>());

            var targetIndex = Scene.ToIndexCoords(target);
            while (queue.Count != 0)
            {
                var location = queue.Dequeue();
                //if (visited.Contains(location)) continue;

                //path.Add(TranslateToCellCenter(scene.ToSceneCoords(location)));

                var dv = Vector2.Zero;
                for (dv.Y = -1; dv.Y <= 1; dv.Y++)
                    for (dv.X = -1; dv.X <= 1; dv.X++)
                    {
                        var nextLocation = location + dv;
                        if (/*dv.X != 0 && dv.Y != 0 ||*/ !scene.Contains(nextLocation) || 
                            visited.Contains(nextLocation) || scene[nextLocation] is Wall) continue;
                        queue.Enqueue(nextLocation);
                        visited.Add(nextLocation);
                        var newPath = new List<Vector2>(paths[location]) 
                        {
                            TranslateToCellCenter(Scene.ToSceneCoords(nextLocation))
                        };
                        paths.Add(nextLocation, newPath);
                        if (nextLocation == targetIndex) return paths[targetIndex];
                    }
            }

            return default;
        }

        private Vector2 TranslateToCellCenter(Vector2 location)
        {
            var offset = 0.5f * Settings.WorldWallSize;
            return new Vector2(location.X + offset, location.Y + offset);
        }
    }
}