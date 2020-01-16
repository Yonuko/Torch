using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterCustomization : Singleton<CharacterCustomization> {

    public SkinnedMeshRenderer target;
    public SkinnedMeshRenderer eyeMesh;

    public string suffixMax = "Max", suffixMin = "Min";

    private CharacterCustomization() { }

    private SkinnedMeshRenderer skinMeshRenderer;
    private Mesh mesh;

    public Dictionary<string, BlendShape> blendShapeDataBase = new Dictionary<string, BlendShape>();

    void Start()
    {
        Initalize();
    }

    #region Pubic Functions

    public void ChangeBlendShapeValue(string blendShapeName, float value)
    {
        if (!blendShapeDataBase.ContainsKey(blendShapeName))
        {
            Debug.LogError("BlendShape " + blendShapeName + " does not exist !");
            return;
        }

        value = Mathf.Clamp(value, -100,100);

        BlendShape blendshape = blendShapeDataBase[blendShapeName];

        if (blendShapeName == "Taille")
        {
            if (value >= 0)
            {
                if (blendshape.positiveIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.positiveIndex, value);
                eyeMesh.SetBlendShapeWeight(0, value);
                if (blendshape.negativeIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.negativeIndex, 0);
                eyeMesh.SetBlendShapeWeight(1, 0);
            }
            else
            {
                if (blendshape.negativeIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                eyeMesh.SetBlendShapeWeight(1, -value);
                if (blendshape.positiveIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.positiveIndex, 0);
                eyeMesh.SetBlendShapeWeight(0, 0);
            }
        }
        else
        {
            if (value >= 0)
            {
                if (blendshape.positiveIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.positiveIndex, value);
                if (blendshape.negativeIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.negativeIndex, 0);
            }
            else
            {
                if (blendshape.negativeIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                if (blendshape.positiveIndex == -1)
                {
                    return;
                }
                skinMeshRenderer.SetBlendShapeWeight(blendshape.positiveIndex, 0);
            }
        }
    }

    #endregion

    #region Private Functions

    private void Initalize()
    {
        skinMeshRenderer = target;
        mesh = skinMeshRenderer.sharedMesh;

        ParseBlendShapesToDataBase();
    }

    private void ParseBlendShapesToDataBase()
    {
        List<string> blendshapeNames = Enumerable.Range(0,mesh.blendShapeCount).Select(x => mesh.GetBlendShapeName(x)).ToList();

        for (int i = 0; blendshapeNames.Count > 0;)
        {
            string altSuffix, noSuffix;
            noSuffix = blendshapeNames[i].TrimEnd(suffixMax.ToCharArray()).TrimEnd(suffixMin.ToCharArray()).Trim();

            string positiveName = string.Empty, negativeName = string.Empty;
            bool exists = false;

            int positiveIndex = -1, negativeIndex = -1;

            if (blendshapeNames[i].EndsWith(suffixMax))
            {
                altSuffix = noSuffix + " " + suffixMin;

                positiveName = blendshapeNames[i];
                negativeName = altSuffix;

                if (blendshapeNames.Contains(altSuffix))
                {
                    exists = true;
                }

                positiveIndex = mesh.GetBlendShapeIndex(positiveName);

                if (exists)
                {
                    negativeIndex = mesh.GetBlendShapeIndex(altSuffix);
                }
            }
            else if (blendshapeNames[i].EndsWith(suffixMin)) {
                altSuffix = noSuffix + " " + suffixMax;

                negativeName = blendshapeNames[i];
                positiveName = altSuffix;

                if (blendshapeNames.Contains(altSuffix))
                {
                    exists = true;
                }

                negativeIndex = mesh.GetBlendShapeIndex(negativeName);

                if (exists)
                {
                    positiveIndex = mesh.GetBlendShapeIndex(altSuffix);
                }
            }
            else
            {
                positiveIndex = mesh.GetBlendShapeIndex(blendshapeNames[i]);
            }

            blendShapeDataBase.Add(noSuffix, new BlendShape(positiveIndex, negativeIndex));

            //Retir l'élements selectionner de la liste
            if (positiveName != string.Empty)
            {
                blendshapeNames.Remove(positiveName);
            }
            if (negativeName != string.Empty)
            {
                blendshapeNames.Remove(negativeName);
            }

        }
    }

    #endregion

}
