﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using EnvDTE;
using System.Linq;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System.IO;
using Microsoft.VisualStudio;

namespace MadsKristensen.FileNesting
{
    static class NestButton
    {
        private static IEnumerable<ProjectItem> _items;

        public static void Register(MenuCommandService mcs)
        {
            CommandID nestId = new CommandID(GuidList.guidFileNestingCmdSet, (int)PkgCmdIDList.cmdNest);
            OleMenuCommand menuNest = new OleMenuCommand(Nest, nestId);
            mcs.AddCommand(menuNest);
            menuNest.BeforeQueryStatus += BeforeNest;
        }

        private static void BeforeNest(object sender, EventArgs e)
        {
            var button = (OleMenuCommand)sender;
            _items = Helpers.GetSelectedItems().Where(i => i.Kind == VSConstants.ItemTypeGuid.PhysicalFile_string);
            button.Enabled = _items.Any();
        }

        private static void Nest(object sender, EventArgs e)
        {
            ManualNester.Nest(_items);
        }
    }
}