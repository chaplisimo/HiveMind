using UnityEngine:

public class Terrain {
	
	public TerrainCell[] cells;
	public int height;
	public int width;
	public float cellRadius = 5;
	
	public Generate(){
		cells = new cells[height * width];
		for(int i=0;i<height;i++){
			for(int j=0;j<width;j++){
				cells[i*width+j] = new TerrainCell(i,j,cellRadius);
			}
		}
	}
}
