using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class InverseMask : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material result = base.materialForRendering;
            result.SetInt("_StencilComp", (int)CompareFunction.Equal);
            return result;
        }
    }
}
