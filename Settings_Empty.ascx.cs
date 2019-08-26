﻿using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System.Collections;
using DotNetNuke.Entities.Tabs;
using System.Collections.Generic;
using DotNetNuke.Security;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Security.Permissions;

namespace Playngo.Modules.ClientZone
{
    public partial class Settings_Empty : ModuleSettingsBase
    {

        /// <summary>
        /// 设置绑定的模块编号
        /// </summary>
        public Int32 Settings_ModuleID
        {
            get { return Settings["ClientZone_ModuleID"] != null ? Convert.ToInt32(Settings["ClientZone_ModuleID"]) : ModuleId; }
        }


        /// <summary>
        /// 设置绑定的页面编号
        /// </summary>
        public Int32 Settings_TabID
        {
            get { return Settings["ClientZone_TabID"] != null ? Convert.ToInt32(Settings["ClientZone_TabID"]) : TabId; }
        }

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    BindModules();
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// 绑定模块
        /// </summary>
        private void BindModules()
        {
            DesktopModuleInfo objDesktopModuleInfo = DesktopModuleController.GetDesktopModuleByModuleName("Playngo.ClientZone", PortalId);


            if ((objDesktopModuleInfo != null))
            {
                TabController objTabController = new TabController();
                ArrayList objTabs = objTabController.GetTabsByPortal(PortalId).ToArrayList();
                foreach (DotNetNuke.Entities.Tabs.TabInfo objTab in objTabs)
                {
                    if ((objTab != null))
                    {
                        if ((objTab.IsDeleted == false))
                        {
                            ModuleController objModules = new ModuleController();
                            foreach (KeyValuePair<int, ModuleInfo> pair in objModules.GetTabModules(objTab.TabID))
                            {
                                ModuleInfo objModule = pair.Value;
                                if ((objModule.IsDeleted == false))
                                {
                                    if ((objModule.DesktopModuleID == objDesktopModuleInfo.DesktopModuleID))
                                    {
                                        if (ModulePermissionController.CanEditModuleContent(objModule) & objModule.IsDeleted == false)
                                        {
                                            string strPath = objTab.TabName;
                                            TabInfo objTabSelected = objTab;
                                            while (objTabSelected.ParentId != Null.NullInteger)
                                            {
                                                objTabSelected = objTabController.GetTab(objTabSelected.ParentId, objTab.PortalID, false);
                                                if ((objTabSelected == null))
                                                {
                                                    break; // TODO: might not be correct. Was : Exit While
                                                }
                                                strPath = objTabSelected.TabName + " -> " + strPath;
                                            }

                                            ListItem objListItem = new ListItem();

                                            objListItem.Value = objModule.TabID.ToString() + "-" + objModule.ModuleID.ToString();
                                            objListItem.Text = strPath + " -> " + objModule.ModuleTitle;

                                            ddlModule.Items.Add(objListItem);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                WebHelper.SelectedListByValue(ddlModule, String.Format("{0}-{1}", Settings_TabID, Settings_ModuleID));

            }
        }





        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                ModuleController objModuleController = new ModuleController();
                

                if ((ddlModule.Items.Count > 0))
                {
                    string[] values = ddlModule.SelectedValue.Split(Convert.ToChar("-"));

                    if (values.Length == 2)
                    {
                        objModuleController.UpdateModuleSetting(this.ModuleId, "ClientZone_TabID", values[0]);
                        objModuleController.UpdateModuleSetting(this.ModuleId, "ClientZone_ModuleID", values[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}