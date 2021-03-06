﻿// QGEditor
// Copyright (c) 2014-2016 GuQiang - <guqiangjs@gmail.com>
// ALL RIGHTS RESERVED

#region

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;

#endregion

namespace QGEditors.WinForms {
    internal static class ObjectHelper {
        #region Methods

        internal static int ToInt32(this float value) {
            return Convert.ToInt32(Math.Round(value));
        }

        internal static string GetObjectText(object obj) {
            return GetObjectText(obj, false);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        internal static string GetObjectText(object obj, bool includeSubObjects) {
            var str = string.Empty;
            try {
                var properties = TypeDescriptor.GetProperties(obj);
                foreach (PropertyDescriptor descriptor in properties) {
                    object obj2;
                    if (
                        !(descriptor.IsBrowsable &&
                          descriptor.SerializationVisibility != DesignerSerializationVisibility.Hidden))
                        continue;
                    if (descriptor.SerializationVisibility == DesignerSerializationVisibility.Content) {
                        if (includeSubObjects) {
                            obj2 = descriptor.GetValue(obj);
                            var str2 = obj2 != null ? obj2.ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(str2)) {
                                if (str.Length > 0)
                                    str = str + ", ";
                                str = str +
                                      string.Format(CultureInfo.InvariantCulture, "{0} = {{ {1} }}", descriptor.Name,
                                          str2);
                            }
                        }
                    }
                    else if (!descriptor.IsReadOnly && descriptor.ShouldSerializeValue(obj)) {
                        if (str.Length > 0)
                            str = str + ", ";
                        str = str + descriptor.Name;
                        obj2 = descriptor.GetValue(obj);
                        if (descriptor.PropertyType.Equals(typeof(string)))
                            str = str + string.Format(CultureInfo.InvariantCulture, " = '{0}'", obj2);
                        else
                            str = str + string.Format(CultureInfo.InvariantCulture, " = {0}", obj2);
                    }
                }
            }
            catch {
            }
            return str;
        }

        internal static SizeF GetTextSize(this string text, Graphics graphics, Font font) {
            if (!string.IsNullOrEmpty(text) && graphics != null && font != null) {
                graphics.PageUnit = GraphicsUnit.Pixel;
                return graphics.MeasureString(text, font);
            }
            return SizeF.Empty;
        }

        #endregion
    }
}