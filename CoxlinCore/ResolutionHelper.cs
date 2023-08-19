/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System.Collections.Generic;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace CoxlinCore
{
    public static class ResolutionHelper
    {
        public static void SetClosest16By9Resolution()
        {
            var userResolution = Screen.currentResolution;
            var resolutions = Screen.resolutions;
            var validResolutions = new List<Resolution>();

            for (var i = 0; i < resolutions.Length; ++i)
            {
                var res = resolutions[i];
                if (Is16By9(res) && res.width <= 1920 && res.height <= 1080)
                {
                    validResolutions.Add(res);
                }
            }

            // Sort resolutions by how close they are to the user's resolution
            validResolutions.Sort((a, b) => 
                GetResolutionDistance(userResolution, a).CompareTo(ResolutionHelper.GetResolutionDistance(userResolution, b)));

            // Select the closest resolution or default to 1080p
            var selectedResolution = validResolutions.Count > 0 ? validResolutions[0] : new Resolution { width = 1920, height = 1080 };
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, true);
        }

        private static bool Is16By9(Resolution res)
        {
            return Mathf.Approximately((float)res.width / res.height, 16f / 9f);
        }

        private static int GetResolutionDistance(Resolution a, Resolution b)
        {
            int widthDifference = Mathf.Abs(a.width - b.width);
            int heightDifference = Mathf.Abs(a.height - b.height);
            return widthDifference + heightDifference;
        }
    }
}