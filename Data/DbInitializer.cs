using Project_WebDev.Models;

namespace Project_WebDev.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            if (context.Items.Any())
            {
                return;
            }

            var items = new Item[]
            {
                // GUNS
                // Assault Rifles
                new Item { Type = "AEG-Assault-Rifle", Name = "AK-12 Mosfet Enchanced", Price = 483.90, Brand = "Acturus", State = "Featured", Image = "images/ak12k-mosfet.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "Custom M4", Price = 799.90, Brand = "AirsoftShop", State = "Featured", Image = "images/custom-m4.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "P90 Plus", Price = 449.90, Brand = "Tokyo Marui", State = "Featured", Image = "images/p90-black.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "G12V", Price = 249.90, Brand = "Specna Arms", State = "ShopSold", Image = "images/sa-g12v.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "J08", Price = 299.90, Brand = "Specna Arms", State = "ShopSold", Image = "images/sa-j08.webp" },
                new Item { Type = "AEG-Assault-Rifle", Name = "PP19", Price = 369.90, Brand = "Acturus", State = "ShopSold", Image = "images/pp19-1-mosfet.webp" },

                // Gas Pistols
                new Item { Type = "Gas-Pistol", Name = "P09", Price = 159.99, Brand = "ASG(CZ)", State = "ShopSold", Image = "images/p-09-gbb-tan.webp" },
                new Item { Type = "Gas-Pistol", Name = "John Wick 4 Viper", Price = 279.90, Brand = "EMG", State = "ShopSold", Image = "images/john-wick-4-pit-viper-gbb.webp"},
                new Item { Type = "Gas-Pistol", Name = "Exa GBB", Price = 199.90, Brand = "RWA", State = "ShopSold", Image = "images/agency-arms-exa-gbb.webp"},
                new Item { Type = "Gas-Pistol", Name = "VX0200", Price = 214.90, Brand = "Armorer Works", State = "ShopSold", Image = "images/vx0200-hex-cut-gbb.webp"},
                new Item { Type = "Gas-Pistol", Name = "HX1003", Price = 204.90, Brand = "Armorer Works", State = "ShopSold", Image = "images/hx1003-gbb.webp"},
                new Item { Type = "Gas-Pistol", Name = "HX2701", Price = 209.90, Brand = "Armorer Works", State = "ShopSold", Image = "images/hx2701-51-operator-gbb.webp"},
                new Item { Type = "Gas-Pistol", Name = "Custom HiCapa-51", Price = 499.90, Brand = "Airsoftshop", State = "ShopSold", Image = "images/custom-tm-hi-capa-51-silver.webp"},
                new Item { Type = "Gas-Pistol", Name = "CFS-GNBB", Price = 79.90, Brand = "Tokyo Marui", State = "ShopSold", Image = "images/curve-fixed-slide-gnbb.webp"},
                new Item { Type = "Gas-Pistol", Name = "Silencero Maxim", Price = 304.90, Brand = "Krytac", State = "ShopSold", Image = "images/silencerco-maxim-9-gbb.webp"},

                // HPA Guns
                new Item { Type = "HPA-Gun", Name = "Article 1", Price = 499.90, Brand = "Heretic Labs", State = "ShopSold", Image = "images/article-1-heretic-labs.webp" },

                // Snipers
                new Item { Type = "Sniper", Name = "SSG96 mk2", Price = 399.90, Brand = "Novritsch", State = "ShopSold", Image = "images/ssg96-mk2.webp" },
                new Item { Type = "Sniper", Name = "Storm PC1", Price = 699.90, Brand = "STORM", State = "ShopSold", Image = "images/storm-pc1-storm.webp"},
                new Item { Type = "Sniper", Name = "VSR 10", Price = 229.90, Brand = "Tokyo Marui", State = "ShopSold", Image = "images/vsr-10-tm.webp"},
                new Item { Type = "Sniper", Name = "L96 AWS", Price = 339.90, Brand = "Tokyo Marui", State = "ShopSold", Image = "images/l96-aws.webp"},

                // Shotguns
                new Item { Type = "Shotgun", Name = "M56b Short", Price = 59.90, Brand = "DE", State = "ShopSold", Image = "images/m56b-shorty.webp" },
                new Item { Type = "Shotgun", Name = "CM366", Price = 159.90, Brand = "Cyma", State = "ShopSold", Image = "images/cm366-shotgun.webp" },
                new Item { Type = "Shotgun", Name = "CM367", Price = 139.90, Brand = "Cyma", State = "ShopSold", Image = "images/cm367-shotgun.webp" },
                new Item { Type = "Shotgun", Name = "CM356", Price = 134.90, Brand = "Cyma", State = "ShopSold", Image = "images/cm356-shotgun.webp" },
                new Item { Type = "Shotgun", Name = "M870 Tactical", Price = 399.90, Brand = "Tokyo Marui", State = "ShopSold", Image = "images/m870-tactical-gas.webp" },

                // AMMO & SUPPLIES
                // BBs
                new Item { Type = "BBs", Name = "Bio - 5 packs - 0.20g", Price = 75, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-5-pack-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 5 packs - 0.23g", Price = 75, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-5-pack-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 5 packs - 0.25g", Price = 75, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-5-pack-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 5 packs - 0.28g", Price = 75, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-5-pack-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 5 packs - 0.30g", Price = 75, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-5-pack-020g.webp" },

                new Item { Type = "BBs", Name = "Bio - 3200rds - 0.20g", Price = 14, Brand = "Colt", State = "ShopSold", Image = "images/bio-bb-3200rds-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 3200rds - 0.23g", Price = 14, Brand = "Colt", State = "ShopSold", Image = "images/bio-bb-3200rds-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 3200rds - 0.25g", Price = 14, Brand = "Colt", State = "ShopSold", Image = "images/bio-bb-3200rds-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 3200rds - 0.28g", Price = 14, Brand = "Colt", State = "ShopSold", Image = "images/bio-bb-3200rds-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - 3200rds - 0.30g", Price = 14, Brand = "Colt", State = "ShopSold", Image = "images/bio-bb-3200rds-020g.webp" },

                new Item { Type = "BBs", Name = "Bio - Bottle - 0.20g", Price = 15, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-bottle-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Bottle - 0.23g", Price = 15, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-bottle-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Bottle - 0.25g", Price = 15, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-bottle-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Bottle - 0.28g", Price = 15, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-bottle-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Bottle - 0.30g", Price = 15, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-bottle-020g.webp" },

                new Item { Type = "BBs", Name = "Bio - Box - 0.20g", Price = 300, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-box-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Box - 0.23g", Price = 300, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-box-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Box - 0.25g", Price = 300, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-box-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Box - 0.28g", Price = 300, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-box-020g.webp" },
                new Item { Type = "BBs", Name = "Bio - Box - 0.30g", Price = 300, Brand = "Xtreme Precision", State = "ShopSold", Image = "images/bio-bb-box-020g.webp" },

                // Gas
                new Item { Type = "Gas", Name = "Gas - 570ml", Price = 10, Brand = "Ultrair", State = "ShopSold", Image = "images/ultrair-gas-570ml.webp" },
                new Item { Type = "Gas", Name = "Gas - mini", Price = 8.60, Brand = "NUPROL", State = "ShopSold", Image = "images/nuprol-mini-green.webp" },
                new Item { Type = "Gas", Name = "30 Premium", Price = 18.90, Brand = "NUPROL", State = "ShopSold", Image = "images/nuprol-30-premium-gas.webp" },
                new Item { Type = "Gas", Name = "Portable Gas", Price = 34.90, Brand = "Novritsch", State = "ShopSold", Image = "images/novritsch-portable-gas-container.webp" },

                // Batteries
                 new Item { Type = "Battery", Name = "LiPo - 11.1V - 1000 mAh", Price = 29.90, Brand = "VB Power", State = "ShopSold", Image = "images/vb-power-lipo-111v-1000mah-20c-stick-type.webp" },
                 new Item { Type = "Battery", Name = "LiPo - 11.1V - 1000 mAh", Price = 23.90, Brand = "NimRod", State = "ShopSold", Image = "images/nimrod-lipo-111v-1000mah-25c-split-type-tamiya.webp" },
                 new Item { Type = "Battery", Name = "LiPo - 11.1V - 3000 mAh", Price = 44.90, Brand = "Titan Power", State = "ShopSold", Image = "images/titan-power-111v-3000mah-li-ion-battery-nunchuck-t.webp" },
                 new Item { Type = "Battery", Name = "LiPo - 7.4V - 1600 mAh", Price = 29.90, Brand = "VB Power", State = "ShopSold", Image = "images/vb-power-lipo-74v-1600mah-20c-mini-type-tamiya.webp" },
                 new Item { Type = "Battery", Name = "LiPo - 7.4V - 2000 mAh", Price = 27.90, Brand = "NimRod", State = "ShopSold", Image = "images/nimrod-lipo-74v-2000mah-25c-twin-type-deans.webp" },
                 new Item { Type = "Battery", Name = "LiPo - 9.6V - 1600 mAh", Price = 29.90, Brand = "VB Power", State = "ShopSold", Image = "images/vb-power-96v-1600mah-nunchuck-type.webp" },
                 new Item { Type = "Battery", Name = "LiPo - 8.4V - 1600 mAh", Price = 24.90, Brand = "VB Power", State = "ShopSold", Image = "images/vb-power-84v-1600mah-mini-type.webp" },

                 // GUN ACCESSOIRES
                 // Mags
                 new Item { Type = "Mags", Name = "LowCap - 60rds", Price = 23.90, Brand = "G&G", State = "ShopSold", Image = "images/g-g-arp-9-lowcap-60rds.webp" },
                 new Item { Type = "Mags", Name = "MidCap - 170rds", Price = 44.90, Brand = "G&G", State = "ShopSold", Image = "images/g-g-arp-9-midcap-170rds.webp" },
                 new Item { Type = "Mags", Name = "M4 MidCap - 230rds", Price = 21.90, Brand = "Hexmag", State = "ShopSold", Image = "images/hexmag-m4-aeg-polymer-midcap-230rds-black.webp" },
                 new Item { Type = "Mags", Name = "M4 MidCap - 160rds", Price = 14.90, Brand = "E&C", State = "ShopSold", Image = "images/e-c-m4-metal-midcap-160rds-black.webp" },
                 new Item { Type = "Mags", Name = "M4 MidCap - 150rds", Price = 19.90, Brand = "Pirate Arms", State = "ShopSold", Image = "images/pirate-arms-m4-m16-midcap-150rds-tan.webp" },
                 new Item { Type = "Mags", Name = "M4 Drum - 2300rds", Price = 76.90, Brand = "G&G", State = "ShopSold", Image = "images/g-g-drum-mag-m4-2300rds.webp" },
                 new Item { Type = "Mags", Name = "AK12 HighCap - 550rds", Price = 18.90, Brand = "Acturus", State = "ShopSold", Image = "images/arcturus-ak12-highcap-550rds.webp" },
                 new Item { Type = "Mags", Name = "AK HighCap - 430rds", Price = 16.90, Brand = "Specna Arms", State = "ShopSold", Image = "images/specna-arms-ak-sa-j-s-mag-highcap-430rds-black.webp" },

                 // Optics
                 new Item { Type = "Optics", Name = "Crossfire - Red Dot", Price = 179.90, Brand = "Vortex Optics", State = "ShopSold", Image = "images/vortex-optics-crossfire-red-dot-led-upgrade.webp" },
                 new Item { Type = "Optics", Name = "11mm - Red Dot", Price = 89.90, Brand = "Swiss Arms", State = "ShopSold", Image = "images/swiss-arms-1x22x33-airgun-11mm-red-dot-sight-dovet.webp" },
                 new Item { Type = "Optics", Name = "Magnifier Scope", Price = 61.90, Brand = "Theta Optics", State = "ShopSold", Image = "images/theta-optics-3x35-magnifier-scope.webp" },
                 new Item { Type = "Optics", Name = "AOE Scope", Price = 85.90, Brand = "Theta Optics", State = "ShopSold", Image = "images/theta-optics-3-12x40-aoe-scope.webp" },
                 new Item { Type = "Optics", Name = "Fiber Optic Sight", Price = 16.90, Brand = "Umarex", State = "ShopSold", Image = "images/umarex-ux-exclusive-notos-fiber-optic-sight.webp" },
                 new Item { Type = "Optics", Name = "Protector", Price = 12.90, Brand = "ASG", State = "ShopSold", Image = "images/asg-sight-protector-with-mount.webp" },

                 // Grips
                 new Item { Type = "Grips", Name = "Forward Grip", Price = 15.90, Brand = "FMA", State = "ShopSold", Image = "images/fma-td-forward-grip-m-lok-black.webp" },
                 new Item { Type = "Grips", Name = "Bipod Grip", Price = 62.90, Brand = "Walther", State = "ShopSold", Image = "images/walther-tmb-i-tbp-ii-bipod.webp" },
                 new Item { Type = "Grips", Name = "Pistol Grip", Price = 7.90, Brand = "Specna Arms", State = "ShopSold", Image = "images/specna-arms-mp102-pistol-grip-black.webp" },

                 // TACTICAL GEAR
                 new Item { Type = "Tactical Gear", Name = "Medium Utility Pouch", Price = 36.90, Brand = "Warrior", State = "ShopSold", Image = "images/warrior-medium-utility-pouch-multicam.jpg" },
                 new Item { Type = "Tactical Gear", Name = "Spider Modular Plate Carrier", Price = 109.90, Brand = "Airsoftshop", State = "ShopSold", Image = "images/airsoftshop-spider-modular-plate-carrier-mpc-multi.webp" },
                 new Item { Type = "Tactical Gear", Name = "Thermal Goggles", Price = 44.90, Brand = "Lancer Tactical", State = "ShopSold", Image = "images/lancer-tactical-aero-thermal-goggles-olive-drab.webp" },
                 new Item { Type = "Tactical Gear", Name = "Disposable Glove Pouch", Price = 29.00, Brand = "5.11 Tactical", State = "ShopSold", Image = "images/511-tactical-flex-disposable-glove-pouch-black.webp" },
                 new Item { Type = "Tactical Gear", Name = "G36 Pouch", Price = 33.90, Brand = "Warrior", State = "ShopSold", Image = "images/warrior-double-covered-g36-pouch-multicam.webp" },
                 new Item { Type = "Tactical Gear", Name = "Compact Drop Leg Platform", Price = 34.90, Brand = "Cytac", State = "ShopSold", Image = "images/cytac-compact-drop-leg-platform-gen3-black.webp" },
                 new Item { Type = "Tactical Gear", Name = "Backpack Belt kit", Price = 20.00, Brand = "5.11 Tactical", State = "ShopSold", Image = "images/rush-backpack-belt-kit-ranger-green.webp" },
                 new Item { Type = "Tactical Gear", Name = "Zypher Combat Shoes", Price = 159.90, Brand = "LOWA", State = "ShopSold", Image = "images/lowa-zephyr-gtx-lo-tf-black.webp" },
                 new Item { Type = "Tactical Gear", Name = "G9 pro Walkie", Price = 109.90, Brand = "Midland", State = "ShopSold", Image = "images/midland-g9-pro.webp" },
            };

            context.Items.AddRange(items);
            context.SaveChanges();
        }
    }
}
