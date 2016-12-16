﻿/*
 *  QGEditor
 *  Copyright (c) 2014-2016 GuQiang - <guqiangjs@gmail.com>
 *  ALL RIGHTS RESERVED
*/

using System;
using System.Windows.Forms;

namespace QGEditors.WinForms.DEMO
{
    internal static class Program
    {
        #region Methods

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ButtonEditDemo());
            //Application.Run(new SplitContainerControlDemo());
        }

        #endregion
    }
}