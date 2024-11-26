using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class Locator
    {
        private static IDataService data;
        public static IDataService Data
        {
            get => data;
            set
            {
                data = value;
            }
        }

        private static IAdsService ads;
        public static IAdsService Ads
        {
            get => ads;
            set
            {
                ads = value;
            }
        }
        private static IAudioService audio;
        public static IAudioService Audio
        {
            get => audio;
            set
            {
                audio = value;
            }
        }

        private static ILevelService level;
        public static ILevelService Level
        {
            get => level;
            set
            {
                level = value;
            }
        }

        private static IGameplayService gameplay;
        public static IGameplayService Gameplay
        {
            get => gameplay;
            set
            {
                gameplay = value;
            }
        }
        private static IIAPService iAPService;
        public static IIAPService IAPService
        {
            get => iAPService;
            set
            {
                iAPService = value;
            }
        }
    }
}