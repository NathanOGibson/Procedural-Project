using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        public List<GameObject> totalRooms;

        enum GenerationBehaviour
        {
            generateState,

        }

        void Start()
        {
            // spawn dungeon room
            GenerateRooms();
        }

        private void GenerateRooms()
        {
            // Create room
            // Increase Room counter
            // Rename Room
            GameObject room = Instantiate(Resources.Load<GameObject>("Rooms/Placeholder_Room"), transform);
            totalRooms.Add(room);
            room.name = "Room " + totalRooms.Count;

            // Find room Doorways
            // Choose a value between 0 and the total doorways
            int roomDoorways = room.transform.GetChild(0).transform.childCount;
            int roomsToSpawn = Random.Range(0, roomDoorways);


            for (int i = 0; i <= roomsToSpawn; i++)
            {
                GameObject roomNew = Instantiate(
                    Resources.Load<GameObject>("Rooms/Placeholder_Room"),
                    room.transform.GetChild(0).GetChild(i).position,
                    room.transform.GetChild(0).GetChild(i).rotation);

                totalRooms.Add(roomNew);
                roomNew.name = "Room " + totalRooms.Count;
                roomNew.transform.parent = this.transform;
            }
        }

        private void CreateRoom()
        {
            GameObject room = Instantiate(Resources.Load<GameObject>("Rooms/Placeholder_Room"), transform);
            totalRooms.Add(room);
            room.name = "Room " + totalRooms.Count;


            CreateLeadingRooms(room, SelectDoorways(room));
        }

        private int SelectDoorways(GameObject room)
        {
            int roomDoorways = room.transform.GetChild(0).transform.childCount;
            return Random.Range(0, roomDoorways);

        }

        private void CreateLeadingRooms(GameObject room, int roomDoorways)
        {
            for (int i = 0; i <= roomDoorways; i++)
            {
                GameObject roomNew = Instantiate(
                    Resources.Load<GameObject>("Rooms/Placeholder_Room"),
                    room.transform.GetChild(0).GetChild(i).position,
                    room.transform.GetChild(0).GetChild(i).rotation);

                totalRooms.Add(room);
                room.name = "Room " + totalRooms.Count;
                roomNew.transform.parent = this.transform;
            }
        }
    }
}
