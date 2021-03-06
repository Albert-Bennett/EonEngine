<?xml version="1.0"?>
<ModelDefination xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <CollisionModelFilepath>Models/Enemies/Scutler/Scutler_Collide</CollisionModelFilepath>
  <RotationalOffset>
    <X>0</X>
    <Y>0</Y>
    <Z>0</Z>
  </RotationalOffset>
  <PositionalOffset>
    <X>0</X>
    <Y>0</Y>
    <Z>0</Z>
  </PositionalOffset>
  <Scale>1</Scale>
  <Shaders>
    <Keys>
      <string>Skull</string>
      <string>Scutler</string>
    </Keys>
    <Values>
      <Shader>
        <Parameters>
          <ShaderParameter>
            <ParameterName>Texture</ParameterName>
            <ValueType>Texture2D</ValueType>
            <ValueString>Models/Enemies/Scutler/Scutler_SkullUV</ValueString>
          </ShaderParameter>
          <ShaderParameter>
            <ParameterName>NormalMap</ParameterName>
            <ValueType>Texture2D</ValueType>
            <ValueString>Models/Enemies/Scutler/Scutler_SkullNM</ValueString>
          </ShaderParameter>
          <ShaderParameter>
            <ParameterName>SpecularMap</ParameterName>
            <ValueType>Texture2D</ValueType>
            <ValueString>Models/Enemies/Scutler/Scutler_SkullSpec</ValueString>
          </ShaderParameter>
        </Parameters>
        <ShaderFilepath>Eon/Shaders/Materials/BasicLPPMaterial</ShaderFilepath>
        <DefaultTechnique>NULL</DefaultTechnique>
        <RenderType>LightingPrePass</RenderType>
      </Shader>
      <Shader>
        <Parameters>
          <ShaderParameter>
            <ParameterName>Texture</ParameterName>
            <ValueType>Texture2D</ValueType>
            <ValueString>Models/Enemies/Scutler/Scutler_BodyUV</ValueString>
          </ShaderParameter>
          <ShaderParameter>
            <ParameterName>NormalMap</ParameterName>
            <ValueType>Texture2D</ValueType>
            <ValueString>Models/Enemies/Scutler/Scutler_BodyNM</ValueString>
          </ShaderParameter>
          <ShaderParameter>
            <ParameterName>SpecularMap</ParameterName>
            <ValueType>Texture2D</ValueType>
            <ValueString>Models/Enemies/Scutler/Scutler_BodySpec</ValueString>
          </ShaderParameter>
        </Parameters>
        <ShaderFilepath>Eon/Shaders/Materials/BasicLPPMaterial</ShaderFilepath>
        <DefaultTechnique>NULL</DefaultTechnique>
        <RenderType>LightingPrePass</RenderType>
      </Shader>
    </Values>
  </Shaders>
</ModelDefination>