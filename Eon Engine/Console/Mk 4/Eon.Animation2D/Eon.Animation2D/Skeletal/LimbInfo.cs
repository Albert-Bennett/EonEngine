/* Created 12/05/2015
 * Last Updated: 10/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define a Limb.
    /// </summary>
    public class LimbInfo
    {
        public string Name;
        public string ParentLimb;

        public int DrawLayer = 0;

        public string TextureFilepath;
        public string NormalMapFilepath = "Eon/Textures/DefaultTexture";
        public string DistortionMapFilepath = "Eon/Textures/DefaultTexture";

        public float RotationX;
        public float RotationY;

        public float OffsetX;
        public float OffsetY;

        public float SizeX;
        public float SizeY;

        public Transformation Transform;

        public int R = 255;
        public int G = 255;
        public int B = 255;
        public int A = 255;

        public AnimateData AnimateData = null;
    }
}
