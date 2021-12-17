using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        public List<GameObject> totalRooms = new List<GameObject>();
        public List<GameObject> endRooms = new List<GameObject>();
        public List<GameObject> overlappers = new List<GameObject>();

        private List<GameObject> roomTypes = new List<GameObject>();

        private void Awake()
        {
            //roomTypes.Add(Resources.Load<GameObject>("Rooms/Placeholder_Room_1"));
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Placeholder_Room_2"));
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Placeholder_Room_4"));
        }

        void Start()
        {
            // spawn dungeon room
            CreateRoom(transform.position, Quaternion.identity);
        }

        private void Update()
        {

            if (totalRooms.Count != 100 && endRooms.Count == 0)
            {
                RestartRoom();
            }
        }

        private void DestroyOverlap()
        {
            foreach (GameObject obj in overlappers)
            {
                totalRooms.Remove(obj);
                endRooms.Remove(obj);
                Destroy(obj);
            }
        }

        private void RestartRoom()
        {
            // Go back through the rooms in the total rooms
            // if room has doorways available add them back to the pathless rooms

            for (int i = 0; i < totalRooms.Count; i++)
            {
                DungeonRoom dr = totalRooms[i].GetComponent<DungeonRoom>();

                if (totalRooms.Contains(totalRooms[i]) && dr.doorways.Count > 0)
                {
                    if (!endRooms.Contains(totalRooms[i]))
                    {
                        endRooms.Add(totalRooms[i]);
                    }
                }

            }
        }

        private void CreateRoom(Vector3 position, Quaternion rotation)
        {
            if (totalRooms.Count < 100)
            {
                //Random room
                int type = Random.Range(0, roomTypes.Count);

                GameObject room = Instantiate(roomTypes[type], position, rotation);
                totalRooms.Add(room);
                endRooms.Add(room);
                room.GetComponent<DungeonRoom>().spawnNum = totalRooms.Count;
                room.name = "Room " + totalRooms.Count;
                room.transform.parent = this.transform;

                if (room != null)
                {
                    StartCoroutine(FindPath(room));
                }
            }
        }

        private IEnumerator FindPath(GameObject emptyRoom)
        {
            if (endRooms.Contains(emptyRoom))
            {
                DungeonRoom dr = emptyRoom.GetComponent<DungeonRoom>();
                Debug.Log(dr.doorways.Count);

                yield return new WaitForSeconds(0.2f);

                if (dr.doorways.Count > 0)
                {
                    int roomsToSpawn = Random.Range(0, dr.doorways.Count);

                    for (int i = 0; i <= roomsToSpawn; i++)
                    {
                        CreateRoom(dr.doorways[i].position, dr.doorways[i].rotation);
                    }
                    endRooms.Remove(emptyRoom);
                    DestroyOverlap();
                }
            }
        }
    }
}
