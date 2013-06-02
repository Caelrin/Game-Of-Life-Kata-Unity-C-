using System;
using UnityEngine;
using Rhino.Mocks;

public class CellLogicTest : UUnitTestCase
{
	[UUnitTest]
	public void TestTurnOnForNeighboorsWhenAlreadyOn ()
	{
		Boolean turnOnCalled = false;
		CellLogic.TurnOn turnOn = (() => {turnOnCalled = true;});
		
		CellLogic underTest = new CellLogic(turnOn, null, true);
		
		underTest.checkForNeighbors(2);
		
		UUnitAssert.True(turnOnCalled);
	}
	
	[UUnitTest]
	public void TestTurnOffForOneNeighboor ()
	{
		Boolean turnOffCalled = false;
		CellLogic.TurnOff turnOff = (() => {turnOffCalled = true;});
		
		CellLogic underTest = new CellLogic(null, turnOff, false);
		
		underTest.checkForNeighbors(1);
		
		UUnitAssert.True(turnOffCalled);
	}
	
	[UUnitTest]
	public void TestTurnOnForThreeNeighboorsWhenOff ()
	{
		Boolean turnOnCalled = false;
		CellLogic.TurnOn turnOn = (() => {turnOnCalled = true;});
		
		CellLogic underTest = new CellLogic(turnOn, null, false);
		
		underTest.checkForNeighbors(3);
		
		UUnitAssert.True(turnOnCalled);
	}
	
	[UUnitTest]
	public void TestTurnOffFor2NeighborsWhenNotAlreadyOn ()
	{
		Boolean turnOffCalled = false;
		CellLogic.TurnOff turnOff = (() => {turnOffCalled = true;});
		
		CellLogic underTest = new CellLogic(null, turnOff, false);
		
		underTest.checkForNeighbors(2);
		
		UUnitAssert.True(turnOffCalled);
	}
	
	[UUnitTest]
	public void TestIsOnShouldReturnDelegate() {
		
		CellLogic underTest = new CellLogic(null, null, true);
		
		UUnitAssert.True(underTest.isOn());
		
	}
		
	
}

