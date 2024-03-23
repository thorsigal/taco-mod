using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections;



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
        public List<int> EPC1Projectiles = new List<int>();
        public List<int> EPC2Projectiles = new List<int>();
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
                int EPC1id = ((Projectiles.EPC2)Main.projectile[EPCChargeIndex].ModProjectile).enable(charge, epcShootPosition, epcVelocity);
                if(EPC1id == -1) {
                    EPC2Projectiles.Add(EPCChargeIndex);
                } else {
                    EPC1Projectiles.Add(EPC1id);
                }
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
            List<int> toRemove = new List<int>();
            foreach(int epc2 in EPC2Projectiles) {
                if(!Main.projectile[epc2].active) {
                    toRemove.Add(epc2);
                    continue;
                }
            }
            foreach(int removed in toRemove) {
                EPC2Projectiles.Remove(removed);
            }
            toRemove.Clear();
            foreach(int epc1 in EPC1Projectiles) {
                if(!Main.projectile[epc1].active) {
                    toRemove.Add(epc1);
                    continue;
                }
                foreach(int epc2 in EPC2Projectiles) {
                    Vector2 pos1 = Main.projectile[epc1].position;
                    Vector2 pos2 = Main.projectile[epc2].position;
                    if(pos1.Distance(pos2)<40f) {
                        Main.projectile[epc1].Kill();
                        ((Projectiles.EPC2)Main.projectile[epc2].ModProjectile).KillTCF();                
                    }
                }
            }
            foreach(int removed in toRemove) {
                EPC2Projectiles.Remove(removed);
            }
            toRemove.Clear();
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
