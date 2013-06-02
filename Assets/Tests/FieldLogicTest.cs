using UnityEngine;
using System.Collections;

public class NewBehaviourScript : UUnitTestCase {
	
	bool cellOn = true;
	bool cellOff = false;
	CellLogic.TurnOn emptyTurnOn = () => {};
	CellLogic.TurnOff emptyTurnOff = () => {};
	
	[UUnitTest]
	public void constructorShouldCreateADefinedGrid() {
		FieldLogic underTest = new FieldLogic(1,3);
		
		UUnitAssert.Equals(1, underTest.getWidth());
		UUnitAssert.Equals(3, underTest.getHeight());
		UUnitAssert.Null(underTest.CellAt(0,2));
	}
	
	
	[UUnitTest]
	public void shouldBeAbleToAddACellLogic() {
		FieldLogic underTest = new FieldLogic(5,5);
		CellLogic cell = new CellLogic(null, null, false);
		
		underTest.AddCellAt(cell, 4,4);
		
		UUnitAssert.Equals(5, underTest.getWidth());
		UUnitAssert.Equals(5, underTest.getHeight());
		UUnitAssert.Equals(cell, underTest.CellAt(4,4));
	}
	
	[UUnitTest]
	public void shouldCalculateNumberOfNeighborsOnForNoNeighbors() {
		FieldLogic underTest = new FieldLogic(1,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 0,0);
		int neighbors = underTest.NeighborsOnAt(0,0);
		
		UUnitAssert.Equals(0, neighbors);
	}
		
	[UUnitTest]
	public void shouldCalculateNumberOfNeighborsOnForAllEmptyNeighbors() {
		FieldLogic underTest = new FieldLogic(3,3);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 0,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 0,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 0,2);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 1,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 1,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 1,2);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 2,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 2,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 2,2);
		
		int neighbors = underTest.NeighborsOnAt(1,1);
		
		UUnitAssert.Equals(0, neighbors);
	}
			
	[UUnitTest]
	public void shouldCalculateNumberOfNeighborsOnForAllRightSideNeighborsOn() {
		FieldLogic underTest = new FieldLogic(3,3);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 0,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 0,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 0,2);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 1,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 1,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 1,2);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 2,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 2,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 2,2);
		
		int neighbors = underTest.NeighborsOnAt(1,1);
		
		UUnitAssert.Equals(3, neighbors);
	}
	
	[UUnitTest]
	public void shouldCalculateNumberOfNeighborsOnForWeirdCase() {
		FieldLogic underTest = new FieldLogic(3,3);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 0,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 0,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 0,2);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 1,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 1,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 1,2);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 2,0);
		underTest.AddCellAt(new CellLogic(null, null, cellOff), 2,1);
		underTest.AddCellAt(new CellLogic(null, null, cellOn), 2,2);
		
		int neighbors = underTest.NeighborsOnAt(1,1);
		
		UUnitAssert.Equals(7, neighbors);
	}
	
		
	[UUnitTest]
	public void shouldCellDieWithTooManyNeighbors() {
		FieldLogic underTest = new FieldLogic(3,3);
		bool turnOffCalled = false;
		CellLogic.TurnOff testTurnOff = () => {turnOffCalled = true;};
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 0,0);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 0,1);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 0,2);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 1,0);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOn), 1,1);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 1,2);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 2,0);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOff), 2,1);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, emptyTurnOff, cellOn), 2,2);
		
		underTest.Iterate();
		
		UUnitAssert.True(turnOffCalled);
	}
	
			
	[UUnitTest]
	public void shouldCallAllCells() {
		FieldLogic underTest = new FieldLogic(3,3);
		int timesTurnOffCalled = 0;
		CellLogic.TurnOff testTurnOff = () => {timesTurnOffCalled ++;};
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOn), 0,0);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOff), 0,1);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOn), 0,2);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOff), 1,0);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOff), 1,1);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOff	), 1,2);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOn), 2,0);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOff), 2,1);
		underTest.AddCellAt(new CellLogic(emptyTurnOn, testTurnOff, cellOn), 2,2);
		
		underTest.Iterate();
		
		UUnitAssert.Equals(9, timesTurnOffCalled);
	}
}
