using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.PCG
{
    public class DoorwayChecker : MonoBehaviour
    {
        public bool isBlocked = false;
        private DungeonRoom dr;

        private void Awake()
        {
            dr = transform.parent.transform.parent.transform.parent.GetComponent<DungeonRoom>();
            dr.doorways.Add(transform);
        }

        private void Update()
        {
            if (isBlocked && dr.doorways.Contains(transform))
            {
                dr.doorways.Remove(transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Doorway"))
            {
                isBlocked = true;
            }
        }
    }
}