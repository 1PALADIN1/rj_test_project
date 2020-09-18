using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace View
{
    public class UnitView
    {
        private static readonly Dictionary<TeamFlag, GameObject> Originals = new Dictionary<TeamFlag, GameObject>();

        private static string GetPrefabPath(TeamFlag teamFlag)
        {
            switch (teamFlag)
            {
                case TeamFlag.Blue: return "Prefabs/BlueTeam/Blue";
                case TeamFlag.Red:  return "Prefabs/RedTeam/Red";
                default:
                    throw new ArgumentOutOfRangeException(nameof(teamFlag), teamFlag, null);
            }
        }
        
        public static void Preload()
        {
            var flags = Enum.GetValues(typeof(TeamFlag)).Cast<TeamFlag>().ToArray();
            foreach (var flag in flags)
            {
                Originals.Add(flag, Resources.Load<GameObject>(GetPrefabPath(flag)));
            }
        }
        
        public static UnitView Create(TeamFlag teamFlag, Transform parent)
        {
            var go = UnityObject.Instantiate(Originals[teamFlag], parent, true);
            return new UnitView(go.transform);
        }
        
        private readonly Transform _transform;

        private UnitView(Transform transform)
        {
            _transform = transform;
        }
        
        public void SetPosition(int x, int y)
        {
            _transform.localPosition = new Vector3(x, _transform.localPosition.y, y);          
        }

        public void Destroy()
        {
            UnityObject.Destroy(_transform.gameObject);
        }
    }
}