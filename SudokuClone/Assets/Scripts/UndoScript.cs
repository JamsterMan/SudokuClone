using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoScript
{
    private readonly Stack<UndoTiles> commandsList;
    private readonly int maxSizeStack;
    private UndoTiles emptyList;

    //consturctor
    public UndoScript()
    {
        maxSizeStack = 50;
        commandsList = new Stack<UndoTiles>();
        emptyList.row = -1;
        emptyList.col = -1;
    }

    //adds a player command to the stack
    public void AddCommand(UndoTiles undo)
    {
        if(commandsList.Count > maxSizeStack)
            RemoveOldestValue();
        commandsList.Push(undo);
    }

    //pops last command off the stack
    public UndoTiles GetLastCommand()
    {
        if (commandsList.Count != 0)
            return commandsList.Pop();
        else
            return emptyList;
    }

    //removes oldest value in the stack (keep the stack from getting too large)
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
