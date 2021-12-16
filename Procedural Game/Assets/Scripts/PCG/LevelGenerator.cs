using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Generator Values")]
        public int roomWidth, RoomLength;
        public int roomWidthMin, RoomLengthMin;
        public int maxIterations;
        public int corridorWidth;

        void Start()
        {
            CreateLevel();
        }

        private void CreateLevel()
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            throw new NotImplementedException();
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}
