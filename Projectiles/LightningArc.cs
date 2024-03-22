using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles
{
    public class LightningArc : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CultistBossLightningOrbArc);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 20;
            AIType = ProjectileID.CultistBossLightningOrbArc;
            
        }

    }
}
