<?xml version="1.0"?>
<ModelInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RotationX>0</RotationX>
  <RotationY>0</RotationY>
  <RotationZ>0</RotationZ>
  <PositionX>0</PositionX>
  <PositionY>0</PositionY>
  <PositionZ>0</PositionZ>
  <Scale>0.8</Scale>
  <IsStatic>true</IsStatic>
  <Models>
    <Keys>
      <int>0</int>
    </Keys>
    <Values>
      <LODModelInfo>
        <CollisionModelFilepath>NULL</CollisionModelFilepath>
        <ModelFilepath>Room/Room</ModelFilepath>
        <Materials>
          <Keys>
            <string>Walls</string>
            <string>Windows</string>
            <string>Floor</string>
            <string>Roof</string>
          </Keys>
          <Values>
            <Material xsi:type="LPPTerrainMaterial">
              <WeightMapFilepath>Room/Walls_UV</WeightMapFilepath>
              <TextureRepeats>6</TextureRepeats>
              <RFilepath>Textures/Brick</RFilepath>
              <GFilepath>Textures/Plaster</GFilepath>
              <BFilepath>Textures/Moss</BFilepath>
              <RNFilepath>Textures/Brick_Norm</RNFilepath>
              <GNFilepath>Textures/Plaster_Norm</GNFilepath>
              <BNFilepath>Textures/Moss_Norm</BNFilepath>
            </Material>
            <Material xsi:type="BasicMaterial">
              <TextureFilepath>Room/Window_UV</TextureFilepath>
              <NormalMapFilepath>Room/Window_Norm</NormalMapFilepath>
            </Material>
            <Material xsi:type="LPPTerrainMaterial">
              <WeightMapFilepath>Room/Floor_UV</WeightMapFilepath>
              <TextureRepeats>6</TextureRepeats>
              <RFilepath>Textures/Concreate</RFilepath>
              <GFilepath>Textures/Blood</GFilepath>
              <BFilepath>Textures/Concreate2</BFilepath>
              <RNFilepath>Textures/Concreate_Norm</RNFilepath>
              <GNFilepath>Textures/Blood_Norm</GNFilepath>
              <BNFilepath>Textures/Concreate2_Norm</BNFilepath>
            </Material>
            <Material xsi:type="LPPTerrainMaterial">
              <WeightMapFilepath>Room/Roof_UV</WeightMapFilepath>
              <TextureRepeats>6</TextureRepeats>
              <RFilepath>Textures/Concreate</RFilepath>
              <GFilepath>Textures/Metal1</GFilepath>
              <BFilepath>Textures/Metal2</BFilepath>
              <RNFilepath>Textures/Concreate_Norm</RNFilepath>
              <GNFilepath>Textures/Metal1_Norm</GNFilepath>
              <BNFilepath>Textures/Metal2_Norm</BNFilepath>
            </Material>
          </Values>
        </Materials>
      </LODModelInfo>
    </Values>
  </Models>
</ModelInfo>