using UnityEngine;

public class AntTerrain {
	
	public TerrainCell[] cells;
	public int height;
	public int width;
	public float cellRadius = 5;
	
	public void Generate(){
		cells = new TerrainCell[height * width];
		for(int i=0;i<height;i++){
			for(int j=0;j<width;j++){
				cells[i*width+j] = new TerrainCell(i,j,cellRadius);
			}
		}
	}
}
