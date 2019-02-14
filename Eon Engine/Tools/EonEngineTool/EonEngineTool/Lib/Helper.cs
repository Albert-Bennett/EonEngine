using System;

namespace EonEngineTool.Lib
{
    /// <summary>
    /// Used to help with getting file extentions
    /// </summary>
    public static class Helper
    {
        static readonly string defaultDir = Environment.CurrentDirectory + "/Content/";
        static string contentDir = "";

        public static readonly string BuildingContentFilepath =
            Environment.CurrentDirectory + "/ContentBuild/";

        public static string BuiltContentDirectory
        {
            get
            {
                if (contentDir == "")
                    return defaultDir;
                else
                    return contentDir;
            }
            set { contentDir = value; }
        }

        /// <summary>
        /// Gets the filepath for a content asset.
        /// </summary>
        /// <param name="filepath">The asset's actual filepath.</param>
        /// <param name="exclude">The end of the exclusion path.</param>
        /// <returns></returns>
        public static string GetFilePath(string filepath, string exclude)
        {
            string[] txt = filepath.Split(new char[]
            {
                '.'
            }, StringSplitOptions.RemoveEmptyEntries);

            string res = txt[0];
            string[] final = res.Split(new string[]
            {
                exclude
            }, StringSplitOptions.RemoveEmptyEntries);

            return final[1];
        }

        /// <summary>
        /// Gets the name of a file that their is a filepath for.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns>The name of the file.</returns>
        public static string GetFileName(string filepath)
        {
            string[] s = filepath.Split(new char[]
            {
                '\\','.'
            }, StringSplitOptions.RemoveEmptyEntries);

            return s[s.Length - 2];
        }

        public static readonly string InitialDirectory = @"C:\";

        public static readonly string[] FileExtentions = new string[]
        {
            ".Png",".Jpg",".Tga",
            ".Part2D",".Part3D",
            ".Ani2D",".Skel",".SkelAni",
            ".Ani3D",".Animatic",
            ".Tile",".Level",
            ".Pref",
            ".Shader", ".JJAX"
        };

        public static string[] ImageExtentions
        {
            get
            {
                return new string[]
                    {
                        FileExtentions[0],
                        FileExtentions[1],
                        FileExtentions[2]
                    };
            }
        }

        public static string Particle2DExtention { get { return FileExtentions[3]; } }
        public static string Particle3DExtention { get { return FileExtentions[4]; } }
        public static string SpriteSheetExtention { get { return FileExtentions[5]; } }
        public static string Skeleton2DExtention { get { return FileExtentions[6]; } }
        public static string Skeletal2DAnimationExtention { get { return FileExtentions[7]; } }
        public static string Animation3DExtention { get { return FileExtentions[8]; } }
        public static string AnimaticExtention { get { return FileExtentions[9]; } }
        public static string TileMapExtenion { get { return FileExtentions[10]; } }
        public static string LevelExtention { get { return FileExtentions[11]; } }
        public static string UserPrefrencesExtention { get { return FileExtentions[12]; } }
        public static string ShaderExtention { get { return FileExtentions[13]; } }
        public static string JJaxExtention { get { return FileExtentions[14]; } }

        public static readonly string MenuManagerFilename = "MenuSystem.Menu";
        public static readonly string LevelManagerFileName = "LevelManager.LevelManager";
        public static readonly string EngineManagerFilename = "Eon.Engine";

        public static readonly string Render2DManagerAssembly = "Eon.Rendering2D.Framework.Framework";
        public static readonly string MenuManagerAssembly = "Eon.UIApi.MenuManager";
        public static readonly string Physics2DAssembly = "Eon.Physics2D.PhysicsD2Framework";
        public static readonly string LevelManagerAssembly = "Eon.Game.LevelManagement.LevelManager";
        public static readonly string AnimaticManagerAssembly = "Eon.AnimaticSystem.AnimaticManager";

        public static readonly string Physics2DAssemblyRef = "Eon.Physics2D";
        public static readonly string Render2DAssemblyRef = "Eon.Rendering2D";
        public static readonly string UIApiAssemblyRef = "Eon.UIApi";
        public static readonly string Game2DAssemblyRef = "Eon.Game2D";
        public static readonly string AnimaticAssemblyRef = "Eon.AnimaticSystem";
        public static readonly string Animation2DAssemblyRef = "Eon.Animation2D";
        public static readonly string GameAssemblyRef = "Eon.Game";
        public static readonly string ParticleAssemblyRef = "Eon.Particles";

        public static readonly string[] Emitter2DTypes = new string[]
        {
            "PointEmitter2D","LinearEmitter2D",
            "CircularEmitter","RectangularEmitter"
        };

        public static readonly string[] Cycle2DTypes = new string[]
        {
           "IntervalCycle","RandomCycle"
        };

        public static readonly string[] Particle2DRenderTypes = new string[]
        {
            "SpriteRenderer","RandomTextureRenderer",
            "AnimatedSpriteRenderer"
        };

        public static readonly string[] Particle2DAttachments = new string[]
        {
            "ColourRandomAttachment",
            "ScaleAttachment", "ScaleDecayAttachment",
            "RotationAttachment","RotationalAttachment",
            "ColourAttachment","ColourDecayAttachment" 
        };
    }
}
