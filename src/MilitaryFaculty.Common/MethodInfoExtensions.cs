﻿using System;
using System.Reflection;

namespace MilitaryFaculty.Common
{
    public static class MemberInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this MemberInfo info)
            where TAttribute : Attribute
        {
            var attr = info.GetCustomAttribute<TAttribute>();
            return attr != null;
        }
    }
}