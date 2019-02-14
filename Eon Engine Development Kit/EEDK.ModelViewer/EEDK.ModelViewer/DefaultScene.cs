/* Created: 05/01/2015
 * Last Updated: 24/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using EEDK.Gui;
using EEDK.Gui.ModelViewer;
using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering2D.Text;
using Eon.Rendering3D;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.Rendering3D.Framework.Shaders;
using Eon.System.Management;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace EEDK.ModelViewer
{
    /// <summary>
    /// Defines the default scene for viewing models.
    /// </summary>
    public sealed class DefaultScene : GameObject
    {
        List<string> coppiedFiles = new List<string>();
        EonDictionary<string, string> materials;
        string coppiedDirectory = "";

        Crosswalk.Message message;

        DirectionalLight direct;
        DirectionalLight direct1;
        DirectionalLight direct2;

        PointLight pl;
        ChaseCamera camera;

        ModelInfo shaders;
        ModelComponent mdl;
        TextItem txt;

        List<TextItem> texts = new List<TextItem>();

        string mdlName = "";
        bool loadedModel = false;

        public DefaultScene(Crosswalk.Message message)
            : base("DefaultScene")
        {
            this.message = message;

            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(ObjectListing),
                    typeof(FrameworkCreation)
                };

                ProjectFile project =
                    SerializationHelper.Deserialize<ProjectFile>(
                    message.Messages[2], false, "", extraTypes);

                materials = project.CreatableObjects.Shaders;
            }
            catch
            {
                materials = new EonDictionary<string, string>();

                materials.Add("Eon.Rendering3D.Framework.Shaders.AnimatedTextureMaterial", "Eon.Rendering3D.dll");
                materials.Add("Eon.Rendering3D.Framework.Shaders.BasicLPPMaterial", "Eon.Rendering3D.dll");
                materials.Add("Eon.Rendering3D.Framework.Shaders.BasicMaterial", "Eon.Rendering3D.dll");
                materials.Add("Eon.Rendering3D.Framework.Shaders.DTLPPMaterial", "Eon.Rendering3D.dll");
            }
        }

        protected override void Initialize()
        {
            SetUpScene();

            if (message != null)
                mdlName = LoadModelData(message.Messages[0] + message.Messages[1]);

            base.Initialize();

            if (message != null)
            {
                ShowModelData(mdlName);
                camera.TargetOffSet = mdl.Origin;
            }

            MdlViewerCommon.Common.OnLoad += new LoadRequestEvent(LoadModel);
            MdlViewerCommon.Common.OnSave += new SaveRequestEvent(SaveModel);
        }

        void SaveModel(string filepath)
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(EonDictionary<string, Shader>));
            extraTypes.Add(typeof(EonDictionary<int, LODModelInfo>));

            for (int i = 0; i < materials.Count; i++)
                extraTypes.Add(AssemblyManager.GetType(
                    materials.Values[i], materials.Keys[i]));

            SerializationHelper.Serialize<ModelInfo>(shaders, filepath, extraTypes.ToArray());
        }

        void LoadModel(string filepath)
        {
            if (File.Exists(filepath))
            {
                if (mdl != null)
                    mdl.Destroy(true);

                mdlName = LoadModelData(filepath);

                loadedModel = true;
            }
        }

        string LoadModelData(string filepath)
        {
            if (coppiedDirectory != "")
                DestroyCopies();

            string[] split = filepath.Split(new char[]
                {
                  '\\','/'  
                }, StringSplitOptions.RemoveEmptyEntries);

            string mdlName = split[split.Length - 1];

            string loadPath = GetLoadPath(split);

            CopyFiles(mdlName, filepath, loadPath);

            Type[] extraTypes = new Type[]
                { 
                    typeof(EonDictionary<string, BasicLPPMaterial>),
                    typeof(EonDictionary<string, BasicMaterial>),
                    typeof(EonDictionary<int, LODModelInfo>)
                };

            if (!mdlName.Contains(".Shader"))
                mdlName += ".Shader";

            shaders = SerializationHelper.Deserialize<ModelInfo>(
                  loadPath + mdlName, true, "", extraTypes);

            MdlViewerCommon.Common.ModelTransform = new Eon.Maths.Transformation()
            {
                Position = new Vector3(shaders.PositionX, shaders.PositionY, shaders.PositionZ),
                Rotation = new Quaternion(shaders.RotationX, shaders.RotationY, shaders.RotationZ, 0),
                Size = new Vector3(shaders.Scale)
            };

           GameObjectManager.FindGameObject("MdlViewer").SendMessage("SetUp", "", null);

            if (mdl != null)
            {
                mdl.Destroy(true);
                mdl = null;
            }

            mdl = new ModelComponent("Mdl", shaders);
            AttachComponent(mdl);

            return mdlName;
        }

        void DestroyCopies()
        {
            for (int i = 0; i < coppiedFiles.Count; i++)
                File.Delete(coppiedFiles[i]);

            coppiedFiles.Clear();

            coppiedDirectory = "";
        }

        string GetLoadPath(string[] split)
        {
            string loadPath = "";
            int idx = 0;

            for (int i = 0; i < split.Length; i++)
                if (split[i] == "Content")
                    idx = i + 1;

            for (int i = idx; i < split.Length - 1; i++)
                loadPath += split[i] + "\\";

            return loadPath;
        }

        void SetUpScene()
        {
            camera = new ChaseCamera("Cam1", 0.01f, 100, new Vector3(0, 0.0f, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            camera.TargetOffSet = new Vector3(0, 0.10f, 0);

            AttachComponent(camera);

            direct = new DirectionalLight("Direct",
                Color.White.ToVector3(), 1f, new Vector3(0, 1, 1), false);

            AttachComponent(direct);

            direct1 = new DirectionalLight("D1", Color.White, new Vector3(1, -0.5f, 0), false);
            AttachComponent(direct1);

            direct2 = new DirectionalLight("D2", Color.White, new Vector3(-1, -0.5f, 0), false);
            AttachComponent(direct2);

            pl = new PointLight(ID + "pl", new Vector3(0, 0, -0.1f), Color.White, 1);
            AttachComponent(pl);
        }

        void ShowModelData(string mdlName)
        {
            if (texts.Count > 0)
            {
                for (int i = 0; i < texts.Count; i++)
                    texts[i].Destroy(true);

                texts.Clear();
            }

            Vector2 initialPos = new Vector2(20, 90);

            if (txt != null)
                txt.ChangeText("Name: " + mdlName);
            else
            {
                txt = new TextItem(ID + "txt", 0, "Name: " + mdlName, "Eon/Fonts/Arial12", initialPos, Color.White, true);
                AttachComponent(txt);
            }

            initialPos.Y += 20;

            string[] meshNames = mdl.GetMeshNames();

            if (meshNames.Length > 0)
            {
                for (int i = 0; i < meshNames.Length; i++)
                {
                    TextItem t = new TextItem(ID + "txt" + meshNames[i], 0, "Mesh: " + meshNames[i],
                        "Eon/Fonts/Arial12", initialPos, Color.White, true);

                    texts.Add(t);

                    AttachComponent(t);

                    initialPos.Y += 14;
                }

                initialPos.Y += 30;

                string[] boneNames = mdl.GetBoneNames();

                if (boneNames.Length > 0)
                {
                    for (int i = 0; i < boneNames.Length; i++)
                    {
                        TextItem t = new TextItem(ID + "txtBone" + boneNames[i], 0, "Bone: " + boneNames[i],
                            "Eon/Fonts/Arial12", initialPos, Color.White, true);

                        texts.Add(t);

                        AttachComponent(t);

                        initialPos.Y += 14;
                    }

                    initialPos.Y += 30;
                }

                string text = "Total Vertices: ";

                if (mdl.GetVertexCount() > 0)
                    text += mdl.GetVertexCount();
                else
                    text += "0";

                TextItem v = new TextItem(ID + "txtVerts", 0, text,
                        "Eon/Fonts/Arial12", initialPos, Color.White, true);

                texts.Add(v);
                AttachComponent(v);
            }
            else
            {
                initialPos.Y += 14;

                TextItem t = new TextItem(ID + "txtNone", 0, "//No meshes are present in the model//",
                        "Eon/Fonts/Arial12", initialPos, Color.Red, true);

                texts.Add(t);
                AttachComponent(t);
            }
        }

        void CopyFiles(string mdlName, string currentDirectory, string loadPath)
        {
            currentDirectory = currentDirectory.Remove(
                currentDirectory.Length - mdlName.Length, mdlName.Length);

            string[] shaders = Directory.GetFiles(currentDirectory, "*.Shader", SearchOption.TopDirectoryOnly);
            string[] files = Directory.GetFiles(currentDirectory, "*.xnb", SearchOption.TopDirectoryOnly);

            coppiedDirectory = Environment.CurrentDirectory + "\\Content\\" + loadPath;

            Directory.CreateDirectory(coppiedDirectory);

            for (int i = 0; i < shaders.Length; i++)
            {
                string filepath = coppiedDirectory + GetFileName(shaders[i]);

                coppiedFiles.Add(filepath);

                File.Copy(shaders[i], filepath, true);
            }

            for (int i = 0; i < files.Length; i++)
            {
                string filepath = coppiedDirectory + GetFileName(files[i]);

                coppiedFiles.Add(filepath);

                File.Copy(files[i], filepath, true);
            }
        }

        string GetFileName(string filepath)
        {
            string[] split = filepath.Split(new char[]
                {
                  '\\','/'  
                }, StringSplitOptions.RemoveEmptyEntries);

            return split[split.Length - 1];
        }

        protected override void Update()
        {
            if (loadedModel)
            {
                loadedModel = false;

                ShowModelData(mdlName);
                camera.TargetOffSet = mdl.Origin;
            }

            if (MdlViewerCommon.Common.Updated)
            {
                mdl.SetBaseTransform(MdlViewerCommon.Common.ModelTransform.Matrix);
                MdlViewerCommon.Common.Updated = false;

                shaders.PositionX = MdlViewerCommon.Common.ModelTransform.Position.X;
                shaders.PositionY = MdlViewerCommon.Common.ModelTransform.Position.Y;
                shaders.PositionZ = MdlViewerCommon.Common.ModelTransform.Position.Z;

                shaders.RotationX = MdlViewerCommon.Common.ModelTransform.Rotation.X;
                shaders.RotationY = MdlViewerCommon.Common.ModelTransform.Rotation.Y;
                shaders.RotationZ = MdlViewerCommon.Common.ModelTransform.Rotation.Z;

                shaders.Scale = MdlViewerCommon.Common.ModelTransform.Size.X;
            }

            UpdateCamera();

            base.Update();
        }

        private void UpdateCamera()
        {
            Vector3 movement = Vector3.Zero;

            float rotationSpeed = 0.002f;

            Vector3 rotation = Vector3.Zero;

            if (InputManager.IsButtonPressed(MouseButtons.Left))
                rotation = new Vector3(InputManager.MouseDelta.Y *
                    rotationSpeed, InputManager.MouseDelta.X * rotationSpeed, 0);

            float speed = 0.002f;

            if (InputManager.IsKeyPressed(Keys.A))
                movement.X += speed;
            else if (InputManager.IsKeyPressed(Keys.D))
                movement.X -= speed;

            if (InputManager.IsKeyPressed(Keys.W))
                movement.Y += speed;
            else if (InputManager.IsKeyPressed(Keys.S))
                movement.Y -= speed;

            if (InputManager.IsKeyPressed(Keys.Z))
                movement.Z += speed;
            else if (InputManager.IsKeyPressed(Keys.X))
                movement.Z -= speed;

            if (movement != Vector3.Zero || rotation != Vector3.Zero)
            {
                Vector3 move = Vector3.Transform(movement,
                    Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z));

                camera.Move(move, rotation);
                camera.FollowingPos += movement;
            }
        }

        public override void Destroy()
        {
            if (File.Exists("Temp.temp"))
                File.Delete("Temp.temp");

            base.Destroy();
        }
    }
}
