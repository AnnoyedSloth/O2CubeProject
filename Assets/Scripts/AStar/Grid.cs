using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

namespace UltimateIsometricToolkit.controller
{

    public class Grid : MonoBehaviour
    {

        private IsoTransform _GridTransform;
        public LayerMask unwalkableMask; // 접근불가지역 마스크
        public Vector2 gridWorldSize; // 그리드 총 사이즈
        public float nodeRadius; // 노드 반경
        Node[,] grid;

        float nodeDiameter;
        int gridSizeX, gridSizeY;

        // Use this for initialization
        void Start()
        {
            _GridTransform = this.GetOrAddComponent<IsoTransform>();
            nodeDiameter = nodeRadius * 2; // 노드 직경 = 반경 * 29
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); // X축 그리드의 사이즈는  (x축 총 길이)/(노드직경)
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); // Y축 그리드의 사이즈는  (y축 총 길이)/(노드직경)
            CreateGrid();
        }

        // Update is called once per frame
        void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            //Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
            Vector3 worldBottomLeft = _GridTransform.Position;
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    //Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                    Vector3 worldPoint = worldBottomLeft + _GridTransform.Axis(new Vector3(x, y, 0) * (nodeDiameter + nodeRadius));
                    Debug.Log(_GridTransform.Position.x + ", " + _GridTransform.Position.y);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                    grid[x, y] = new Node(walkable, worldPoint); // 좌표 isometric으로 바꾸는 작업중이었음!!
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}