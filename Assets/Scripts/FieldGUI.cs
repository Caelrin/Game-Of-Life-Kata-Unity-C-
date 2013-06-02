using UnityEngine;
using System.Collections;

public class FieldGUI : MonoBehaviour {
	
	public GUITexture baseBlock;
	public Texture onTexture;
	public Texture offTexture;
	private FieldLogic fieldLogic;
	private float lastIterateTime;
	public int fieldWidth;
	public int fieldHeight;
	
	
	// Use this for initialization
	void Start () {
		lastIterateTime = Time.time;
		fieldLogic = new FieldLogic(fieldWidth, fieldHeight);
		for(int i =0; i< fieldWidth; i++) {
			for(int j = 0; j< fieldHeight; j++) {
				AddCellAt (i, j);
			}
		}
	
	}

	void AddCellAt (int i, int j)
	{
		GUITexture texture = (GUITexture)Instantiate(baseBlock);
		texture.pixelInset = new Rect(i * 50, j* 50, 50, 50);
		CellLogic.TurnOn turnOn = (() => {texture.texture = onTexture;});
		CellLogic.TurnOff turnOff = () => {texture.texture = offTexture;};
		
		bool isOn = false;
		if(Random.Range(0,2) == 0) {
			texture.texture = onTexture;
			isOn = true;
		} else {
			texture.texture = offTexture;
		}
		fieldLogic.AddCellAt(new CellLogic(turnOn, turnOff, isOn), i, j);
	}
	
	// Update is called once per frame
	void Update () {
		if((Time.time - lastIterateTime) > .5) {
			lastIterateTime = Time.time;
			fieldLogic.Iterate();
		}
	}
}
