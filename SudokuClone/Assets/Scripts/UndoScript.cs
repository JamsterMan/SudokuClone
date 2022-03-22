using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoScript
{
    private Stack<UndoTiles> commandsList;
    private readonly int maxSizeStack;
    private UndoTiles emptyList;

    public UndoScript()
    {
        maxSizeStack = 50;
        commandsList = new Stack<UndoTiles>();
        emptyList.row = -1;
        emptyList.col = -1;
    }

    public void AddCommand(UndoTiles undo)
    {
        if(commandsList.Count > maxSizeStack)
            RemoveOldestValue();
        commandsList.Push(undo);
    }

    public UndoTiles GetLastCommand()
    {
        if (commandsList.Count != 0)
            return commandsList.Pop();
        else
            return emptyList;
    }

    private void RemoveOldestValue()
    {
        Stack<UndoTiles> tempList = new Stack<UndoTiles>();

        for (int i = 0; i < commandsList.Count; i++)//get oldest value to the top of tempList
        {
            tempList.Push(commandsList.Pop());
        }
        tempList.Pop();//remove oldest value
        for (int i = 0; i < commandsList.Count; i++)//put valuse back in order in commands list
        {
            commandsList.Push(tempList.Pop());
        }
    }
}
