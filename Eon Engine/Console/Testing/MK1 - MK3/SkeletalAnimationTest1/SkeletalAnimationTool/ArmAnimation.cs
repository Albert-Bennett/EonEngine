using Eon;
using Eon.Animation2D.Skeletal;
using Eon.Animation2D.Skeletal.Animating;
using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace SkeletalAnimationTool
{
    public class ArmAnimation : GameObject
    {
        SkeletonInfo info;
        SkeletalAnimation ani;
        SkeletalAnimationPlayer player;

        public ArmAnimation() : base("Arm") { }

        public override void Initialize()
        {
            World.Size = new Vector3(0.5f);

            Limb[] limbs = new Limb[]
            {
                new Limb()
                {
                    ParentLimb = "",
                    Name = "Piston",
                    OffsetX = 0,
                    OffsetY = 0,
                    RotationX = 106,
                    RotationY = 0,
                    SizeX = 212,
                    SizeY = 142,
                    TextureFilepath = "Cog/Piston",
                    Transform = new Transformation()
                    {
                        ScaleX = 1,
                        ScaleY = 1,
                        X = 700,
                        Y = 0,
                        Rotation = 0
                    },
                    Order = 2
                },

                new Limb()
                {
                    ParentLimb = "Piston",
                    TextureFilepath = "Cog/FeederArm",
                    OffsetX = 0,
                    OffsetY = 0,
                    Name = "Feeder",
                    RotationX = 57,
                    RotationY = 221,
                    SizeX = 114,
                    SizeY = 442,
                    Transform = new Transformation()
                    {
                        Rotation = 0,
                        ScaleX = 1,
                        ScaleY = 1,
                        X = 0,
                        Y = -31
                    },
                    Order = 0
                },

                new Limb()
                {
                    ParentLimb = "Piston",
                    Name = "Spoke",
                    OffsetX = 0,
                    OffsetY = 0,
                    TextureFilepath = "Cog/Spoke",
                    SizeX = 40,
                    SizeY = 468,
                    Order = 1,
                    RotationX = 20,
                    RotationY = 234,
                    Transform = new Transformation()
                    {
                        ScaleX = 1,
                        ScaleY = 1,
                        X = 0,
                        Y = -80
                    }
                },

                new Limb()
                {
                    ParentLimb = "Feeder",
                    SizeX = 106,
                    SizeY = 56,
                    OffsetX = 0,
                    OffsetY = 0,
                    Name = "HolderArmRight",
                    RotationX = 10,
                    RotationY = 10,
                    TextureFilepath = "Cog/HolderArmRight",
                    Transform = new Transformation()
                    {
                        X = 47,
                        Y = 211,
                        ScaleX = 1,
                        ScaleY = 1,
                        Rotation = 0
                    },
                    Order = 1
                },

                new Limb()
                {
                    ParentLimb = "Feeder",
                    SizeX = 106,
                    SizeY = 56,
                    OffsetX = 0,
                    OffsetY = 0,
                    Name = "HolderArmLeft",
                    RotationX = 97,
                    RotationY = 10,
                    TextureFilepath = "Cog/HolderArmLeft",
                    Transform = new Transformation()
                    {
                        X = -47,
                        Y = 211,
                        ScaleX = 1,
                        ScaleY = 1,
                        Rotation = 0
                    },
                    Order = 1
                }
            };

            info = new SkeletonInfo
            {
                R = 255,
                G = 255,
                B = 255,
                A = 255,
                PostRender = false,
                DrawLayer = 0,
                Limbs = limbs
            };

            Skeleton skeleton = new Skeleton("Skeleton", info);

            AttachComponent(skeleton);

            player = new SkeletalAnimationPlayer(ID + "Player", skeleton);

            ani = new SkeletalAnimation()
            {
                BlendRate = 5,
                Name = "FeederArm_Move",
                FrameRate = 5,

                LimbAnimations = new LimbKeyFrameCollection[]
                {
                    new LimbKeyFrameCollection
                    {
                        LimbName = "Feeder",

                        LimbFrames = new LimbKeyFrame[]
                        {
                            new LimbKeyFrame
                            {
                                FrameNumber = 0,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = 0
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 450,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 370,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1490,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 370,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1940,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            }
                        }
                    },

                    new LimbKeyFrameCollection
                    {
                        LimbName = "Spoke",

                        LimbFrames = new LimbKeyFrame[]
                        {
                            new LimbKeyFrame
                            {
                                FrameNumber = 0,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = 0
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 975,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = 0
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1020,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 520,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            }
                        }
                    },

                    new LimbKeyFrameCollection
                    {
                        LimbName = "HolderArmRight",

                        LimbFrames = new LimbKeyFrame[]
                        {
                            new LimbKeyFrame
                            {
                                FrameNumber = 0,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = 0
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 450,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = MathHelper.ToRadians(-5)
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 460,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-15),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 470,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(10),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 480,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-5),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 900,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(90),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1070,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(90),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1490,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1940,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-7),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1950,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(10),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1960,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-5),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1970,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            }
                        }
                    },

                    new LimbKeyFrameCollection
                    {
                        LimbName = "HolderArmLeft",

                        LimbFrames = new LimbKeyFrame[]
                        {
                            new LimbKeyFrame
                            {
                                FrameNumber = 0,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = 0
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 450,
                                Transform = new Transformation()
                                {
                                    Y = 0,
                                    X = 0,
                                    ScaleX = 1,
                                    ScaleY = 1,
                                    Rotation = MathHelper.ToRadians(5)
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 460,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(15),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 470,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-10),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 480,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(5),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 900,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-90),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1070,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-90),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1490,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1940,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(7),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1950,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(-10),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1960,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = MathHelper.ToRadians(5),
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            },

                            new LimbKeyFrame
                            {
                                FrameNumber = 1970,
                                Transform = new Transformation()
                                {
                                    X = 0,
                                    Y = 0,
                                    Rotation = 0,
                                    ScaleY = 1,
                                    ScaleX = 1
                                }
                            }
                        }
                    }

                 }
            };

            AttachComponent(player);

            base.Initialize();
        }

        public void PlayAnimation()
        {
            player.AddAnimation(ani);
            player.PlayAnimation("FeederArm_Move");
        }

        public void Save()
        {
            Type[] extraTypes = new Type[]
            {
                 typeof(Transformation),
                 typeof(Limb[]),
                 typeof(int),
                 typeof(string),
                 typeof(float)
            };

            XmlHelper.Serialize<SkeletonInfo>(info, "FeederArm.Skel", extraTypes);

            Type[] types = new Type[]
            {
                typeof(LimbKeyFrameCollection[]),
                typeof(LimbKeyFrame[]),
                typeof(Transformation),
                typeof(string),
                typeof(float),
                typeof(int)
            };

            XmlHelper.Serialize<SkeletalAnimation>(ani, "FeederArm_Move.SkelAni", types);
        }
    }
}
