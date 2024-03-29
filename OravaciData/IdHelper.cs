﻿namespace OravaciData
{
    public static class IdHelper
    {
        public static string SimpleId(this string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var split = id.Split('/');
                if (split.Length == 2)
                {
                    return split[1];
                }
            }
            return string.Empty;
        }
    }
}
