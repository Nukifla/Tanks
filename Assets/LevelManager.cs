using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	struct Wall {
		public Vector3 start;
		public Vector3 end;
		public Wall(Vector3 _start, Vector3 _end) {
			start = _start;
			end = _end;
		}
	}

	Wall makeWall(int x1, int y1, int x2, int y2) {
		return new Wall (new Vector3 (x1, y1, 0), new Vector3 (x2, y2, 0));
	}

	struct Level {
		public Vector3 tank1Pos;
		public Vector3 tank2Pos;
		public List<Wall> walls;
	}

	static List<Level> levels = new List<Level> ();
	public GameObject wallObjectTrix;
	static GameObject wallObject;
	static float wallWidth = 5.0f / 100.0f;

	// Use this for initialization
	void Start () {
		wallObject = wallObjectTrix;
		Level level = new Level () { 
			tank1Pos = new Vector3 (0, 0, 0),
			tank2Pos = new Vector3 (0, 0, 0),
			walls = new List<Wall> () {
				makeWall (0, 0, 7, 0),
				makeWall (1, 1, 2, 1),
				makeWall (5, 1, 7, 1),
				makeWall (1, 2, 2, 2),
				makeWall (3, 2, 4, 2),
				makeWall (5, 2, 6, 2),
				makeWall (3, 3, 4, 3),
				makeWall (5, 3, 7, 3),
				makeWall (0, 4, 1, 4),
				makeWall (6, 4, 7, 4),
				makeWall (1, 5, 4, 5),
				makeWall (5, 5, 6, 5),
				makeWall (1, 6, 3, 6),
				makeWall (4, 6, 6, 6),
				makeWall (7, 7, 0, 7),

				makeWall (0, 0, 0, 7),
				makeWall (1, 0, 1, 2),
				makeWall (1, 3, 1, 4),
				makeWall (1, 5, 1, 6),
				makeWall (2, 2, 2, 3),
				makeWall (2, 4, 2, 5),
				makeWall (2, 6, 2, 7),
				makeWall (3, 0, 3, 1),
				makeWall (3, 4, 3, 5),
				makeWall (4, 0, 4, 2),
				makeWall (4, 3, 4, 4),
				makeWall (4, 6, 4, 7),
				makeWall (5, 2, 5, 3),
				makeWall (5, 4, 5, 5),
				makeWall (6, 5, 6, 6),
				makeWall (7, 0, 7, 7),
			}
		};

		LoadLevel (level);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static void LoadLevel(Level level) {
		ClearLevel ();
		float maxX = 0f, maxY = 0f;
		foreach (Wall w in level.walls) {
			maxX = w.start.x > maxX ? w.start.x : maxX;
			maxY = w.start.y > maxY ? w.start.y : maxY;
			maxX = w.end.x > maxX ? w.end.x : maxX;
			maxY = w.end.y > maxY ? w.end.y : maxY;
		}
		foreach (Wall w in level.walls) {
			PlaceWall (w, new Vector3 (-maxX / 2,-maxY / 2, 0));
		}

	}

	private static void ClearLevel() {
	}

	private static void PlaceWall(Wall w, Vector3 origin) {
		Quaternion rotation = Quaternion.AngleAxis(Vector3.Angle(new Vector3(1,0,0), w.end-w.start), new Vector3(0, 0, 1));
		Vector3 pos = w.start + ((w.end-w.start) / 2) + origin;
		GameObject placed = Instantiate (wallObject, pos, rotation);
		placed.transform.localScale = new Vector3 (Vector3.Distance (w.start, w.end) / wallWidth, 1, 1);
	}
}
