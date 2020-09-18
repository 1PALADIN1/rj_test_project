using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class PathFinding
    {
        private class PathNode
        {
            public int PositionX { get; set; }
            public int PositionY { get; set; }
            public int PathWeight { get; set; }
            public double EstimatePathLength { get; set; }
            public double EstimateFullPathLength => PathWeight + EstimatePathLength;
        }
        
        private const int MaxWeight = 999999;
        private const int CellBusyWeight = -2;
        private const int CellStartWeight = -1;
        private const int MapWidth = MapGrid<Unit>.Width;
        private const int MapHeight = MapGrid<Unit>.Height;
        private const int MaxSteps = MapGrid<Unit>.Capacity;
        
	    public static Vector2 FindNextPoint(MapGrid<Unit> map, int startX, int startY, int targetX, int targetY, int searchDistance)
        {
            var weightsMap = FindWave(map, startX, startY, targetX, targetY);
            var aroundPoints = new List<PathNode>();
            
            while (searchDistance > 0)
            {
                foreach (var nextNeighbourPoint in GetNeighboursPoints(startX, startY, targetX, targetY, weightsMap, searchDistance))
                {
                    if (nextNeighbourPoint.PathWeight == CellBusyWeight)
                    {
                        nextNeighbourPoint.PathWeight = MaxWeight;
                    }
                    aroundPoints.Add(nextNeighbourPoint);
                }

                var sortedPointsByWeight = aroundPoints.OrderBy(node => node.EstimateFullPathLength);
                if (!sortedPointsByWeight.Any())
                {
                    searchDistance -= 1;
                    aroundPoints.Clear();
                    continue;
                }

                foreach (var point in sortedPointsByWeight)
                {
                    if (map[point.PositionX, point.PositionY] == null && (point.PositionX != startX || point.PositionY != startY))
                    {
                        return new Vector2(point.PositionX, point.PositionY);
                    }
                }
            }
            return Vector2.negativeInfinity;
        }
        
        private static double GetPathLength(int startX, int startY, int finishX, int finishY)
        {
            return Math.Sqrt((finishX - startX) * (finishX - startX) + (finishY - startY) * (finishY - startY));
        }
        
        private static int[,] FindWave(MapGrid<Unit> map, int startX, int startY, int targetX, int targetY)
        {
            var weightMap = new int[MapWidth, MapHeight];
            var step = 0;
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++) 
                {
                    if (!map.InBounds(x, y) || map[x, y] != null)
                    {
                        weightMap[x, y] = CellBusyWeight;
                    }
                    else
                    {
                        weightMap[x, y] = CellStartWeight;
                    }
                }
            }

            weightMap[startX, startY] = 0; 
            while (weightMap[targetX, targetY] > 0 || step > MaxSteps) 
            {
                for (var x = 0; x < MapWidth; x++) 
                {
                    for (var y = 0; y < MapHeight; y++) 
                    {
                        if (weightMap[x, y] == step)
                        {
                            var nextPosX = x + 1;
                            var nextPosY = y;
                            if (map.InBounds(nextPosX, nextPosY) && weightMap[nextPosX, nextPosY] != CellBusyWeight && weightMap[nextPosX, nextPosY] == CellStartWeight)
                            {
                                weightMap[nextPosX, nextPosY] = step + 1;
                            }
                            nextPosX = x - 1;
                            nextPosY = y;
                            if (map.InBounds(nextPosX, nextPosY) && weightMap[nextPosX, nextPosY] != CellBusyWeight && weightMap[nextPosX, nextPosY] == CellStartWeight)
                            {
                                weightMap[nextPosX, nextPosY] = step + 1;
                            }
                            nextPosX = x;
                            nextPosY = y + 1;
                            if (map.InBounds(nextPosX, nextPosY) && weightMap[nextPosX, nextPosY] != CellBusyWeight && weightMap[nextPosX, nextPosY] == CellStartWeight)
                            {
                                weightMap[nextPosX, nextPosY] = step + 1;
                            }
                            nextPosX = x;
                            nextPosY = y - 1;
                            if (map.InBounds(nextPosX, nextPosY) && weightMap[nextPosX, nextPosY] != CellBusyWeight && weightMap[nextPosX, nextPosY] == CellStartWeight)
                            {
                                weightMap[nextPosX, nextPosY] = step + 1;
                            }
                        }
                    }
                }
                step++;
            }
            return weightMap;
        }
        
        private static IEnumerable<PathNode> GetNeighboursPoints(int x, int y, int targetX, int targetY, int[,] map, int step)
        {
            var result = new List<PathNode>();
            var nextPosX = x + step;
            var nextPosY = y;
            if (x >= 0 && x < MapWidth && nextPosX >= 0 && nextPosY < MapHeight)
            {
                var neighbourNode = new PathNode
                {
                    PositionX = nextPosX,
                    PositionY = nextPosY,
                    PathWeight = map[nextPosX, nextPosY],
                    EstimatePathLength = GetPathLength(nextPosX, nextPosY, targetX, targetY)
                };
                result.Add(neighbourNode);
            }

            nextPosX = x - step;
            nextPosY = y;
            if (x >= 0 && x < MapWidth && nextPosX >= 0 && nextPosY < MapHeight)
            {
                var neighbourNode = new PathNode
                {
                    PositionX = nextPosX,
                    PositionY = nextPosY,
                    PathWeight = map[nextPosX, nextPosY],
                    EstimatePathLength = GetPathLength(nextPosX, nextPosY, targetX, targetY)
                };
                result.Add(neighbourNode);
            }

            nextPosX = x;
            nextPosY = y + step;
            if (x >= 0 && x < MapWidth && nextPosX >= 0 && nextPosY < MapHeight)
            {
                var neighbourNode = new PathNode
                {
                    PositionX = nextPosX,
                    PositionY = nextPosY,
                    PathWeight = map[nextPosX, nextPosY],
                    EstimatePathLength = GetPathLength(nextPosX, nextPosY, targetX, targetY)
                };
                result.Add(neighbourNode);
            }

            nextPosX = x;
            nextPosY = y - step;
            if (x >= 0 && x < MapWidth && nextPosX >= 0 && nextPosY < MapHeight)
            {
                var neighbourNode = new PathNode
                {
                    PositionX = nextPosX,
                    PositionY = nextPosY,
                    PathWeight = map[nextPosX, nextPosY],
                    EstimatePathLength = GetPathLength(nextPosX, nextPosY, targetX, targetY)
                };
                result.Add(neighbourNode);
            }

            return result;
        }
    }
}
