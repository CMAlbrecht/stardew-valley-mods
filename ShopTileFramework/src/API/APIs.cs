﻿using ShopTileFramework.src.API;
using StardewModdingAPI;

namespace ShopTileFramework.API
{
    /// <summary>
    /// This class is used to register external APIs and hold the instances of those APIs to be accessed
    /// by the rest of the mod
    /// </summary>
    class APIs
    {
        internal static IJsonAssetsApi JsonAssets;
        internal static IBFAVApi BFAV;
        internal static IConditionsApi Conditions;
        internal static ICustomFurnitureApi CustomFurniture;

        /// <summary>
        /// Register the API for Json Assets
        /// </summary>
        public static void RegisterJsonAssets()
        {
            // Boots were added to the JA API at the end of 2020, but we may not be dealing with that version.
            // Use the older version of the API as a fall-back.
            try
            {
                JsonAssets = ModEntry.helper.ModRegistry.GetApi<IJsonAssetsApiWithBoots>("spacechase0.JsonAssets");
            }
            catch
            {
                JsonAssets = ModEntry.helper.ModRegistry.GetApi<IJsonAssetsApi>("spacechase0.JsonAssets");
            }

            if (JsonAssets == null)
            {
                ModEntry.monitor.Log("Json Assets API not detected. Custom JA items will not be added to shops.",
                    LogLevel.Info);
            }

        }

        /// <summary>
        /// Registers the API for Better Farm Animal Variety, and check if it has been disabled in the user's options.
        /// If so, set it to null
        /// </summary>
        public static void RegisterBFAV()
        {
            BFAV = ModEntry.helper.ModRegistry.GetApi<IBFAVApi>("Paritee.BetterFarmAnimalVariety");

            if (BFAV == null)
            {
                ModEntry.monitor.Log("BFAV API not detected. Custom farm animals will not be added to animal shops.",
                    LogLevel.Info);
            }
            else if (!BFAV.IsEnabled())
            {
                BFAV = null;
                ModEntry.monitor.Log("BFAV is installed but not enabled. Custom farm animals will not be added to animal shops.",
                    LogLevel.Info);
            }
        }

        /// <summary>
        /// Register the API for Expanded Preconditions Utility
        /// </summary>
        public static void RegisterExpandedPreconditionsUtility()
        {
            Conditions = ModEntry.helper.ModRegistry.GetApi<IConditionsApi>("Cherry.ExpandedPreconditionsUtility");

            if (Conditions == null)
            {
                ModEntry.monitor.Log("Expanded Preconditions Utility API not detected. Something went wrong, please check that your installation of Expanded Preconditions Utility is valid",
                    LogLevel.Error);
                return;
            }

            Conditions.Initialize(ModEntry.VerboseLogging, "Cherry.ShopTileFramework");

        }

        /// <summary>
        /// Register the API for Custom Furniture
        /// </summary>
        public static void RegisterCustomFurniture()
        {
            CustomFurniture = ModEntry.helper.ModRegistry.GetApi<ICustomFurnitureApi>("Platonymous.CustomFurniture");

            if (CustomFurniture == null)
            {
                ModEntry.monitor.Log("Custom Furniture API not detected. Custom furniture will not be added to shops.",
                    LogLevel.Info);
            }

        }

        public static void RegisterFAVR()
        {
            //TODO: when FAVR is released, start deprecating support for BFAV
        }

    }
}
