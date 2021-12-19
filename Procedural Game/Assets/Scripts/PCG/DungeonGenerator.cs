using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        public int maxRooms = 100;

        public List<GameObject> totalRooms = new List<GameObject>();
        public List<GameObject> overlappers = new List<GameObject>();
        private List<GameObject> roomTypes = new List<GameObject>();

        public int spawnIndex;

        public int index;
        private bool stopper;
        private bool once;

        private void Awake()
        {
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Room_Normal"));
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Room_Large"));
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Room_Long"));
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Room_Corner_DL"));
            roomTypes.Add(Resources.Load<GameObject>("Rooms/Room_Corner_DR"));
        }

        private void FixedUpdate()
        {
            DestroyOverlap();
            if (!once)
            {
                CreateRoom(transform.position, Quaternion.identity);
                once = true;
            }

            if (totalRooms.Count != maxRooms && spawnIndex == totalRooms.Count && !stopper)
            //if (totalRooms.Count < maxRooms && spawnIndex == totalRooms.Count)
            {
                RestartRoom();
            }
            else if (totalRooms.Count > maxRooms)
            {
                DestroyLatestRooms();
            }
            else if (totalRooms.Count == maxRooms && !stopper)
            {
                StartCoroutine(ClearData());
                stopper = true;
            }
        }

        private void DestroyLatestRooms()
        {
            //find latest rooms
            totalRooms.Remove(totalRooms[totalRooms.Count]);
            if (overlappers.Contains(totalRooms[totalRooms.Count]))
            {
                overlappers.Remove(totalRooms[totalRooms.Count]);
            }
            Destroy(totalRooms[totalRooms.Count]);
        }

        private IEnumerator ClearData()
        {
            yield return new WaitForSeconds(1f);
            foreach (GameObject obj in totalRooms)
            {
                obj.GetComponent<DungeonRoom>().enabled = false;
            }
            yield return new WaitForSeconds(0.4f);
            totalRooms.Clear();
        }

        private void DestroyOverlap()
        {
            foreach (GameObject obj in overlappers)
            {
                totalRooms.Remove(obj);
                Destroy(obj);
            }
            overlappers.Clear();
        }

        private void RestartRoom()
        {
            DungeonRoom dr = totalRooms[index].GetComponent<DungeonRoom>();

            if (dr.doorways.Count > 0)
            {
                for (int i = 0; i < dr.doorways.Count; i++)
                {
                    CreateRoom(dr.doorways[i].position, dr.doorways[i].rotation);
                }
            }
            else if (index != totalRooms.Count - 1)
            {
                index++;
            }
            else
            {
                Debug.Log("restart");
                RestartGenerator();
            }
        }

        private void RestartGenerator()
        {
            GameObject gen = Instantiate(Resources.Load<GameObject>("Dungeon_Generator_Controller"), transform);
            gen.transform.parent = null;
            Destroy(gameObject);
        }

        private void CreateRoom(Vector3 position, Quaternion rotation)
        {
            if (totalRooms.Count < maxRooms)
            {
                //Random room
                int type = Random.Range(0, roomTypes.Count);

                GameObject room = Instantiate(roomTypes[type], position, rotation);
                totalRooms.Add(room);
                room.GetComponent<DungeonRoom>().spawnNum = totalRooms.Count;
                room.name = "Room " + totalRooms.Count;
                room.transform.parent = this.transform;

                StartCoroutine(FindPath(room));
            }
        }


        private IEnumerator FindPath(GameObject room)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (room != null)
            {
                DungeonRoom dr = room.GetComponent<DungeonRoom>();

                if (dr.doorways.Count > 0)
                {
                    int roomsToSpawn = Random.Range(0, dr.doorways.Count);

                    for (int i = 0; i <= roomsToSpawn; i++)
                    {
                        CreateRoom(dr.doorways[i].position, dr.doorways[i].rotation);
                    }
                }
                spawnIndex++;
            }
        }
    }
}
