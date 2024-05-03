using UnityEngine;

public class Choice
{
    private int index;
    private int clockwise;

    public Choice(int index, int clockwise)
    {
        this.index = index;
        this.clockwise = clockwise;
    }

    public Choice()
    {
        this.index = 0;
        this.clockwise = 0;
    }

    public void setChoice(int index, int clockwise)
    {
        this.index = index;
        this.clockwise = clockwise;
    }

    public int getIndex()
    {
        return this.index;
    }

    public int getClockwise()
    {
        return this.clockwise;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public void setClockwise(int clockwise)
    {
        this.clockwise = clockwise;
    }
}