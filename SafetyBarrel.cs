using Newtonsoft.Json;
using ProtoBuf;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("Safety Barrel", "Dana", "1.0.0")]
    [Description("Be invincible like a superhero but with a barrel on your head.")]
    public class SafetyBarrel : RustPlugin
    {
        #region Fields

        private static SafetyBarrel _instance;
        private static Configuration _config;

        #endregion Fields

        #region Configuration

        private class Configuration
        {
            [JsonProperty("Version")]
            public string Version { get; set; }

            [JsonProperty(PropertyName = "Clothing")]
            public List<string> Clothing { get; set; }

            [JsonProperty(PropertyName = "Enable Damage Immunity")]
            public bool EnableDamageImmunity { get; set; }

            [JsonProperty(PropertyName = "Can Be Seen By NPC")]
            public bool CanBeSeenByNPC { get; set; }

            [JsonProperty(PropertyName = "Can Be Seen By Helicopter")]
            public bool CanBeSeenByHelicopter { get; set; }

            [JsonProperty(PropertyName = "Can Be Seen By Bradley")]
            public bool CanBeSeenByBradley { get; set; }

            [JsonProperty(PropertyName = "Can Be Seen By Trap")]
            public bool CanBeSeenByTrap { get; set; }
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            _config = Config.ReadObject<Configuration>();

            if (string.Compare(_config.Version, Version.ToString()) < 0)
                UpdateConfig();

            SaveConfig();
        }

        protected override void LoadDefaultConfig()
        {
            _config = GetDefaultConfig();
        }

        protected override void SaveConfig()
        {
            Config.WriteObject(_config, true);
        }

        private void UpdateConfig()
        {
            PrintWarning("Detected changes in configuration! Updating...");
            Configuration defaultConfig = GetDefaultConfig();

            if (string.Compare(_config.Version, "1.0.0") < 0)
                _config = defaultConfig;

            PrintWarning("Configuration update complete! Updated from version " + _config.Version + " to " + Version.ToString());
            _config.Version = Version.ToString();
        }

        private Configuration GetDefaultConfig()
        {
            return new Configuration
            {
                Version = Version.ToString(),
                EnableDamageImmunity = true,
                CanBeSeenByNPC = false,
                CanBeSeenByHelicopter = false,
                CanBeSeenByBradley = false,
                CanBeSeenByTrap = false,
                Clothing = new List<string>()
                {
                    "barrelcostume",
                    "cratecostume",
                    "ghostsheet",
                    "scarecrow.suit"
                }
            };
        }

        #endregion Configuration

        #region Oxide Hooks

        private void Init()
        {
            _instance = this;
            PermissionUtils.Register();
        }

        private void Unload()
        {
            _config = null;
            _instance = null;
        }

        private object OnEntityTakeDamage(BasePlayer player, HitInfo hitInfo)
        {
            if (VerifyPlayerAttire(player) && _config.EnableDamageImmunity)
                return true;

            return null;
        }

        private object OnNpcPlayerTarget(ScientistNPC npc, BasePlayer targetPlayer)
        {
            if (VerifyPlayerAttire(targetPlayer) && !_config.CanBeSeenByNPC)
                return false;

            return null;
        }

        private object OnNpcTarget(ScientistNPC npc, BasePlayer targetPlayer)
        {
            if (VerifyPlayerAttire(targetPlayer) && !_config.CanBeSeenByNPC)
                return false;

            return null;
        }

        private object CanBeTargeted(BasePlayer player)
        {
            if (VerifyPlayerAttire(player) && !_config.CanBeSeenByTrap)
                return false;

            return null;
        }
        
        private object CanBradleyApcTarget(BasePlayer player)
        {
            if (VerifyPlayerAttire(player) && !_config.CanBeSeenByBradley)
                return false;

            return null;
        }

        private object CanHelicopterTarget(BasePlayer targetPlayer)
        {
            if (VerifyPlayerAttire(targetPlayer) && !_config.CanBeSeenByHelicopter)
                return false;

            return null;
        }

        #endregion Oxide Hooks

        #region Helper Functions

        private bool VerifyPlayerAttire(BasePlayer player)
        {
            if (!player.IsValid() || !player.userID.IsSteamId() || player.IsNpc)
                return false;

            if (!PermissionUtils.Verify(player))
                return false;

            foreach (Item item in player.inventory.containerWear.itemList)
            {
                if (item == null)
                    continue;
                
                if (_config.Clothing.Contains(item.info.shortname))
                    return true;
            }

            return false;
        }

        #endregion Helper Functions

        #region Helper Classes

        private static class PermissionUtils
        {
            public const string USE = "safetybarrel.use";

            public static void Register()
            {
                _instance.permission.RegisterPermission(USE, _instance);
            }

            public static bool Verify(BasePlayer player, string permissionName = USE)
            {
                if (_instance.permission.UserHasPermission(player.UserIDString, permissionName))
                    return true;

                return false;
            }
        }

        #endregion Helper Classes
    }
}