using System;

[Serializable]
public class BlendShape {

    public string name { get; set; }
    public int positiveIndex { get; set; }
    public int negativeIndex { get; set; }

    public BlendShape (int positiveIndex, int negatuveIndex)
    {
        this.positiveIndex = positiveIndex;
        this.negativeIndex = negatuveIndex;
    }

}
