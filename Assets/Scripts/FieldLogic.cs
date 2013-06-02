using UnityEngine;
using System;
using System.Collections;

public class FieldLogic {
	
	private CellLogic[,] cells;
	
	public FieldLogic(int width, int height) {
		cells = new CellLogic[width, height];
	}
	
	public int getWidth() {
		return cells.GetUpperBound(0) + 1;
	}
	
	public int getHeight() {
		return cells.GetUpperBound(1) + 1;
	}
	
	public void AddCellAt(CellLogic cell, int x, int y) {
		cells[x,y] = cell;
	}
	
	public CellLogic CellAt(int x, int y) {
		return cells[x,y];
	}
	
	public int NeighborsOnAt(int x, int y) {
		int neighbors = 0;
		
		for(int testingX = x-1; testingX <= x + 1; testingX++) {
			for(int testingY = y -1; testingY <= y + 1; testingY++) {
				if(ValidXNeighbor (testingX) 
					&& ValidYNeighbor (testingY)
					&& NotTestingSelf (x, y, testingX, testingY)
					&& cells[testingX, testingY].isOn()) {
					neighbors ++;
				}
			}
		}
		return neighbors;
	}
	
	public void Iterate() {
		CellLogic[,] newCellBoard = new CellLogic[getWidth (), getHeight()];
		for(int i = 0; i < getWidth(); i ++) {
			for(int j = 0; j < getHeight(); j ++ ) {
				newCellBoard[i,j] = cells[i,j].checkForNeighbors(NeighborsOnAt(i,j));
			}
		}
		cells = newCellBoard;
	}

	bool ValidXNeighbor (int testingX)
	{
		return testingX >=0 && testingX < getWidth();
	}

	bool ValidYNeighbor (int testingY)
	{
		return testingY >= 0 && testingY < getHeight();
	}

	bool NotTestingSelf (int x, int y, int testingX, int testingY)
	{
		return testingX != x || testingY != y;
	}
}
