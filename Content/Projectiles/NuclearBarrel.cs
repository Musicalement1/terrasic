using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrasic.Content.Projectiles
{
    public class NuclearBarrel : ModProjectile
    {
        private const int ExplosionPower = 1000;
        public override void SetStaticDefaults()
        {

        }
        public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;

        Vector2 origin = new Vector2(8f, 8f);//centrer le bail

        Main.EntitySpriteDraw(
            texture,
            Projectile.Center - Main.screenPosition,
            null,
            lightColor,
            Projectile.rotation,
            origin,
            Projectile.scale,
            SpriteEffects.None,
            0);

        return false;
    }
        


        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.hostile = true;

            Projectile.DamageType = DamageClass.Ranged;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 5*60; // (60 ticks = 1 sec)

            Projectile.aiStyle = 0;

            Projectile.scale = 3;
        }
        
		public override void PrepareBombToBlow() {
			Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the bomb explodes on slopes.
			Projectile.alpha = 255; // Set to transparent. This projectile technically lives as transparent for about 3 frames

			// Change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
			Projectile.Resize(ExplosionPower, ExplosionPower);

			Projectile.damage = 6000; // Bomb: 100, Dynamite: 250
			Projectile.knockBack = ExplosionPower/20; // Bomb: 8f, Dynamite: 10f
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
			// Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
			if (Main.expertMode) {
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail) {
					modifiers.FinalDamage /= 5;
				}
			}
		}

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3) {
				Projectile.PrepareBombToBlow(); // Get ready to explode.
			}

            Projectile.velocity.Y += 0.5f;


            Projectile.rotation += Projectile.velocity.X * 0.14f;


            Dust.NewDust(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                DustID.Smoke
            );
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Petit rebond
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X * 0.65f;

            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y * 0.65f;

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            

			// Play explosion sound
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 50; i++) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
				dust.velocity *= 1.4f;
			}

			// Fire Dust spawn
			for (int i = 0; i < 80; i++) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
				dust.noGravity = true;
				dust.velocity *= 5f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
				dust.velocity *= 3f;
			}

			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++) {
				var goreSpawnPosition = new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f);
				Gore gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X += 1.5f;
				gore.velocity.Y += 1.5f;
				gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X -= 1.5f;
				gore.velocity.Y += 1.5f;
				gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X += 1.5f;
				gore.velocity.Y -= 1.5f;
				gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X -= 1.5f;
				gore.velocity.Y -= 1.5f;
			}

			// Finally, actually explode the tiles and walls. Run this code only for the owner
			if (Projectile.owner == Main.myPlayer) {
				int explosionRadius = ExplosionPower/40; // Bomb: 4, Dynamite: 7, Explosives & TNT Barrel: 10
				int minTileX = (int)(Projectile.Center.X / 16f - explosionRadius);
				int maxTileX = (int)(Projectile.Center.X / 16f + explosionRadius);
				int minTileY = (int)(Projectile.Center.Y / 16f - explosionRadius);
				int maxTileY = (int)(Projectile.Center.Y / 16f + explosionRadius);

				// Ensure that all tile coordinates are within the world bounds
				Utils.ClampWithinWorld(ref minTileX, ref minTileY, ref maxTileX, ref maxTileY);

				// These 2 methods handle actually mining the tiles and walls while honoring tile explosion conditions
				bool explodeWalls = Projectile.ShouldWallExplode(Projectile.Center, explosionRadius, minTileX, maxTileX, minTileY, maxTileY);
				Projectile.ExplodeTiles(Projectile.Center, explosionRadius, minTileX, maxTileX, minTileY, maxTileY, explodeWalls);
            }
        }
    }
}