using UnityEngine;
using System.Collections;
using System;

public class MapGenerator : MonoBehaviour {

	public int width;
	public int height;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	int[,] map, msqrs;
	
	public Sprite[] marchSquares;
	public float resolution = 1;
	public GameObject prefab ;

	void Start() {
		GenerateMap();
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			for(int i = 0;i<transform.childCount;i++){
				Destroy(transform.GetChild(i).gameObject);
			}
			GenerateMap();
		}
	}

	void GenerateMap() {
		map = new int[width,height];
		msqrs = new int[width,height];
		RandomFillMap();

		//for (int i = 0; i < 5; i ++) {
			SmoothMap();
		//}
		MarchSquares();
		TriangleMeshes();
	}


	void RandomFillMap() {
		if (useRandomSeed) {
			seed = Time.time.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (x == 0 || x == width-1 || y == 0 || y == height -1) {
					map[x,y] = 1;
				}
				else {
					map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
				}
			}
		}
	}

	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

	void TriangleMeshes(){
		for(int i=0; i<width-1; i++){
			for(int j=0; j<height-1; j++){
				GameObject p = Instantiate(prefab ,transform.position  +  new Vector3(resolution * i,resolution * j,0),transform.rotation);
				switch (msqrs[i,j]){
					case 0 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[0];break;}
					case 1 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[1];break;}
					case 2 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[2];break;}
					case 3 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[3];break;}
					case 4 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[4];break;}
					case 5 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[5];break;}
					case 6 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[6];break;}
					case 7 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[7];break;}
					case 8 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[8];break;}
					case 9 :  {p.GetComponent<SpriteRenderer>().sprite = marchSquares[9];break;}
					case 10 : {p.GetComponent<SpriteRenderer>().sprite = marchSquares[10];break;}
					case 11 : {p.GetComponent<SpriteRenderer>().sprite = marchSquares[11];break;}
					case 12 : {p.GetComponent<SpriteRenderer>().sprite = marchSquares[12];break;}
					case 13 : {p.GetComponent<SpriteRenderer>().sprite = marchSquares[13];break;}
					case 14 : {p.GetComponent<SpriteRenderer>().sprite = marchSquares[14];break;}
					case 15 : {p.GetComponent<SpriteRenderer>().sprite = marchSquares[15];break;}
					default : {Debug.Log("Not matched");break;}
				} 
				p.transform.parent = this.transform;
			}
		}
	}

	/*void OnDrawGizmos() {
		if (map != null) {
			for (int x = 0; x < width; x ++) {
				for (int y = 0; y < height; y ++) {
					Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
					Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
					Gizmos.DrawCube(pos,Vector3.one);
				}
			}
		}
	}*/
	
	public void MarchSquares(){
		for(int i=0; i<width-1; i++){
			for(int j=0; j<height-1; j++){
				msqrs[i,j] = GetBinaryWalls(i,j);
			}
		}
	}

	int GetBinaryWalls(int x, int y){
		byte binary = 0x0; //0000

		for (int neighbourX = x ; neighbourX <= x + 1; neighbourX ++) {
			for (int neighbourY = y ; neighbourY <= y + 1; neighbourY ++) {
				if(neighbourX == x && neighbourY == y && map[neighbourX,neighbourY]==1 ){
					binary |= 0x1;
				}else if(neighbourX > x && neighbourY == y && map[neighbourX,neighbourY]==1 ){
					binary |= 0x2;
				}else if(neighbourX > x && neighbourY > y && map[neighbourX,neighbourY]==1 ){
					binary |= 0x4;
				}else if(neighbourX == x && neighbourY > y && map[neighbourX,neighbourY]==1 ){
					binary |= 0x8;
				}
			}
		}
		return binary;
	}

}
