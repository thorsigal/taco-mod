using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles
{
    public class NukeEffect : ModProjectile
    {

        Vector2 originalPosition;
        uint initialTime = 0;
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
            Projectile.penetrate = 999;
            
        }

        public override void OnSpawn(IEntitySource source)
        {
            originalPosition = Projectile.position;
            base.OnSpawn(source);
        }

        public override void AI()
        {
            //Nuclear effect
            //Why did I put so much effort into this
            Projectile.ai[2] += 1f;
            originalPosition.Y -= 2.2f;
            Projectile.velocity *= 0.93f;
            Vector2 dist = Projectile.position - originalPosition;
            dist /= 200;
            Projectile.velocity.Y -= 0.17f * (float)Math.Sin(Projectile.ai[0]);
            Projectile.velocity -= 0.3f * dist * Math.Abs(dist.X) * (float)(Math.Sin(Projectile.ai[0]));
            int width = 33;
            int height = 33;
            Vector2 diff = new Vector2(16,16);
            int ydiff = 16;
            int n = 5;
            if (dist.Y < 0)
            {
                width = 400;
                diff.X = 200;
                diff.Y = 40;
                height = 80;
                n = 10;
                if (dist.X < 0)
                {
                    Projectile.velocity.X += .15f * dist.Y * dist.Y * (1 - 120.0f / (120 + (240 - Projectile.timeLeft) * (240 - Projectile.timeLeft)))*4;
                }
                if (dist.X > 0)
                {
                    Projectile.velocity.X -= .15f * dist.Y * dist.Y * (1 - 120.0f / (120 + (240 - Projectile.timeLeft) * (240 - Projectile.timeLeft)))*4;
                }
                Projectile.velocity.Y += 0.12f * Math.Abs(dist.X)* Math.Abs(dist.X) * (1 - 120.0f / (120 + (240 - Projectile.timeLeft))) * 40;
            }
            if(Projectile.ai[2]>=4f)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    int alpha = 100;
                    if (Projectile.timeLeft < 155)
                    {
                        alpha = 255 - Projectile.timeLeft;
                    }
                    Dust.NewDust(Projectile.position - diff, width, height, DustID.Smoke, Projectile.velocity.X / 5, Projectile.velocity.Y, alpha, Color.Black, 3f);
                    for (int i = 0; i < (Projectile.timeLeft * n) / 240; i++)
                    {
                        int dnum = Dust.NewDust(Projectile.position - diff, width, height, DustID.Torch, Projectile.velocity.X / 5, Projectile.velocity.Y, alpha, Color.Orange, 3f);
                        Main.dust[dnum].noGravity = true;
                    }
                    //Dust.NewDustDirect(originalPosition, 1, 1, DustID.Smoke, 0f, 0f, 100, Color.Red, 1.5f);
                }
                Projectile.ai[2] = 0f;
            }
        }


        public override bool PreDraw(ref Color lightColor)
        {
            //Invisible helper entity
            return false;
        }

    }
}
