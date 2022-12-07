using UnityEngine;

public class MandelbrotTerrain : MonoBehaviour
{
    private float x, y, lengthPerPixelx, lengthPerPixely, minx, maxx, miny, maxy, x0, y0;
    private int limit, displayScale, width, height;
    private float[,] heightData;
    private TerrainData _terrainData;
    private Terrain terrain;
    void Start()
    {
        x0 = 0;
        y0 = 0;

        minx = -2.2f;
        maxx = 0.8f;
        miny = -1.5f;
        maxy = 1.5f;

        limit = 256;

        terrain = GetComponent<Terrain>();
        _terrainData = terrain.terrainData;
        width = _terrainData.heightmapResolution;
        height = width;
        _terrainData.size = new Vector3(width, limit, height);

        heightData = new float[width, height];

        lengthPerPixelx = (maxx - minx) / width;
        lengthPerPixely = (maxy - miny) / height;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float a = i * lengthPerPixelx + minx;
                float b = j * lengthPerPixely + miny;
                float x_, y_;
                x = x0;
                y = y0;
                for (int k = 0; k < limit; k++)
                {
                    x_ = (x * x) - (y * y) + a;
                    y_ = 2 * x * y + b;
                    if (x_ * x_ + y_ * y_ > 8)
                    {
                        heightData[i, j] = k / (float)limit;
                        break;
                    }
                    if (k == limit - 1)
                    {
                        heightData[i, j] = limit;
                    }
                    x = x_;
                    y = y_;
                }
            }
        }
        _terrainData.SetHeights(0, 0, heightData);
        terrain.terrainData = _terrainData;

        Debug.Log("Complete");
    }
}
