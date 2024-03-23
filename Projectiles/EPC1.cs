using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles
{
    public class EPC1 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.scale = 1f;
            Projectile.friendly=true;
            Projectile.hostile=false;
            
        }
        public override void OnSpawn(IEntitySource source)
        {
            //TODO: Play sound
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.position,1f,0f,1f);
        }

    }
}
