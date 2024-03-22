using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles
{
    public class Spotify : ModProjectile
    {

        uint initialTime = 0;
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 1800;
            Projectile.tileCollide = false;
            Projectile.penetrate = 999;
        }

        public override void AI()
        {
            if (Projectile.ai[0]==0f)
            {
                SoundEngine.PlaySound(new SoundStyle("TacoMikesMod/Sounds/Projectile/Spotify"), this.Projectile.position);
            }
            Projectile.ai[0] = 1f;
            base.AI();
        }




    }
}
