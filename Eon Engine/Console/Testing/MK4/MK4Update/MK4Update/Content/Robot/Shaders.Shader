<?xml version="1.0"?>
<ModelInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RotationX>0</RotationX>
  <RotationY>0</RotationY>
  <RotationZ>0</RotationZ>
  <PositionX>0</PositionX>
  <PositionY>0</PositionY>
  <PositionZ>0.2</PositionZ>
  <Scale>0.15</Scale>
  <OmitLOD>false</OmitLOD>
  <Models>
    <Keys>
      <int>0</int>
    </Keys>
    <Values>
      <LODModelInfo>
        <CollisionModelFilepath>NULL</CollisionModelFilepath>
        <ModelFilepath>Robot/RM-03A</ModelFilepath>
        <Materials>
          <Keys>
            <string>Base</string>
            <string>Head</string>
            <string>Body</string>
          </Keys>
          <Values>
            <Material xsi:type="BasicLPPMaterial">
              <RenderType>LightingPrePass</RenderType>
              <TextureFilepath>Robot/Base_UV</TextureFilepath>
              <NormalMapFilepath>Robot/Base_Norm</NormalMapFilepath>
              <SpecularMapFilepath>Robot/Base_Spec</SpecularMapFilepath>
            </Material>
            <Material xsi:type="BasicLPPMaterial">
              <RenderType>LightingPrePass</RenderType>
              <TextureFilepath>Robot/Head_UV</TextureFilepath>
              <NormalMapFilepath>Robot/Head_Norm</NormalMapFilepath>
              <SpecularMapFilepath>Robot/Head_Spec</SpecularMapFilepath>
            </Material>
            <Material xsi:type="BasicLPPMaterial">
              <RenderType>LightingPrePass</RenderType>
              <TextureFilepath>Robot/Body_UV</TextureFilepath>
              <NormalMapFilepath>Robot/Body_Norm</NormalMapFilepath>
              <SpecularMapFilepath>Robot/Body_Spec</SpecularMapFilepath>
            </Material>
          </Values>
        </Materials>
      </LODModelInfo>
    </Values>
  </Models>
</ModelInfo>