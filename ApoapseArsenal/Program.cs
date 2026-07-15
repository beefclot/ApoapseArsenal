using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Noggog;

namespace ApoapseArsenal
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddRunnabilityCheck(CheckRunnability)
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "SynthesisApoapseArsenal.esp")
                .Run(args);
        }

        private static readonly ModKey BDSModKey = ModKey.FromNameAndExtension("Apoapse's Arsenal.esp");
        private static readonly ModKey TelescopeModKey = ModKey.FromNameAndExtension("KWTelescope.esp");
        private static readonly ModKey PrimesFixesCollectionModKey = ModKey.FromNameAndExtension("Primes_Fixes_Collection.esp");
        private static readonly ModKey AccoutrementModKey = ModKey.FromNameAndExtension("Apoapse's Accoutrement.esp");

        

        public static void CheckRunnability(IRunnabilityState state)
        {
            if (!state.LoadOrder.ContainsKey(BDSModKey))
            {
                throw new ArgumentException("Apoapse's Arsenal.esp is required in the load order.");
            }
            if (!state.LoadOrder.ContainsKey(TelescopeModKey))
            {
                throw new ArgumentException("KWTelescope.esp is required in the load order.");
            }
            if (!state.LoadOrder.ContainsKey(PrimesFixesCollectionModKey))
            {
                throw new ArgumentException("Primes_Fixes_Collection.esp is required in the load order.");
            }
            if (!state.LoadOrder.ContainsKey(AccoutrementModKey))
            {
                throw new ArgumentException("Apoapse's Accoutrement.esp is required in the load order.");
            }
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            //Your code here!
            Console.WriteLine("Somnium Guns and Weapons Patching v1.0.0");
            Console.WriteLine($"Beginning patching process...");

            // gun keywords
            var apo_rifle = FormKey.Factory("000849:Apoapse's Arsenal.esp"); // apo_rifle
            var apo_pistol = FormKey.Factory("00084A:Apoapse's Arsenal.esp"); // apo_pistol
            var apo_blunderbuss = FormKey.Factory("00084B:Apoapse's Arsenal.esp"); // apo_blunderbuss
            var apo_grenlauncher = FormKey.Factory("00084C:Apoapse's Arsenal.esp"); // apo_grenlauncher

            var WeapTypeGun = FormKey.Factory("000867:Apoapse's Arsenal.esp");
            var WeapTypeScythe = FormKey.Factory("000868:Apoapse's Arsenal.esp");
            var WeapTypeJavelin = FormKey.Factory("000869:Apoapse's Arsenal.esp");
            var WeapTypeWhip = FormKey.Factory("00086A:Apoapse's Arsenal.esp");

            var MagicDisallowEnchanting = FormKey.Factory("0C27BD:Skyrim.esm");

            // accoutrement keywords
            var apo_1h_sabre = FormKey.Factory("00080A:Apoapse's Accoutrement.esp");
            var apo_2h_sabre = FormKey.Factory("00080B:Apoapse's Accoutrement.esp");
            var apo_1h_rapier = FormKey.Factory("00080D:Apoapse's Accoutrement.esp");
            var apo_2h_rapier = FormKey.Factory("00080C:Apoapse's Accoutrement.esp");
            var apo_1h_straight = FormKey.Factory("00080F:Apoapse's Accoutrement.esp");
            var apo_2h_straight = FormKey.Factory("00080E:Apoapse's Accoutrement.esp");
            var apo_long = FormKey.Factory("001757:Apoapse's Accoutrement.esp");
            var apo_short = FormKey.Factory("001758:Apoapse's Accoutrement.esp");
            var apo_1h_katana = FormKey.Factory("00175B:Apoapse's Accoutrement.esp");
            var apo_2h_katana = FormKey.Factory("00175C:Apoapse's Accoutrement.esp");

            var WeapTypeKatana = FormKey.Factory("001754:Apoapse's Accoutrement.esp");
            var WeapTypeRapier = FormKey.Factory("001755:Apoapse's Accoutrement.esp");
            var WeapTypeQtrStaff = FormKey.Factory("001767:Apoapse's Accoutrement.esp");

            var apo_rapier = FormKey.Factory("001774:Apoapse's Accoutrement.esp");
            var apo_straight = FormKey.Factory("001775:Apoapse's Accoutrement.esp");
            var apo_curved = FormKey.Factory("001776:Apoapse's Accoutrement.esp");

            foreach (var weapon in state.LoadOrder.PriorityOrder.Weapon().WinningOverrides())
            {
                if (weapon.Model != null)
                {
                    // rifle
                    if (weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_rifle)))
                    {
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);
                        if (newWeapon.Data != null)
                        {
                            newWeapon.Data.Skill = Skill.Archery;
                            if (newWeapon.Keywords == null)
                                newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                            newWeapon.Keywords.Add(WeapTypeGun);
                            newWeapon.Keywords.Add(MagicDisallowEnchanting);
                            Console.WriteLine($"Rifle: {weapon.Name?.String ?? "Unnamed Weapon"}");
                        }

                        continue;
                    }

                    // blunderbuss
                    if (weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_blunderbuss)))
                    {
                        //Console.WriteLine($"Weapon: {weapon.Name?.String ?? "Unnamed Weapon"}");
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                        if (newWeapon.Data != null)
                        {
                            newWeapon.Data.Skill = Skill.Archery;
                            if (newWeapon.Keywords == null)
                                newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                            newWeapon.Keywords.Add(WeapTypeWhip);
                            newWeapon.Keywords.Add(MagicDisallowEnchanting);
                            Console.WriteLine($"Blunderbuss: {weapon.Name?.String ?? "Unnamed Weapon"}");
                        }

                        continue;
                    }

                    // grenade launcher
                    if (weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_grenlauncher)))
                    {
                        //Console.WriteLine($"Weapon: {weapon.Name?.String ?? "Unnamed Weapon"}");
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                        if (newWeapon.Data != null)
                        {
                            newWeapon.Data.Skill = Skill.Archery;
                            if (newWeapon.Keywords == null)
                                newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                            newWeapon.Keywords.Add(WeapTypeJavelin);
                            newWeapon.Keywords.Add(MagicDisallowEnchanting);
                            Console.WriteLine($"Grenade Launcher:   {weapon.Name?.String ?? "Unnamed Weapon"}");
                        }
                        continue;
                    }

                    // pistols
                    if (weapon.Data != null && weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_pistol)))
                    {
                        //Console.WriteLine($"Weapon: {weapon.Name?.String ?? "Unnamed Weapon"}");
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                        if (newWeapon.Data != null)
                        {
                            newWeapon.Data.Skill = Skill.OneHanded;
                            // Ensure Keywords is not null before adding
                            if (newWeapon.Keywords == null)
                                newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                            newWeapon.Keywords.Add(WeapTypeScythe);
                            newWeapon.Keywords.Add(MagicDisallowEnchanting);
                            Console.WriteLine($"Pistol:     {weapon.Name?.String ?? "Unnamed Weapon"}");

                        }
                        continue;
                    }

                    // one handed melee weapons
                    // Need to check if weapon does not have the apo_pistol keyword and has skill set to OneHanded -> set to TwoHanded
                    if (weapon.Data != null
                        && !weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_pistol))
                        && weapon.Data.Skill == Skill.OneHanded)
                    {
                        //Console.WriteLine($"Weapon: {weapon.Name?.String ?? "Unnamed Weapon"} has skill set to OneHanded but does not have apo_pistol keyword");
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                        // Ensure Keywords is not null before adding
                        if (newWeapon.Data == null)
                            newWeapon.Data = new WeaponData();

                        newWeapon.Data.Skill = Skill.TwoHanded;

                        //Console.WriteLine($"Weapon: {weapon.Name?.String ?? "Unnamed Weapon"} -> Two Handed Skill");
                    }

                    // Curved Swords and Katanas
                    if (weapon.Data != null)
                    {
                        var isCurvedSword = weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_1h_sabre))
                            || weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_2h_sabre));
                        var isKatana = weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_1h_katana))
                            || weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_2h_katana));

                        if (isCurvedSword || isKatana)
                        {
                            var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                            if (newWeapon.Keywords == null)
                                newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();

                            if (isKatana)
                            {
                                newWeapon.Keywords.Add(WeapTypeKatana);
                                Console.WriteLine($"Katana:         {weapon.Name?.String ?? "Unnamed Weapon"}");
                            }
                            else
                            {
                                newWeapon.Keywords.Add(apo_curved);
                                Console.WriteLine($"Curved Sword:   {weapon.Name?.String ?? "Unnamed Weapon"}");
                            }

                            continue;
                        }
                    }

                    // Rapiers
                    if (weapon.Data != null
                        && (weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_1h_rapier))
                            || weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_2h_rapier)))
                        )
                    {
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                        if (newWeapon.Keywords == null)
                            newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                        newWeapon.Keywords.Add(WeapTypeRapier);
                        newWeapon.Keywords.Add(apo_rapier);
                        Console.WriteLine($"Rapier:         {weapon.Name?.String ?? "Unnamed Weapon"}");
                        continue;
                    }

                    // Straight Swords
                    if (weapon.Data != null
                        && (weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_1h_straight))
                            || weapon.Keywords.EmptyIfNull().Any(k => k.FormKey.Equals(apo_2h_straight)))
                        )
                    {
                        var newWeapon = state.PatchMod.Weapons.GetOrAddAsOverride(weapon);

                        if (newWeapon.Keywords == null)
                            newWeapon.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                        newWeapon.Keywords.Add(WeapTypeQtrStaff);
                        newWeapon.Keywords.Add(apo_straight);
                        Console.WriteLine($"Straight Sword: {weapon.Name?.String ?? "Unnamed Weapon"}");
                        continue;
                    }
                }
            }

            // Armor handling
            var ArmorShield = FormKey.Factory("0965B2:Skyrim.esm");
            var apo_key_telescope = FormKey.Factory("0073C9:KWTelescope.esp");

            foreach (var armor in state.LoadOrder.PriorityOrder.Armor().WinningOverrides())
            {
                if (armor.Keywords != null)
                    if (armor.MajorFlags.HasFlag(Armor.MajorFlag.Shield) && !armor.Keywords.Contains(apo_key_telescope))
                    {
                        var newArmor = state.PatchMod.Armors.GetOrAddAsOverride(armor);

                        newArmor.MajorFlags = newArmor.MajorFlags.SetFlag(Armor.MajorFlag.NonPlayable, true);
                        Console.WriteLine($"Armor: {armor.Name?.String ?? "Unnamed Armor"} is a shield");
                    }
            }

            foreach (var myNpc in state.LoadOrder.PriorityOrder.Npc().WinningOverrides())
            {
                if (myNpc.Weight > 90)
                {
                    var newNPC = state.PatchMod.Npcs.GetOrAddAsOverride(myNpc);

                    newNPC.Weight = 90;
                    Console.WriteLine($"Fixed NPC weight for {newNPC.Name?.String ?? "Unnamed NPC"}");
                }
            }

            // Cell handling - CURRENTLY DOES NOTHING
            var dungeon_hideout = FormKey.Factory("0B3674:Skyrim.esm");
            var dungeon_cave = FormKey.Factory("09CFDD:Skyrim.esm");
            var dungeon_crypts = FormKey.Factory("0B366F:Skyrim.esm");
            var dungeon_ice = FormKey.Factory("0E48AF:Skyrim.esm");
            var dungeon_undercity = FormKey.Factory("074A75:Skyrim.esm");
            var dungeon_undercity_int = FormKey.Factory("0EEE2D:Skyrim.esm");
            var dungeon_default = FormKey.Factory("01BECD:Skyrim.esm");
            var apo_eczn_dungeon = FormKey.Factory("00081D:Primes_Fixes_Collection.esp");

            // Define the set of valid music type EditorIDs for filtering
            var validMusicTypeEditorIDs = new HashSet<string>
            {
                "_00E_Music_Dungeon_Hideout",
                "_00E_Music_Dungeon_Cave",
                "_00E_Music_Dungeon_Crypts",
                "_00E_Music_Dungeon_Ice",
                "_00E_Music_Explore_Undercity",
                "_00E_Music_Explore_UndercityINT",
                "_00E_Music_Dungeon_Default"
            };

            foreach (var myCell in state.LoadOrder.PriorityOrder.Cell().WinningContextOverrides(state.LinkCache))
            {
                var musicLink = myCell.Record.Music;
                // Filter: skip if music type is null
                if (musicLink.IsNull)
                    continue;

                IMusicTypeGetter? resolvedMusicType;
                try
                {
                    // Resolve can throw MissingRecordException for links that point to missing records
                    resolvedMusicType = state.LinkCache.Resolve<IMusicTypeGetter>(musicLink);
                }
                catch (Mutagen.Bethesda.Plugins.Exceptions.MissingRecordException ex)
                {
                    Console.WriteLine($"Skipping cell {myCell.Record.FormKey}: missing music type {musicLink.FormKey} ({ex.Message})");
                    continue;
                }

                // If resolution failed for other reasons, skip
                if (resolvedMusicType == null)
                    continue;

                var editorId = resolvedMusicType.EditorID ?? string.Empty;
                if (!validMusicTypeEditorIDs.Contains(editorId))
                    continue;

                // Patch: set EncounterZone if not set
                if (myCell.Record.EncounterZone.IsNull)
                {
                    var overrideCell = myCell.GetOrAddAsOverride(state.PatchMod);
                    overrideCell.EncounterZone.SetTo(apo_eczn_dungeon);
                    Console.WriteLine($"Updated encounter zone for cell {overrideCell.Name?.String ?? "Unnamed Cell"}");
                }
            }

            // Spell keywords
            var apo_spell_elementalism = FormKey.Factory("00081E:Primes_Fixes_Collection.esp");
            var apo_spell_mentalism = FormKey.Factory("00081F:Primes_Fixes_Collection.esp");
            var apo_spell_entropy = FormKey.Factory("000820:Primes_Fixes_Collection.esp");
            var apo_spell_light = FormKey.Factory("000821:Primes_Fixes_Collection.esp");
            var apo_spell_psionics = FormKey.Factory("000822:Primes_Fixes_Collection.esp");

            // Half-cost perks
            // If spells have a half cost perk
            // Map half-cost perk FormKeys to spell types
            var halfCostPerkToSpellType = new Dictionary<FormKey, string>
            {
                // Elementalism (Destruction)
                { FormKey.Factory("0F2CA8:Skyrim.esm"), "Elementalism" }, // Novice Destruction
                { FormKey.Factory("0C44BF:Skyrim.esm"), "Elementalism" }, // Apprentice Destruction
                { FormKey.Factory("0C44C0:Skyrim.esm"), "Elementalism" }, // Adept Destruction
                { FormKey.Factory("0C44C1:Skyrim.esm"), "Elementalism" }, // Expert Destruction
                { FormKey.Factory("0C44C2:Skyrim.esm"), "Elementalism" }, // Master Destruction

                // Mentalism (Alteration)
                { FormKey.Factory("0F2CA6:Skyrim.esm"), "Mentalism" }, // Novice Alteration
                { FormKey.Factory("0C44B7:Skyrim.esm"), "Mentalism" }, // Apprentice Alteration
                { FormKey.Factory("0C44B8:Skyrim.esm"), "Mentalism" }, // Adept Alteration
                { FormKey.Factory("0C44B9:Skyrim.esm"), "Mentalism" }, // Expert Alteration
                { FormKey.Factory("0C44BA:Skyrim.esm"), "Mentalism" }, // Master Alteration

                // Entropy (Conjuration)
                { FormKey.Factory("0F2CA7:Skyrim.esm"), "Entropy" }, // Novice Conjuration
                { FormKey.Factory("0C44BB:Skyrim.esm"), "Entropy" }, // Apprentice Conjuration
                { FormKey.Factory("0C44BC:Skyrim.esm"), "Entropy" }, // Adept Conjuration
                { FormKey.Factory("0C44BD:Skyrim.esm"), "Entropy" }, // Expert Conjuration
                { FormKey.Factory("0C44BE:Skyrim.esm"), "Entropy" }, // Master Conjuration

                // Light (Restoration)
                { FormKey.Factory("0F2CAA:Skyrim.esm"), "Light" }, // Novice Restoration
                { FormKey.Factory("0C44C7:Skyrim.esm"), "Light" }, // Apprentice Restoration
                { FormKey.Factory("0C44C8:Skyrim.esm"), "Light" }, // Adept Restoration
                { FormKey.Factory("0C44C9:Skyrim.esm"), "Light" }, // Expert Restoration
                { FormKey.Factory("0C44CA:Skyrim.esm"), "Light" }, // Master Restoration

                // Psionics (Illusion)
                { FormKey.Factory("0F2CA9:Skyrim.esm"), "Psionics" }, // Novice Illusion
                { FormKey.Factory("0C44C3:Skyrim.esm"), "Psionics" }, // Apprentice Illusion
                { FormKey.Factory("0C44C4:Skyrim.esm"), "Psionics" }, // Adept Illusion
                { FormKey.Factory("0C44C5:Skyrim.esm"), "Psionics" }, // Expert Illusion
                { FormKey.Factory("0C44C6:Skyrim.esm"), "Psionics" }, // Master Illusion
            };

            foreach (var mySpell in state.LoadOrder.PriorityOrder.Spell().WinningOverrides())
            {
                var halfCostPerk = mySpell.HalfCostPerk.FormKey;
                if (!halfCostPerk.IsNull && halfCostPerkToSpellType.TryGetValue(halfCostPerk, out var spellType))
                {
                    var newSpell = state.PatchMod.Spells.GetOrAddAsOverride(mySpell);

                    if (newSpell.Keywords == null)
                        newSpell.Keywords = new ExtendedList<IFormLinkGetter<IKeywordGetter>>();

                    if (spellType == "Elementalism")
                        newSpell.Keywords.Add(apo_spell_elementalism);
                    else if (spellType == "Mentalism")
                        newSpell.Keywords.Add(apo_spell_mentalism);
                    else if (spellType == "Entropy")
                        newSpell.Keywords.Add(apo_spell_entropy);
                    else if (spellType == "Light")
                        newSpell.Keywords.Add(apo_spell_light);
                    else if (spellType == "Psionics")
                        newSpell.Keywords.Add(apo_spell_psionics);

                    Console.WriteLine($"Added {spellType} keyword to spell {mySpell.Name?.String ?? "Unnamed Spell"}");
                }
            }
        }
    }
}
