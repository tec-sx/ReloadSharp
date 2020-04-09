<?xml version="1.0"?>
<project version="2">
	<pipeline>
		<pass name="Simple" type="shader" active="true">
			<shader type="vs" path="shaders/reload.main_SimpleVS.glsl" entry="main" />
			<shader type="ps" path="shaders/reload.main_SimplePS.glsl" entry="main" />
			<inputlayout>
				<item value="Position" semantic="POSITION" />
				<item value="Normal" semantic="NORMAL" />
				<item value="Texcoord" semantic="TEXCOORD0" />
			</inputlayout>
			<rendertexture />
			<items>
				<item name="Box" type="geometry">
					<type>Cube</type>
					<width>1</width>
					<height>1</height>
					<depth>1</depth>
					<topology>TriangleList</topology>
				</item>
			</items>
			<itemvalues />
			<variables>
				<variable type="float4x4" name="matVP" system="ViewProjection" />
				<variable type="float4x4" name="matGeo" system="GeometryTransform" />
			</variables>
			<macros />
		</pass>
		<pass name="reload.main" type="shader" active="true">
			<shader type="vs" path="main.vert" entry="main" />
			<shader type="ps" path="main.frag" entry="main" />
			<inputlayout>
				<item value="Position" semantic="POSITION" />
				<item value="Normal" semantic="NORMAL" />
				<item value="Texcoord" semantic="TEXCOORD0" />
			</inputlayout>
			<rendertexture />
			<items />
			<itemvalues />
			<macros />
		</pass>
	</pipeline>
	<objects />
	<cameras />
	<settings>
		<entry type="camera" fp="false">
			<distance>4</distance>
			<pitch>28</pitch>
			<yaw>317</yaw>
			<roll>360</roll>
		</entry>
		<entry type="clearcolor" r="0" g="0" b="0" a="0" />
		<entry type="usealpha" val="false" />
	</settings>
</project>
