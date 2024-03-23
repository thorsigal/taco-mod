using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles
{
    public class EPC2 : ModProjectile
    {

        uint initialTime = 0;
        bool empowered = false;
        bool enabled = false;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
            Projectile.penetrate = 999;
            Projectile.alpha=255;

        }
        int charge = 0;
        public void addCharge(Vector2 position, Vector2 velocity, int chargeAmount) {
            Projectile.timeLeft = 600;
            Projectile.position=position;
            Projectile.velocity=velocity;
            charge += chargeAmount;
        }

        public override void AI() {
            if(!enabled) {
                Player myOwner;
                if(Projectile.TryGetOwner(out myOwner)) {
                    if(!myOwner.HasBuff<Buffs.EPCBuff>()) {
                        if(charge < 90) {
                            if (Main.myPlayer == Projectile.owner)
                            {
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity*12, ModContent.ProjectileType<EPC1>(), 0, 0, Owner: Projectile.owner);
                            }
                            Projectile.Kill();
                            } else {
                                Projectile.timeLeft = 600;
                                Projectile.alpha=0;
                                if (charge > 180) {
                                    empowered = true;
                                }
                            }
                    }
                }
            }
            base.AI();
        }


        public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 5;
		}
        public override bool PreDraw(ref Color lightColor)
        {
            uint time = Main.GameUpdateCount;
            if(initialTime==0)
            {
                initialTime = time;
            }
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            uint deltaTime = ((time - initialTime)/6)%5;
            int startY = 0;
            
            startY = 176 - frameHeight * (int)deltaTime;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 0f;
            origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0, 0),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }

    }
}
