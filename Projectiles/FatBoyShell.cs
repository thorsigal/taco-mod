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
    public class FatBoyShell : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.scale = 1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void AI()
        {
            base.AI();
            Projectile.velocity.Y += 0.13f;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && nPC.position.Distance(Projectile.position) < 330)
                {
                    nPC.StrikeNPC(new NPC.HitInfo() with { Damage = Projectile.damage, DamageType = Projectile.DamageType, Knockback = Projectile.knockBack, HitDirection = Projectile.direction });
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, i, Projectile.damage, Projectile.knockBack, Projectile.direction);
                    }
                }
            }
            for (int i = 0; i < 255; i++)
            {
                Player play = Main.player[i];
                if (play.active && play.position.Distance(Projectile.position) < 330)
                {
                    Player.HurtInfo info = new Player.HurtInfo
                    {
                        HitDirection = Projectile.direction,
                        Knockback = Projectile.knockBack,
                        Damage = Projectile.damage,
                        DamageSource = PlayerDeathReason.ByCustomReason(play.name + " was slain using a reasonable display of force."),
                        PvP = false,
                        Dodgeable = false,
                    };
                    play.Hurt(PlayerDeathReason.ByCustomReason(play.name + " was slain using a reasonable display of force."), Projectile.damage, Projectile.direction);
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendPlayerHurt(i, info, -1);
                    }
                }
            }
            if (Main.myPlayer == Projectile.owner)
            {
                int maxj = 6;
                for (int j = 0; j < maxj; j++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        float mag = 0.2f + j * 25f/maxj;
                        float angle = (3.14159f / 15) * i;
                        float vX = mag * (float)Math.Cos(angle);
                        float vY = -mag * (float)Math.Sin(angle);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(vX, vY), ModContent.ProjectileType<NukeEffect>(), 0, 0, ai0: angle);
                    }
                }
            }
            SoundEngine.PlaySound(new SoundStyle("TacoMikesMod/Sounds/Projectile/MiniNuke"), this.Projectile.position);
            base.OnKill(timeLeft);
            

        }

    }
}
