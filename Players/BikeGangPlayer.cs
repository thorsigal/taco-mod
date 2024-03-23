using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;



namespace TacoMikesMod.Players
{
    class BikeGangPlayer : ModPlayer
    {
        public bool isCharging = false;
        public Vector2 epcShootPosition;
        public Vector2 epcVelocity;
        public int charge = 0;
        public int EPCChargeIndex=0;
        public int EPC1Index=0;
        public int EPC2Index=0;
        public int[] EPC1Projectiles = new int[20];
        public int[] EPC2Projectiles = new int[20];
        int EPCCooldown=0;
        
        
        /*
        * Checks that the player has let go of the EPC fire button. Prevents accidentally 
        * firing a second shot while keeping the cooldown low.
        */
        bool hasLetGo = true;

        public override void PreUpdate()
        {
            doEPCChargeLogic();
           
            base.PreUpdate();
        }

        /*
        * Charge up, check fire input, and manage release.
        * The projectile type is determined in EPC2.cs in the enable function, as well as the damage.
        * Charge per tick is in EPC.cs in Shoot().
        */
        private void doEPCChargeLogic() {
            if(isCharging && EPCChargeIndex!=-1)
            if(!Player.controlUseItem || charge >= 270) {
                hasLetGo=false;
                ((Projectiles.EPC2)Main.projectile[EPCChargeIndex].ModProjectile).enable(charge, epcShootPosition, epcVelocity);
                isCharging=false;
                charge=0;
                EPCChargeIndex=-1;
                if(charge < 90) {
                    EPCCooldown=12;
                } else if (charge < 270) {
                    EPCCooldown = 24;
                } else {
                    EPCCooldown = 60;
                }
            }
            if(!Player.controlUseItem) {
                hasLetGo = true;
            }
            if(EPCCooldown>0) {
                EPCCooldown--;
            }
        }

        //If I don't do this it shoots 8 billion projectiles
        public override bool CanUseItem(Item item)
        {
            if(item.type==ModContent.ItemType<Items.EPC>()) {
                if (EPCCooldown>0 || !hasLetGo) {
                    return false;
                }
            }
            return base.CanUseItem(item);
        }

        //Might as well add both just in case.
        public override bool CanShoot(Item item)
        {
            if(item.type==ModContent.ItemType<Items.EPC>()) {
                if (EPCCooldown>0 || !hasLetGo) {
                    return false;
                }
            }
            return base.CanShoot(item);
        }


    }
}
