using System.Collections.Generic;
using UnityEngine;

namespace Game.Source.LevelSystem
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public List<Location> Locations { get;  private set; }
        [field: SerializeField] public LevelBoundingBox LevelBoundingBox { get;  private set; }

        //bounding box
        private void OnValidate()
        {
            Locations.Clear();
            foreach (Location location in transform.GetComponentsInChildren<Location>())
            {
                Locations.Add(location);
            }

            LevelBoundingBox = GetComponent<LevelBoundingBox>();
        }
        
    }
}
