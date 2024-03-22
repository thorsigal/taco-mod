using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles
{
    public class Grob : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 20;
            AIType = ProjectileID.Starfury;
            Projectile.scale = 0.4f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("TacoMikesMod/Sounds/Item/VineBoom") with { MaxInstances=0}, this.Projectile.position);
            base.OnKill(timeLeft);
        }

    }
}
