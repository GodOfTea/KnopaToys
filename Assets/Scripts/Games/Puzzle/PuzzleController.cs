using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Games.Puzzle {
	public static class PuzzleController {
        public static List<PuzzleObject> SpawnedObjects = new List<PuzzleObject>();


		public static void CheckConnections() {
            var mainPuzzle = SpawnedObjects.FirstOrDefault(p => p.ConnectedParent == p.transform);
            if (!mainPuzzle) return;
            var completed = SpawnedObjects.All(p => p.ConnectedParent == mainPuzzle.transform);

            if (completed) {
                GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>().EndGame();
            }
		}
	}
}