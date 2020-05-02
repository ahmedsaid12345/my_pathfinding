/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual : MonoBehaviour {

    private Grid grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid grid) {
        this.grid = grid;
        UpdateHeatMapVisual();

       // grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

  /*  private void Grid_OnGridValueChanged(object sender, Grid.OnGridValueChangedEventArgs e) {
        //UpdateHeatMapVisual();
        updateMesh = true;
    }*/

    private void LateUpdate() {
        if (updateMesh) {
            updateMesh = false;
            UpdateHeatMapVisual();
        }
    }

    private void UpdateHeatMapVisual() {
        MeshUtils.CreateEmptyMeshArrays(grid.grideSizeX * grid.grideSizeY, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.grideSizeX; x++) {
            for (int y = 0; y < grid.grideSizeY; y++) {
                int index = x * grid.grideSizeY + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.NodeRaduis*2;

                //int gridValue = grid.GetValue(x, y);
               // float gridValueNormalized = (float)gridValue / Grid.HEAT_MAP_MAX_VALUE;
               // Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);
                //MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

}
