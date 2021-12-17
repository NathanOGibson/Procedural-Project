using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.PCG
{
    public class DungeonRoom : MonoBehaviour
    {
        public List<Transform> doorways = new List<Transform>();
        public DungeonGenerator dg;
        public int spawnNum;

        private void Start()
        {
            dg = transform.parent.GetComponent<DungeonGenerator>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("Room") && !dg.overlappers.Contains(gameObject))
            {
                if (other.GetComponent<DungeonRoom>().spawnNum > spawnNum)
                {
                    dg.overlappers.Add(other.gameObject);
                }
            }
        }
    }
}