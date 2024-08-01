using System;
using System.Collections.Generic;
using System.IO;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using Generator.Scripts;
using Generator.Scripts.Common.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class GridConfigurator
    {
        private readonly int _quantity;

        public GridConfigurator(int gridQueueQuantity)
        {
            _quantity = gridQueueQuantity;
        }

        public Queue<Grid> Get(GridTypes gridTypes, GateTypes gateTypes)
        {
            var gridData = LoadData(gridTypes,gateTypes);
            var gridsQueue = new Queue<Grid>();

            for (var i = 0; i < _quantity; i++)
            {
                var tiles2D = ConvertTo2DArray(gridData.Tiles);
                var grid = new Grid(tiles2D);
                gridsQueue.Enqueue(grid);
            }

            return gridsQueue;
        }

        private TileType[,] ConvertTo2DArray(TileType[] tiles)
        {
            TileType[,] tiles2D = new TileType[GridConstants.SizeX, GridConstants.SizeY];
            
            for (int x = 0; x < GridConstants.SizeX; x++)
            {
                for (int y = 0; y < GridConstants.SizeY; y++)
                {
                    tiles2D[x,y] = tiles[x * GridConstants.SizeY + y];
                }
            }

            return tiles2D;
        }

        private GridData LoadData(GridTypes gridTypes, GateTypes gateTypes)
        {
            switch (gridTypes)
            {
                case GridTypes.Forest:
                    var randomIndex = Random.Range(0, GetFolderItemsCount($"{AssetPaths.ForestGridData}{gateTypes}"));
                    var path = $"{AssetPaths.ForestGridData + gateTypes + "/" + randomIndex}";
                    Debug.Log(path);
                    return Services.ResourceLoadService.Load<GridData>(path);
                case GridTypes.Sand:
                    break;
                case GridTypes.Snow:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gridTypes), gridTypes, null);
            }

            return null;
        }

        private static int GetFolderItemsCount(string path)
        {
            var directoryInfo = new DirectoryInfo($"{AssetPaths.Resources}{path}");

            var objectsCount = directoryInfo.GetFiles(
                "*.asset", 
                SearchOption.TopDirectoryOnly)
                .Length;

            return objectsCount;
        }
    }
}