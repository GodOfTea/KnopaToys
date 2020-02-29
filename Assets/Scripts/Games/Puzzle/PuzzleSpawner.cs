using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games.Puzzle {
	public class PuzzleSpawner : MonoBehaviour
    {
        public SoundManager sm;
		/// <summary>
		/// Размер: 2 - 2х2, 3 - 3х3 или 4 - 4х4
		/// </summary>
		public static int Size;
		public static Texture2D PuzzleTexture;
        public Texture2D[] PuzzleTextures;
        public Texture2D[] PuzzleContour;

        [SerializeField] private Material         PuzzleMaterial;
		//[SerializeField] private Texture2D        PuzzleTestTexture;
		[SerializeField] private List<GameObject> Puzzles_2x2;
		[SerializeField] private List<GameObject> Puzzles_3x3;
		[SerializeField] private List<GameObject> Puzzles_4x4;
		[SerializeField] private Vector2          SpawnFrom, SpawnTo;


		private void Start()
        {
            sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
            Size = Singleton.Instance.Extra_Index;
            PuzzleTexture = PuzzleTextures[Random.Range(0, PuzzleTextures.Length)];
			PuzzleController.SpawnedObjects.Clear();

            PuzzleMaterial.EnableKeyword("_DETAIL_MULX2");
            PuzzleMaterial.SetTexture("_DetailAlbedoMap", PuzzleContour[Size]);
			PuzzleMaterial.mainTexture = PuzzleTexture;
            
			SpawnPuzzles();

            int tip;                                                                      /* 0 - первый раз, 1 - уже было */
                                                                                          /* Подсказка если игра открыта в первый раз */
            if (!PlayerPrefs.HasKey("PuzzleGame")) { PlayerPrefs.SetInt("PuzzleGame", 0); } /* Создали если не было */
            tip = PlayerPrefs.GetInt("PuzzleGame");                                        /* Получили значение */

            if (tip == 0 && Setting_Menu.Sound)
            {
                sm.Play("PuzzleGame");
                PlayerPrefs.SetInt("PuzzleGame", 1);
            }
        }


		private void SpawnPuzzles() {
			var puzzles = GetSpawnPuzzles();

			foreach (var p in puzzles) {
				var go      = Instantiate(p);
				var pObject = go.GetComponent<PuzzleObject>();
				go.transform.position = GetRandomSpawnPos();

				PuzzleController.SpawnedObjects.Add(pObject);
			}
		}


		private List<GameObject> GetSpawnPuzzles() {
			switch (Size) {
				case 2: return Puzzles_2x2;
				case 3: return Puzzles_3x3;
				case 4: return Puzzles_4x4;
				default: {
					Debug.LogError("Incorrect puzzle size");
					return null;
				}
			}
		}


		private Vector3 GetRandomSpawnPos() {
			var fromX = Random.Range(SpawnFrom.x, SpawnTo.x);
			var fromY = Random.Range(SpawnFrom.y, SpawnTo.y);

			return new Vector3(fromX, fromY, 0);
		}
	}
}