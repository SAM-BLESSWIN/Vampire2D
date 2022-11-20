using UnityEngine;

public static class CameraExtension 
{
    public static void LayerCullingShow(this Camera cam, int layerMask)
    {
        cam.cullingMask |= layerMask;  //add a layer
    }

    public static void LayerCullingHide(this Camera cam, int layerMask)
    {
        cam.cullingMask &= ~layerMask; //remove a layer
    }
}
