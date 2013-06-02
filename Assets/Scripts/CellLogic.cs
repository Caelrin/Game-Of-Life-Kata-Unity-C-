using UnityEngine;
using System.Collections;

public class CellLogic {
	
	public delegate void TurnOn();
	public delegate void TurnOff();

	
	private TurnOn turnOn;
	private TurnOff turnOff;
	private bool cellOn;
	
	public bool isOn() {
		return cellOn;
	}
	
	public CellLogic(TurnOn turnOn, TurnOff turnOff, bool cellOn) {
		this.turnOn = turnOn;
		this.turnOff = turnOff;
		this.cellOn = cellOn;
	}
	
	public CellLogic checkForNeighbors(int numberOfNeighbors) {
		bool newIsOn = false;
		if((numberOfNeighbors == 2 && isOn()) || numberOfNeighbors == 3) {
			turnOn();
			newIsOn = true;
		} else {
			turnOff();
			newIsOn = false;
		}
		return new CellLogic(turnOn, turnOff, newIsOn);
	}
}
