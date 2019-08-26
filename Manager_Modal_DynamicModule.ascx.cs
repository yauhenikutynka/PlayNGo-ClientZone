using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Playngo.Modules.ClientZone
{
    public partial class Manager_Modal_DynamicModule : BaseModule
    {

        /// <summary>
        /// 动态模块编号
        /// </summary>
        public Int32 DynamicID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);

        /// <summary>
        /// 模块类型
        /// </summary>
        public Int32 DynamicType = WebHelper.GetIntParam(HttpContext.Current.Request, "Type", 0);

        /// <summary>
        /// 链接ID
        /// </summary>
        public Int32 LinkID = WebHelper.GetIntParam(HttpContext.Current.Request, "LinkID", 0);


        private Playngo_ClientZone_DynamicModule _DynamicModule;
        /// <summary>
        /// 动态模块项
        /// </summary>
        public Playngo_ClientZone_DynamicModule DynamicModule
        {
            get
            {
                if (!(_DynamicModule != null && _DynamicModule.ID > 0))
                {
                    if (DynamicID > 0)
                        _DynamicModule = Playngo_ClientZone_DynamicModule.FindByKeyForEdit(DynamicID);
                    else
                        _DynamicModule = new Playngo_ClientZone_DynamicModule();
                }
                return _DynamicModule;
            }
            set { _DynamicModule = value; }
        }



        private List<KeyValueEntity> _ItemSettings;
        /// <summary>
        /// 封装的参数集合
        /// </summary>
        public List<KeyValueEntity> ItemSettings
        {
            get
            {
                if (!(_ItemSettings != null && _ItemSettings.Count > 0))
                {
                    if (DynamicModule != null && DynamicModule.ID > 0 && !String.IsNullOrEmpty(DynamicModule.Options))
                    {
                        try
                        {
                            _ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(DynamicModule.Options);
                        }
                        catch
                        {
                            _ItemSettings = new List<KeyValueEntity>();
                        }
                    }
                    else
                        _ItemSettings = new List<KeyValueEntity>();
                }
                return _ItemSettings;
            }
        }





        #region "==方法=="

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataToPage()
        {

            Playngo_ClientZone_DynamicModule Dynamic = DynamicModule;


            txtTitle.Text = Dynamic.Title;
            cbIncludeTabLink.Checked = Dynamic.IncludeTabLink == 1;
            cbPDFGenerator.Checked = Dynamic.PDFGenerator == 1;

 

            //增加权限用户
            cbPermissionsAllUsers.Checked = Dynamic.Per_AllUsers == 0;

            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();
            WebHelper.BindList(cblPermissionsRoles, rc.GetPortalRoles(PortalId), "RoleName", "RoleName");
            WebHelper.SelectedListMultiByValue(cblPermissionsRoles, Dynamic.Per_Roles);






        }

        #endregion

        #region "==事件=="


        /// <summary>
        /// 页面加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //绑定数据
                    BindDataToPage();
                }
             
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                Dictionary<String, Object> DynamicModuleJsons = new Dictionary<String, Object>();

                Playngo_ClientZone_DynamicModule Dynamic = DynamicModule;


                Dynamic.Title = txtTitle.Text;

                Dynamic.IncludeTabLink = cbIncludeTabLink.Checked ? 1 : 0;
                Dynamic.PDFGenerator = cbPDFGenerator.Checked ? 1 : 0;


                //权限
                Dynamic.Per_AllUsers = cbPermissionsAllUsers.Checked ? 0 : 1;

                String textStr, idStr = String.Empty;
                WebHelper.GetSelected(cblPermissionsRoles, out textStr, out idStr);
                Dynamic.Per_Roles = idStr;

                //更新项
                Dynamic.LastIP = WebHelper.UserHost;
                Dynamic.LastTime = xUserTime.UtcTime();
                Dynamic.LastUser = UserId;


                if (Dynamic.ID > 0)
                {
                    //更新
                }
                else
                {
                    //新增
                    Dynamic.ModuleId = ModuleId;
                    Dynamic.PortalId = PortalId;

                    Dynamic.LinkID = LinkID;
                    Dynamic.Type = DynamicType;

                    QueryParam qp = new QueryParam();
                    qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicModule._.ModuleId, ModuleId, SearchType.Equal));
                    qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicModule._.LinkID, LinkID, SearchType.Equal));
                    qp.Where.Add(new SearchParam(Playngo_ClientZone_DynamicModule._.Type, DynamicType, SearchType.Equal));

 
                    Dynamic.Sort = Playngo_ClientZone_DynamicModule.FindCount(qp) + 10;
                }


                int ResultEvent = 0;

                if (Dynamic.ID > 0)
                {
                    ResultEvent = Dynamic.Update();
                    DynamicModuleJsons.Add("Action", "Update");
                }   
                else
                {
                    ResultEvent = Dynamic.ID = Dynamic.Insert();
                    DynamicModuleJsons.Add("Action", "Insert");
                }
                   


                foreach (var Field in Playngo_ClientZone_DynamicModule.Meta.Fields)
                {
                    DynamicModuleJsons.Add(Field.ColumnName, Dynamic[Field.ColumnName]);
                }

                //DynamicItemJsons.Add("TypeText", EnumHelper.GetEnumTextVal(Dynamic.Type, typeof(EnumDynamicItemType)));
 
                DynamicModuleJsons.Add("EditUrl", DynamicModule_IframeUrl(Dynamic.ID));


                DynamicModuleJsons.Add("AddUrlText", DynamicItem_IframeUrl(0,Dynamic.ID, "Text"));
                DynamicModuleJsons.Add("AddUrlImage", DynamicItem_IframeUrl(0, Dynamic.ID, "Image"));
                DynamicModuleJsons.Add("AddUrlImageText", DynamicItem_IframeUrl(0, Dynamic.ID, "ImageText"));
                DynamicModuleJsons.Add("AddUrlVideo", DynamicItem_IframeUrl(0, Dynamic.ID, "Video"));
                DynamicModuleJsons.Add("AddUrliFrame", DynamicItem_IframeUrl(0, Dynamic.ID, "xFrame"));



                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                jsSerializer.MaxJsonLength = Int32.MaxValue;
                String JsonString = jsSerializer.Serialize(DynamicModuleJsons);

                Response.Write(String.Format("<script>window.parent.EditDynamicModules({0});</script>", JsonString));


            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

               
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


         


        #endregion
    }
}