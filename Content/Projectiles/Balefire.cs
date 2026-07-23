using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrasic.Content.Projectiles
{
    public class Balefire : ModProjectile
    {
    public override void SetDefaults()
    {
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.aiStyle = 23;
        Projectile.friendly = true;
        Projectile.alpha = (int) byte.MaxValue;
        Projectile.penetrate = 50;
        Projectile.extraUpdates = 2;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.scale = 3;
    }
    }
}
