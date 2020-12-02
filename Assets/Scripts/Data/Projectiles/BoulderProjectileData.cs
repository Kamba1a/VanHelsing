using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "BoulderProjectileData", menuName = "CreateProjectileData/CreateBoulderProjectileData", order = 0)]
    public class BoulderProjectileData : ProjectileData
    {
        //boulder hit filter
        public override bool FilterCollision(Collision touchedCollider)
        {
            throw new NotImplementedException();
        }

        //what happens when a boulder hits
        public override void HitProjectile(IProjectile projectileInterface, Collision touchedCollider)
        {
            throw new NotImplementedException();
        }
    }
}
