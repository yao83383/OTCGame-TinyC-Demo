Shader "Custom/GridShader" {
	Properties {
		_GridSize("Grid Size", Float) = 1.0
		_GridColor("Grid Color", Color) = (1, 1, 1, 1)
		_GridWidth("Grid Width", Float) = 0
		_LargeGridScale("Large Grid Scale", Integer) = 10

		_Far("Far", Float) = 1500
		_GroundDistance("Ground Distance", Float) = 0
		_CameraFadingThreshold("Camera Fading Threshold", Float) = 100

		[KeywordEnum(X, Y, Z)]_Direction("Direction", Float) = 0
	}
	SubShader {
		Tags {
			"RenderPipeline"="UniversalPipeline"
			"RenderType"="Transparent"
			"IgnoreProjector"="True"
			"Queue"="Transparent"
		}
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull off
			ZTest Off
			HLSLPROGRAM
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature_local _DIRECTION_X _DIRECTION_Y _DIRECTION_Z

			float _GridSize;
			float _Far;
			float4 _GridColor;
			float _GroundDistance;
			float _GridWidth;
			float _CameraFadingThreshold;
			float _LargeGridScale;

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varings
			{
				float4 positionCS : SV_POSITION;
				float3 nearPoint : TEXCOORD0;
				float3 farPoint : TEXCOORD1;
			};

			float3 TransformHClipToWorld(float3 positionCS, float4x4 inv_VP)
			{
				float4 unprojectedPoint = mul(inv_VP, float4(positionCS, 1.0));
				return unprojectedPoint.xyz / unprojectedPoint.w;
			}

			Varings vert(Attributes input)
			{
				//此shader专用于quad mesh
				//所以使用4个顶点的uv值进行变换 作为 裁切空间的坐标
				//保证这是一个覆盖全屏幕的渲染
				Varings o;
				float2 uv = input.uv * 2.0 - 1.0;
				//默认情况下，Zndc = 1是远平面
				half farPlane = 1;
				half nearPlane = 0;

				#if defined(UNITY_REVERSED_Z)
				//有时候会反转z
				farPlane = 1 - farPlane;
				nearPlane = 1 - nearPlane;
				#endif

				float4 position = float4(uv, farPlane, 1);
				float3 nearPoint = TransformHClipToWorld(float3(position.xy, nearPlane), UNITY_MATRIX_I_VP);
				float3 farPoint = TransformHClipToWorld(float3(position.xy, farPlane), UNITY_MATRIX_I_VP);
				o.positionCS = position;
				o.nearPoint = nearPoint;
				o.farPoint = farPoint;
				return o;
			}

			float Grid(float2 uv, float width)
			{
				const float2 derivative = fwidth(uv) + width;
				uv = frac(uv - 0.5); //中心对齐
				uv = abs(uv - 0.5);
				uv = uv / derivative;
				const float min_value = min(uv.x, uv.y);
				const float grid = 1.0 - saturate(min_value);
				return grid;
			}

			float GetViewZ(float3 pos)
			{
				float4 clip_space_pos = mul(UNITY_MATRIX_VP, float4(pos.xyz, 1.0));
				const float view_z = clip_space_pos.w; //根据projection矩阵定义，positionCS.w = viewZ
				return view_z;
			}

			float GetT(float3 near, float3 far, float distance)
			{
				float t;
				#define GROUND_GET_T(axis) ((-near.axis + distance) / (far.axis - near.axis))
				#if _DIRECTION_X
				t = GROUND_GET_T(x);
				#elif _DIRECTION_Y
				t = GROUND_GET_T(y);
				#elif _DIRECTION_Z
				t = GROUND_GET_T(z);
				#endif
				return t;
			}

			float2 GetUv(float3 position)
			{
				float2 uv;
				#if _DIRECTION_X
				uv = position.yz;
				#elif _DIRECTION_Y
				uv = position.xz;
				#elif _DIRECTION_Z
				uv = position.xy;
				#endif
				return uv;
			}

			float GetCameraDistance()
			{
				float3 camera = _WorldSpaceCameraPos;
				float distance;
				#if _DIRECTION_X
				distance = camera.x;
				#elif _DIRECTION_Y
				distance = camera.y;
				#elif _DIRECTION_Z
				distance = camera.z;
				#endif
				return abs(distance);
			}

			half4 frag(Varings input) : SV_TARGET
			{
				const float t = GetT(input.nearPoint, input.farPoint, _GroundDistance);
				const float ground = step(0, t);
				const float3 world_position = input.nearPoint + t * (input.farPoint - input.nearPoint);

				const float camera_distance = GetCameraDistance();
				const float camera_grid_distance = (camera_distance % _CameraFadingThreshold) / _CameraFadingThreshold;
				const float camera_fading = lerp(1, 0, min(1.0, camera_grid_distance)); // 相机越远，线框越淡

				const float grid_size = 1 / _GridSize;
				const float grid_scale = 1.0f / _LargeGridScale;
				const float grid_size_power = floor(camera_distance / _CameraFadingThreshold);
				const float small_grid_size = grid_size * pow(grid_scale, grid_size_power);
				const float large_grid_size = small_grid_size * grid_scale;
				const float2 uv = GetUv(world_position);
				const float small_grid = Grid(uv * small_grid_size, _GridWidth) * camera_fading;
				const float large_grid = Grid(uv * large_grid_size, _GridWidth);
				const float grid = small_grid + large_grid;

				const float fading = max(0.0, 1.0 - GetViewZ(world_position) / _Far); // 横向越远，线框越淡

				half4 color = _GridColor;
				color.w *= ground * grid * fading;
				return color;
			}
			ENDHLSL
		}
	}
}
