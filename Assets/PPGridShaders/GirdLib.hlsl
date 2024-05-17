#ifndef GRID_LIB_HLSL
#define GRID_LIB_HLSL

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

float3 GetNormal()
{
    float3 normal;
    #if _DIRECTION_X
    normal = float3(1, 0, 0);
    #elif _DIRECTION_Y
    normal = float3(0, 1, 0);
    #elif _DIRECTION_Z
    normal = float3(0, 0, 1);
    #else
    normal = float3(0, 1, 0);
    #endif
    return normal;
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
    #else
    uv = position.xz;
    #endif
    return uv;
}

float GetCameraDistance(float3 position)
{
    float distance;
    #if _DIRECTION_X
    distance = position.x;
    #elif _DIRECTION_Y
    distance = position.y;
    #elif _DIRECTION_Z
    distance = position.z;
    #else
    distance = position.y;
    #endif
    return abs(distance);
}

void Main_float(float3 CameraPosition, float3 ViewDirection, float GridSize, float LargeGridScale, float GridWidth,
                float4 GridColor, float CameraFadingThreshold, float Far, out float4 Color)
{
    const float3 ground_normal = GetNormal();
    float t = -dot(CameraPosition, ground_normal) / dot(ground_normal, ViewDirection);
    const float ground = step(0, t);
    const float3 world_position = CameraPosition + t * ViewDirection;

    const float camera_distance = GetCameraDistance(CameraPosition);
    const float camera_grid_distance = (camera_distance % CameraFadingThreshold) / CameraFadingThreshold;
    const float camera_fading = lerp(1, 0, min(1.0, camera_grid_distance)); // 相机越远，线框越淡

    const float grid_size = 1 / GridSize;
    const float grid_scale = 1.0f / LargeGridScale;
    const float grid_size_power = floor(camera_distance / CameraFadingThreshold);
    const float small_grid_size = grid_size * pow(grid_scale, grid_size_power);
    const float large_grid_size = small_grid_size * grid_scale;
    const float2 uv = GetUv(world_position);
    const float small_grid = Grid(uv * small_grid_size, GridWidth) * camera_fading;
    const float large_grid = Grid(uv * large_grid_size, GridWidth);
    const float grid = small_grid + large_grid;

    const float fading = max(0.0, 1.0 - t / Far); // 横向越远，线框越淡

    float4 color = GridColor;
    color.w *= ground * grid * fading;
    Color = color;
}

#endif
