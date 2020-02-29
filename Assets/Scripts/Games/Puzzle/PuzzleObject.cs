using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games.Puzzle {
	public class PuzzleObject : MonoBehaviour {
        public  SoundManager  sm;
		public  BoardPosition BPosition;
		public  Transform     ConnectedParent;
		private Vector3       mOffset;
		private float         DistanceThreshold;

        /*
         * Добавить: 
         * sm.Play("Success");
         * sm.Play("Error");
         */

        private void Start() {
            sm = GameObject.FindGameObjectWithTag("Sound_Manager").GetComponent<SoundManager>();
            DistanceThreshold = 1f / PuzzleSpawner.Size;
		}


		private void OnMouseDown() {
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mOffset = transform.position - mousePos;
		}


		private void OnMouseDrag() {
			MovePuzzle();
		}


		private void OnMouseUp() {
			MovePuzzle(true);
		}


		private void MovePuzzle(bool endDrag = false) {
			// Получаем позицию мыши в мировом пространстве
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var tPos     = mousePos + mOffset;
			tPos.z = 0;

			// Если пазл присоединен к другому, то вызываем метод у родительского пазла
			if (ConnectedParent && ConnectedParent != transform) {
                var parentPuzzle = ConnectedParent.GetComponent<PuzzleObject>();
				parentPuzzle.mOffset = mOffset;
				parentPuzzle.MovePuzzle(endDrag);
			}
			else {
                
                transform.position = tPos;
				// Ищем соседние пазлы
				PuzzleObject nearPuzzle = null;

				// Получаем все соединенные пазлы с данным объектом, включая этот
				var childPuzzle =
					PuzzleController.SpawnedObjects.Where(p => p.ConnectedParent == transform || p == this).ToList();

				// У всех ищем рядом пазлы
				foreach (var cPuzzle in childPuzzle) {
					nearPuzzle = cPuzzle.CheckNearPuzzle();
					if (nearPuzzle) break;
				}

				if (!nearPuzzle) return;
				var nearPuzzleTransform = nearPuzzle.transform;

                // Соединяем с найденным пазлом
                if (ConnectedParent)
                {
                    ConnectedParent.position = nearPuzzleTransform.position;
                }
                else
                {
                    transform.position = nearPuzzleTransform.position;
                }

				if (endDrag) {
					if (ConnectedParent) {
						if (nearPuzzle.ConnectedParent) {
							foreach (var cPuzzle in childPuzzle) {
								cPuzzle.ConnectedParent  = nearPuzzle.ConnectedParent;
								cPuzzle.transform.parent = nearPuzzle.ConnectedParent;
							}
						}
						else {
							foreach (var cPuzzle in childPuzzle) {
								cPuzzle.ConnectedParent    = nearPuzzleTransform;
								cPuzzle.transform.parent   = nearPuzzleTransform;
								nearPuzzle.ConnectedParent = nearPuzzleTransform;
							}
						}
					}
					else {
						// Если у найденного пазла уже есть родитель
						if (nearPuzzle.ConnectedParent) {
							ConnectedParent  = nearPuzzle.ConnectedParent;
							transform.parent = nearPuzzle.ConnectedParent;
						}
						else {
							ConnectedParent            = nearPuzzleTransform;
							transform.parent           = nearPuzzleTransform;
							nearPuzzle.ConnectedParent = nearPuzzleTransform;
						}
					}

					PuzzleController.CheckConnections();
				}
			}
		}


		public PuzzleObject CheckNearPuzzle() {
			var tPos = transform.position;

			// Получаем остальные, не соединенные с текущими, пазлы
			var otherPuzzles =
				PuzzleController.SpawnedObjects.Where(p => p.ConnectedParent  == null ||
															p.ConnectedParent != ConnectedParent);
//			otherPuzzles.ToList().ForEach(p => print($"Other puzzle: {p.gameObject.name}"));

			foreach (var puzzleObject in otherPuzzles) {
				if (puzzleObject == this) continue;

				var pTransform = puzzleObject.transform;
				var pPos       = pTransform.position;
				var dst        = Vector2.Distance(tPos, pPos);

				// Если this слишком далеко от перебираемого экземпляра, то пропускаем его
				if (dst > DistanceThreshold) continue;

				var offset = pPos - tPos;
				offset.x = RoundOffset(offset.x);
				offset.y = RoundOffset(offset.y);
				offset.z = 0;

				// Проверка рядом ли перебираемый пазл
				if (offset == Vector3.zero && PuzzleIsNear(BPosition, puzzleObject.BPosition)) {
					return puzzleObject;
				}
			}

			return null;
		}


		private float RoundOffset(float num) {
			return Mathf.Round(num * PuzzleSpawner.Size) / PuzzleSpawner.Size;
		}


		private bool PuzzleIsNear(BoardPosition p1, BoardPosition p2) {
			var boardOffset = p2 - p1;
            return boardOffset  == BoardPosition.Up   || boardOffset == BoardPosition.Down ||
					boardOffset == BoardPosition.Left || boardOffset == BoardPosition.Right;
		}


		[Serializable]
		public struct BoardPosition {
			public int x;
			public int y;

			public static BoardPosition Left  => new BoardPosition {x = -1, y = 0};
			public static BoardPosition Right => new BoardPosition {x = 1, y  = 0};
			public static BoardPosition Up    => new BoardPosition {x = 0, y  = 1};
			public static BoardPosition Down  => new BoardPosition {x = 0, y  = -1};


			public static BoardPosition operator + (BoardPosition a, BoardPosition b) {
				return new BoardPosition {x = a.x + b.x, y = a.y + b.y};
			}


			public static BoardPosition operator - (BoardPosition a, BoardPosition b) {
				return new BoardPosition {x = a.x - b.x, y = a.y - b.y};
			}


			public static bool operator == (BoardPosition a, BoardPosition b) {
				return a.x == b.x && a.y == b.y;
			}


			public static bool operator != (BoardPosition a, BoardPosition b) {
				return a.x != b.x || a.y != b.y;
			}
		}
	}
}