﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles.Summon
{
    class ObamaSentry : ModProjectile
    {
        float xvel = 0;
        float yvel = 0;
        int laser = -1;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.scale = 0.5f;
            Projectile.gfxOffY = -800;
            this.DrawOriginOffsetY = -275;
            this.DrawOffsetX = -187;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void AI()
        {
            uint time = Main.GameUpdateCount;

            //if (time%1200==0)
            //{
            //    int type = 455;
            //    float knockback = 0.15f;
            //    summonProjectile(owner, ref this.projectile.position, ref xvel, ref yvel, ref type, ref damage, ref knockback);
            //}
        }

        void SummonProjectile(ref Vector2 position, ref float speedX, ref float speedY, ref int damage, ref float knockBack, bool pvp=false)
        {

            Vector2 heading = (position - Projectile.position);
            heading.Normalize();

            //laser1 = Projectile.NewProjectileDirect(projectile.position + Vector2.UnitX * 28 - Vector2.UnitY * 27, heading, ProjectileID.PhantasmalDeathray, damage, this.projectile.owner);
            //laser2 = Projectile.NewProjectileDirect(projectile.position - Vector2.UnitX * 28 - Vector2.UnitY * 27, heading, ProjectileID.PhantasmalDeathray, damage, this.projectile.owner);
            laser = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position + Vector2.UnitX * 64 - Vector2.UnitY * 4, heading, ModContent.ProjectileType<PDRClone>(), Projectile.damage, 0f, Projectile.owner, /*((float)Math.PI * 2f) / 540f*/ 0, Projectile.whoAmI);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position + Vector2.UnitX * 2 - Vector2.UnitY * 4, heading, ModContent.ProjectileType<PDRClone>(), Projectile.damage, 0f, Projectile.owner, /*((float)Math.PI * 2f) / 540f*/0, Projectile.whoAmI);
    }

        public void Attack(Vector2 position)
        {
            SummonProjectile(ref position, ref xvel, ref yvel, ref this.Projectile.damage, ref this.Projectile.knockBack, true);
        }

        public bool isFiring()
        {
            if (laser == -1) return false;
            if (Main.projectile[laser].active)
            {
                return true;
            }  else
            {
                laser = -1;
                return false;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = 0;
            if (this.isFiring())
            {
                startY = frameHeight;
            }

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 20f;
            origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(80f, -16f),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }
    }

}
   
