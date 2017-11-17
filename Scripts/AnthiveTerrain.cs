using UnityEngine;

public class AnthiveTerrain {
	
	public TerrainCell[] cells;
	public TerrainCell cellPrefab;
	public int height;
	public int width;
	
	//Maybe remove it later
	public Text cellLabelPrefab;
	Canvas gridCanvas;

	HexMesh hexMesh;
	
	public void Generate(){
		//Maybe remove it later
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();
	
		cells = new TerrainCell[height * width];
		//z = height, x = width, i = index
		for(int i=0,z=0;z<height;z++){
			for(int x=0;x<width;x++){
				CreateCell(x,z,i++);
			}
		}
		
		//This was on Start function
		hexMesh.Triangulate(cells);
	}
	
	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		
		//Maybe remove it later
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = x.ToString() + "\n" + z.ToString();
}