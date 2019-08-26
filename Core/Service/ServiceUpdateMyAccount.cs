using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Playngo.Modules.ClientZone
{


    /// <summary>
    /// 更新用户账户信息的服务
    /// </summary>
    public class ServiceUpdateMyAccount : iService
    {
        public ServiceUpdateMyAccount()
        {
            IsResponseWrite = true;
        }


        public string ResponseString
        {
            get;
            set;
        }

        /// <summary>
        /// 是否写入输出
        /// </summary>
        public bool IsResponseWrite
        {
            get;
            set;
        }

        public void Execute(BasePage Context)
        {
            Dictionary<String, Object> jsonDicts = new Dictionary<String, Object>();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Common.UpdateDictionary(jsonDicts, "Message", "");

            Boolean Result = false;
            int UpdateResult = 0;
            var json = WebHelper.GetStringParam(Context.Request, "json", "");
            var UserItem = Context.UserInfo;

            if(UserItem != null && UserItem.UserID > 0)
            {
                ProfilePropertyDefinitionCollection profileProperties = new ProfilePropertyDefinitionCollection();

                var FirstName = WebHelper.GetStringParam(Context.Request, "FirstName", "");
                if (!String.IsNullOrEmpty(FirstName) && FirstName != UserItem.Profile.FirstName)
                {
                    UserItem.FirstName = FirstName;
                    UpdateResult++;
                }


                var LastName = WebHelper.GetStringParam(Context.Request, "LastName", "");
                if (!String.IsNullOrEmpty(LastName) && LastName != UserItem.Profile.LastName)
                {
                    UserItem.LastName = LastName;
                    UpdateResult++;
                }

                var Company = WebHelper.GetStringParam(Context.Request, "Company", "");
                if (!String.IsNullOrEmpty(Company) && Company != UserItem.Profile.GetPropertyValue("Company"))
                {
                    //DotNetNuke.Entities.Profile.ProfileController.UpdateUserProfile()
                    //UserItem.Profile["Company"] = Company;
                    UserItem.Profile.SetProfileProperty("Company", Company);
                    UpdateResult++;
                }


                var Newsletter_GameSheets = WebHelper.GetStringParam(Context.Request, "Newsletter_GameSheets", "");
                if (Newsletter_GameSheets != UserItem.Profile.GetPropertyValue("Newsletter_GameSheets").ToLower())
                {
                    UserItem.Profile.SetProfileProperty("Newsletter_GameSheets", ConvertBool(Newsletter_GameSheets));
                    UpdateResult++;
                }


                var Newsletter_Downloads = WebHelper.GetStringParam(Context.Request, "Newsletter_Downloads", "");
                if (Newsletter_Downloads != UserItem.Profile.GetPropertyValue("Newsletter_Downloads").ToLower())
                {
                    UserItem.Profile.SetProfileProperty("Newsletter_Downloads", ConvertBool(Newsletter_Downloads));
                    UpdateResult++;
                }



                var Newsletter_Campaigns = WebHelper.GetStringParam(Context.Request, "Newsletter_Campaigns", "");
                if (Newsletter_Campaigns != UserItem.Profile.GetPropertyValue("Newsletter_Campaigns").ToLower())
                {
                    UserItem.Profile.SetProfileProperty("Newsletter_Campaigns", ConvertBool(Newsletter_Campaigns));
                    UpdateResult++;
                }



                var Newsletter_Events = WebHelper.GetStringParam(Context.Request, "Newsletter_Events", "");
                if (Newsletter_Events != UserItem.Profile.GetPropertyValue("Newsletter_Events").ToLower())
                {
                    UserItem.Profile.SetProfileProperty("Newsletter_Events", ConvertBool(Newsletter_Events));
                    UpdateResult++;
                }


                if (UpdateResult > 0)
                {
                    DataCache.ClearPortalCache(Context.PortalId, true);
                    DataCache.ClearCache();


                    UserController.UpdateUser(Context.PortalId, UserItem);

                    ProfileController.UpdateUserProfile(UserItem);

                    Common.UpdateDictionary(jsonDicts, "Message", "Update user account success!");
                    Result = true;
                }
                else
                {
                    Result = false;
                    Common.UpdateDictionary(jsonDicts, "Message", "User Profile no change.");
                }


                var Password = WebHelper.GetStringParam(Context.Request, "Password", "");
                if (!String.IsNullOrEmpty(Password))
                {
                    if (UserController.ValidatePassword(Password))
                    {
                        string resetPassword = UserController.ResetPassword(UserItem, String.Empty);

                        if (UserController.ChangePassword(UserItem, resetPassword, Password))
                        {
                            //DataCache.ClearPortalCache(UserItem.PortalID, false);
                            //DataCache.ClearUserCache(UserItem.PortalID, UserItem.Username);
                            Common.UpdateDictionary(jsonDicts, "Message", "Update user account success!");
                            Result = true;
                          

                        }
                        else
                        {
                         
                            Common.UpdateDictionary(jsonDicts, "Message", Localization.GetString("PasswordResetFailed"));
                            Result = false;
                        }

                    }
                    else
                    {
                        Common.UpdateDictionary(jsonDicts, "Message", Localization.GetString("PasswordInvalid"));
                        Result = false;
                    }
                }
           


               
            }

 

            jsonDicts.Add("UserItem", UserItem);
            jsonDicts.Add("UpdateResult", UpdateResult);
            jsonDicts.Add("Result", Result);


            //转换数据为json
            ResponseString = jsSerializer.Serialize(jsonDicts);

        }



        public String ConvertBool(String ss)
        {
            if (!string.IsNullOrEmpty(ss))
            {
                return ss == "true" ? "True" : ss;
            }
            return "false";

        }




    }
}