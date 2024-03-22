using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace TacoMikesMod.Projectiles.Summon
{
    class PDRClone : ModProjectile
    {
        Vector2 initialPosition = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PhantasmalDeathray);
            Projectile.aiStyle = 0;
            Projectile.hostile = false;
            Projectile.friendly = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life < damageDone)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<Explosion>(), 0, 0) ;
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void AI()
        {
            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }

            /**
             * TODO: Items to discover
             * MoonLord localAI[1]
             * MoonLord localAI[0]
             */
            if(initialPosition==Vector2.Zero)
            {
                initialPosition = Projectile.position;
            }
            Vector2 elipseSizes = new Vector2(27f, 59f) * 1.0f;// Main.npc[(int)this.ai[1]].localAI[1];
            Vector2 vector79 = Utils.Vector2FromElipse(/*Main.npc[(int)this.ai[1]].localAI[0]*/ MathHelper.Pi.ToRotationVector2(), elipseSizes);
            Projectile.position = initialPosition + vector79 - new Vector2(Projectile.width, Projectile.height) / 2f;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f)
            {
                
                SoundEngine.PlaySound(SoundID.Zombie104, Projectile.position);
            }
            float num819 = 0.8f;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 180f)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * (float)Math.PI / 180f) * 10f * num819;
            if (Projectile.scale > num819)
            {
                Projectile.scale = num819;
            }
            float num822 = Projectile.velocity.ToRotation();

            num822 += Projectile.ai[0];

            Projectile.rotation = num822 - (float)Math.PI / 2f;
            Projectile.velocity = num822.ToRotationVector2();
            float num823 = 0f;
            float num824 = 0f;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }

            num823 = 3f;
            num824 = Projectile.width;
            
            float[] array5 = new float[(int)num823];
            Collision.LaserScan(samplingPoint, Projectile.velocity, num824 * Projectile.scale, 2400f, array5);
            float num825 = 0f;
            for (int num826 = 0; num826 < array5.Length; num826++)
            {
                num825 += array5[num826];
            }
            num825 /= num823;
            float amount = 0.5f;
            if (Projectile.type == 632)
            {
                amount = 0.75f;
            }
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num825, amount);

            
            Vector2 vector84 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
            for (int num827 = 0; num827 < 2; num827++)
            {
                float num828 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                float num829 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector85 = new Vector2((float)Math.Cos(num828) * num829, (float)Math.Sin(num828) * num829);
                int num830 = Dust.NewDust(vector84, 0, 0, 229, vector85.X, vector85.Y);
                Main.dust[num830].noGravity = true;
                Main.dust[num830].scale = 1.7f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 vector86 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                int num831 = Dust.NewDust(vector84 + vector86 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust119 = Main.dust[num831];
                Dust dust2 = dust119;
                dust2.velocity *= 0.5f;
                Main.dust[num831].velocity.Y = 0f - Math.Abs(Main.dust[num831].velocity.Y);
            }
            DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, DelegateMethods.CastLight);
            float laserLuminance = 0.5f;
            float laserAlphaMultiplier = 0f;
            float prismHue = Projectile.GetLastPrismHue(Projectile.ai[0], ref laserLuminance, ref laserAlphaMultiplier);
            Color color = Main.hslToRgb(prismHue, 1f, laserLuminance);
            color.A = 0;
            Color color2 = color;
            Vector2 vector96 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14.5f * Projectile.scale);
            float x2 = Main.rgbToHsl(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)).X;
            for (int num847 = 0; num847 < 2; num847++)
            {
                float num848 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                float num849 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                Vector2 vector97 = new Vector2((float)Math.Cos(num848) * num849, (float)Math.Sin(num848) * num849);
                int num850 = Dust.NewDust(vector96, 0, 0, 267, vector97.X, vector97.Y);
                Main.dust[num850].color = color;
                Main.dust[num850].scale = 1.2f;
                if (Projectile.scale > 1f)
                {
                    Dust dust123 = Main.dust[num850];
                    Dust dust2 = dust123;
                    dust2.velocity *= Projectile.scale;
                    dust123 = Main.dust[num850];
                    dust2 = dust123;
                    dust2.scale *= Projectile.scale;
                }
                Main.dust[num850].noGravity = true;
                if (Projectile.scale != 1.4f)
                {
                    Dust dust124 = Dust.CloneDust(num850);
                    dust124.color = Color.White;
                    Dust dust125 = dust124;
                    Dust dust2 = dust125;
                    dust2.scale /= 2f;
                }
                float hue = (x2 + Main.rand.NextFloat() * 0.4f) % 1f;
                Main.dust[num850].color = Color.Lerp(color, Main.hslToRgb(hue, 1f, 0.75f), Projectile.scale / 1.4f);
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 vector98 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                int num851 = Dust.NewDust(vector96 + vector98 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust126 = Main.dust[num851];
                Dust dust2 = dust126;
                dust2.velocity *= 0.5f;
                Main.dust[num851].velocity.Y = 0f - Math.Abs(Main.dust[num851].velocity.Y);
            }
            DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
            float value8 = 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 20f);
            Vector2 size = new Vector2(Projectile.velocity.Length() * Projectile.localAI[1], (float)Projectile.width * Projectile.scale);
            float num852 = Projectile.velocity.ToRotation();
            if (Main.netMode != 2)
            {
                ((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(Projectile.position + new Vector2(size.X * 0.5f, 0f).RotatedBy(num852), new Color(0.5f, 0.1f * (float)Math.Sign(value8) + 0.5f, 0f, 1f) * Math.Abs(value8), size, RippleShape.Square, num852);
            }
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, DelegateMethods.CastLight);
            base.AI();
        }



        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint6 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], 36f * Projectile.scale, ref collisionPoint6))
            {
                return true;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D18 = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D texture2D19 = TextureAssets.Extra[21].Value;
            Texture2D texture2D20 = TextureAssets.Extra[22].Value;
            float num207 = Projectile.localAI[1];
            Microsoft.Xna.Framework.Color color40 = new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * 0.9f;
            Main.spriteBatch.Draw(texture2D18, Projectile.Center - Main.screenPosition, null, color40, Projectile.rotation, texture2D18.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            num207 -= (float)(texture2D18.Height / 2 + texture2D20.Height) * Projectile.scale;
            Vector2 center3 = Projectile.Center;
            center3 += Projectile.velocity * Projectile.scale * texture2D18.Height / 2f;
            if (num207 > 0f)
            {
                float num208 = 0f;
                Microsoft.Xna.Framework.Rectangle value12 = new Microsoft.Xna.Framework.Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), texture2D19.Width, 16);
                while (num208 + 1f < num207)
                {
                    if (num207 - num208 < (float)value12.Height)
                    {
                        value12.Height = (int)(num207 - num208);
                    }
                    Main.spriteBatch.Draw(texture2D19, center3 - Main.screenPosition, value12, color40, Projectile.rotation, new Vector2(value12.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num208 += (float)value12.Height * Projectile.scale;
                    center3 += Projectile.velocity * value12.Height * Projectile.scale;
                    value12.Y += 16;
                    if (value12.Y + value12.Height > texture2D19.Height)
                    {
                        value12.Y = 0;
                    }
                }
            }
            Main.EntitySpriteDraw(texture2D20, center3 - Main.screenPosition, null, color40, Projectile.rotation, texture2D20.Frame().Top(), Projectile.scale, SpriteEffects.None, 0f);
            return true;
        }
    }
}
