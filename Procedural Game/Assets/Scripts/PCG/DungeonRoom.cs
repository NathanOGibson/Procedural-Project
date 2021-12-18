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
        public bool finishedGen;

        private void Start()
        {
            dg = transform.parent.GetComponent<DungeonGenerator>();
        }

        private void FixedUpdate()
        {
            if (doorways.Count == 0 && !finishedGen)
            {
                finishedGen = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Room"))
            {
                if (other.GetComponent<DungeonRoom>().spawnNum > spawnNum && other.gameObject != null)
                {
                    dg.overlappers.Add(other.gameObject);
                }
            }
        }
    }
}