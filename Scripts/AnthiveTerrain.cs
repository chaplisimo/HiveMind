using UnityEngine;
using UnityEngine.UI;

public class AnthiveTerrain : MonoBehaviour{
	
	public TerrainCell[] cells;
	public TerrainCell cellPrefab;
	public int height;
	public int width;
	
	//Maybe remove it later
	public Text cellLabelPrefab;
	Canvas gridCanvas;

	AnthiveMesh hexMesh;
	
	public void Start(){
		//Maybe remove it later
		//gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<AnthiveMesh>();
	
		cells = new TerrainCell[height * width];
		//z = height, x = width, i = index
		for(int i=0,y=0;y<height;y++){
			for(int z=0;z<width;z++){
				CreateCell(z,y,i++);
			}
		}
		
		//This was on Start function
		hexMesh.Triangulate(cells);
	}
	
	void CreateCell (int z, int y, int i) {
		Vector3 position;
		position.z = (z + y * 0.5f - y / 2) * (AnthiveMetrics.innerRadius * 2f);
		position.x = 0f;
		position.y = y * (AnthiveMetrics.outerRadius * 1.5f);

		TerrainCell cell = cells[i] = Instantiate<TerrainCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		
		//Maybe remove it later
		/*Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = x.ToString() + "\n" + z.ToString();*/
	}
}
